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
    public partial class testImageBox : Form
    {
        public int _CameraIndex;//ktera kamera se pouzije
        Capture capWebcam;
        public Mat actualImage;
        Image<Gray, Byte> colorResult;
        Point click;

        public testImageBox()
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            _CameraIndex = 0;
        }

        private void testImageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnNewCapture_Click(object sender, EventArgs e)
        {
            capWebcam = new Capture(_CameraIndex);

            Mat imgOriginal = new Mat();
            capWebcam.Grab();
            capWebcam.Retrieve(imgOriginal, 0);
            //Image<Bgr, Byte> frame = mat.ToImage<Bgr, Byte>();
            ibCamera.Image = imgOriginal;

            actualImage = imgOriginal;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            capWebcam = new Capture(_CameraIndex);

            Mat imgOriginal = new Mat();
            capWebcam.Grab();
            capWebcam.Retrieve(imgOriginal, 0);
            //Image<Bgr, Byte> frame = mat.ToImage<Bgr, Byte>();
            ibCamera.Image = imgOriginal;

            actualImage = imgOriginal;
        }

        private void btnMarkers_Click(object sender, EventArgs e)
        {
            imageManipulation.findMarkers findMarkers = new imageManipulation.findMarkers();

            //template matching
            //DialogResult drChosenFile;
            //drChosenFile = ofdOpenFile.ShowDialog();
            //Mat imgOriginal;
            //imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);
            //ibMarker.Image = imgOriginal;
            //ibCamera.Image = findMarkers.findTemplate(actualImage, imgOriginal);


            //colorMatching
            //ibCamera.Image = findMarkers.hsvTransform(actualImage);

            //find red things
            ibCamera.Image = findMarkers.findColor(actualImage);

        }

        private void btnSelectText_Click(object sender, EventArgs e)
        {
            //binarize image
            imageManipulation.bwThresholding bwThresholding = new imageManipulation.bwThresholding();
            Mat binarized = bwThresholding.adaptiveBwCrop(actualImage,0);
            ibCamera.Image = binarized;

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(binarized, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);

            //conver to real coordinates from ouse click
            Point regularRoiStart;
            Point regularActualPoint;
            coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, click, click, out regularRoiStart, out regularActualPoint);
        }

        private void ibCamera_MouseDown(object sender, MouseEventArgs e)
        {
            click = e.Location;
            Console.WriteLine(click);
        }
    }
}
