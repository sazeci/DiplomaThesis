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

/// <summary>
/// windows form for setting up camera in space
/// </summary>
namespace Diploma
{
    public partial class setCameraInSpaceSettings : Form
    {
        private selectCameraSettings selectCameraSettings;
        public int _CameraIndex;//ktera kamera se pouzije
        Capture capWebcam;
        public Mat actualImage;
        public bool isStreamEnabled = true;

        /////////////////////////////////////////////////////////////////////////////////////
        public setCameraInSpaceSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //constructor
        public setCameraInSpaceSettings(selectCameraSettings selectCameraSettings)
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            this.selectCameraSettings = selectCameraSettings;
            _CameraIndex = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera]._CameraIndex;
            Console.WriteLine(_CameraIndex);

            //start camera
            startCamera(_CameraIndex);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //start camera
        private void startCamera(int _CameraIndex)
        {
            //try
            //{
            //    capWebcam = new Capture(_CameraIndex);
            //    capWebcam.Grab();
            //    capWebcam.Retrieve();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("unable to read from webcam, error: " + Environment.NewLine + Environment.NewLine +
            //                    ex.Message + Environment.NewLine + Environment.NewLine +
            //                    "exiting program");
            //    Environment.Exit(0);
            //    return;
            //}

            //Application.Idle += processFrameAndUpdateGUI;

            //Capture capture;
            capWebcam = new Capture(_CameraIndex);

            //test
            //capWebcam.Start();
            //Mat imgOriginal = new Mat();
            //capWebcam.
            //capWebcam.Grab();
            //capWebcam.Retrieve(imgOriginal, 0);
            ////Image<Bgr, Byte> frame = mat.ToImage<Bgr, Byte>();
            //ibCamera.Image = imgOriginal;

            Application.Idle += ProcessFrame;

        }

        /////////////////////////////////////////////////////////////////////////////////////
        //take a frame every time when app is in idle state
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (isStreamEnabled == true)
            {
                Mat imgOriginal = new Mat();
                capWebcam.Grab();
                capWebcam.Retrieve(imgOriginal, 0);
                //Image<Bgr, Byte> frame = mat.ToImage<Bgr, Byte>();
                ibCamera.Image = imgOriginal;

                //for upcomming crop
                actualImage = imgOriginal;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //processFrameAndUpdateGUI
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

            //for upcomming crop
            actualImage = imgOriginal;

            //show actual frame
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

            isStreamEnabled = false;
            this.Hide();
        }
    }
}
