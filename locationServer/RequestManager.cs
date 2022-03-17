using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace locationserver
{
    //consider makeing static or singlton
    class RequestManager
    {
        static readonly int ThreadSleepInterval = 1;
        private static Thread[] Threads;
        public readonly Settings ServerSettings;
        private static readonly ConcurrentQueue<TcpClient> ClientQueue = new ConcurrentQueue<TcpClient>();
        private static readonly ConcurrentDictionary<string, string> Lookup = new ConcurrentDictionary<string, string>();
        private TcpListener listener;
        public RequestManager(Settings ServerSettings)
        {
            this.ServerSettings = ServerSettings;
        }
        /// <summary>
        /// Starts the threads
        /// </summary>
        public void CreateThreads()
        {
            Threads = new Thread[ServerSettings.Threads];
            for (int i = 0; i < ServerSettings.Threads; i++)
            {
                Threads[i] = new Thread(ClientRequestProcessor);
                Threads[i].Start();
            }
        }
        /// <summary>
        /// Stops listiner to avoid exception
        /// </summary>
        public void Stop()
        {
            Settings.TurnServerOff();
            listener.Stop();

        }

        /// <summary>
        /// Main thread that deals with accpeting tcp clients and adding them to the queue
        /// </summary>
        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, ServerSettings.Port);
            listener.Start();
            try
            {
                while (Settings.ServerOn)
                {
                    TcpClient cli = listener.AcceptTcpClient();
                    ClientQueue.Enqueue(cli);
                }
            }
            catch (SocketException)
            { 
            
            }
        }
        /// <summary>
        /// Tries to get the next client for the queue
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
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
        /// <summary>
        /// sends lookup response
        /// </summary>
        /// <param name="ClientRequest"></param>
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
            Logger.Log(ClientRequest.IPAdress, "GET", ClientRequest.User,
                ClientRequest.Location, "200");
            ClientRequest.Protocol.QueryResponse(Location);
            ClientRequest.Client.Close();       
        }

        /// <summary>
        /// sends update response
        /// </summary>
        /// <param name="ClientRequest"></param>
        private void DoUpdate(Request ClientRequest)
        {
            Lookup.AddOrUpdate(ClientRequest.User, ClientRequest.Location,
                (key, oldValue) => ClientRequest.Location);
            Logger.Log(ClientRequest.IPAdress, "POST", ClientRequest.User,
                ClientRequest.Location, "200");
            ClientRequest.Protocol.UpdateResponse();
            ClientRequest.Client.Close();      
        }

        /// <summary>
        /// Thread workers
        /// </summary>
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
