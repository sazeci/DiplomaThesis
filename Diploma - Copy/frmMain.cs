using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diploma
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStartSession_Click(object sender, EventArgs e)
        {
            welcomeSettings welcomeSettings = new welcomeSettings(this);
            welcomeSettings.Show();
            this.Hide();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            testImageBox testImageBox = new testImageBox();
            testImageBox.Show();
            this.Hide();
        }
    }
}
