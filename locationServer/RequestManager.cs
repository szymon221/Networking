using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading;


namespace locationserver
{
    //consider makeing static or singlton
    class RequestManager
    {
        static readonly int ThreadSleepInterval = 1;
        private static Thread[] Threads;
        private readonly Settings ServerSettings;
        private static readonly ConcurrentQueue<TcpClient> ClientQueue = new ConcurrentQueue<TcpClient>();
        private static readonly ConcurrentDictionary<string, string> Lookup = new ConcurrentDictionary<string, string>();

        public RequestManager(Settings ServerSettings)
        {
            this.ServerSettings = ServerSettings;
        }

        public void CreateThreads()
        {
            Threads = new Thread[ServerSettings.Threads];
            for (int i = 0; i < ServerSettings.Threads; i++)
            {
                Threads[i] = new Thread(ClientRequestProcessor);
                Threads[i].Start();
            }
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, ServerSettings.Port);
            listener.Start();

            while (Settings.ServerOn)
            {
                TcpClient cli = listener.AcceptTcpClient();
                ClientQueue.Enqueue(cli);
            }
        }

        private bool GetNextClient(out Request Client)
        {
            Client = null;

            if (ClientQueue.IsEmpty)
            {
                return false;
            }

            if (!ClientQueue.TryDequeue(out TcpClient NextClient))
            {
                return false;
            }

            try
            {
                Client = new Request(NextClient);
                return true;
            }
            catch (InvalidProtocolExcpetion)
            {
                NextClient.Close();
                return false;
            }
        }

        private void DoLookup(Request ClientRequest)
        {
            if (!Lookup.TryGetValue(ClientRequest.User, out string Location))
            {
                ClientRequest.Protocol.ErrorResponse();

                Logger.Log(ClientRequest.IPAdress, "GET", ClientRequest.User,
                    ClientRequest.Location, "404");

                ClientRequest.Client.Close();
                return;
            }

            ClientRequest.Protocol.QueryResponse(Location);
            ClientRequest.Client.Close();

            Logger.Log(ClientRequest.IPAdress, "GET", ClientRequest.User,
                ClientRequest.Location, "200");
        }


        private void DoUpdate(Request ClientRequest)
        {
            Lookup.AddOrUpdate(ClientRequest.User, ClientRequest.Location,
                (key, oldValue) => ClientRequest.Location);

            ClientRequest.Protocol.UpdateResponse();
            ClientRequest.Client.Close();

            Logger.Log(ClientRequest.IPAdress, "POST", ClientRequest.User,
                ClientRequest.Location, "200");
        }


        private void ClientRequestProcessor()
        {
            while (Settings.ServerOn)
            {
                if (!GetNextClient(out Request ClientRequest))
                {        
                    Thread.Sleep(ThreadSleepInterval);
                    continue;
                }

                DebugWriter.Write($"Client conntected from {ClientRequest.IPAdress}");

                try
                {
                    if (RequestType.IsLookup(ClientRequest.Protocol.Type))
                    {
                        DoLookup(ClientRequest);
                    }

                    if (RequestType.IsUpdate(ClientRequest.Protocol.Type))
                    {
                        DoUpdate(ClientRequest);
                    }
                }
                catch (IOException) { }
            }
        }

    }
}
