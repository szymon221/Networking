﻿using System;
using System.Net.Sockets;
using System.IO;
public class Whois
{
    static void Main(string[] args)
    {
        int c;
        TcpClient client = new TcpClient();
        client.Connect("whois.networksolutions.com", 43);
        StreamWriter sw = new StreamWriter(client.GetStream());
        StreamReader sr = new StreamReader(client.GetStream());
        sw.WriteLine(args[0]);
        sw.Flush();
        Console.WriteLine(sr.ReadToEnd());
    }
}