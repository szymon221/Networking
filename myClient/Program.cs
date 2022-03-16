using location;
using System;


public class WhoIsClient
{


    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("Staring GUI...");
            Environment.Exit(0);
        }

        ClientSettings Settings = new ClientSettings(args);

        DoRequest(new LocationClient(Settings));
    }

    static void DoRequest(LocationClient Client)
    {
        Client.SendRequest();
        Console.WriteLine(Client.GetResponse());
        
    }

}