using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

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



        public Request(TcpClient Client) {

            sw = new StreamWriter(Client.GetStream());
            sr = new StreamReader(Client.GetStream());

            string Request = ReadRequest(sr);
            Protocol = Ptcl.GetProtocol(Request.Split("\r\n")[0],sw);

            Type = Protocol.GetRequestType(Request);


            User = Protocol.SetUser(Request);
            if (Type.GetType() == typeof(RequestUpdate))
            {
                Location = Protocol.SetLocation(Request);
            }
        }

        private string ReadRequest(StreamReader sr)
        {
            string Response = "";
            try
            {
                while (!sr.EndOfStream)
                {
                    Response += (char)sr.Read();
                }
            }
            catch(IOException){}

            return Response;
        }
    }

    public  class RequestType 
    {    
    }
    public class RequestUpdate : RequestType
    { 
    }

    public class RequestLookup : RequestType
    { 
    }


}
