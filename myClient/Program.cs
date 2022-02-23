using System;
using System.IO;
using System.Net.Sockets;
using location;
using System.Collections.Generic;
public class Whois
{
    static TcpClient client = new TcpClient();
    static StreamWriter sw;
    static StreamReader sr;
    static Settings Settings;


    static void Main(string[] args)
    {

        if (args.Length == 0) {

            Console.WriteLine("Staring GUI...");
            Environment.Exit(0);
        }


        Settings = new Settings(args);



        if (Settings.Update) {

            update();
            return;
        }

        query();


    }



static void update()
    {

        client.Connect(Settings.ServerName,Settings.Port);

        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());

        client.ReceiveTimeout = Settings.Timeout;
        client.SendTimeout = Settings.Timeout;

        string request = Settings.Proto.Update();
        string response = "";
        try
        {
            sw.Write(request);
            sw.Flush();

            response = sr.ReadToEnd();

            if (response == Settings.Proto.UReply())
            {
                Console.WriteLine($"{Settings.User} location changed to be {Settings.Location}");
            }
            else
            {
                Console.WriteLine(response);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(0);
        }


    }


    static void query()
    {
        client.Connect(Settings.ServerName, Settings.Port);


        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());

        client.ReceiveTimeout = Settings.Timeout;
        client.SendTimeout = Settings.Timeout;
        List<string> Response = new List<string>();
        try
        {

            string request = Settings.Proto.Query();

            sw.Write(request);
            sw.Flush();

            while (!sr.EndOfStream) {
                Response.Add(sr.ReadLine()+"\r\n");
            }


            Console.WriteLine($"{Settings.User} is {String.Join(' ', Response)}");


        }
        catch (Exception e) {

            if (Response.Count != 0) {

                int Length = GetContentLength(Response);
                string temp = String.Join("", Response);
                string body = "";
                
                for (int i = 0; i < Length; i++) {
                    //Need the 2 offset for whatever reason
                    body += temp[temp.Length - Length + i -2];
                }
                Console.WriteLine($"{Settings.User} is {body.Trim()}");

                Environment.Exit(-1);
            }

            Console.WriteLine(e.Message);


            Environment.Exit(0);
        }


        static int GetContentLength(List<string> Response) {

            foreach(string x in Response)
            {

                if ( x.Length < 14) {

                    continue;

                }
                string y = x[0..14];

                if (x[0..14] == "Content-Length") {

                    return int.Parse(x.Split(" ")[1]);
                }

            }


            return 0;
        }

    }

}