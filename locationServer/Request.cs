using System.IO;
using System.Net.Sockets;
using System;
using System.Net;

namespace locationserver
{
    public class Request
    {
        public readonly TcpClient Client;
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
            this.Client = Client;
            Client.ReceiveTimeout = 1000;
            Client.SendTimeout = 1000;

            sw = new StreamWriter(Client.GetStream());
            sr = new StreamReader(Client.GetStream());

            IPAdress = Client.Client.RemoteEndPoint.ToString();


            RawRequest = ReadRequest(sr);
            Protocol = Ptcl.GetProtocol(RawRequest);
            Protocol.SetWriter(sw);

            if (RequestType.IsUpdate(Protocol.Type))
            {
                User = Protocol.SetUserPOST(RawRequest);
                Location = Protocol.SetLocationPOST(RawRequest);
                return;
            }

            if (RequestType.IsLookup(Protocol.Type))
            {
                User = Protocol.SetUserGET(RawRequest);
                return;
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
    public abstract class RequestType
    {
        public static bool IsLookup(RequestType req) {

            if (req.GetType() == typeof(RequestLookup))
            {
                return true;
            }
            return false;
        }

        public static bool IsUpdate(RequestType req)
        {
            if (req.GetType() == typeof(RequestUpdate))
            {
                return true;
            }
            return false;
        }

    }
    public class RequestUpdate : RequestType
    {
    }
    public class RequestLookup : RequestType
    {
    }
}
