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
using System.Collections;

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
        public Point centroidBB;
        public int topRowBB;
        private int lowRowBB;
        public int leftCollumBB;
        public int widthBB;
        public int heightBB;
        public Rectangle BB;
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
            BB = new Rectangle();
        }

        internal void addClickedPoint(Point regularRoiStart, Point localPoint, Mat actualImage)
        {
            centroidBB = new Point();
            clickedPoint = regularRoiStart;
            this.localPoint = localPoint;

            binarizeCrop(actualImage);
            markAreas();
            findSymbolBySnail();
            getRefColor();
            findBounding();
        }

        private void findBounding()
        {
            //go to the left(max distance is equal to letter width) and check if: size, color 
            ArrayList candidates = new ArrayList();
            candidates.Add(actualLabel);
            int startRow;
            int startCollum;
            int startLabel = actualLabel;
            int step = 0;

            startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
            startCollum = statsImg.Data[startLabel, 0, 0];
            //initial bounding
            topRowBB = statsImg.Data[startLabel, 1, 0];
            leftCollumBB = startCollum;
            widthBB = (int)statsImg.Data[startLabel, 2, 0];
            heightBB = (int)statsImg.Data[startLabel, 3, 0];
            lowRowBB = topRowBB + heightBB;
            centroidBB.Y = (topRowBB + heightBB) / 2;
            centroidBB.Y = (leftCollumBB + widthBB) / 2;


            for (int i = startCollum - 1; i > startCollum - statsImg.Data[actualLabel, 2, 0]; i--)
            {
                if (i <= 0) {
                    break;
                }

                //Console.WriteLine("row= " + startRow + " collum= " + i);
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
                {//not background and bigger than 20px
                    //TODO next conditions
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //choose new initial letter and cotinue to left
                    step = (statsImg.Data[actualLabel, 0, 0] - (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0]));
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    startRow = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2;
                    startCollum = statsImg.Data[actualLabel, 0, 0];
                    i = startCollum - 1;
                    //set new BB
                    if (statsImg.Data[actualLabel, 1, 0] < topRowBB)
                    {
                        topRowBB = statsImg.Data[actualLabel, 1, 0];
                    }
                    if (statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] > lowRowBB)
                    {
                        lowRowBB = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0];
                    }
                    leftCollumBB = startCollum;
                    widthBB = widthBB + (int)statsImg.Data[startLabel, 2, 0] + step;
                    heightBB = lowRowBB - topRowBB;
                    centroidBB.Y = (topRowBB + heightBB) / 2;
                    centroidBB.Y = (leftCollumBB + widthBB) / 2;
                }
                step++;
            }
            //go to the right(max distance is equal to letter width) and check if: size, color 
            step = 0;
            startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
            startCollum = statsImg.Data[startLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0];
            actualLabel = startLabel;
            for (int i = startCollum + 1; i < startCollum + statsImg.Data[actualLabel, 2, 0]; i++)
            {
                if (i >= actualCroppedImage.Width) {
                    break;
                }

                Console.WriteLine("row= " + startRow + " collum= " + i);
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
                {//not background and bigger than 20px
                    //TODO next conditions
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //choose new initial letter and cotinue to left
                    step = (statsImg.Data[actualLabel, 0, 0] - (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0]));
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    startRow = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2;
                    startCollum = statsImg.Data[actualLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0];
                    i = startCollum + 1;
                    //set new BB
                    if (statsImg.Data[actualLabel, 1, 0] < topRowBB)
                    {
                        topRowBB = statsImg.Data[actualLabel, 1, 0];
                    }
                    if (statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] > lowRowBB)
                    {
                        lowRowBB = topRowBB + (int)statsImg.Data[actualLabel, 3, 0];
                    }
                    widthBB = widthBB + (int)statsImg.Data[actualLabel, 2, 0] + step;
                    heightBB = lowRowBB - topRowBB;
                    centroidBB.Y = (topRowBB + heightBB) / 2;
                    centroidBB.Y = (leftCollumBB + widthBB) / 2;
                }
                step++;
            }

            topRowBB = topRowBB + roi.Location.Y;
            leftCollumBB = leftCollumBB + roi.Location.X;

            //Point location = new Point();
            //location.Y = topRowBB;
            //location.X = leftCollumBB;
            BB.Location = new Point(leftCollumBB, topRowBB);
            BB.Size = new Size(widthBB, heightBB);

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
            //actualCroppedImage._EqualizeHist();
            //threshold
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
        }
    }
}
