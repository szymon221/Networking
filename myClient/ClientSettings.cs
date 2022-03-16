using location.Protocols;
using System;
namespace location
{
    public class ClientSettings
    {
        //Default Settings
        public readonly int Port = 43;
        public readonly string ServerName = "whois.net.dcs.hull.ac.uk";
        public readonly BaseProtocol Proto = new WhoIs();
        public readonly int Timeout = 1000;

        public readonly string LeftOverArguments = String.Empty;


        public ClientSettings(string Args) : this(Args.Split(" ")) { }


        public ClientSettings(string[] Args)
        {
            bool ProcSet = false;
            //DANGER
            //The body of this for loop modifies the counter
            for (int ArgCounter = 0; ArgCounter < Args.Length; ArgCounter++)
            {
                switch (Args[ArgCounter].ToLower())
                {
                    case ("-h"):
                        if (ArgCounter + 1 > Args.Length - 1)
                        {
                            LeftOverArguments = String.Join(" ", Args[ArgCounter]);
                            break;
                        }
                        ServerName = Args[ArgCounter + 1];
                        ArgCounter++;
                        break;

                    case ("-p"):
                        if (ArgCounter + 1 > Args.Length - 1)
                        {
                            LeftOverArguments = String.Join(" ", Args[ArgCounter]);
                            break;
                        }

                        if (!int.TryParse(Args[ArgCounter + 1], out Port))
                        {
                            LeftOverArguments = String.Join(" ", Args[ArgCounter]);
                            Port = 43;
                            break;
                        }
                        ArgCounter++;
                        break;

                    case ("-h9"):
                        CheckProtocol(ProcSet);
                        ProcSet = true;
                        Proto = new H9();
                        break;

                    case ("-h0"):
                        CheckProtocol(ProcSet);
                        ProcSet = true;
                        Proto = new H0();
                        break;

                    case ("-h1"):
                        CheckProtocol(ProcSet);
                        ProcSet = true;
                        Proto = new H1();
                        break;

                    case ("-t"):
                        if (ArgCounter + 1 > Args.Length - 1)
                        {
                            LeftOverArguments = String.Join(" ", Args[ArgCounter]);

                            break;
                        }

                        if (!int.TryParse(Args[ArgCounter + 1], out Timeout))
                        {
                            LeftOverArguments = String.Join(" ", Args[ArgCounter]);

                            break;
                        }
                        ArgCounter++;
                        break;

                    default:
                        LeftOverArguments = String.Join(" ", LeftOverArguments, Args[ArgCounter]);
                        break;
                }
            }
            LeftOverArguments = LeftOverArguments.Trim();
        }

        public void CheckProtocol(bool ProcSet)
        {
            if (ProcSet)
            {
                Console.WriteLine("Error: cannot set multiple protocols");
                Environment.Exit(-1);
            }
        }

    }
}
