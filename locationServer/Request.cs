using System.IO;
using System.Net.Sockets;
using System;

namespace locationserver
{
    public class Request
    {
        public readonly RequestType Type;
        public readonly Ptcl Protocol;
        public readonly string User;
        public readonly string Location;

        private readonly StreamReader sr;
        private readonly StreamWriter sw;

        public Request(TcpClient Client)
        {

            sw = new StreamWriter(Client.GetStream());
            sr = new StreamReader(Client.GetStream());
            Client.ReceiveTimeout = 1000;
            Client.SendTimeout = 1000;
            string Request = ReadRequest(sr);
            Protocol = Ptcl.GetProtocol(Request);
            Protocol.SetWriter(sw);

            if (Protocol.Type.GetType() == typeof(RequestUpdate))
            {
                User = Protocol.SetUserPOST(Request);
                Location = Protocol.SetLocationPOST(Request);
            }

            if (Protocol.Type.GetType() == typeof(RequestLookup))
            {
                User = Protocol.SetUserGET(Request);
            }

        }

        private string ReadRequest(StreamReader sr)
        {
            string Response = "";
            try
            {
                while (true)
                {
                    if (sr.Peek() == -1) {break;}
                    Response += (char)sr.Read();
                }
            }
            catch (IOException) {}

            return Response;
        }
    }
    public class RequestType
    {
    }
    public class RequestUpdate : RequestType
    {
    }
    public class RequestLookup : RequestType
    {
    }
}
