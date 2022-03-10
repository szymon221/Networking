using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace locationserver
{
    class Program
    {
        static Settings ServerSettings;
        private static Thread[] Threads;
        private static readonly ConcurrentQueue<TcpClient> ClientQueue = new ConcurrentQueue<TcpClient>();
        private static readonly ConcurrentDictionary<string, string> Lookup = new ConcurrentDictionary<string, string>();
        
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
            Request ClientRequest;

            while (Settings.ServerOn)
            {
                if (ClientQueue.IsEmpty)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (!ClientQueue.TryDequeue(out TcpClient Client))
                {
                    Thread.Sleep(1);
                    continue;
                }

                try
                {
                    ClientRequest = new Request(Client);
                }
                catch (InvalidProtocolExcpetion) 
                {
                    Client.Close();
                    continue;
                }

                try
                {
                    if (RequestType.IsLookup(ClientRequest.Protocol.Type))
                    {
                        if (!Lookup.TryGetValue(ClientRequest.User, out string Location))
                        {
                            ClientRequest.Protocol.ErrorResponse();
                            Logger.Log(ClientRequest.IPAdress, "GET", ClientRequest.User, ClientRequest.Location, "404");
                            Client.Close();
                            continue;
                        }

                        ClientRequest.Protocol.QueryResponse(Location);
                        Client.Close();
                        Logger.Log(ClientRequest.IPAdress, "GET", ClientRequest.User, ClientRequest.Location, "200");
                        continue;
                    }

                    if (RequestType.IsUpdate(ClientRequest.Protocol.Type))
                    {
                        Lookup.AddOrUpdate(ClientRequest.User, ClientRequest.Location, (key, oldValue) => ClientRequest.Location);
                        ClientRequest.Protocol.UpdateResponse();
                        Client.Close();
                        Logger.Log(ClientRequest.IPAdress, "POST", ClientRequest.User, ClientRequest.Location, "200");
                        continue;
                    }


                }
                catch (IOException) { }
            }
        }
    }
}
