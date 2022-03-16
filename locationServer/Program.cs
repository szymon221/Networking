using System;

namespace locationserver
{
    class Program
    {
        static RequestManager Manager;
           
        static void Main(string[] args)
        {

            Manager = new RequestManager(new Settings(args));
            if (Manager.ServerSettings.Graphical)
            {
                Console.WriteLine("Starting graphical enviroment");
                Environment.Exit(0);
            }

            StartServer();
        }

        public static void StartServer()
        {
            Manager.CreateThreads();
            Manager.Start();
        }

    }
}
