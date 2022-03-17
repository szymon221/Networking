using System;
using System.IO;

namespace locationserver
{
    public abstract class Ptcl
    {
        /// <summary>
        /// based on the client reqquest creats appropirate protocl obbject
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static Ptcl GetProtocol(string Request)
        {
            string FirstLine = Request.Split("\r\n")[0];
            string[] SpaceArray = FirstLine.Split(" ");
            DebugWriter.Write(Request);
            Ptcl Protocol;

            if (SpaceArray[^1] == "HTTP/1.1")
            {
                if (SpaceArray[0] == "GET")
                {
                    Protocol = new H1();
                    Protocol.SetRequestType(new RequestLookup());
                    return Protocol;
                }

                if (SpaceArray[0] == "POST")
                {
                    Protocol = new H1();
                    Protocol.SetRequestType(new RequestUpdate());
                    return Protocol;
                }
                throw new InvalidProtocolExcpetion();

            }

            if (SpaceArray[^1] == "HTTP/1.0")
            {

                if (SpaceArray[0] == "GET")
                {
                    Protocol = new H0();
                    Protocol.SetRequestType(new RequestLookup());
                    return Protocol;
                }

                if (SpaceArray[0] == "POST")
                {
                    Protocol = new H0();
                    Protocol.SetRequestType(new RequestUpdate());
                    return Protocol;
                }
                throw new InvalidProtocolExcpetion();


            }

            if (SpaceArray[0] == "GET" & Request.Split(" ").Length == 2)
            {
                Protocol = new H9();
                Protocol.SetRequestType(new RequestLookup());
                return Protocol;
            }

            if (SpaceArray[0] == "PUT" & Request.Split("\r\n").Length > 2)
            {
                Protocol = new H9();
                Protocol.SetRequestType(new RequestUpdate());
                return Protocol;
            }

            if (SpaceArray.Length >= 2 & Request.Split("\r\n").Length == 2)
            {
                Protocol = new WhoIsProtocol();
                Protocol.SetRequestType(new RequestUpdate());
                return Protocol;
            }

            if (SpaceArray.Length == 1 & Request.Split(" ").Length == 1)
            {
                Protocol = new WhoIsProtocol();
                Protocol.SetRequestType(new RequestLookup());
                return Protocol;
            }







            throw new InvalidProtocolExcpetion();
        }
        public abstract RequestType Type { get; }
        public abstract void SetWriter(StreamWriter sr);
        public abstract void SetRequestType(RequestType type);
        public abstract string SetUserGET(string Request);
        public abstract string SetUserPOST(string Request);
        public abstract string SetLocationPOST(string Request);
        public abstract void QueryResponse(string Location);
        public abstract void UpdateResponse();
        public abstract void ErrorResponse();

    }

    public class WhoIsProtocol : Ptcl
    {
        public override RequestType Type { get { return _Type; } }
        private RequestType _Type;
        private StreamWriter sw;
        public override void SetWriter(StreamWriter StreamW) { sw = StreamW; }
        public override void SetRequestType(RequestType type) { _Type = type; }

        public override void ErrorResponse()
        {
            sw.Write("ERROR: no entries found\r\n");
            sw.Flush();
        }
        public override void QueryResponse(string Location)
        {
            sw.Write($"{Location}\r\n");
            sw.Flush();
        }
        public override void UpdateResponse()
        {
            sw.Write("OK\r\n");
            sw.Flush();
        }

        public override string SetUserGET(string Request)
        {
            return Request.Split(" ")[0].Split("\r\n")[0];
        }

        public override string SetUserPOST(string Request)
        {
            return Request.Split(" ")[0].Split("\r\n")[0];
        }

        public override string SetLocationPOST(string Request)
        {
            return string.Join(" ", Request.Split(" ")[1..]).Split("\r\n")[0];
        }
    }
    public class H9 : Ptcl
    {
        public override RequestType Type { get { return _Type; } }
        private RequestType _Type;
        private StreamWriter sw;
        public override void SetWriter(StreamWriter StreamW) { sw = StreamW; }
        public override void SetRequestType(RequestType type) { _Type = type; }

        public override void UpdateResponse()
        {
            sw.Write($"HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }
        public override void ErrorResponse()
        {
            sw.Write("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }
        public override void QueryResponse(string Location)
        {
            sw.Write($"HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n{Location}\r\n");
            sw.Flush();
        }

        public override string SetUserGET(string Request)
        {
            return Request.Split("/")[^1].Split("\r\n")[0];
        }

        public override string SetUserPOST(string Request)
        {
            return Request.Split("\r\n")[0].Split("/")[^1];
        }

        public override string SetLocationPOST(string Request)
        {
            return Request.Split("\r\n")[^2];
        }
    }

    public class H0 : Ptcl
    {
        public override RequestType Type { get { return _Type; } }
        private RequestType _Type;
        private StreamWriter sw;
        public override void SetWriter(StreamWriter StreamW) { sw = StreamW; }
        public override void SetRequestType(RequestType type) { _Type = type; }

        public override void ErrorResponse()
        {
            sw.Write("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }

        public override void QueryResponse(string Location)
        {
            sw.Write($"HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n{Location}\r\n");
            sw.Flush();
        }

        public override void UpdateResponse()
        {
            sw.Write($"HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }
        public override string SetUserGET(string Request)
        {
            string TopLine = Request.Split("\r\n")[0];
            string[] SpaceArray = TopLine.Split(" ");
            return string.Join("?", SpaceArray[1].Split("?")[1..]);

        }

        public override string SetUserPOST(string Request)
        {

            return Request.Split("\r\n")[0].Split("/")[1].Split(" ")[0];
        }

        public override string SetLocationPOST(string Request)
        {
            return Request.Split("\r\n")[^1];
        }
    }

    public class H1 : Ptcl
    {
        public override RequestType Type { get { return _Type; } }
        private RequestType _Type;
        private StreamWriter sw;
        public override void SetWriter(StreamWriter StreamW) { sw = StreamW; }
        public override void SetRequestType(RequestType type) { _Type = type; }
        public override void ErrorResponse()
        {
            sw.Write("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }
        public override void QueryResponse(string Location)
        {
            sw.Write($"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n{Location}\r\n");
            sw.Flush();
        }
        public override void UpdateResponse()
        {
            sw.Write($"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
            sw.Flush();
        }
        public override string SetUserGET(string Request)
        {
            string TopLine = Request.Split("\r\n")[0];
            string[] SpaceArray = TopLine.Split(" ");
            return SpaceArray[1].Split("=")[^1];
        }
        public override string SetUserPOST(string Request)
        {
            return Request.Split("\r\n")[^1].Split("&")[0].Split("=")[^1];
        }
        public override string SetLocationPOST(string Request)
        {
            return Request.Split("\r\n")[^1].Split("&")[^1].Split("=")[^1];

        }
    }
    class InvalidProtocolExcpetion : Exception
    {
        public InvalidProtocolExcpetion() { }
        public InvalidProtocolExcpetion(string message) : base(message) { }
        public InvalidProtocolExcpetion(string message, Exception inner) : base(message, inner) { }
    }


}
