using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// windows form for first step during the settings
/// </summary>
namespace Diploma
{
    public partial class welcomeSettings : Form
    {
        private frmMain frmMain;

        public welcomeSettings(frmMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMain.Show();
            this.Hide();
        }

        private void welcomeSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectCameraSettings selectCameraSettings = new selectCameraSettings(this);
            selectCameraSettings.Show();
            this.Hide();
        }
    }
}
