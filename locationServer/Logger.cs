using System;
using System.IO;

namespace locationserver
{
    static public class Logger
    {
        static private string FullLocation;
        static private bool Enabled = false;
        static private bool LocationSet = false;

        static public void EnableLogger()
        {
            Enabled = true;
        }

        static public void SetLocation(string LogLocation, string LogName) {

            FullLocation = $@"{LogLocation}\{LogName}";
            CheckIfFileExists();
            CheckReadWrite();
            LocationSet = true;
        }

        static public void Log<T>(params T[] Vals)
        {

            if (!Enabled)
            {
                return;
            }

            if (!LocationSet)
            {
                throw new LocationNotSetException("Location for log file has not been set");
            }

            foreach (object Paramter in Vals) {
                Type temp = Paramter.GetType();
                Console.WriteLine(temp.Name);
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
                    throw new LoggerFileExcpetion("Cannot create file");
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
                throw new LoggerReadWriteException("Cannot write/write to file");
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

            if (!Enabled) {
                return;
            }

            Console.WriteLine(Message);
        }
   
    }


    class LoggerFileExcpetion : Exception
    {
        public LoggerFileExcpetion()
        {
        }
        public LoggerFileExcpetion(string message)
            : base(message)
        {
        }
        public LoggerFileExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }

    }

    class LoggerReadWriteException : Exception
    {

        public LoggerReadWriteException()
        {
        }
        public LoggerReadWriteException(string message)
            : base(message)
        {
        }

        public LoggerReadWriteException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }

    class LocationNotSetException : Exception
    {

        public LocationNotSetException()
        {
        }
        public LocationNotSetException(string message)
            : base(message)
        {
        }

        public LocationNotSetException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
