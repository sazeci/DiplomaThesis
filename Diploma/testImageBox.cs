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
using System.Collections;

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

            //convert to real coordinates from our click
            Point regularRoiStart;
            coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, click, click, out regularRoiStart, out regularRoiStart);

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(binarized, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            Image<Gray, Int16> labelsImg = labels.ToImage<Gray, Int16>();
            Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();
            Image<Gray, Int16> centroidsImg = centroids.ToImage<Gray, Int16>();

            //closest centroid to click
            int actualLabel = 0;
            int closestLabel = 0;
            int pointRow;
            int pointCollum;
            int width = 1;
            int height = 1;
            if (labelsImg.Data[regularRoiStart.Y, regularRoiStart.X, 0] != 0)
            {
                Console.WriteLine("Click on label: " + labelsImg.Data[regularRoiStart.Y, regularRoiStart.X, 0]);
                actualLabel = labelsImg.Data[regularRoiStart.Y, regularRoiStart.X, 0];
            }
            else {//dont use centroids, snail is better because of iner labels
                pointRow = regularRoiStart.Y;
                pointCollum = regularRoiStart.X;
                int help;

                while (closestLabel == 0) {
                    //top
                    help = pointRow + height;
                    for (int i = pointRow; i < help; i++) {
                        //Console.WriteLine(pointRow + " x " + pointCollum);
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0) {
                            closestLabel = labelsImg.Data[pointRow, pointCollum, 0];
                            break;
                        }
                        pointRow--;
                    }
                    height++;
                    //right
                    help = pointCollum + width;
                    for (int i = pointCollum; i < help; i++)
                    {
                        //Console.WriteLine(pointRow + " x " + pointCollum);
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0)
                        {
                            closestLabel = labelsImg.Data[pointRow, pointCollum, 0];
                            break;
                        }
                        pointCollum++;
                    }
                    width++;
                    //down
                    help = pointRow + height;
                    for (int i = pointRow; i < help; i++)
                    {
                        //Console.WriteLine(pointRow + " x " + pointCollum);
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0)
                        {
                            closestLabel = labelsImg.Data[pointRow, pointCollum, 0];
                            break;
                        }
                        pointRow++;
                    }
                    height++;
                    //left
                    help = pointCollum + width;
                    for (int i = pointCollum; i < help; i++)
                    {
                        //Console.WriteLine(pointRow + " x " + pointCollum);
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0)
                        {
                            closestLabel = labelsImg.Data[pointRow, pointCollum, 0];
                            break;
                        }
                        pointCollum--;
                    }
                    width++;
                }

                Console.WriteLine("Closest label: " + closestLabel);
            }

            ///find possible candidates
            if (actualLabel == 0 && closestLabel != 0) {
                actualLabel = closestLabel;
            }

            //help to understand stats
            //Console.WriteLine("Stats: " + statsImg.Data[actualLabel, 0, 0] + " | " + statsImg.Data[actualLabel, 1, 0] + " | " + statsImg.Data[actualLabel, 2, 0] + " | " + statsImg.Data[actualLabel, 3, 0] + " | " + statsImg.Data[actualLabel, 4, 0]);
            //LEFT_x, top_Y, Width, height, area

            //go to the left(max distance is equal to letter width) and check if: size, color 
            ArrayList candidates = new ArrayList();
            candidates.Add(actualLabel);
            int startRow;
            int startCollum;
            int startLabel = actualLabel;

            startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
            startCollum = statsImg.Data[startLabel, 0, 0];

            for (int i = startCollum-1; i > startCollum - statsImg.Data[actualLabel, 2, 0]; i--) {
                //Console.WriteLine("row= " + startRow + " collum= " + i);
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20) {//not background and bigger than 20px
                    //TODO next conditions
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //choose new initial letter and cotinue to left
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    startRow = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2;
                    startCollum = statsImg.Data[actualLabel, 0, 0];
                    i = startCollum - 1;
                }
            }
            //go to the right(max distance is equal to letter width) and check if: size, color 
            startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
            startCollum = statsImg.Data[startLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0];
            actualLabel = startLabel;
            for (int i = startCollum + 1; i < startCollum + statsImg.Data[actualLabel, 2, 0]; i++)
            {
                Console.WriteLine("row= " + startRow + " collum= " + i);
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
                {//not background and bigger than 20px
                    //TODO next conditions
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //choose new initial letter and cotinue to left
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    startRow = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2;
                    startCollum = statsImg.Data[actualLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0];
                    i = startCollum + 1;
                }
            }

            //print candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                Console.WriteLine(candidates[i]);
            }

            /////select selest closest area
            ////Console.WriteLine("Number of labels = " + numberOfLabels);
            ////Console.WriteLine("Size of labels = " + labels.Size + " element size: " + labels.ElementSize + " Number of dimension: " + labels.SizeOfDimemsion);
            ////Console.WriteLine("Size of stats = " + stats.Size + " element size: " + stats.ElementSize + " Number of dimension: " + stats.SizeOfDimemsion);
            ////Console.WriteLine("Size of centroids = " + centroids.Size + " element size: " + centroids.ElementSize + " Number of dimension: " + stats.SizeOfDimemsion);
            ////pres obrazek = jde pres int
            //Image<Gray, Int16> img = labels.ToImage<Gray, Int16>();
            //Console.WriteLine(img.Data[181, 581, 0]);//row, collum
            //Console.WriteLine(img.Data[206, 917, 0]);//row, collum
            ////test zda jdou stats
            //Console.WriteLine("Size of stats = " + stats.Size + " element size: " + stats.ElementSize + " Number of dimension: " + stats.SizeOfDimemsion);
            //Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();
            ////test zda jdou centroids
            //Image<Gray, Int16> centroidsImg = centroids.ToImage<Gray, Int16>();


            //double max = 0;
            //double helpa;
            ////max value in
            //for (int i = 0; i < centroidsImg.Rows; i++) {
            //    for (int j = 0; j < centroidsImg.Cols; j++) {
            //        helpa = centroidsImg.Data[i, j, 0];
            //        if (helpa > max) {
            //            max = helpa;
            //        }
            //    }        
            //}
            //Console.WriteLine("Max label = " + max);
        }

        private void ibCamera_MouseDown(object sender, MouseEventArgs e)
        {
            click = e.Location;
            Console.WriteLine(click);
        }
    }
}
