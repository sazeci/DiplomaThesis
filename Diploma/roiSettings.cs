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

        private setCameraInSpaceSettings setCameraInSpaceSettings;

        private Point roiStart;
        private Rectangle roi = new Rectangle();
        Image<Bgr, byte> actualCroppedImage;

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings(setCameraInSpaceSettings setCameraInSpaceSettings)
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            this.setCameraInSpaceSettings = setCameraInSpaceSettings;

            //showImage
            ibCamera.Image = setCameraInSpaceSettings.actualImage;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            setCameraInSpaceSettings.Show();
            setCameraInSpaceSettings.isStreamEnabled = true;
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void roiSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseDown(object sender, MouseEventArgs e)
        {
            roiStart = e.Location;
            Console.WriteLine(roiStart);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //actual point
                Point actualPoint = e.Location;

                //convert coordinates from ZOOM to regular image
                Point regularRoiStart;
                Point regularActualPoint;
                coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, roiStart, actualPoint, out regularRoiStart, out regularActualPoint);

                //rectangle
                roi.Location = new Point(Math.Min(regularRoiStart.X, regularActualPoint.X), Math.Min(regularRoiStart.Y, regularActualPoint.Y));
                roi.Size = new Size(Math.Abs(regularRoiStart.X - regularActualPoint.X), Math.Abs(regularRoiStart.Y - regularActualPoint.Y));

                //add roi to camera
                camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi.Location = roi.Location;
                camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi.Size = roi.Size;

                //draw rectangle on image
                actualCroppedImage = setCameraInSpaceSettings.actualImage.ToImage<Bgr, byte>();
                actualCroppedImage.Draw(roi, new Bgr(Color.Red));
                ibCamera.Image = actualCroppedImage;
            }
            else {
                return;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnNext_Click(object sender, EventArgs e)
        {
            //actualCroppedImage.ROI = roi;
            //ibCamera.Image = actualCroppedImage;

            //open new form BW - useless
            //bwTresholdSettings bwTresholdSettings = new bwTresholdSettings(this);
            //bwTresholdSettings.Show();
            //this.Hide();

            //open adition options
            additionalSettings additionalSettings = new additionalSettings(this);
            additionalSettings.Show();
            this.Hide();

            //open new form areas
            //addAreasSettings addAreasSettings = new addAreasSettings(this);
            //addAreasSettings.Show();
            //this.Hide();
        }
    }
}
