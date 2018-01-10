using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Training6.Com
{
    public class Communication
    {
        Socket serverSocket;
        List<ClientHandler> Clients;
        Action<string> informer;
        Action NewClientInformer;
        const int port = 10100;
        public bool IsServer { get; set; }
        byte[] buffer = new byte[512];
        
        public Communication(Action<string> informer, Action newClientInformer, bool isServer)
        {
            this.informer = informer;
            this.NewClientInformer = newClientInformer;
            this.IsServer = isServer;
            Clients = new List<ClientHandler>();

            if (isServer)
            {
                serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
                serverSocket.Listen(10);
                StartAccepting();
            }
            else
            {
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Loopback, port);
                Clients.Add(new ClientHandler(client.Client, informer));
            }
        }

        private void StartAccepting()
        {
            Task.Factory.StartNew(Accept);
        }

        private void Accept()
        {
            Clients.Add(new ClientHandler(serverSocket.Accept(), informer));
            NewClientInformer();
        }

        public void Broadcast(string data)
        {

            foreach (var client in Clients)
            {
                client.Send(Encoding.UTF8.GetBytes(data));
            }
            
        }



    }
}
