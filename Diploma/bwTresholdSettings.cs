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
    public partial class bwTresholdSettings : Form
    {
        private roiSettings roiSettings;
        Capture capWebcam;
        public Mat actualImage;

        /////////////////////////////////////////////////////////////////////////////////////
        public bwTresholdSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public bwTresholdSettings(roiSettings roiSettings)
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            this.roiSettings = roiSettings;

            //inicialize radiobuttons
            globalSelected();

            //start camera
            startCamera(camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera]._CameraIndex);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void globalSelected()
        {
            //turn off adaptive
            this.rdbAdaptive.Checked = false;
            this.tbAdaptive.Enabled = false;
            //turn on global
            this.tbGlobal.Enabled = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            roiSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void rdbGlobal_CheckedChanged(object sender, EventArgs e)
        {
            //turn off adaptive and turn on global
            globalSelected();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void rdbGlobal_MouseDown(object sender, MouseEventArgs e)
        {
            this.rdbGlobal.Checked = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void rdbAdaptive_CheckedChanged(object sender, EventArgs e)
        {
            //turn off global
            this.rdbGlobal.Checked = false;
            this.tbGlobal.Enabled = false;

            //turn on adaptive
            this.tbAdaptive.Enabled = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void rdbAdaptive_MouseDown(object sender, MouseEventArgs e)
        {
            this.rdbAdaptive.Checked = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void bwTresholdSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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



            //show actual frame


            Image<Bgr, byte> actualCroppedImage = imgOriginal.ToImage<Bgr, byte>();
            actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            ibCamera.Image = actualCroppedImage;
            //ibCamera.Image = imgOriginal;
        }

        //private IImage bwImage(Mat imgOriginal)
        //{
        //    if (this.rdbGlobal.Checked == true)
        //    {
        //        return(imageManipulation.bwTreshold.global(imgOriginal, this.tbGlobal.Value));
        //    }
        //    else if (this.rdbAdaptive.Checked == false) {

        //    }
        //}
    }
}
