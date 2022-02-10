using System;
using System.IO;
using System.Net.Sockets;
public class Whois
{
    static TcpClient client = new TcpClient();
    static StreamWriter sw;
    static StreamReader sr;

    static void Main(string[] args)
    {

        if (args.Length > 3 || args.Length < 1)
        {
            Console.WriteLine("Please supply 1 or 2 arguments");
            return;
        }

        if (args.Length == 1)
        {
            query(args[0]);
            return;
        }

        update(args[0], args[1]);

    }

    static void update(string user, string data)
    {

        client.Connect("whois.net.dcs.hull.ac.uk", 43);
        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());
        sw.WriteLine($"{user} {data}");
        sw.Flush();

        string reply = sr.ReadToEnd();

        if (reply == "OK\r\n")
        {
            Console.WriteLine($"{user} location changed to be {data}");
        }
        else
        {
            Console.WriteLine(reply);
        }

    }


    static void query(string q)
    {

        client.Connect("whois.net.dcs.hull.ac.uk", 43);
        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());
        sw.WriteLine(q);
        sw.Flush();
        Console.WriteLine($"{q} is {sr.ReadToEnd()}");

    }

}