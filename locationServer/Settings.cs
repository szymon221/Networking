using System;
using System.IO;
using System.Threading;
namespace locationserver
{
    public class Settings
    {
        public readonly bool Graphical;
        public readonly bool Logging;
        public readonly bool Debug;
        public readonly int Threads = 1;
        public readonly int Port = 43;
        public readonly string DataBaseLocation;
        public readonly bool DatabaseExists = false;
        public readonly string DatabaseContents;
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
                Console.WriteLine("aa");
                switch (Args[ArgCounter].ToLower())
                {
                    case ("-f"):
                        if (ArgCounter + 1 > Args.Length - 1)
                        {
                            throw new ArgumentException("Missing argument for -f");
                        }
                        DataBaseLocation = Args[ArgCounter + 1];
                        DatabaseExists = true;
                        ArgCounter++;
                        break;

                    case ("-w"):
                        Graphical = true;
                        break;

                    case ("-l"):
                        if (ArgCounter + 1 > Args.Length - 1)
                        {
                            throw new LoggerInvalidFilePath("Invalid log file path");
                        }
                        Logger.EnableLogger();
                        Logger.SetLocation(Args[ArgCounter + 1]);
                        Logger.StartThread();
                        ArgCounter++;
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
