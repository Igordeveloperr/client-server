using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace More_Client
{
    class Server
    {
        private TcpListener Listener;
        public Server(int port) 
        {   
            // проверка ввода данных в конструктор
            if (port < 0) throw new ArgumentException(nameof(port));
            // реализую прослушку на таком-то ip и порту
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"),port);
            Listener = listener;
            // запускаю сервер
            listener.Start();
        }

        public async void ObserveData()
        {
            await Task.Run(() => {
                // подключаю клиента
                TcpClient client = Listener.AcceptTcpClient();
                // сетевой поток для чтения и записи
                NetworkStream stream = client.GetStream();
                // массив для получения данных от клента
                byte[] clientData = new byte[1024];
                StringBuilder response = new StringBuilder();
                // проверка на наличие данных в потоке
                if (stream.CanRead)
                {
                    // читаем данные, пока они есть
                    do
                    {
                        // определяю длину полученного сообщения
                        int bytes = stream.Read(clientData, 0, clientData.Length);
                        // расшифровываю сообщение
                        response.Append(Encoding.UTF8.GetString(clientData, 0, bytes));
                    } while (stream.DataAvailable);
                }
                // вывожу полученное сообщение
                Console.WriteLine(response);
                //формирую и кодирую сообщение для отправки клиенту
                byte[] data = Encoding.UTF8.GetBytes("я принял твой запрос!");
                // отправка сообщению клиенту
                stream.Write(data, 0, data.Length);
                // закрываю поток
                stream.Close();
                // закрываю соединение
                client.Close();
            });
        }
    }
}
