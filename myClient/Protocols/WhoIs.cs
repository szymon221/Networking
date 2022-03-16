using System;
using System.Collections.Generic;
using System.Text;

namespace location.Protocols
{
    public class WhoIs : BaseProtocol
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

        public override string Query()
        {
            return $"{_User}\r\n";
        }

        public override string Update()
        {
            return $"{_User} {_Location}\r\n";
        }

        public override string Body(string response, string location = null)
        {
            if (location == null)
            {
                return response.Split("\r\n")[0];
            }
            return location;
        }
        public override bool OK(string response)
        {
            if (response == "OK\r\n")
            {
                return true;
            }

            return false;
        }

        public override bool Error(string response)
        {
            if (response == "ERRor: no entries found\r\n")
            {

                return true;
            }

            return false;
        }
    }
}
