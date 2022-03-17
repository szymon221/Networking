using location.Protocols;
using System;
using System.IO;
using System.Net.Sockets;


namespace location
{
    class LocationClient
    {
        ClientSettings Settings;
        BaseProtocol Proto;

        StreamReader? sr;
        StreamWriter? sw;


        readonly TcpClient Client = new TcpClient();

        private bool Update = false;
        private bool Query = false;

        string User = String.Empty;
        string Location = String.Empty;

        string ServerResponse = String.Empty;

        public LocationClient(ClientSettings Settings)
        {

            this.Settings = Settings;
            Proto = Settings.Proto;

        }



        /// <summary>
        /// Connects to tcp socket and sets timeouts
        /// </summary>
        private void Connect()
        {
            try
            {
                Client.ReceiveTimeout = Settings.Timeout;
                Client.SendTimeout = Settings.Timeout;
                Client.Connect(Settings.ServerName, Settings.Port);         
                sr = new StreamReader(Client.GetStream());
                sw = new StreamWriter(Client.GetStream());
            }
            catch
            {
                throw new IOException($"Unable to establish connection with {Settings.ServerName}\r\n");
            }
        }
        /// <summary>
        /// Handles the creating sending and reading of request
        /// </summary>
        public void SendRequest()
        {
            ParseArguments();
            Connect();
            try
            {
                SendData();
            }
            catch (IOException){ 

                throw new IOException($"Unable to establish connection with {Settings.ServerName}\r\n");
            }
            ReadReply();
        }

        /// <summary>
        /// Reads clients arguments and creates readers and writers for protocl
        /// </summary>
        private void ParseArguments()
        {
            if (Proto.GetType() == typeof(H1))
            {
                Proto.SetHostName(Settings.ServerName);
            }

            string[] ClientArgs = Settings.LeftOverArguments.Split(" ");

            if (ClientArgs.Length == 1)
            {
                Query = true;
                User = Settings.LeftOverArguments;
                Proto.SetVariables(User);
                return;
            }

            Update = true;
            User = ClientArgs[0];
            Location = String.Join(" ", ClientArgs[1..]);


            Proto.SetVariables(User, Location);


        }

        /// <summary>
        /// Based on response creates string for console output
        /// </summary>
        /// <returns></returns>
        public string GetResponse()
        {
            if (Query)
            {
                if (Proto.Error(ServerResponse))
                {
                    return "ERROR: no entries found\r\n";

                }

                return $"{User} is {Proto.Body(ServerResponse)}\r\n";

            }

            if (Update)
            {
                if (Proto.Error(ServerResponse))
                {
                    return $"Error {Proto.Body(ServerResponse)}";

                }

                if (Proto.OK(ServerResponse))
                {
                    return $"{User} location changed to be {Proto.Body(ServerResponse, Location)}\r\n";

                }

                return ServerResponse;
            }

            return String.Empty;
        }
        /// <summary>
        /// Sends request to the server
        /// </summary>
        private void SendData()
        {
            if (Update)
            {
                Proto.Update(sw);
            }

            if (Query)
            {
                Proto.Query(sw);
            }

        }
        /// <summary>
        /// Reads data from the server and writes it to Locationclient.SererResponse
        /// </summary>
        private void ReadReply()
        {
            try
            {
                while (!sr.EndOfStream)
                {
                    ServerResponse += (char)sr.Read();
                }
            }
            catch
            {
                if (ServerResponse.Length == 0)
                {
                    Console.WriteLine($"Unable to establish connection with {Settings.ServerName}\r\n");
                    Environment.Exit(-1);
                }
            }

        }

    }
}
