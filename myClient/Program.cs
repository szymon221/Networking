using location;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class WhoIsClient
{
    //Stolen from https://limbioliong.wordpress.com/2011/10/14/minimizing-the-console-window-in-c/

    const Int32 SW_MINIMIZE = 6;
    [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);


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

    static void DoRequest(LocationClient Client)
    {
        Client.SendRequest();
        Console.WriteLine(Client.GetResponse());

    }

}