using System.IO;
namespace location.Protocols
{
    public class H0 : BaseProtocol
    {
        private string? _User;
        private string? _Location;

        public override void SetHostName(string _)
        {
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
            sw.Write($"GET /?{_User} HTTP/1.0\r\n\r\n");
            sw.Flush();
        }

        public override void Update(StreamWriter sw)
        {
            sw.Write($"POST /{_User} HTTP/1.0\r\nContent-Length: {_Location.Length}\r\n\r\n{_Location}");
            sw.Flush();

        }

        public override bool OK(string response)
        {
            if (response.Split("\r\n")[0] == "HTTP/1.0 200 OK")
            {
                return true;
            }
            return false;
        }

        public override string Body(string response, string location = null)
        {
            if (location == null)
            {
                return response.Split("\r\n")[^2];
            }

            return location;
        }

        public override bool Error(string response)
        {
            string[] temp = response.Split("\r\n");

            if (temp[0] == "HTTP/1.0 404 Not Found")
            {
                return true;
            }
            return false;
        }
    }
}
