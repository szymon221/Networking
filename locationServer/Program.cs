using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace locationserver
{
    class Program
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

        static RequestManager Manager;
        [STAThread]
        static void Main(string[] args)
        {

            Manager = new RequestManager(new Settings(args));


            if (Manager.ServerSettings.Graphical)
            {
                Manager = null;
                MinimizeConsoleWindow();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GraphicalUI());
                Environment.Exit(0);
            }


            StartServer();



        }

        public static void StartServer()
        {
            Settings.ServerOn = true;
            Manager.CreateThreads();
            Manager.Start();
        }

    }
}
