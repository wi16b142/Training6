using GalaSoft.MvvmLight;
using System;

namespace Training6.ViewModel
{
    public class DataVM:ViewModelBase
    {
        string firstname;
        string lastname;
        int age;
        string id;
        string idType;
        int rating;
        Action<DataVM> Informer;

        public static string[] Types = new string[] { "Driving License", "Identity Card", "Signature", "All" };

        public DataVM(string firstname, string lastname, int age, string id, string idType, int rating, Action<DataVM> Informer)
        {
            this.Informer = Informer;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
            this.id = id;
            this.idType = idType;
            this.rating = rating;
            DoChange();
        }

        public string Firstname
        {
            get => firstname; set
            {
                firstname = value;
                RaisePropertyChanged();
                DoChange();
            }
        }
        public string Lastname
        {
            get => lastname; set
            {
                lastname = value;
                RaisePropertyChanged();
                DoChange();
            }
        }
        public int Age
        {
            get => age; set
            {
                age = value;
                RaisePropertyChanged();
                DoChange();
            }
        }
        public string Id
        {
            get => id; set
            {
                id = value;
                RaisePropertyChanged();
                DoChange();
            }
        }
        public string IdType
        {
            get => idType; set
            {
                idType = value;
                RaisePropertyChanged();
                DoChange();
            }
        }
        public int Rating
        {
            get => rating; set
            {
                rating = value;
                RaisePropertyChanged();
                DoChange();
            }
        }

        private void DoChange()
        {
            Informer(this);
        }

    }
}
