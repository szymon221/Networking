using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace locationserver
{



    public partial class GraphicalUI : Form
    {
        //Stolen from https://stackoverflow.com/questions/5282588/how-can-i-bring-my-application-window-to-the-front
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        static RequestManager Manager;
        static int ThreadCount;
        static Thread t;

        public GraphicalUI()
        {
            InitializeComponent();
            rtxtErrors.ForeColor = Color.Red;
            txtThreads.GotFocus += ThreadsGotFocus;
            txtThreads.LostFocus += ThreadsLostFocus;
            txtThreads.KeyPress += ThreadsKeyPress;
            txtThreads.Text = "10";
            txtThreads.ForeColor = Color.LightGray;
            Settings.ServerOn = false;

        }

        public void ThreadsKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ThreadsGotFocus(object sender, EventArgs e)
        {
            txtThreads.Text = "";
            txtThreads.ForeColor = Color.Black;
        }
        private void ThreadsLostFocus(object sender, EventArgs e)
        {
            if (txtThreads.Text == String.Empty)
            {
                txtThreads.Text = "10";
                txtThreads.ForeColor = Color.LightGray;      
            }
        }

        private void GraphicalUI_Load(object sender, EventArgs e)
        {
            SetForegroundWindow(Handle.ToInt32());
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!Settings.ServerOn)
            {
                StartServer();
                return;
            }

            StopServer();      

        }

        public void StartServer()
        {
            rtxtErrors.Visible = false;
            if (!CreateArgs(out string arguments))
            {
                rtxtErrors.Visible = true;
                StopServer();
                return;
            }

            Settings.ServerOn = true;
            btnStartStop.Text = "Stop";
            DisableControls();

            Manager = new RequestManager(new Settings(arguments));
            Manager.ServerSettings.SetThreads(ThreadCount);
            Manager.CreateThreads();
            t = new Thread(() => Manager.Start());
            t.Start();

        }

        public void StopServer()
        {
            btnStartStop.Text = "Start";
            EnableControls();
            Manager.Stop();
            Manager = null;
        }


        public bool CreateArgs(out string Args)
        {
            string DatabaseLocation="";
            string LoggingLocation="";
            string Debug="";
            if (chbxDatabase.Checked)
            {
                if (txtDatabase.Text == String.Empty) {

                    rtxtErrors.Text = "Database Location cannot be empty";
                    Args = "";
                    return false;
                }

                DatabaseLocation = $"-f {txtDatabase.Text}";            
            }


            if (chbxLogging.Checked)
            {
                if (txtLogging.Text == String.Empty)
                {
                    rtxtErrors.Text = "Logging Location cannot be empty";
                    Args = "";
                    return false;
                }
                LoggingLocation = $"-l {txtLogging.Text}";
            }

            if (chbxDebug.Checked)
            {
                Debug = "-d";         
            }

            ThreadCount = int.Parse(txtThreads.Text);
            if (ThreadCount > 999) {
                ThreadCount = 999;
                txtThreads.Text = "999";
            }

            Args = String.Join("", DatabaseLocation, LoggingLocation, Debug);
            return true;
        }

        public void EnableControls()
        {
            chbxDatabase.Enabled = true;
            chbxDebug.Enabled = true;
            chbxLogging.Enabled = true;
            txtDatabase.Enabled = true;
            txtLogging.Enabled = true;
            txtThreads.Enabled = true;
            lblThreads.ForeColor = Color.Black;

        }

        public void DisableControls()
        {
            chbxDatabase.Enabled = false;
            chbxDebug.Enabled = false;
            chbxLogging.Enabled = false;
            txtDatabase.Enabled = false;
            txtLogging.Enabled = false;
            txtThreads.Enabled = false;
            lblThreads.ForeColor = Color.Gray;
        }

        private void rtxtErrors_TextChanged(object sender, EventArgs e)
        {

        }

        private void chbxDatabase_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
