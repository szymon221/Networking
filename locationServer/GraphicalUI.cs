using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace locationserver
{
    public partial class GraphicalUI : Form
    {
        //Stolen from https://stackoverflow.com/questions/5282588/how-can-i-bring-my-application-window-to-the-front
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);
        public GraphicalUI()
        {
            InitializeComponent();
        }

        private void GraphicalUI_Load(object sender, EventArgs e)
        {
            SetForegroundWindow(Handle.ToInt32());
        }
    }
}
