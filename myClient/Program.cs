using System;
using System.Net.Sockets;
using System.IO;
public class Whois
{
    static void Main(string[] args)
    {

        if (args.Length > 3) {

            Console.WriteLine("Maximum of 2 arguments expected");
            return;
        }

        if (args.Length < 1) {

            Console.WriteLine("Minimum of 1 argument expected");
            return;
        }


        int c;
        TcpClient client = new TcpClient();
        client.Connect("whois.net.dcs.hull.ac.uk", 43);
        StreamWriter sw = new StreamWriter(client.GetStream());
        StreamReader sr = new StreamReader(client.GetStream());
        sw.WriteLine(args[0]);
        sw.Flush();
        Console.WriteLine(sr.ReadToEnd());
    }
}