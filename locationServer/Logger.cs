using System;
using System.IO;

namespace locationserver
{
    static public class Logger
    {
        static private string FullLocation;
        static private string Path;
        static private string FileName;
        static private bool Enabled = false;
        static private bool LocationSet = false;

        static public void EnableLogger()
        {
            Enabled = true;
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
            CheckIfFileExists();
            CheckReadWrite();
            LocationSet = true;
        }

        static public void Log(string IPAddress, string RequestStyle, string User, string Location, string Status)
        {

            //Create Log string and then send it to the logging pipeline

            if (!Enabled)
            {
                return;
            }

            if (!LocationSet)
            {
                throw new LocationNotSetException("Location for log file has not been set");
            }
            //There shouldn't be any security vulnerability 

            string FullRequest = string.Join(" ", User, Location);
            string DT = $"[{DateTime.Now} {TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)}]";

            using (StreamWriter sw = File.AppendText(FullLocation))
            {
                sw.WriteLine($"{IPAddress} - - {DT} \"{RequestStyle} {FullRequest}\" {Status}");
            }

        }

        static private void CheckIfFileExists()
        {
            if (!File.Exists(FullLocation))
            {
                try
                {
                    File.Create(FullLocation);
                }
                catch
                {
                    throw new LoggerFileExcpetion($"Cannot create {FileName}");
                }
            }
        }
        static private void CheckReadWrite()
        {
            bool canRead;
            bool canWrite;

            using (FileStream fs = File.Open(FullLocation, FileMode.Open))
            {
                canRead = fs.CanRead;
                canWrite = fs.CanWrite;
            }

            if (!canRead & !canWrite)
            {
                throw new LoggerReadWriteException($"Cannot read/write to {FileName}");
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
