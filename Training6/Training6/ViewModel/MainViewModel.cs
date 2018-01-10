using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Threading;
using Training6.Com;

namespace Training6.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        Communication com;
        private ObservableCollection<DataVM> data;
        private RelayCommand clientBtnClick;
        private RelayCommand serverBtnClick;
        private DataVM selectedData;
        bool isConnected = false;
        string selectedFilter;

        public string[] Types { get { return DataVM.Types; } }
        public RelayCommand ClientBtnClick { get => clientBtnClick; set => clientBtnClick = value; }
        public RelayCommand ServerBtnClick { get => serverBtnClick; set => serverBtnClick = value; }
        public ObservableCollection<DataVM> Data
        {
            get => data;
            set
            {
                data = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<DataVM> persons;
        public DataVM SelectedData
        {
            get => selectedData; set
            {
                selectedData = value;
                RaisePropertyChanged();
            }
        }
        public string SelectedFilter
        {
            get { return selectedFilter; }
            set
            {
                selectedFilter = value;
                RaisePropertyChanged();
                Persons.Clear();

                if (SelectedFilter == "All")
                {
                    Persons = new ObservableCollection<DataVM>(Data);
                }
                else
                {
                    foreach(var item in Data)
                    {
                        if (item.IdType.Equals(selectedFilter))
                        {
                            Persons.Add(item);
                        }
                    }
                }
                
            }
        }
        public ObservableCollection<DataVM> Persons
        {
            get => persons; set
            {
                persons = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            ServerBtnClick = new RelayCommand(()=> 
            {
                com = new Communication(GuiUpdate, NewClient, true);
                isConnected = true;
                CreateData(); //create original demo data
                Persons = new ObservableCollection<DataVM>(Data); //copy data in current displayed persons

            }, ()=> { return !isConnected; });

            ClientBtnClick = new RelayCommand(() =>
            {
                com = new Communication(GuiUpdate, NewClient, false);
                isConnected = true;
                Data = new ObservableCollection<DataVM>();
                Persons = new ObservableCollection<DataVM>(Data);
            }, () => { return !isConnected; });



        }

        private void GuiUpdate(string data)
        {
            App.Current.Dispatcher.Invoke(() => 
            {
                string[] newData = data.Split(':');
                bool isOld = false;

                foreach (var item in Data)
                {
                    if (item.Firstname.Equals(newData[0]) && item.Lastname.Equals(newData[1]))
                    {
                        item.Age = int.Parse(newData[2]);
                        item.Id = newData[3];
                        item.IdType = newData[4];
                        item.Rating = int.Parse(newData[5]);
                        isOld = true;
                        break;
                    }

                }
                if(!isOld)
                {
                    Data.Add(new DataVM(newData[0], newData[1], int.Parse(newData[2]), newData[3], newData[4], int.Parse(newData[5]), ApplyChanges));
                }

                Persons = new ObservableCollection<DataVM>(Data);
            });


            //split string and update gui
        }

        private void NewClient()
        {
            foreach(var item in Data)
            {
                ApplyChanges(item);
                Thread.Sleep(50);
            }
        }

        private void ApplyChanges(DataVM newData)
        {
            if(isConnected && com.IsServer)
            {
                com.Broadcast(newData.Firstname + ":" + newData.Lastname + ":" + newData.Age + ":" + newData.Id + ":" + newData.IdType + ":" + newData.Rating);
            }
        }

        private void CreateData()
        {
            Data = new ObservableCollection<DataVM>();
            Data.Add(new DataVM("Sascha","Boeck",33, "1",Types[1],4, ApplyChanges));
            Data.Add(new DataVM("Hodor", "Hodorson", 22, "2", Types[2], 1, ApplyChanges));
            Data.Add(new DataVM("Nils", "Frodo", 21, "3", Types[0], 2, ApplyChanges));
            Data.Add(new DataVM("Saruman", "Samweis", 34, "4", Types[2], 3, ApplyChanges));
            Data.Add(new DataVM("Mary", "Poppins", 75, "5", Types[0], 0, ApplyChanges));
            Data.Add(new DataVM("Severus", "Snape", 25, "6", Types[1], 2, ApplyChanges));
        }
    }
}