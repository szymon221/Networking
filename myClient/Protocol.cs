using System;
using System.Collections.Generic;
using System.Text;

namespace location
{
    public abstract class Protocol
    {

        public abstract void SetVariables(string User,string Location);
        public abstract void SetVariables(string User);

        public abstract void SetHostName(string HostName);

        public abstract string Query();

        public abstract string Update();

        public abstract string Body(string response,string location=null);

        public abstract bool OK(string response);

        public abstract bool Error(string response);



    }

    public class DefaultProt : Protocol 
    {
        private string _User;
        private string _Location;

        public override void SetHostName(string _) {

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


        public override string Body(string response,string location=null)
        {
            //Query Parsing
            if (location == null) {

                return response.Split("\r\n")[0];
            
            }

            return location;
            //UpdateParsing

        }
        public override bool OK(string response)
        {
            if (response != "ERROR: no entries found\r\n")
            {
                return true;

            }


            return false;
        }

        public override bool Error(string response)
        {
            return !OK(response);
        }
    }

    public class H9 : Protocol
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
            return $"GET /{_User}\r\n";
        }

        public override string Update()
        {
            return $"PUT /{_User}\r\n\r\n{_Location}\r\n";

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

    public class H0 : Protocol
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
            return $"GET /?{_User} HTTP/1.0\r\n\r\n";
        }

        public override string Update()
        {
            return $"POST /{_User} HTTP/1.0\r\nContent-Length: {_Location.Length}\r\n\r\n{_Location}";

        }

        public override bool OK(string response)
        {
            if (response.Split("\r\n")[0] == "HTTP/1.0 200 OK") {

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

    public class H1 : Protocol
    {
        private string _User;
        private string _Location;
        private string _HostName;

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

        public override string Query()
        {
            return $"GET /?name={_User} HTTP/1.1\r\nHost: {_HostName}\r\n\r\n";
        }

        public override string Update()
        {            
            return $"POST / HTTP/1.1\r\nHost: {_HostName}\r\nContent-Length: {_Location.Length + _User.Length+15}\r\n\r\nname={_User}&location={_Location}";

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

            if (location == null) {

                //Console.WriteLine(response);

                string[] temp = response.Split("\r\n");

                string body = "";
                string returncarriage = "";

                bool append = false;
                for (int i = 0; i < temp.Length-1; i++) {


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

            return location;

        }

        public override bool Error (string response)
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
