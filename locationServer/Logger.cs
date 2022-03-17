using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
namespace locationserver
{
    static public class Logger
    {
        static private string FullLocation;
        static private string Path;
        static private string FileName;
        static private bool Enabled = false;
        static private bool LocationSet = false;
        static private ConcurrentQueue<string> LoggingQueue = new ConcurrentQueue<string>();
        static private Thread LogThread;
        static public void EnableLogger()
        {
            Enabled = true;
        }

        static public void StartThread()
        {
            LogThread = new Thread(ThreadLogger);
            LogThread.Start();
            
        }

        static public void SetLocation(string FullPath)
        {
            try
            {
                Path = string.Join(@"\", FullPath.Split(@"\")[0..^2]);
                FileName = FullPath.Split(@"\")[^1];
            }
            catch
            {

                throw new LoggerInvalidFilePath();
            }
            FullLocation = FullPath;
            
            LocationSet = true;
        }

        static public void Log(string IPAddress, string RequestStyle, string User, string Location, string Status)
        {

            //Create Log string and then send it to the logging pipeline


            //There shouldn't be any security vulnerability 

            string FullRequest = string.Join(" ", User, Location);
            string DT = $"[{DateTime.Now} {TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)}]";

            string Log = $"{IPAddress} - - {DT} \"{RequestStyle} {FullRequest}\" {Status}";
            LoggingQueue.Enqueue(Log);

        }


        private static void ThreadLogger()
        {
            while (true)
            {
                if (LoggingQueue.IsEmpty)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (!LoggingQueue.TryDequeue(out string Log))
                {
                    Thread.Sleep(1);
                    continue;
                }


                using (StreamWriter writetext = new StreamWriter(FullLocation,append:true))
                {
                    writetext.WriteLine(Log);
                }


            }
        }

    }

    static public class DebugWriter
    {
        static private bool Enabled = false;
        static public void EnableDebug()
        {
            Enabled = true;
        }

        static public void Write(string Message)
        {
            if (!Enabled)
            {
                return;
            }
            Console.WriteLine(Message);
        }
    }
    class LoggerFileExcpetion : Exception
    {
        public LoggerFileExcpetion() { }
        public LoggerFileExcpetion(string message) : base(message) { }
        public LoggerFileExcpetion(string message, Exception inner) : base(message, inner) { }
    }
    class LoggerInvalidFilePath : Exception
    {
        public LoggerInvalidFilePath() { }
        public LoggerInvalidFilePath(string message) : base(message) { }
        public LoggerInvalidFilePath(string message, Exception inner) : base(message, inner) { }
    }

    class LoggerReadWriteException : Exception
    {
        public LoggerReadWriteException() { }
        public LoggerReadWriteException(string message) : base(message) { }
        public LoggerReadWriteException(string message, Exception inner) : base(message, inner) { }
    }
    class LocationNotSetException : Exception
    {
        public LocationNotSetException() { }
        public LocationNotSetException(string message) : base(message) { }
        public LocationNotSetException(string message, Exception inner) : base(message, inner) { }
    }
}
