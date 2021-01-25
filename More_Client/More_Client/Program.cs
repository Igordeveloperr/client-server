using System;
using System.Threading;

namespace More_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(80);
            while (true)
            {
                server.ObserveData();
            }
        }
    }
}
