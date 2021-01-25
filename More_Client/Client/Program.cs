using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(80);
            while (true)
            {
                client.SendData();
            }
        }
    }
}
