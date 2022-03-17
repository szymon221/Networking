using System.IO;

namespace location.Protocols
{
    public class H1 : BaseProtocol
    {
        private string? _User;
        private string? _Location;
        private string? _HostName;

        public override void SetHostName(string HostName)
        {
            _HostName = HostName;
            return;
        }
        public override void SetVariables(string User)
        {
            _User = User;
        }

        public override void SetVariables(string User, string Location)
        {
            _User = User;
            _Location = Location;
        }

        public override void Query(StreamWriter sw)
        {
            sw.Write($"GET /?name={_User} HTTP/1.1\r\nHost: {_HostName}\r\n\r\n");
            sw.Flush();
        }

        public override void Update(StreamWriter sw)
        {
            //I call this job security
            sw.Write($"POST / HTTP/1.1\r\nHost: {_HostName}\r\nContent-Length: {_Location.Length + _User.Length + 15}\r\n\r\nname={_User}&location={_Location}");
            sw.Flush();
        }

        public override bool OK(string response)
        {
            if (response.Split("\r\n")[0] == "HTTP/1.1 200 OK")
            {
                return true;
            }

            return false;
        }

        public override string Body(string response, string location = null)
        {
            if (location != null)
            {
                return location;
            }

            string[] temp = response.Split("\r\n");
            string body = "";
            string returncarriage = "";

            bool append = false;
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (append)
                {
                    body += returncarriage + temp[i];
                    returncarriage = "\r\n";
                }

                if (temp[i] == "")
                {
                    append = true;
                }
            }

            return body;
        }

        public override bool Error(string response)
        {
            string[] temp = response.Split("\r\n");

            if (temp[0] == "HTTP/1.1 404 Not Found")
            {
                return true;
            }

            return false;
        }
    }
}
