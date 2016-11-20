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
    public partial class checkTrackingSettings : Form
    {
        private addAreasSettings addAreasSettings;
        public Mat actualImage;
        Capture capWebcam;
        Image<Gray, byte> actualCroppedImage;
        private bool isStreamEnabled = true;

        public checkTrackingSettings()
        {
            InitializeComponent();
        }

        public checkTrackingSettings(addAreasSettings addAreasSettings)
        {
            InitializeComponent();
            this.addAreasSettings = addAreasSettings;
            actualImage = new Mat();
            //start camera
            startCamera(camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera]._CameraIndex);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void startCamera(int _CameraIndex)
        {
            //Capture capture;
            capWebcam = new Capture(_CameraIndex);

            Application.Idle += ProcessFrame;

        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (isStreamEnabled == true)
            {
                capWebcam.Grab();
                capWebcam.Retrieve(actualImage, 0);

                trackLabels();

                //ibCamera.Image = actualImage;
            }
        }

        private void trackLabels()
        {
            isStreamEnabled = false;
            for (int i = 0; i < camera.labelSettings.labelList.Count; i++) {
                //get roi from image
                actualCroppedImage = actualImage.ToImage<Gray, byte>();
                actualCroppedImage.ROI = camera.labelSettings.labelList[i].BB;
                //binarize image
                actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                ibCamera.Image = actualCroppedImage;
                //compare to old one

                //refresh
            }
        }

        private void checkTrackingSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
