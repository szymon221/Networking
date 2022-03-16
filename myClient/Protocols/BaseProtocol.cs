using System.IO;
namespace location.Protocols
{
    public abstract class BaseProtocol
    {
        public abstract void SetVariables(string User, string Location);
        public abstract void SetVariables(string User);
        public abstract void SetHostName(string HostName);
        public abstract void Query(StreamWriter sw);
        public abstract void Update(StreamWriter sw);
        public abstract string Body(string response, string? location = null);
        public abstract bool OK(string response);
        public abstract bool Error(string response);

    }
}
