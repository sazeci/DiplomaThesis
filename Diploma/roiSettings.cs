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
    public partial class roiSettings : Form
    {

        Capture capWebcam;
        private setCameraInSpaceSettings setCameraInSpaceSettings;

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings(setCameraInSpaceSettings setCameraInSpaceSettings)
        {
            InitializeComponent();
            this.setCameraInSpaceSettings = setCameraInSpaceSettings;

            //show video
            startCamera(setCameraInSpaceSettings._CameraIndex);
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
            setCameraInSpaceSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void roiSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
