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
    public partial class selectCameraSettings : Form
    {
        private welcomeSettings welcomeSettings;
        public int _CameraIndex;//ktera kamera se pouzije
        List<KeyValuePair<int, string>> cameraList = new List<KeyValuePair<int, string>>();
        /////////////////////////////////////////////////////////////////////////////////////
        public selectCameraSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public selectCameraSettings(welcomeSettings welcomeSettings)
        {
            InitializeComponent();
            this.welcomeSettings = welcomeSettings;

            //get list of instaled cameras
            camera.cameraSettings cameraSettings = new camera.cameraSettings();
            cameraList = cameraSettings.getListOfCameras();

            //set values to comboBox
            setComboBox();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void setComboBox()
        {
            //cleaning of combobox
            ComboBoxCameraList.DataSource = null;
            ComboBoxCameraList.Items.Clear();

            //add values to combobox
            ComboBoxCameraList.DataSource = new BindingSource(cameraList, null);
            ComboBoxCameraList.DisplayMember = "Value";
            ComboBoxCameraList.ValueMember = "Key";
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            welcomeSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void selectCameraSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnNext_Click(object sender, EventArgs e)
        {
            //get index of selected camera
            KeyValuePair<int, string> SelectedItem = (KeyValuePair<int, string>)ComboBoxCameraList.SelectedItem;
            _CameraIndex = SelectedItem.Key;
            Console.WriteLine(_CameraIndex);

            //open new form
            setCameraInSpaceSettings setCameraInSpaceSettings = new setCameraInSpaceSettings(this);
            setCameraInSpaceSettings.Show();
            this.Hide();
        }
    }
}
