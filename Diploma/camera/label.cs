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
        private int oldNumberOfCandidates = -1;
        bool isFirstLetter = true;
        //helpy
        Image<Gray, byte> actualCroppedImage;
        Image<Rgb, byte> actualCroppedImageColor;
        Mat labels;
        Mat stats;
        Mat centroids;
        Image<Gray, Int16> labelsImg;
        Image<Gray, Int16> statsImg;
        Image<Gray, Int16> centroidsImg;
        int numberOfLabels;
        int actualLabel;
        //tesseract
        private Tesseract ocr;
        private Rectangle oneChar;

        bool initialStep = true;
        backUpProcess backUpProcess;
        bool noTextInRoi = false;
        public bool noText = false;

        int counterSave = 0;

        public label(Rectangle roi)
        {
            this.roi = roi;
            labels = new Mat();
            stats = new Mat();
            centroids = new Mat();
            BB = new Rectangle();
            oneChar = new Rectangle();
            backUpProcess = new backUpProcess();

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
            findBounding(actualImage);
            saveBBFill(actualImage);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //actualize label after change in BB
        internal void actualizeLabel(Mat actualImage)
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("widthRef " + widthRef + " heightRef " + heightRef + " redRef " + redRef + " greenRef " + greenRef + " blueRef " + blueRef);
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
            if (leftCollumBB - 2 * heightBB > 0)
            {
                leftRoi = leftCollumBB - 2 * heightBB;
            }

            int topRoi = 1;
            if (topRowBB - heightBB > 0)
            {
                topRoi = topRowBB - heightBB;
            }

            int widthRoi = actualImage.Width - 1;
            if (widthBB + 4 * heightBB + leftRoi < actualImage.Width)
            {
                widthRoi = widthBB + 4 * heightBB;
            }

            int heightRoi = actualImage.Height - 1;
            if (3 * heightBB + topRoi < actualImage.Height)
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
            actualCroppedImageColor = actualImage.ToImage<Rgb, byte>();
            actualCroppedImageColor.ROI = roi;
            //gray image crop
            actualCroppedImage = actualImage.ToImage<Gray, byte>();
            actualCroppedImage.ROI = roi;
            //threshold
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
            //actualCroppedImage.Save("actualCroppedImageOriginal.jpeg");
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //mark areas in binarize image (with stats LEFT_x = 0, top_Y = 1, Width = 2, height = 3, area = 4)
        private void markAreas()
        {
            //label image
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(actualCroppedImage, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            labelsImg = labels.ToImage<Gray, Int16>();
            statsImg = stats.ToImage<Gray, Int16>();
            centroidsImg = centroids.ToImage<Gray, Int16>();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //obtain referene color from first found symbol
        private void setRefColor(int label)
        {
            int blue = 0;
            int green = 0;
            int red = 0;
            int counter = 0;

            Image<Gray, Byte>[] channels = actualCroppedImageColor.Split();
            //go throught whole symbol and obtain a avg value of color
            for (int i = 0; i < actualCroppedImageColor.Height; i++)
            {
                for (int j = 0; j < actualCroppedImageColor.Width; j++)
                {
                    if (labelsImg.Data[i, j, 0] == label)
                    {
                        counter++;
                        red += channels[0].Data[i, j, 0];
                        green += channels[1].Data[i, j, 0];
                        blue += channels[2].Data[i, j, 0];
                    }
                }
            }

            redRef = red / counter;
            greenRef = green / counter;
            blueRef = blue / counter;
            //Console.WriteLine(" red = " + redRef + " green = " + greenRef + " blue = " + blueRef + " COUNTER = " + counter);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //check if found marked area contains symbol
        private bool checkIfChar(int startLabel)
        {
            if (isFirstLetter == true) {
                isFirstLetter = false;
                return true;
            }

            if (startLabel < 0) {
                return false;
            }

            //aspect ratioCheck
            if ((((double)statsImg.Data[startLabel, 3, 0] / (double)statsImg.Data[startLabel, 2, 0]) < 1.1) || (((double)statsImg.Data[startLabel, 3, 0] / (double)statsImg.Data[startLabel, 2, 0]) > 4)) {
                //Console.WriteLine("Bad AspectRatio " + ((double)statsImg.Data[startLabel, 3, 0] / (double)statsImg.Data[startLabel, 2, 0]));

                return false;
            }

            //white ratio
            Rectangle roicek = new Rectangle();
            roicek.Location = new Point(statsImg.Data[startLabel, 0, 0], statsImg.Data[startLabel, 1, 0]);//left top
            roicek.Size = new Size(statsImg.Data[startLabel, 2, 0], statsImg.Data[startLabel, 3, 0]);//width height
            Image<Gray, byte> save = this.actualCroppedImage.Clone();
            //save.Save("whiteRatio" + (counterSave) + ".jpeg");
            //Console.WriteLine("roicek 1 : " + roicek.Size + " location: " + roicek.Location);
            save.ROI = roicek;
            //save.Save("whiteRatio AFTER ROI" + (counterSave) + ".jpeg");
            if (((((double)statsImg.Data[startLabel, 3, 0] * (double)statsImg.Data[startLabel, 2, 0]) / (double)save.CountNonzero().Max()) < 1.1) || ((((double)statsImg.Data[startLabel, 3, 0] * (double)statsImg.Data[startLabel, 2, 0]) / (double)save.CountNonzero().Max()) > 2.5))
            {
                //Console.WriteLine("Bad WhiteRatio " + (((double)statsImg.Data[startLabel, 3, 0] * (double)statsImg.Data[startLabel, 2, 0]) / (double)save.CountNonzero().Max()));
                return false;
            }

            //number of lakes
            var valueScalar = new MCvScalar(0);
            int numberOfLakes;
            CvInvoke.CopyMakeBorder(save, save, 100, 100, 100, 100, BorderType.Constant, valueScalar);
            CvInvoke.BitwiseNot(save, save);//invert
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            numberOfLakes = CvInvoke.ConnectedComponentsWithStats(save, labels, stats, centroids, LineType.FourConnected, DepthType.Cv32S);
            if (numberOfLakes > 4) {
                //Console.WriteLine("Bad Lakes " + (numberOfLakes-2));
                return false;
            }

            //color
            Image<Rgb, byte> cropNew = this.actualCroppedImageColor.Clone();
            //cropNew.Save("RoicekColor" + (counterSave) + ".jpeg");
            //Console.WriteLine("roicek 2 : " + roicek.Size + " location: " + roicek.Location);
            roicek.Location = new Point(roi.X+roicek.Location.X, roi.Y+roicek.Location.Y);//left top
            cropNew.ROI = roicek;
            //cropNew.Save("RoicekColor AFTER ROI" + (counterSave) + ".jpeg");
            int blue = 0;
            int green = 0;
            int red = 0;
            int redAvg;
            int greenAvg;
            int blueAvg;
            int counter = 0;
            counterSave++;

            Image<Gray, Byte>[] channels = cropNew.Split();
            for (int i = 0; i < cropNew.Height; i++)
            {
                for (int j = 0; j < cropNew.Width; j++)
                {
                    if (labelsImg.Data[i + roicek.Location.Y-roi.Y, j + roicek.Location.X-roi.X, 0] == startLabel)//if (labelsImg.Data[i + roicek.Location.Y, j + roicek.Location.X, 0] == startLabel)
                    {
                        counter++;
                        red += channels[0].Data[i, j, 0];
                        green += channels[1].Data[i, j, 0];
                        blue += channels[2].Data[i, j, 0];
                    }
                }
                //Console.WriteLine("");
            }

            redAvg = red / counter;
            greenAvg = green / counter;
            blueAvg = blue / counter;

            //Console.WriteLine(" red = " + redAvg + " green = " + greenAvg + " blue = " + blueAvg + " COUNTER = " + counter);
            //Console.WriteLine(" redRef = " + redRef + " greenRef = " + greenRef + " blueRef = " + blueRef);

            if (redAvg <= redRef - 50 || redAvg >= redRef + 50 || greenAvg <= greenRef - 50 || greenAvg >= greenRef + 50 || blueAvg <= blueRef - 50 || blueAvg >= blueRef + 50)
            {
                //Console.WriteLine("Bad Color ");
                return false;
            }

            return true;

            //oneChar.Location = new Point(statsImg.Data[startLabel, 0, 0], statsImg.Data[startLabel, 1, 0]);
            //oneChar.Size = new Size(statsImg.Data[startLabel, 2, 0], statsImg.Data[startLabel, 3, 0]);
            //Image<Gray, byte> cropNew = actualCroppedImage.Clone();
            //cropNew.ROI = oneChar;
            ////cropNew = cropNew.Resize(5, Inter.Cubic);

            //Mat invert = cropNew.Mat;
            //CvInvoke.CopyMakeBorder(invert, invert, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
            //ocr.Recognize(invert);
            //counterFirstCharSave++;

            ////cropNew.Save("FirstChar" + counterFirstCharSave + ".jpeg");
            ////Console.WriteLine("Number of obtained symbols: " + ocr.GetText().Length);
            //if (ocr.GetText().Length < 1 || ocr.GetText().Length > 5)
            //{
            //    //return true; //only for testing, bad classifier
            //    return false;
            //}
            //else
            //{
            //    string dodo = ocr.GetText();
            //    Console.WriteLine("First char = " + dodo);
            //    return true;
            //}
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
                        //out of range
                        if (pointRow < 0) {
                            //Console.WriteLine("NO AREA FOUND IN THIS ROI");
                            actualLabel = -1;
                            return;
                        }
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
                        //out of range
                        if (pointCollum > actualCroppedImageColor.Width - 1) {
                            //Console.WriteLine("NO AREA FOUND IN THIS ROI");
                            actualLabel = -1;
                            return;
                        }
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
                        //out of range
                        if (pointRow > actualCroppedImageColor.Height-1)
                        {
                            //Console.WriteLine("NO AREA FOUND IN THIS ROI");
                            actualLabel = -1;
                            return;
                        }
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
                        //out of range
                        if (pointCollum < 0)
                        {
                            //Console.WriteLine("NO AREA FOUND IN THIS ROI");
                            actualLabel = -1;
                            return;
                        }
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
                //Console.WriteLine("NO AREA FOUND IN THIS ROI");
                actualLabel = -1;
            }


            ////ref width and height
            //if (initialStep == false)
            //{
            //    //update ref value only if change is really small because of perspective disortion
            //    if (Math.Abs((int)statsImg.Data[actualLabel, 2, 0] - widthRef) < 3)
            //    {
            //        widthRef = (int)statsImg.Data[actualLabel, 2, 0];
            //    }
            //    if (Math.Abs((int)statsImg.Data[actualLabel, 3, 0] - heightRef) < 3)
            //    {
            //        heightRef = (int)statsImg.Data[actualLabel, 3, 0];
            //    }
            //}
            //else
            //{
            //    //referece value of symbol in label
            //    widthRef = (int)statsImg.Data[actualLabel, 2, 0];
            //    heightRef = (int)statsImg.Data[actualLabel, 3, 0];
            //    initialStep = false;
            //}

        }

        /////////////////////////////////////////////////////////////////////////////////////
        //set start values for method findBounding
        private void setStartValuesFindBounding(out int startRow, out int startCollum, out int startLabel, Mat actualImage)
        {
            //check if symbol finded by snail is symbol
            if (checkIfChar(actualLabel) == false)
            {
                ////TODO set new coordinates/ probbably middle of the roi	
                ////set row and collum
                //startRow = (int)(actualCroppedImage.Height - 1) / 2 + roi.Y;
                //startCollum = (int)(actualCroppedImage.Height - 1) / 2 + roi.X;
                ////set start bounding
                //topRowBB = startRow;
                //leftCollumBB = startCollum;
                //widthBB = 0;
                //heightBB = 0;
                //lowRowBB = startRow;
                //Console.WriteLine("No symbol find after snail - set defaul values");

                ////FOR TESTING
                //startLabel = actualLabel;

                //TODO use backUpProcess
                int bbCol;
                int bbRow;
                int bbWidth;
                int bbHeight;
                int labelCandidate;
                labelCandidate = backUpProcess.backUpLabel(actualCroppedImageColor, numberOfLabels, labelsImg, statsImg, redRef, greenRef, blueRef, heightRef, widthRef, out bbCol, out bbRow, out bbWidth, out bbHeight);
                //candidate on letter founded
                if (labelCandidate > 0)// && checkIfChar(labelCandidate) == true
                {
                    //Console.WriteLine("BACKUP - i have candidate");
                    if (checkIfChar(labelCandidate) == true)
                    {
                        //set new actual label
                        startLabel = labelCandidate;
                        //set variables to find 
                        startRow = bbRow + (int)(bbHeight / 2);
                        startCollum = bbCol;
                        //set start bounding
                        topRowBB = bbRow;
                        leftCollumBB = startCollum;
                        widthBB = bbWidth;
                        heightBB = bbHeight;
                        if (topRowBB + heightBB < actualImage.Height)
                        {
                            lowRowBB = topRowBB + heightBB;
                        }
                        else
                        {
                            lowRowBB = actualImage.Height - 1;
                        }
                        //setRefColor
                        setRefColor(startLabel);
                        noTextInRoi = false;
                        //setRef size of char
                        setRefSize(startLabel);
                        Console.WriteLine("BACKUP - symbol was found | ROI: " + roi.Width + " | " + roi.Height);
                        noText = false;
                    }
                    else
                    {
                        //area contain 0 symbols = is covered by something/monitor is off/monitor has been moven so quickly
                        ////////////////////////////////////////TODO
                        //set new actual label
                        startLabel = labelCandidate;
                        //set variables to find 
                        startRow = bbRow + (int)(bbHeight / 2);
                        startCollum = bbCol;
                        //set actual state of labe
                        noTextInRoi = true;
                        Console.WriteLine("BACKUP - symbol was NOT found | ROI: " + roi.Width + " | " + roi.Height);
                        //////////////////////////////////////////TODO
                        noText = true;
                    }
                }
                else {
                    //area contain 0 symbols = is covered by something/monitor is off/monitor has been moven so quickly
                    ////////////////////////////////////////TODO
                    //set new actual label
                    startLabel = labelCandidate;
                    //set variables to find 
                    startRow = bbRow + (int)(bbHeight / 2);
                    startCollum = bbCol;
                    //set actual state of labe
                    noTextInRoi = true;
                    Console.WriteLine("BACKUP - symbol was NOT found | ROI: " + roi.Width + " | " + roi.Height);
                    noText = true;
                    //////////////////////////////////////////TODO
                }
            }
            //first candidate is symbol
            else
            {
                //initialStep = false &&
                //Console.WriteLine("widthRef: " + widthRef + " heightRef " + heightRef + " widthLabel " + statsImg.Data[actualLabel, 2, 0] + " heightLabel " + statsImg.Data[actualLabel, 3, 0] + " initialStep " + initialStep);
                if (initialStep == true || ((statsImg.Data[actualLabel, 3, 0] > (int)(heightRef * 0.75) && statsImg.Data[actualLabel, 3, 0] < (int)(heightRef * 1.3) && statsImg.Data[actualLabel, 2, 0] > (int)(widthRef * 0.4) && statsImg.Data[actualLabel, 2, 0] < (int)(widthRef * 1.7))))
                {
                    //check color
                    //TODO

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
                    //get new refColor
                    setRefColor(startLabel);
                    noTextInRoi = false;
                    //setRef size of char
                    setRefSize(startLabel);
                    noText = false;
                    Console.WriteLine("NORMAL - symbol was found | ROI: " + roi.Width + " | " + roi.Height);

                    Rectangle dodo = new Rectangle();
                    dodo.Location = new Point(leftCollumBB, topRowBB);
                    dodo.Size = new Size(widthBB, heightBB);
                    Image<Gray, byte> dodik = actualCroppedImage.Clone();
                    dodik.ROI = dodo;
                    //dodik.Save("NormalSYmbol" + (counterSave) + ".jpeg");
                    counterSave++;
                }
                else {
                    //set new actual label
                    startLabel = 0;
                    //set variables to find 
                    startRow = 0;
                    startCollum = 0;
                    //set actual state of labe
                    noTextInRoi = true;
                    noText = true;
                    Console.WriteLine("NORMAL - symbol was NOT found | ROI: " + roi.Width + " | " + roi.Height);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //set ref size of char
        private void setRefSize(int label)
        {
            //ref width and height
            if (initialStep == false)
            {
                //update ref value only if change is really small because of perspective disortion
                if (Math.Abs((int)statsImg.Data[label, 2, 0] - widthRef) < 3)
                {
                    widthRef = (int)statsImg.Data[label, 2, 0];
                }
                if (Math.Abs((int)statsImg.Data[label, 3, 0] - heightRef) < 3)
                {
                    heightRef = (int)statsImg.Data[label, 3, 0];
                }
            }
            else
            {
                //referece value of symbol in label
                widthRef = (int)statsImg.Data[label, 2, 0];
                heightRef = (int)statsImg.Data[label, 3, 0];
                initialStep = false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //chceck candidate on left if its a symbol
        private void checkLeftFindBounding(ref int startRow, ref int startCollum,ref int i,ref int step, Mat actualImage)
        {
            //basic condition for tested label(not background, between 0.5-1.5 refHeight, width < 1.5*refHeight, bigger than 20 px)
            if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] > (int)(heightRef * 0.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] < (int)(heightRef * 1.5) && statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0] < (int)(heightRef * 1.5) && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
            {
                Console.WriteLine("ZLEVA");
                //color check works badly
                //if ((actualCroppedImageColor.Data[startRow, i, 0] > redRef - 50 && actualCroppedImageColor.Data[startRow, i, 0] < redRef + 50) && (actualCroppedImageColor.Data[startRow, i, 1] > greenRef - 50 && actualCroppedImageColor.Data[startRow, i, 1] < greenRef + 50) && (actualCroppedImageColor.Data[startRow, i, 2] > blueRef - 50 && actualCroppedImageColor.Data[blueRef, i, 2] < blueRef + 50))
                if (checkIfChar(labelsImg.Data[startRow, i, 0]) == true)
                {
                    //add new letter
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //calculate distance between chars
                    step = (statsImg.Data[actualLabel, 0, 0] - (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0]));
                    //choose new initial letter
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    //set row and collum
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
                }
                else {
                    step = step + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0];
                    i = i - statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0];
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //chceck candidate on right if its a symbol
        private void checkRightFindBounding(ref int startRow, ref int startCollum,ref int i,ref int step, Mat actualImage)
        {
            //basic condition for tested label(not background, between 0.5-1.5 refHeight, width < 1.5*refHeight, bigger than 20 px)
            if (labelsImg.Data[startRow, i, 0] > 0 && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] > (int)(heightRef * 0.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 3, 0] < (int)(heightRef * 1.5) && statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0] < (int)(heightRef * 1.75) && statsImg.Data[labelsImg.Data[startRow, i, 0], 4, 0] > 20)
            {
                Console.WriteLine("ZPRAVA");
                //if ((actualCroppedImageColor.Data[startRow, i, 0] > redRef - 50 && actualCroppedImageColor.Data[startRow, i, 0] < redRef + 50) && (actualCroppedImageColor.Data[startRow, i, 1] > greenRef - 50 && actualCroppedImageColor.Data[startRow, i, 1] < greenRef + 50) && (actualCroppedImageColor.Data[startRow, i, 2] > blueRef - 50 && actualCroppedImageColor.Data[blueRef, i, 2] < blueRef + 50))
                if (checkIfChar(labelsImg.Data[startRow, i, 0]) == true)
                {
                    //add new letter
                    candidates.Add(labelsImg.Data[startRow, i, 0]);
                    //calculate distance between chars
                    step = (statsImg.Data[labelsImg.Data[startRow, i, 0], 0, 0] - (statsImg.Data[actualLabel, 0, 0] + statsImg.Data[actualLabel, 2, 0]));
                    //choose new initial letter
                    actualLabel = labelsImg.Data[startRow, i, 0];
                    //set row and collum
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
                        lowRowBB = statsImg.Data[actualLabel, 1, 0] + (int)statsImg.Data[actualLabel, 3, 0];
                    }
                    widthBB = widthBB + (int)statsImg.Data[actualLabel, 2, 0] + step;
                    heightBB = lowRowBB - topRowBB;
                }
                else
                {
                    step = step + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0];
                    i = i + statsImg.Data[labelsImg.Data[startRow, i, 0], 2, 0];
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //go to the left and right(max distance is equal to 2*letter width) and check if: its letter
        private void findBounding(Mat actualImage)
        {
            //recovery values
            int topRowBBOld = topRowBB;
            int lowRowBBOld = lowRowBB;
            int leftCollumBBOld = leftCollumBB;
            int widthBBOld = widthBB;
            int heightBBOld = heightBB;
            //local values
            int startRow;
            int startCollum;
            int startLabel;
            int step = 0;
            //candidates to symbols/symbols
            candidates = new ArrayList();
            //set start values for method findBounding
            setStartValuesFindBounding(out startRow, out startCollum, out startLabel, actualImage);

            if (noTextInRoi == false)
            {
                //find symbols on the left
                for (int i = startCollum - 1; i > startCollum - statsImg.Data[actualLabel, 2, 0] || i > 0; i--)//how far to find a symbol
                {
                    if (i <= 0)
                    {
                        break;
                    }
                    checkLeftFindBounding(ref startRow, ref startCollum, ref i, ref step, actualImage);
                    step++;
                }

                //set new refernce values before go to right
                step = 0;
                startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
                startCollum = statsImg.Data[startLabel, 0, 0] + statsImg.Data[startLabel, 2, 0];
                actualLabel = startLabel;

                //Console.WriteLine("actualCroppedImage.Height " + actualCroppedImage.Height + " actualCroppedImage.Width " + actualCroppedImage.Width);
                //Console.WriteLine("startRow " + startRow + " startCollum " + startCollum + " actualLabel " + actualLabel);
                //actualCroppedImage.Save("actualCroppedImage.jpeg");
                //find symbols on the right
                for (int i = startCollum + 1; i < startCollum + statsImg.Data[actualLabel, 2, 0]; i++)//how far to find a symbol
                {
                    if (i >= labelsImg.Width)//actualCroppedImage.width
                    {
                        break;
                    }
                    checkRightFindBounding(ref startRow, ref startCollum, ref i, ref step, actualImage);
                    step++;
                }

                //ATENTIONE + point to bounding (bounding in whole image)
                topRowBB = topRowBB + roi.Location.Y;
                leftCollumBB = leftCollumBB + roi.Location.X;
            }

            //diference in number of candidates
            if (oldNumberOfCandidates == -1)
            {
                oldNumberOfCandidates = candidates.Count;
            }
            else {
                //too many numbers has been changed = wrong position of label
                //Console.WriteLine("oldNumberOfCandidates " + oldNumberOfCandidates + " Actual number of candidates: " + candidates.Count);
                if (oldNumberOfCandidates > candidates.Count + 2 || oldNumberOfCandidates < candidates.Count - 2)
                {
                    topRowBB = topRowBBOld;
                    lowRowBB = lowRowBBOld;
                    leftCollumBB = leftCollumBBOld;
                    widthBB = widthBBOld;
                    heightBB = heightBBOld;
                    //Console.WriteLine("BAD CANDIDATES");
                }
                else
                {
                    oldNumberOfCandidates = candidates.Count;
                }
            }


            //areas is probably hidden now or not showing numbers
            if (candidates.Count < 1) {
                topRowBB = topRowBBOld;
                lowRowBB = lowRowBBOld;
                leftCollumBB = leftCollumBBOld;
                widthBB = widthBBOld;
                heightBB = heightBBOld;
                //Console.WriteLine("NO SYMBOLS");
            }
            BB.Location = new Point(leftCollumBB, topRowBB);
            BB.Size = new Size(widthBB, heightBB);
            //Console.WriteLine("candidates " + candidates.Count);

            centroidBB.Y = (topRowBB + heightBB) / 2;
            centroidBB.X = (leftCollumBB + widthBB) / 2;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //actual BB for OCR
        internal void saveBBFill(Mat actualImage)
        {
            actualBBFill = actualImage.ToImage<Gray, byte>();
            actualBBFill.ROI = BB;
            //actualBBFill = actualBBFill.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
        }
    }
}
