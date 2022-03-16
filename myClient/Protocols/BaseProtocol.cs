using System;
using System.Collections.Generic;
using System.Text;

namespace location.Protocols
{
    public abstract class BaseProtocol
    {
        public abstract void SetVariables(string User, string Location);
        public abstract void SetVariables(string User);
        public abstract void SetHostName(string HostName);
        public abstract string Query();
        public abstract string Update();
        public abstract string Body(string response, string location = null);
        public abstract bool OK(string response);
        public abstract bool Error(string response);

    }
}
