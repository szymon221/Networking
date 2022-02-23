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

        public abstract string QReply();
        public abstract string UReply();


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

        public override string QReply()
        {
            return "OK\r\n";
        }

        public override string UReply()
        {
            return "OK\r\n";
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

        public override string UReply()
        {
            return $"HTTP/0.9 200 OK \r\nContent-Type: text/plan\r\n\r\n{_Location}\r\n";
        }

        public override string QReply()
        {
            return $"HTTP/0.9 200 OK \r\nContent-Type: text/plan\r\n\r\n";
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
        public override string QReply()
        {
            return "HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n";
        }
        public override string UReply()
        {
            return $"HTTP/1.0 200 OK \r\nContent_type: text/plain\r\n\r\n{_Location}\r\n";
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

        public override string QReply()
        {
            return $"HTTP/1.1 200 Ok\r\nContent-Type: text/plain\r\n\r\n{_Location}\r\n";
        }

        public override string UReply()
        {
            return $"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n";
        }

    }



}
