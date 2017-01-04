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
/// windows form with additional settings before actual data acquisition
/// </summary>
namespace Diploma
{
    public partial class additionalSettings : Form
    {
        private roiSettings roiSettings;

        public additionalSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //constructor
        public additionalSettings(roiSettings roiSettings)
        {
            InitializeComponent();
            this.roiSettings = roiSettings;
            //fill combobox
            comboBoxFill();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //fill comboBox with frame
        private void comboBoxFill()
        {
            List<KeyValuePair<int, string>> frameList = new List<KeyValuePair<int, string>>();
            frameList.Add(new KeyValuePair<int, string>(1000, "1000"));
            frameList.Add(new KeyValuePair<int, string>(2000, "2000"));
            frameList.Add(new KeyValuePair<int, string>(5000, "5000"));
            frameList.Add(new KeyValuePair<int, string>(10000, "10000"));

            //add values to combobox
            cbFrame.DataSource = new BindingSource(frameList, null);
            cbFrame.DisplayMember = "Value";
            cbFrame.ValueMember = "Key";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            roiSettings.Show();
            this.Hide();
        }

        private void additionalSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            //set option to camera
            setOptionToCamera();
            
            //new form
            addAreasSettings addAreasSettings = new addAreasSettings(this);
            addAreasSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //set name of file, and selected settings for upcomming data acquisition
        private void setOptionToCamera()
        {
            //file name
            camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].fileName = tbFileName.Text;
            //frame
            KeyValuePair<int, string> SelectedItem = (KeyValuePair<int, string>)cbFrame.SelectedItem;
            camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].frame = SelectedItem.Key;
            //backUpEnabled
            if (cbBackup.Checked == true)
            {
                camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].isBackUpEnabled = true;
            }
            else {
                camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].isBackUpEnabled = false;
            }
        }
    }
}
