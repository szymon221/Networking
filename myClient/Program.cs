using location;
using System;
using System.IO;
using System.Net.Sockets;

public class Whois
{
    static TcpClient client = new TcpClient();
    static StreamWriter sw;
    static StreamReader sr;
    static Settings Settings;

    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("Staring GUI...");
            Environment.Exit(0);
        }

        Settings = new Settings(args);

        client.ReceiveTimeout = Settings.Timeout;
        client.SendTimeout = Settings.Timeout;
        try
        {
            client.Connect(Settings.ServerName, Settings.Port);
        }
        catch
        {
            Console.WriteLine($"Unable to establish connection with {Settings.ServerName}\r\n");
            Environment.Exit(-1);
        }

        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());

        if (Settings.Update)
        {
            update();
            return;
        }

        query();
    }



    static void update()
    {
        string Response = SeverResponse(Settings.Proto.Update());

        if (Settings.Proto.OK(Response))
        {
            Console.WriteLine($"{Settings.User} location changed to be {Settings.Proto.Body(Response, Settings.Location)}\r\n");
            return;
        }

        Console.WriteLine($"Error {Settings.Proto.Body(Response)}");

    }


    static void query()
    {
        string Response = SeverResponse(Settings.Proto.Query());

        //Looking for OK,200
        if (Settings.Proto.OK(Response))
        {
            Console.WriteLine($"{Settings.User} is {Settings.Proto.Body(Response)}\r\n");
            return;
        }

        //Looking for Error,400
        if (Settings.Proto.Error(Response))
        {
            Console.WriteLine($"Error {Settings.User} not found\r\n");
            return;
        }
        Console.WriteLine($"{Settings.User} is {Settings.Proto.Body(Response)}\r\n");
    }


    static string SeverResponse(string request)
    {
        string Response = "";

        try
        {
            sw.Write(request);
            sw.Flush();
            while (!sr.EndOfStream)
            {
                Response += (char)sr.Read();
            }
        }
        catch (Exception e)
        {
            if (Response.Length == 0)
            {
                Console.WriteLine($"Unable to establish connection with {Settings.ServerName}\r\n");
                Environment.Exit(-1);
            }
        }

        return Response;
    }


}