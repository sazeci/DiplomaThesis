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
    public partial class addAreasSettings : Form
    {
        private roiSettings roiSettings;
        public Mat actualImage;
        Capture capWebcam;
        Image<Bgr, byte> actualCroppedImage;
        private Point roiStart;
        private Rectangle roi = new Rectangle();
        private bool isStreamEnabled = true;

        /////////////////////////////////////////////////////////////////////////////////////
        public addAreasSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public addAreasSettings(roiSettings roiSettings)
        {
            InitializeComponent();
            this.roiSettings = roiSettings;
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
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

                //crop
                actualCroppedImage = actualImage.ToImage<Bgr, byte>();
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;

                ibCamera.Image = actualCroppedImage;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void addAreasSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            roiSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnAddArea_Click(object sender, EventArgs e)
        {
            lblInstruction.Text = "2) Select area of interest in image";
            ibCamera.Enabled = true;
            btnAddArea.Enabled = false;

            //turn of stream
            //croppedImageInitial = actualCroppedImage;//DOESNT WORK
            isStreamEnabled = false;
            
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseDown_1(object sender, MouseEventArgs e)
        {
            roiStart = e.Location;
            Console.WriteLine(roiStart);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //actual point
                Point actualPoint = e.Location;

                //convert coordinates from ZOOM to regular image
                Point regularRoiStart;
                Point regularActualPoint;
                coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, roiStart, actualPoint, out regularRoiStart, out regularActualPoint);
                Console.WriteLine(regularRoiStart + " | " + regularActualPoint);

                //rectangle
                roi.Location = new Point(Math.Min(regularRoiStart.X, regularActualPoint.X), Math.Min(regularRoiStart.Y, regularActualPoint.Y));
                roi.Size = new Size(Math.Abs(regularRoiStart.X - regularActualPoint.X), Math.Abs(regularRoiStart.Y - regularActualPoint.Y));

                //add roi to labels class
                //Mouse UP              

                //draw rectangle on image
                capWebcam.Grab();
                capWebcam.Retrieve(actualImage, 0);

                //crop
                actualCroppedImage = actualImage.ToImage<Bgr, byte>();
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
                actualCroppedImage.Draw(roi, new Bgr(Color.Red));
                ibCamera.Image = actualCroppedImage;
            }
            else
            {
                return;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseUp(object sender, MouseEventArgs e)
        {
            camera.labelSettings.addLabel(roi);
        }
    }
}
