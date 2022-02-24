using System;
using System.Collections.Generic;
using System.Text;

namespace location
{
    public class Settings
    {
        //Default Settings
        public readonly int Port = 43;
        public readonly string ServerName = "whois.net.dcs.hull.ac.uk";
        public readonly Protocol Proto = new DefaultProt();
        public readonly int Timeout = 1000;
        public readonly bool Update = false;

        public readonly string User;
        public readonly string Location;


        public Settings(string[] Args)
        {
            string Overflow = "";
            bool ProcSet = false;
            string space = "";

            //DANGER
            //The body of this for loop modifies the counter
            for (int i = 0; i < Args.Length; i++)
            {
                switch (Args[i].ToLower()) 
                {
                    case ("-h"):
                        if (i + 1 > Args.Length-1) {
                            Overflow = $"{Overflow}{space}{Args[i]}";
                            space = " ";
                            break;
                        }
                        ServerName = Args[i + 1];
                        i++;
                        break;

                    case ("-p"):
                        if (i + 1 > Args.Length-1)
                        {
                            Overflow = $"{Overflow}{space}{Args[i]}";
                            space = " ";
                            break;
                        }

                        if (!int.TryParse(Args[i + 1], out Port)) {
                            Port = 43;
                            Overflow = $"{Overflow}{space}{Args[i]}";
                            space = " ";
                            break;
                        }
                        i++;
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
                        if (i + 1 > Args.Length-1)
                        {
                            Overflow = $"{Overflow}{space}{Args[i]}";
                            space = " ";
                            break;
                        }

                        if (!int.TryParse(Args[i + 1], out Timeout))
                        {
                            Overflow = $"{Overflow}{space}{Args[i]}";
                            space = " ";
                            break;
                        }
                        i++;
                        break;

                    default:
                        Overflow = $"{Overflow}{space}{Args[i]}";
                        space = " ";
                        break;            
                }
            }

            string[] OverFlowArray = Overflow.Split(" ");
            //Ugly hack to get the H1.1 Protocol to spec
            if (Proto is H1)
            {
                Proto.SetHostName(ServerName);
            }


            //If only one argument is passed then it's a get request
            if (OverFlowArray.Length == 1) {
                User = OverFlowArray[0];
                Proto.SetVariables(User);
                return;
            }

            //Else it is a update request
            Update = true;
            User = OverFlowArray[0];
            Location = String.Join(' ', OverFlowArray[1..OverFlowArray.Length]);

            Proto.SetVariables(User,Location);
        }


        public void CheckProtocol(bool ProcSet) {

            if (ProcSet) {
                Console.WriteLine("Error: cannot set multiple protocols");
                Environment.Exit(-1);
            }
            
        }



    }
}
