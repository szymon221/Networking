using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections.Generic;
namespace locaitonserver
{
    class Program
    {


        static readonly int PORT = 43;
        static Socket listener;
        static Dictionary<string, string> LocationLookup = new Dictionary<string, string>();

#if DEBUG
        static string path = @"..\Logs\DebugServer.txt";
#else
        static string path = @"..\Logs\ReleaseServer.txt";       
#endif


        //Create global task queue
        //Create global return queue

        //Create all socket objects


        static void Main(string[] args)
        {


            try
            {
                //Register signal handler for ctr+c


                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    File.Create(path);
                }
                log("Server has started");

                bool serverOn = true;

                setUpSocket();

                setUpThreads();

                while (serverOn) {

                    processRequest();
                }

            
            }
            catch (Exception e)
            {

                log(e.Message);
            }
        }

        

        static void setUpSocket()
        {

            log("Starting socket setup");

            try
            {
                IPHostEntry HOST = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = HOST.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

                listener = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(ipAddress, PORT));
                listener.Listen(20);
                log("Completed socket setup");

            }
            catch 
            {

                log("Error has occured");

                Environment.Exit(-1);
            }
        
        }

        static void processRequest() {

            log("entered scoket request");
            try
            {
                Socket handler = listener.Accept();

                log("Socket has been conneted to");

                string data = "";
                byte[] readBuffer = null;


                //1024 bytes is all anyone will ever need
                readBuffer = new byte[1024];
                int total = handler.Receive(readBuffer);
                data += Encoding.ASCII.GetString(readBuffer, 0, total);
                log($"{data}");


                string response = parseRequest(data);

                handler.Send(Encoding.ASCII.GetBytes(response));
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch{

                log("fucked it");
            }


        }

        static string parseRequest(string data) {


            if (data.Split(" ").Length > 1) {

                //update
                string user = data.Split(" ")[0];
                string location = string.Join(" ",data.Split(" ")[1..]).Split("\r\n")[0];

                if (LocationLookup.TryGetValue(user, out _))
                {
                    LocationLookup[user] = location;
                    return "OK\r\n";
                }

                LocationLookup.Add(user,location);
                return "OK\r\n";


            
            }

            string loc;

            if (!LocationLookup.TryGetValue(data.Split("\r\n")[0],out loc)) {
                loc = "ERROR: no entries found\r\n";
            }

            return loc;
        }


        static void setUpThreads() {            
            //Create threadpool

        }



        static void log(string message) {

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{DateTime.Now} {message}");
            }


        }
    }
}
