using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        private TcpClient TcpClient;
        public Client(int port)
        {
            if (port < 0) throw new ArgumentException(nameof(port));
            TcpClient = new TcpClient();
            // конекчусь к серверу
            TcpClient.Connect(IPAddress.Parse("127.0.0.1"), port);
        }

        public async void SendData()
        {
            await Task.Run(() => {
                // отправляю данные через поток, которые поплывут к серверу
                NetworkStream stream = TcpClient.GetStream();
                Console.WriteLine("Введите сообщение: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                // записываю данные в поток
                stream.Write(data, 0, data.Length);
            });
        }
    }
}
