using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //

namespace Diploma
{
    public partial class setCameraInSpaceSettings : Form
    {
        private selectCameraSettings selectCameraSettings;
        public int _CameraIndex;//ktera kamera se pouzije
        Capture capWebcam;

        /////////////////////////////////////////////////////////////////////////////////////
        public setCameraInSpaceSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public setCameraInSpaceSettings(selectCameraSettings selectCameraSettings)
        {
            InitializeComponent();
            this.selectCameraSettings = selectCameraSettings;
            _CameraIndex = selectCameraSettings._CameraIndex;
            startCamera(selectCameraSettings._CameraIndex);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void startCamera(int _CameraIndex)
        {
            try
            {
                capWebcam = new Capture(_CameraIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to read from webcam, error: " + Environment.NewLine + Environment.NewLine +
                                ex.Message + Environment.NewLine + Environment.NewLine +
                                "exiting program");
                Environment.Exit(0);
                return;
            }

            Application.Idle += processFrameAndUpdateGUI;

        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        void processFrameAndUpdateGUI(object sender, EventArgs arg)
        {
            Mat imgOriginal;

            imgOriginal = capWebcam.QueryFrame();

            if (imgOriginal == null)
            {
                MessageBox.Show("unable to read frame from webcam" + Environment.NewLine + Environment.NewLine +
                                "exiting program");
                Environment.Exit(0);
                return;
            }
            ibCamera.Image = imgOriginal;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            selectCameraSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void setCameraInSpaceSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnNext_Click(object sender, EventArgs e)
        {
            //open new form
            roiSettings roiSettings = new roiSettings(this);
            roiSettings.Show();
            this.Hide();
        }
    }
}
