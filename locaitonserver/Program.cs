using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using locationserver;
using System.Threading;
using System.Net;

namespace locaitonserver
{
    class Program
    {
        static Settings ServerSettings;
        private static Thread[] Threads;
        private static ConcurrentQueue<TcpClient> ClientQueue = new ConcurrentQueue<TcpClient>();

        static void Main(string[] args)
        {
            ServerSettings = new Settings(args);

            if (ServerSettings.Graphical)
            {
                Console.WriteLine("Starting graphical enviroment");
                Environment.Exit(0);
            }

            Threads = new Thread[ServerSettings.Threads];
            for (int i = 0; i < ServerSettings.Threads; i++)
            {
                Threads[i] = new Thread(ClientRequestProcessor);
                Threads[i].Start();        
            }

            TcpListener listener = new TcpListener(IPAddress.Any, ServerSettings.Port);

            listener.Start();
            while (Settings.ServerOn)
            {
                TcpClient cli = listener.AcceptTcpClient();
                ClientQueue.Enqueue(cli);
            }
        }

        public static void ClientRequestProcessor()
        {
            TcpClient Client;
            Lookup DBLookup = Lookup.GetInstance;

            while (Settings.ServerOn)
            {
                Request ClientRequest;

                if (ClientQueue.IsEmpty) {
                    Thread.Sleep(1);
                    continue;
                }

                if(!ClientQueue.TryDequeue( out Client))
                {
                    Thread.Sleep(1);
                    continue;
                }

                Client.ReceiveTimeout = 20;

                ClientRequest = new Request(Client);
                try
                {
                    if (ClientRequest.Type.GetType() == typeof(RequestLookup))
                    {
                        string Location;

                        if (!DBLookup.Location.TryGetValue(ClientRequest.User, out Location))
                        {
                            ClientRequest.Protocol.ErrorResponse();
                            Client.Close();
                            continue;
                        }
                        ClientRequest.Protocol.QueryRequest(Location);
                        Client.Close();
                        continue;
                    }
                    ClientRequest.Protocol.UpdateRequest(ClientRequest.User, ClientRequest.Location);
                    Client.Close();
                }
                catch (IOException) { 
                
                }
            }
        }
    }
}
