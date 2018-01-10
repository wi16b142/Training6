using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Training6.Com
{
    class ClientHandler
    {
        private Socket clientSocket;
        Action<string> informer;
        byte[] buffer = new byte[512];

        public ClientHandler(Socket socket, Action<string> action)
        {
            this.ClientSocket = socket;
            this.informer = action;
            Task.Factory.StartNew(Receive);
        }

        public Socket ClientSocket { get => clientSocket; set => clientSocket = value; }

        public void Send(byte[] data)
        {
            if(clientSocket.Connected) clientSocket.Send(data);
        }

        private void Receive()
        {
            int length;
            while (true)
            {
                length = clientSocket.Receive(buffer);
                informer(Encoding.UTF8.GetString(buffer, 0, length));
            }
        }

    }
}
