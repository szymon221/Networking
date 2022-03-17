using location;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
public class WhoIsClient
{
    //Stolen from https://limbioliong.wordpress.com/2011/10/14/minimizing-the-console-window-in-c/
    //See readme
    const Int32 SW_MINIMIZE = 6;
    [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();
    //See readme

    [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

    //See readme

    private static void MinimizeConsoleWindow()
    {
        IntPtr hWndConsole = GetConsoleWindow();
        ShowWindow(hWndConsole, SW_MINIMIZE);
    }

    [STAThread]
    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            MinimizeConsoleWindow();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GraphicalUI());
            Environment.Exit(0);
        }

        ClientSettings Settings = new ClientSettings(args);

        DoRequest(new LocationClient(Settings));
    }
    /// <summary>
    /// Starts the request process
    /// </summary>
    /// <param name="Client"></param>
    static void DoRequest(LocationClient Client)
    {
        try
        {
            Client.SendRequest();
            Console.WriteLine(Client.GetResponse());
        }
        catch ( IOException e)
        {
            Console.WriteLine(e.Message);
        }

    }

}