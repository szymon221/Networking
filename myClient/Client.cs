using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using location.Protocols;


namespace location
{
    class LocationClient
    {
        ClientSettings Settings;
        BaseProtocol Proto;

        StreamReader sr;
        StreamWriter sw;
        

        readonly TcpClient Client = new TcpClient();

        private bool Update = false;
        private bool Query = false;

        string User = String.Empty;
        string Location = String.Empty;

        string ServerResponse = String.Empty;

        public LocationClient(ClientSettings Settings) {

            this.Settings = Settings;
            Proto = Settings.Proto;
        
        }




        private void Connect()
        {
            try
            {
                Client.Connect(Settings.ServerName, Settings.Port);
                Client.ReceiveTimeout = Settings.Timeout;
                Client.SendTimeout = Settings.Timeout;
                sr = new StreamReader(Client.GetStream());
                sw = new StreamWriter(Client.GetStream());
            }
            catch
            {
                Console.WriteLine($"Unable to establish connection with {Settings.ServerName}\r\n");
                throw new IOException();
            }
        }

        public void SendRequest()
        {
            ParseArguments();
            Connect();
            SendData();
            ReadReply();        
        }
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


        public string GetResponse()
        {
            if(Query) 
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
