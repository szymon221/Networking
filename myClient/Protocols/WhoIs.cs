using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

        public override void Query(StreamWriter sr)
        {
            sr.Write($"{_User}\r\n");
            sr.Flush();
        }

        public override void Update(StreamWriter sr)
        {
            sr.Write($"{_User} {_Location}\r\n");
            sr.Flush();

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
            if (response == "ERROR: no entries found\r\n")
            {

                return true;
            }

            return false;
        }
    }
}
