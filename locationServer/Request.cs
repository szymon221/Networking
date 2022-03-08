using System.IO;
using System.Net.Sockets;
using System;
using System.Net;

namespace locationserver
{
    public class Request
    {
        public readonly RequestType Type;
        public readonly Ptcl Protocol;
        public readonly string User;
        public readonly string Location;
        public readonly string RawRequest;
        public readonly string IPAdress;

        private readonly StreamReader sr;
        private readonly StreamWriter sw;

        public Request(TcpClient Client)
        {

            sw = new StreamWriter(Client.GetStream());
            sr = new StreamReader(Client.GetStream());
            IPAdress = Client.Client.RemoteEndPoint.ToString();
            Client.ReceiveTimeout = 1000;
            Client.SendTimeout = 1000;
            RawRequest = ReadRequest(sr);
            Protocol = Ptcl.GetProtocol(RawRequest);
            Protocol.SetWriter(sw);

            if (Protocol.Type.GetType() == typeof(RequestUpdate))
            {
                User = Protocol.SetUserPOST(RawRequest);
                Location = Protocol.SetLocationPOST(RawRequest);
            }

            if (Protocol.Type.GetType() == typeof(RequestLookup))
            {
                User = Protocol.SetUserGET(RawRequest);
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
