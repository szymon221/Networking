﻿using System;

namespace locationserver
{
    public class Settings
    {
        public readonly bool Graphical;
        public readonly bool Logging;
        public readonly bool Debug;
        public readonly int Threads = 10;
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
                        if (ArgCounter + 1 > Args.Length - 1) {
                            throw new LoggerInvalidFilePath("Invalid log file path");
                        }
                        Logger.EnableLogger();
                        Logger.SetLocation(Args[ArgCounter + 1]);
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
