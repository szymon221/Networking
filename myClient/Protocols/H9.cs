using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace location.Protocols
{
    public class H9 : BaseProtocol
    {

        private string _User;
        private string _Location;

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
            sw.Write($"GET /{_User}\r\n");
            sw.Flush();
        }

        public override void Update(StreamWriter sw)
        {
            sw.Write($"PUT /{_User}\r\n\r\n{_Location}\r\n");
            sw.Flush();
        }

        public override string Body(string response, string location = null)
        {
            if (location == null)
            {
                return response.Split("\r\n")[^2];
            }
            return location;
        }

        public override bool OK(string response)
        {
            string[] temp = response.Split("\r\n");

            if (temp[0] == "HTTP/0.9 200 OK")
            {
                return true;
            }
            return false;
        }

        public override bool Error(string response)
        {
            string[] temp = response.Split("\r\n");

            if (temp[0] == "HTTP/0.9 404 Not Found")
            {
                return true;
            }
            return false;
        }
    }
}
