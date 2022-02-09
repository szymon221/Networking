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


        if (args.Length == 1) {

            query(args[0]);
            //throw exception

            return;
        }

        if (args.Length == 2)
        {
            update(args[0], args[1]);
        }
      
    }


    static void update(string user, string data) {

        TcpClient client = new TcpClient();
        client.Connect("whois.net.dcs.hull.ac.uk", 43);
        StreamWriter sw = new StreamWriter(client.GetStream());
        StreamReader sr = new StreamReader(client.GetStream());


        sw.WriteLine($"{user} {data}");
        sw.Flush();
       
        

        string reply = sr.ReadToEnd();


        if (reply == "OK\r\n")
        {
            Console.WriteLine($"{user} location changed to be {data}");
        }
        else {

            Console.WriteLine(reply);
        }
        

    }


    static void query(string q) {

        TcpClient client = new TcpClient();
        client.Connect("whois.net.dcs.hull.ac.uk", 43);
        StreamWriter sw = new StreamWriter(client.GetStream());
        StreamReader sr = new StreamReader(client.GetStream());
        sw.WriteLine(q);
        sw.Flush();
        Console.WriteLine($"{q} is {sr.ReadToEnd()}");

    }


}