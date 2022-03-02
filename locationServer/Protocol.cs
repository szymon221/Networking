using System;
using System.IO;

namespace locationserver
{
    public abstract class Ptcl
    {
        public static Ptcl GetProtocol(string FirstLine,StreamWriter sw)
        {
            throw new NotImplementedException();
        }

        public abstract string QueryRequest(string User);
        public abstract string UpdateRequest(string User, string Location);
        public abstract string ErrorResponse();
        public abstract string SetUser(string Request);
        public abstract string SetLocation(string Request);
        public abstract RequestType GetRequestType(string Request);

    }

    public class WhoIsProtocol : Ptcl
    {
        public override string ErrorResponse()
        {
            throw new NotImplementedException();
        }

        public override RequestType GetRequestType(string Request)
        {
            throw new NotImplementedException();
        }

        public override string QueryRequest(string User)
        {
            throw new NotImplementedException();
        }

        public override string SetLocation(string Request)
        {
            throw new NotImplementedException();
        }

        public override string SetUser(string Request)
        {
            throw new NotImplementedException();
        }

        public override string UpdateRequest(string User, string Location)
        {
            throw new NotImplementedException();
        }
    }
    public class H9 : Ptcl
    {
        public override string ErrorResponse()
        {
            throw new NotImplementedException();
        }

        public override RequestType GetRequestType(string Request)
        {
            throw new NotImplementedException();
        }

        public override string QueryRequest(string User)
        {
            throw new NotImplementedException();
        }

        public override string SetLocation(string Request)
        {
            throw new NotImplementedException();
        }

        public override string SetUser(string Request)
        {
            throw new NotImplementedException();
        }

        public override string UpdateRequest(string User, string Location)
        {
            throw new NotImplementedException();
        }
    }

    public class H0 : Ptcl
    {
        public override string ErrorResponse()
        {
            throw new NotImplementedException();
        }

        public override RequestType GetRequestType(string Request)
        {
            throw new NotImplementedException();
        }

        public override string QueryRequest(string User)
        {
            throw new NotImplementedException();
        }

        public override string SetLocation(string Request)
        {
            throw new NotImplementedException();
        }

        public override string SetUser(string Request)
        {
            throw new NotImplementedException();
        }

        public override string UpdateRequest(string User, string Location)
        {
            throw new NotImplementedException();
        }
    }

    public class H1 : Ptcl
    {
        public override string ErrorResponse()
        {
            throw new NotImplementedException();
        }

        public override RequestType GetRequestType(string Request)
        {
            throw new NotImplementedException();
        }

        public override string QueryRequest(string User)
        {
            throw new NotImplementedException();
        }

        public override string SetLocation(string Request)
        {
            throw new NotImplementedException();
        }

        public override string SetUser(string Request)
        {
            throw new NotImplementedException();
        }

        public override string UpdateRequest(string User, string Location)
        {
            throw new NotImplementedException();
        }
    }
}
