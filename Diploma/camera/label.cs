using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using Emgu.CV.OCR;
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
        public Image<Gray, byte> actualBBFill;
        //first selected letter
        public int blueRef;
        public int greenRef;
        public int redRef;
        public int widthRef;
        public int heightRef;
        ArrayList candidates;
        //helpy
        Image<Gray, byte> actualCroppedImage;
        Image<Rgb, Int16> actualCroppedImageColor;
        private int counterFirstCharSave;
        Mat labels;
        Mat stats;
        Mat centroids;
        Image<Gray, Int16> labelsImg;
        Image<Gray, Int16> statsImg;
        Image<Gray, Int16> centroidsImg;
        int actualLabel;
        //tesseract
        private Tesseract ocr;
        private Rectangle oneChar;

        bool initialStep = true;

        public label(Rectangle roi)
        {
            this.roi = roi;
            labels = new Mat();
            stats = new Mat();
            centroids = new Mat();
            BB = new Rectangle();
            oneChar = new Rectangle();

            //tessarect
            ocr = new Tesseract("", "eng", OcrEngineMode.TesseractOnly);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789/()");
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //inicialize label before data aquizition
        internal void addClickedPoint(Point regularRoiStart, Point localPoint, Mat actualImage)
        {
            centroidBB = new Point();
            clickedPoint = regularRoiStart;
            this.localPoint = localPoint;

            binarizeCrop(actualImage);
            markAreas();
            findSymbolBySnail();
            getRefColor();
            findBounding(actualImage);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //actualize label after change in BB
        internal void actualizeLabel(Mat actualImage)
        {
            //new roi from old bounding
            newRoi(actualImage);
            //actualize roi + gray
            binarizeCrop(actualImage);
            markAreas();
            findSymbolBySnail();
            findBounding(actualImage);
            saveBBFill(actualImage);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //actualize ROI from BB
        private void newRoi(Mat actualImage)
        {
            //checks if roi is in the image
            int leftRoi = 1;
            if (leftCollumBB - heightBB > 0)
            {
                leftRoi = leftCollumBB - heightBB;
            }

            int topRoi = 1;
            if (topRowBB - heightBB > 0)
            {
                topRoi = topRowBB - heightBB;
            }

            int widthRoi = actualImage.Width - 1;
            if (widthBB + 2 * heightBB < actualImage.Width)
            {
                widthRoi = widthBB + 2 * heightBB;
            }

            int heightRoi = actualImage.Height - 1;
            if (3 * heightBB < actualImage.Height)
            {
                heightRoi = 3 * heightBB;
            }
            //define new roi
            roi.Location = new Point(leftRoi, topRoi);
            roi.Size = new Size(widthRoi, heightRoi);

            //new clicked point//start of snail
            localPoint.X = roi.Size.Width / 2;
            localPoint.Y = roi.Size.Height / 2;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //binarize scene inside the ROI and makes a ROI from color image
        private void binarizeCrop(Mat actualImage)
        {
            //clop color one
            actualCroppedImageColor = actualImage.ToImage<Rgb, Int16>();
            actualCroppedImageColor.ROI = roi;
            //gray image crop
            actualCroppedImage = actualImage.ToImage<Gray, byte>();
            actualCroppedImage.ROI = roi;
            //threshold
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //mark areas in binarize image (with stats LEFT_x = 0, top_Y = 1, Width = 2, height = 3, area = 4)
        private void markAreas()
        {
            //label image
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(actualCroppedImage, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            labelsImg = labels.ToImage<Gray, Int16>();
            statsImg = stats.ToImage<Gray, Int16>();
            centroidsImg = centroids.ToImage<Gray, Int16>();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //obtain referene color from first found symbol
        private void getRefColor()
        {
            int blue = 0;
            int green = 0;
            int red = 0;
            int counter = 0;

            //go throught whole symbol and obtain a avg value of color
            for (int i = 0; i < actualCroppedImageColor.Height; i++)
            {
                for (int j = 0; j < actualCroppedImageColor.Width; j++)
                {
                    if (labelsImg.Data[i, j, 0] == actualLabel)
                    {
                        counter++;
                        red += actualCroppedImageColor.Data[i, j, 0];//red
                        green += actualCroppedImageColor.Data[i, j, 1];//green
                        blue += actualCroppedImageColor.Data[i, j, 2];//blue
                    }
                }
            }

            redRef = red / counter;
            greenRef = green / counter;
            blueRef = blue / counter;
            //Console.WriteLine(" red = " + redRef + " green = " + greenRef + "blue = " + blueRef + " COUNTER = " + counter);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //check if found marked area contains symbol
        private bool checkIfChar(int startLabel)
        {
            oneChar.Location = new Point(statsImg.Data[startLabel, 0, 0], statsImg.Data[startLabel, 1, 0]);
            oneChar.Size = new Size(statsImg.Data[startLabel, 2, 0], statsImg.Data[startLabel, 3, 0]);
            Image<Gray, byte> cropNew = actualCroppedImage;
            cropNew.ROI = oneChar;
            //cropNew = cropNew.Resize(5, Inter.Cubic);

            Mat invert = cropNew.Mat;
            CvInvoke.CopyMakeBorder(invert, invert, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
            ocr.Recognize(invert);
            counterFirstCharSave++;

            cropNew.Save("FirstChar" + counterFirstCharSave + ".jpeg");
            Console.WriteLine("Number of obtained symbols: " + ocr.GetText().Length);
            if (ocr.GetText().Length < 1)
            {
                return true; //only for testing, bad classifier
                //return false;
            }
            else {
                Console.WriteLine("First char = " + ocr.GetText());
                return true;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //found nearest area to clicked or calculated point (snail process)
        private void findSymbolBySnail()
        {
            actualLabel = 0;
            int closestLabel = 0;
            int pointRow;
            int pointCollum;
            int width = 1;
            int height = 1;
            //Console.WriteLine(" localPoint.Y " + localPoint.Y + " localPoint.X " + localPoint.X + " height " + labelsImg.Height + " width " + labelsImg.Width);

            //if clicked or calculated is directly on symbol and its bigger than 20px
            if (labelsImg.Data[localPoint.Y, localPoint.X, 0] != 0 && statsImg.Data[labelsImg.Data[localPoint.Y, localPoint.X, 0], 4, 0] > 20)
            {
                //Console.WriteLine("Click on label: " + labelsImg.Data[localPoint.Y, localPoint.X, 0]);
                actualLabel = labelsImg.Data[localPoint.Y, localPoint.X, 0];
            }
            //if point isnt in any marked area
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
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0 && statsImg.Data[labelsImg.Data[pointRow, pointCollum, 0], 4, 0] > 20)
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
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0 && statsImg.Data[labelsImg.Data[pointRow, pointCollum, 0], 4, 0] > 20)
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
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0 && statsImg.Data[labelsImg.Data[pointRow, pointCollum, 0], 4, 0] > 20)
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
                        if (labelsImg.Data[pointRow, pointCollum, 0] != 0 && statsImg.Data[labelsImg.Data[pointRow, pointCollum, 0], 4, 0] > 20)
                        {
                            closestLabel = labelsImg.Data[pointRow, pointCollum, 0];
                            break;
                        }
                        pointCollum--;
                    }
                    width++;
                }

                //Console.WriteLine("Closest label: " + closestLabel);
            }
            ///find possible candidates
            if (actualLabel == 0 && closestLabel != 0)
            {
                actualLabel = closestLabel;
            }

            //if no area bigger than 20px wasnt found
            if (actualLabel == 0 && closestLabel == 0)
            {
                Console.WriteLine("NO AREA FOUND IN THIS ROI");
                actualLabel = -1;
            }


            //ref width and height
            if (initialStep == false)
            {
                //update ref value only if change is really small because of perspective disortion
                if (Math.Abs((int)statsImg.Data[actualLabel, 2, 0] - widthRef) < 3)
                {
                    widthRef = (int)statsImg.Data[actualLabel, 2, 0];
                }
                if (Math.Abs((int)statsImg.Data[actualLabel, 3, 0] - heightRef) < 3)
                {
                    heightRef = (int)statsImg.Data[actualLabel, 3, 0];
                }
            }
            else
            {
                //referece value of symbol in label
                widthRef = (int)statsImg.Data[actualLabel, 2, 0];
                heightRef = (int)statsImg.Data[actualLabel, 3, 0];
                initialStep = false;
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////
        //set start values for method findBounding
        private void setStartValuesFindBounding(out int startRow, out int startCollum, out int startLabel, Mat actualImage)
        {
            //check if symbol finded by snail is symbol
            if (checkIfChar(actualLabel) == false)
            {
                //TODO set new coordinates/ probbably middle of the roi	
                //set row and collum
                startRow = (int)(actualCroppedImage.Height - 1) / 2 + roi.Y;
                startCollum = (int)(actualCroppedImage.Height - 1) / 2 + roi.X;
                //set start bounding
                topRowBB = startRow;
                leftCollumBB = startCollum;
                widthBB = 0;
                heightBB = 0;
                if (topRowBB + heightBB < actualImage.Height)
                {
                    lowRowBB = topRowBB + heightBB;
                }
                else
                {
                    lowRowBB = actualImage.Height - 1;
                }
                Console.WriteLine("No symbol find after snail - set defaul values");

                //FOR TESTING
                startLabel = actualLabel;
            }
            //first candidate is symbol
            else
            {
                candidates.Add(actualLabel);
                startLabel = actualLabel;
                //set start finding row, check is maybe useless
                if (statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2 < actualImage.Height)
                {
                    startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
                }
                else
                {
                    startRow = actualImage.Height - 1;
                }
                //start collum
                startCollum = statsImg.Data[startLabel, 0, 0];
                //set bounding
                topRowBB = statsImg.Data[startLabel, 1, 0];
                leftCollumBB = startCollum;
                widthBB = (int)statsImg.Data[startLabel, 2, 0];
                heightBB = (int)statsImg.Data[startLabel, 3, 0];
                if (topRowBB + heightBB < actualImage.Height)
                {
                    lowRowBB = topRowBB + heightBB;
                }
                else
                {
                    lowRowBB = actualImage.Height - 1;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //chceck candidate on left if its a symbol
        private void checkLeftFindBounding()
        {
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //go to the left and right(max distance is equal to 2*letter width) and check if: its letter
        private void findBounding(Mat actualImage)
        {
            int startRow;
            int startCollum;
            int startLabel;
            int step = 0;
            //candidates to symbols/symbols
            candidates = new ArrayList();

            setStartValuesFindBounding(out startRow, out startCollum, out startLabel, actualImage);

            for (int i = startCollum - 1; i > startCollum - 2*statsImg.Data[actualLabel, 2, 0] || i > 0; i--)
            {
                if (i <= 0) {
                    break;
                }
                checkLeftFindBounding();
                //Console.WriteLine("heighRef= " + heightRef + " (int)(heightRef * 1.5)= " + (int)(heightRef * 1.5));
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] > (int)(heightRef * 0.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] < (int)(heightRef * 1.5) && statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0] < (int)(heightRef * 1.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
                {//not background and bigger than 20px
                    //if ((actualCroppedImageColor.Data[startRow, i, 0] > redRef - 50 && actualCroppedImageColor.Data[startRow, i, 0] < redRef + 50) && (actualCroppedImageColor.Data[startRow, i, 1] > greenRef - 50 && actualCroppedImageColor.Data[startRow, i, 1] < greenRef + 50) && (actualCroppedImageColor.Data[startRow, i, 2] > blueRef - 50 && actualCroppedImageColor.Data[blueRef, i, 2] < blueRef + 50))
                    //{
                        //TODO next conditions
                        candidates.Add(labelsImg.Data[startRow, i, 0]);
                        //calculate distance between chars
                        step = (statsImg.Data[actualLabel, 0, 0] - (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0]));
                        //choose new initial letter and cotinue to left
                        actualLabel = labelsImg.Data[startRow, i, 0];
                        if (statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2 < actualImage.Height)
                        {
                            startRow = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] / 2;
                        }
                        else
                        {
                            startRow = actualImage.Height - 1;
                        }
                        startCollum = statsImg.Data[actualLabel, 0, 0];
                        i = startCollum - 1;
                        //set new BB
                        if (statsImg.Data[actualLabel, 1, 0] < topRowBB)
                        {
                            topRowBB = statsImg.Data[actualLabel, 1, 0];
                        }
                        if (statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] > lowRowBB && statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0] < actualImage.Height)
                        {
                            lowRowBB = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0];
                        }
                        leftCollumBB = startCollum;
                        widthBB = widthBB + (int)statsImg.Data[actualLabel, 2, 0] + step;
                        heightBB = lowRowBB - topRowBB;
                        centroidBB.Y = (topRowBB + heightBB) / 2;
                        centroidBB.X = (leftCollumBB + widthBB) / 2;
                    }
                    step++;
                //}
            }
            //go to the right(max distance is equal to 2*letter width) and check if: size, color 
            step = 0;
            startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
            startCollum = statsImg.Data[startLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0];
            actualLabel = startLabel;
            for (int i = startCollum + 1; i < startCollum + 2*statsImg.Data[actualLabel, 2, 0]; i++)
            {
                if (i >= actualCroppedImage.Width) {
                    break;
                }

                //Console.WriteLine("row= " + startRow + " collum= " + i);
                if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] > (int)(heightRef * 0.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] < (int)(heightRef * 1.5) && statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0] < (int)(heightRef * 1.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
                {//not background and bigger than 20px
                    //if ((actualCroppedImageColor.Data[startRow, i, 0] > redRef - 50 && actualCroppedImageColor.Data[startRow, i, 0] < redRef + 50) && (actualCroppedImageColor.Data[startRow, i, 1] > greenRef - 50 && actualCroppedImageColor.Data[startRow, i, 1] < greenRef + 50) && (actualCroppedImageColor.Data[startRow, i, 2] > blueRef - 50 && actualCroppedImageColor.Data[blueRef, i, 2] < blueRef + 50))
                    //{
                        //TODO next conditions
                        candidates.Add(labelsImg.Data[startRow, i, 0]);
                        //choose new initial letter and cotinue to left
                        step = (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] - (statsImg.Data[actualLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0]));
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
                        centroidBB.X = (leftCollumBB + widthBB) / 2;
                    }
                    step++;
                //}
            }

            //ATENTIONE + point to bounding
            topRowBB = topRowBB + roi.Location.Y;
            leftCollumBB = leftCollumBB + roi.Location.X;

            //Point location = new Point();
            //location.Y = topRowBB;
            //location.X = leftCollumBB;
            BB.Location = new Point(leftCollumBB, topRowBB);
            BB.Size = new Size(widthBB, heightBB);
            //Console.WriteLine("candidates " + candidates.Count);
        }


        internal void saveBBFill(Mat actualImage)
        {
            actualBBFill = actualImage.ToImage<Gray, byte>();
            actualBBFill.ROI = BB;
            //actualBBFill = actualBBFill.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
        }
    }
}
