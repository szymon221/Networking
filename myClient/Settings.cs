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
                        if (i + 1 > Args.Length) {
                            Console.WriteLine("Fucked it");
                            Environment.Exit(-1);
                        }
                        ServerName = Args[i + 1];
                        i++;
                        break;

                    case ("-p"):
                        if (i + 1 > Args.Length)
                        {
                            Console.WriteLine("Fucked it");
                            Environment.Exit(-1);
                        }
                        if (!int.TryParse(Args[i + 1], out Port)) {

                            Console.WriteLine("Fucked it");
                            Environment.Exit(-1);
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
                        if (i + 1 > Args.Length)
                        {
                            Console.WriteLine("Fucked it");
                            Environment.Exit(-1);
                        }
                        if (!int.TryParse(Args[i + 1], out Timeout))
                        {

                            Console.WriteLine("Fucked it");
                            Environment.Exit(-1);
                        }
                        i++;
                        break;

                    default:
                        Overflow = $"{Overflow}{space}{Args[i]}";
                        space = " ";
                        break;            
                }
            }

            string[] OverFlowArray= Overflow.Split(" ");
            //Ugly hack to get the H1.1 Protocol to spec
            if (Proto is H1)
            {
                Proto.SetHostName(ServerName);
            }


            //If one argument then get
            if (OverFlowArray.Length == 1) {
                User = OverFlowArray[0];
                Proto.SetVariables(User);
                return;
            }

            //If more than 2 then update
            Update = true;
            User = OverFlowArray[0];
            Location = String.Join(' ', OverFlowArray[1..OverFlowArray.Length]);



            Proto.SetVariables(User,Location);





        }


        public void CheckProtocol(bool ProcSet) {

            if (ProcSet) {
                Console.WriteLine("fucked it");
                Environment.Exit(-1);
            }
        
        
        }



    }
}
