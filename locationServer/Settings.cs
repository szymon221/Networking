using System;

namespace locationserver
{
    public class Settings
    {
        public readonly bool Graphical;
        public readonly bool Logging;
        public readonly bool Debug;
        public readonly int Threads = 1001;
        public readonly int Port = 43;

        private static bool _ServerOn = true;
        public static bool ServerOn { get { return _ServerOn; } set { } }

        public static void TurnServerOff()
        {
            _ServerOn = false;
        }


        public Settings(string[] Args)
        {
            for (int ArgCounter = 0; ArgCounter < Args.Length; ArgCounter++)
            {
                switch (Args[ArgCounter].ToLower())
                {
                    case ("-w"):
                        Graphical = true;
                        break;

                    case ("-l"):
                        Logger.EnableLogger();
                        break;

                    case ("-d"):
                        DebugWriter.EnableDebug();
                        break;

                    default:
                        Console.WriteLine($"Warning!: Unrecognised argument {Args[ArgCounter]}");
                        break;
                }

            }

        }
    }
}
