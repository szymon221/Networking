using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace location
{


    public partial class GraphicalUI : Form
    {
        //Stolen from https://stackoverflow.com/questions/5282588/how-can-i-bring-my-application-window-to-the-front
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        List<string> ConsoleOutput = new List<string>();

        public GraphicalUI()
        {
            InitializeComponent();
            txtbxTimeout.KeyPress += TimeoutKeyDown;
            txtArgs.KeyPress += SendRequest;

        }

        private void SendData()
        {
            if (txtArgs.Text.Trim() == String.Empty)
            {
                lblEmpty.Visible = true;
                return;
            }
            lblEmpty.Visible = false;

            string protcol = SetProtocol();
            string Timeout = GetTimeout();

            string ServerName = GetServerName();
            string Port = GetPort();


            ClientSettings Settings = new ClientSettings(string.Join(" ", protcol, Timeout, ServerName, Port, txtArgs.Text).Trim());
            LocationClient Client = new LocationClient(Settings);

            Client.SendRequest();
            ConsoleOutput.Add(Client.GetResponse());

            DrawConsole();
        }

        private string GetTimeout()
        {
            if (txtbxTimeout.Text == String.Empty)
            {

                return "";
            }

            return $"-t {txtbxTimeout.Text}";

        }


        private void DrawConsole()
        {
            rtxtServerOuput.Text = String.Join("", ConsoleOutput);
            rtxtServerOuput.SelectionStart = rtxtServerOuput.Text.Length;
            rtxtServerOuput.ScrollToCaret();

        }

        private string GetPort()
        {
            string Text = txtbxServer.Text.Trim();



            if (Text.Contains(":"))
            {
                //needs error handling
                return $"-p {Text.Split(":")[1]}";
            }

            return "";

        }

        private string GetServerName()
        {
            string Text = txtbxServer.Text.Trim();

            if (Text == String.Empty)
            {
                return "";
            }

            if (Text.Contains(":"))
            {

                return Text.Split(":")[0];
            }

            return $"-h {Text}";

        }

        private string SetProtocol()
        {

            if (btnH9.Checked)
            {
                return "-h9";

            }
            if (btnH0.Checked)
            {
                return "-h0";

            }
            if (btnH1.Checked)
            {
                return "-h1";

            }

            return String.Empty;

        }


        private void GraphicalUI_Load(object sender, EventArgs e)
        {
            SetForegroundWindow(Handle.ToInt32());
        }

        private void txtbxTimeout_TextChanged(object sender, EventArgs e)
        {


        }
        private void TimeoutKeyDown(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SendRequest(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {

                SendData();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void txtArgs_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsoleOutput = new List<string>();
            DrawConsole();
        }
    }
}
