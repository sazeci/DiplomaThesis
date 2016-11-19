using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using System.Drawing;

namespace Diploma.camera
{
    class label
    {
        public Point clickedPoint;//clicked point in whole scene
        public Point localPoint;
        public string name;
        //ROI in regular image
        public Rectangle roi;
        //selected Bounding box letter by letter in regular image
        public Point centroid;
        public int topRowBB;
        public int leftCollumBB;
        public int widthBB;
        public int heightBB;
        //first selected letter
        public int blueRef;
        public int greenRef;
        public int redRef;
        public int widthRef;
        public int heightRef;
        //helpy
        Image<Gray, byte> actualCroppedImage;
        Image<Rgb, Int16> actualCroppedImageColor;
        private int thresholdValue;
        Mat labels;
        Mat stats;
        Mat centroids;
        Image<Gray, Int16> labelsImg;
        Image<Gray, Int16> statsImg;
        Image<Gray, Int16> centroidsImg;
        int actualLabel;

        public label(Rectangle roi)
        {
            this.roi = roi;
            labels = new Mat();
            stats = new Mat();
            centroids = new Mat();
        }

        internal void addClickedPoint(Point regularRoiStart, Point localPoint, Mat actualImage)
        {
            clickedPoint = regularRoiStart;
            this.localPoint = localPoint;

            binarizeCrop(actualImage);
            markAreas();
            findSymbolBySnail();
            getRefColor();
        }

        private void getRefColor()
        {
            int blue = 0;
            int green = 0;
            int red = 0;
            int counter = 0;

            for (int i = 0; i < actualCroppedImageColor.Height; i++) {
                for (int j = 0; j < actualCroppedImageColor.Width; j++) {
                    if (labelsImg.Data[i, j, 0] == actualLabel) {
                        counter++;
                        red += actualCroppedImageColor.Data[i, j, 0];//red
                        green += actualCroppedImageColor.Data[i, j, 1];//green
                        blue += actualCroppedImageColor.Data[i, j, 2];//blue
                    }
                }
            }

            blueRef = blue / counter;
            greenRef = green / counter;
            redRef = red / counter;
            Console.WriteLine(" red = " + redRef + " green = " + greenRef + "blue = " + blueRef + " COUNTER = " + counter);
        }

        private void markAreas()
        {
            //label image
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(actualCroppedImage, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            labelsImg = labels.ToImage<Gray, Int16>();
            statsImg = stats.ToImage<Gray, Int16>();
            centroidsImg = centroids.ToImage<Gray, Int16>();
        }

        private void findSymbolBySnail()
        {
            //Point regularRoiStart = new Point();
            //regularRoiStart.X = clickedPoint.X - roi.Location.X;
            //regularRoiStart.Y = clickedPoint.Y - roi.Location.Y;

            //closest centroid to click
            actualLabel = 0;
            int closestLabel = 0;
            int pointRow;
            int pointCollum;
            int width = 1;
            int height = 1;
            if (labelsImg.Data[localPoint.Y, localPoint.X, 0] != 0)
            {
                Console.WriteLine("Click on label: " + labelsImg.Data[localPoint.Y, localPoint.X, 0]);
                actualLabel = labelsImg.Data[localPoint.Y, localPoint.X, 0];
            }
            else
            {//dont use centroids, snail is better because of iner labels
                pointRow = localPoint.Y;
                pointCollum = localPoint.X;
                int help;

                while (closestLabel == 0)
                {
                    //top
                    help = pointRow + height;
                    for (int i = pointRow; i < help; i++)
                    {
                        //Console.WriteLine(pointRow + " x " + pointCollum);
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0)
                        {
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
            if (actualLabel == 0 && closestLabel != 0)
            {
                actualLabel = closestLabel;
            }


            //ref width and height
            widthRef = (int)statsImg.Data[actualLabel, 3, 0];
            heightRef = (int)statsImg.Data[actualLabel, 4, 0];

        }

        private void binarizeCrop(Mat actualImage)
        {
            //clop color one
            actualCroppedImageColor = actualImage.ToImage<Rgb, Int16>();
            actualCroppedImageColor.ROI = roi;
            //binarize image
            actualCroppedImage = actualImage.ToImage<Gray, byte>();
            actualCroppedImage.ROI = roi;
            //equalize hist
            actualCroppedImage._EqualizeHist();
            //threshold
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
        }
    }
}
