﻿using System;
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
using Emgu.CV.OCR;
using Emgu.CV.Util;
using System.Linq;

namespace Diploma
{
    public partial class testImageBox : Form
    {
        public int _CameraIndex;//ktera kamera se pouzije
        Capture capWebcam;
        public Mat actualImage;
        Image<Gray, Byte> colorResult;
        Point click;
        private Tesseract ocr;
        List<Image<Gray, byte>> listOfImages;
        private int listCounter;

        public testImageBox()
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            _CameraIndex = 0;

            //tessarect
            ocr = new Tesseract("", "eng", OcrEngineMode.TesseractOnly);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789/()");
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
            //imageManipulation.findMarkers findMarkers = new imageManipulation.findMarkers();

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
            //ibCamera.Image = findMarkers.findColor(actualImage);

            //sort array
            int[] array1 = new int[3] { 1, 2, 3 };
            int[] array2 = new int[3] { 9, 8, 7 };
            Array.Sort(array2, array1);
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                Console.WriteLine(array1[i]);
            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                Console.WriteLine(array2[i]);
            }

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

        private void btnOCR_Click(object sender, EventArgs e)
        {
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            Mat imgOriginal;
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);
                        //Mat invert = new Mat();
                        //CvInvoke.BitwiseNot(imgOriginal, invert);
            //threshold binary
            Image<Gray, byte> BW = imgOriginal.ToImage<Gray, byte>();
            BW = BW.ThresholdBinary(new Gray(100), new Gray(255));
            //give black border
            var valueScalar = new MCvScalar(0);
            CvInvoke.CopyMakeBorder(BW, BW, 100, 100, 100, 100, BorderType.Constant, valueScalar);
            //Image<Gray, byte> cropped = imgOriginal.ToImage<Gray, byte>();
            //make it bigger
            BW.Resize(5, Inter.Cubic);
            ibCamera.Image = BW;
            ocr.Recognize(BW);
            Console.WriteLine("Text = " + ocr.GetText());
        }

        private void btnTemplates_Click(object sender, EventArgs e)
        {
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            Mat imgOriginal;
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);

            Image<Gray, byte> actualCroppedImage = imgOriginal.ToImage<Gray, byte>();
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
            ibCamera.Image = actualCroppedImage;

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(actualCroppedImage, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            Image<Gray, Int16> labelsImg = labels.ToImage<Gray, Int16>();
            Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();
            Image<Gray, Int16> centroidsImg = centroids.ToImage<Gray, Int16>();

            Rectangle roicek = new Rectangle();
            Image<Gray, byte> cropped = actualCroppedImage;
            for (int i = 0; i < numberOfLabels; i++) {
                if (statsImg.Data[i, 4, 0] > 50)
                {
                    cropped = actualCroppedImage;
                    //define new roi
                    roicek.Location = new Point(statsImg.Data[i, 0, 0], statsImg.Data[i, 1, 0]);//left top
                    roicek.Size = new Size(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0]);//width height
                    cropped.ROI = roicek;
                    cropped.Save("Template" + i + ".jpeg");
                }
            }

            //resize work
            cropped = cropped.Resize(50, 50, Emgu.CV.CvEnum.Inter.Cubic);
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            ////load all templates to array
            //number of files in directory
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("TemplatesTestMonitor");
            int count = dir.GetFiles().Length;
            Console.WriteLine(count);
            //load all templates
            Mat imgOriginal;
            listOfImages = new List<Image<Gray, byte>>();
            for (int i = 0; i < count; i++) {
                imgOriginal = new Mat("./TemplatesTestMonitor/" + (i+1) + ".jpeg", LoadImageType.Color);
                listOfImages.Add(imgOriginal.ToImage<Gray, byte>());
            }
            //test if its loaded
            //listCounter = 0;
            //timer1.Enabled = true;

            //dialog to open new file with candidates
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);

            Image<Gray, byte> actualCroppedImage = imgOriginal.ToImage<Gray, byte>();
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
            ibCamera.Image = actualCroppedImage;

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(actualCroppedImage, labels, stats, centroids, LineType.EightConnected, DepthType.Cv32S);
            Image<Gray, Int16> labelsImg = labels.ToImage<Gray, Int16>();
            Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();
            Image<Gray, Int16> centroidsImg = centroids.ToImage<Gray, Int16>();

            Rectangle roicek = new Rectangle();
            Image<Gray, byte> cropped = actualCroppedImage;
            Image<Gray, byte> template;
            Console.WriteLine("Number of candidates = " + numberOfLabels);
            for (int i = 0; i < numberOfLabels; i++)
            {
                if (statsImg.Data[i, 4, 0] > 20)
                {
                    //crop candidate
                    cropped = actualCroppedImage;
                    //define new roi
                    roicek.Location = new Point(statsImg.Data[i, 0, 0], statsImg.Data[i, 1, 0]);//left top
                    roicek.Size = new Size(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0]);//width height
                    cropped.ROI = roicek;


                    //compare to candidates
                    for (int j = 0; j < listOfImages.Count; j++)
                    {
                        template = listOfImages[j].Resize(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0], Emgu.CV.CvEnum.Inter.Cubic);
                        var a = cropped.AbsDiff(template);
                        //morphological open
                        Mat kernel1 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(2, 2), new Point(1, 1));
                        CvInvoke.MorphologyEx(a, a, MorphOp.Open, kernel1, new Point(0, 0), 2, BorderType.Default, new MCvScalar());
                        int[] nonZeroPixels = a.CountNonzero();
                        double percent = (nonZeroPixels.Max() * 100) / (template.Width * template.Height);
                        //Console.WriteLine("Percent = " + percent);
                        //if (percent < 20)
                        //{
                            a.Save("DiffFor" + j + " = " + percent + "%.jpeg");
                        //}
                    }
                }
            }
            Console.WriteLine("END");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listCounter < listOfImages.Count)
            {
                ibCamera.Image = listOfImages[listCounter];
                listCounter++;
            }
        }

        private void btnFindText_Click(object sender, EventArgs e)
        {
            Mat imgOriginal;
            Mat gray = new Mat();
            Mat canny = new Mat();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            //dialog to open new photo
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);

            //actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));

            //CvInvoke.CvtColor(imgOriginal, gray, ColorConversion.Bgr2Gray);
            Image<Gray, byte> bwImgOriginal = imgOriginal.ToImage<Gray, byte>();
            bwImgOriginal = bwImgOriginal.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(bwImgOriginal, labels, stats, centroids, LineType.FourConnected, DepthType.Cv32S);
            Image<Gray, Int16> labelsImg = labels.ToImage<Gray, Int16>();
            Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();
            Image<Gray, Int16> centroidsImg = centroids.ToImage<Gray, Int16>();

            int refHeight = 311 - 273;//50
            int refWidth = 772 - 746;//33;//
            Rectangle oneChar = new Rectangle();

            Console.WriteLine(numberOfLabels);
            Image<Rgb,byte> imgOriginalColor = imgOriginal.ToImage<Rgb, byte>();
            int redRef = 91;
            int greenRef = 233;
            int blueRef = 17;

            int redAvg;
            int greenAvg;
            int blueAvg;
            for (int i = 0; i < numberOfLabels; i++) {
                //too small
                if (statsImg.Data[i, 4, 0] < 20) {
                    continue;
                }
                //too big
                if (statsImg.Data[i, 4, 0] > (int)((imgOriginal.Size.Width * imgOriginal.Size.Height)/4))//for whole image 16
                {
                    continue;
                }
                //wider than higher
                if (statsImg.Data[i, 2, 0] > statsImg.Data[i, 3, 0])
                {
                    continue;
                }
                //similar size of reference
                //if (statsImg.Data[i, 3, 0] > (int)(refHeight * 0.75) && statsImg.Data[i, 3, 0] < (int)(refHeight * 1.3) && statsImg.Data[i, 2, 0] > (int)(refWidth * 0.4) && statsImg.Data[i, 2, 0] < (int)(refWidth * 1.7)) {
                    oneChar.Location = new Point(statsImg.Data[i, 0, 0], statsImg.Data[i, 1, 0]);
                    oneChar.Size = new Size(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0]);
                    Image<Rgb, byte> cropNew = imgOriginalColor.Clone();
                    cropNew.ROI = oneChar;
                    getRefColor(labelsImg, cropNew, i, out redAvg, out greenAvg, out blueAvg, oneChar);
                    //if (redAvg >= redRef - 50 && redAvg <= redRef + 50 && greenAvg >= greenRef - 50 && greenAvg <= greenRef + 50 && blueAvg >= blueRef - 50 && blueAvg <= blueRef + 50)
                    //{
                        cropNew.Save("Candidate" + i + "R=" + redAvg + "G=" + greenAvg + "B=" + blueAvg + ".jpeg");
                    //}
                //}
            }

            ibCamera.Image = bwImgOriginal;
        }

        private void getRefColor(Image<Gray, Int16> labelsImg, Image<Rgb, byte> cropNew, int labelI, out int redAvg, out int greenAvg, out int blueAvg, Rectangle oneChar)
        {
            int actualLabel = labelI;
            int blue = 0;
            int green = 0;
            int red = 0;
            int counter = 0;

            //Console.WriteLine("Looking for: " + actualLabel);
            //Console.WriteLine(cropNew.Width + " |crop| " + cropNew.Height);
            //Console.WriteLine(labelsImg.Width + " |labels| " + labelsImg.Height);
            //Console.WriteLine("Start of roi: row = " + oneChar.Location.Y + " col = " + oneChar.Location.X);
            //Console.WriteLine(labelsImg.Data[47, 1187, 0]);
            //Console.WriteLine(labelsImg.Data[47 - oneChar.Location.Y, 1187 - oneChar.Location.X, 0]);
            ////labelsImg.ROI = oneChar;
            //Console.WriteLine(labelsImg.Width + " |labelsAfterRoi| " + labelsImg.Height);
            //go throught whole symbol and obtain a avg value of color

            Image<Gray, Byte>[] channels = cropNew.Split();
            for (int i = 0; i < cropNew.Height; i++)
            {
                for (int j = 0; j < cropNew.Width; j++)
                {
                    //Console.Write(" | " + (i + oneChar.Location.Y) + "////" + (j + oneChar.Location.X));
                    //Console.Write(" | " + labelsImg.Data[i+ oneChar.Location.Y, j+ oneChar.Location.X, 0]);
                    if (labelsImg.Data[i + oneChar.Location.Y, j + oneChar.Location.X, 0] == actualLabel)
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
            //Console.WriteLine(" red = " + redRef + " green = " + greenRef + "blue = " + blueRef + " COUNTER = " + counter);
        }

        //variables for backUpRoi
        int redRef = 137;
        int greenRef = 214;
        int blueRef = 85;

        private void btnBackUpRoi_Click(object sender, EventArgs e)
        {
            //open image which i send to method in backUpProcess
            Mat imgOriginal;
            int refHeight = 50;
            int refWidth = 33;
            int bbCol;
            int bbRow;
            int bbWidth;
            int bbHeight;
            //dialog to open new photo
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);

            //allready done in label
            //CvInvoke.CvtColor(imgOriginal, gray, ColorConversion.Bgr2Gray);
            Image<Gray, byte> bwImgOriginal = imgOriginal.ToImage<Gray, byte>();
            bwImgOriginal = bwImgOriginal.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(bwImgOriginal, labels, stats, centroids, LineType.FourConnected, DepthType.Cv32S);
            Image<Gray, Int16> labelsImg = labels.ToImage<Gray, Int16>();
            Image<Gray, Int16> statsImg = stats.ToImage<Gray, Int16>();


            //call backUp
            int label;
            camera.backUpProcess backUpProcess = new camera.backUpProcess();
            Image<Rgb, byte> imgOriginalColor = imgOriginal.ToImage<Rgb, byte>();
            label = backUpProcess.backUpLabel(imgOriginalColor, numberOfLabels, labelsImg, statsImg, redRef, greenRef, blueRef, refHeight, refWidth, out bbCol, out bbRow, out bbWidth, out bbHeight);

            if (label > 0)
            {
                Console.WriteLine("Candiadte: " + label);
                var color = new Rgb(255, 0, 0);
                Rectangle dodo = new Rectangle();
                dodo.Location = new Point(bbCol, bbRow);
                dodo.Size = new Size(bbWidth, bbHeight);
                imgOriginalColor.Draw(dodo, color, 3);
                ibCamera.Image = imgOriginalColor;
            }
        }

        //make quares
        private void btnSymbolProperties_Click(object sender, EventArgs e)
        {
            //load all templates
            ////load all templates to array
            //number of files in directory
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("TemplatesTestMonitor");
            int count = dir.GetFiles().Length;
            Console.WriteLine(count);
            //load all templates
            Mat imgOriginal;
            listOfImages = new List<Image<Gray, byte>>();
            for (int i = 0; i < count; i++)
            {
                imgOriginal = new Mat("./TemplatesTestMonitor/" + (i + 1) + ".jpeg", LoadImageType.Color);
                listOfImages.Add(imgOriginal.ToImage<Gray, byte>());
            }

            Image<Gray, byte> save;
            //int size = 50;
            //new resized images
            //for (int i = 0; i < listOfImages.Count; i++)
            //{
            //    save = makeNewImage(listOfImages[i], size);
            //    save.Save("New35x35" + i + ".jpeg");
            //}

            Console.WriteLine("ASPECT RATIO");
            double aspectRatio;
            for (int i = 0; i < listOfImages.Count; i++)
            {
                aspectRatio = (double)listOfImages[i].Height / (double)listOfImages[i].Width;
                save = listOfImages[i];
                Console.WriteLine("Symbol(" + i + ")| AspectRatio(" + aspectRatio + ")");
                save.Save("AspectRatio" + aspectRatio + ".jpeg");
            }

            Console.WriteLine("NUMBER OF LAKES");
            //Mat invert = new Mat();
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            var valueScalar = new MCvScalar(0);
            for (int i = 0; i < listOfImages.Count; i++)
            {
                listOfImages[i] = listOfImages[i].ThresholdBinary(new Gray(100), new Gray(255));
                //give black border
                save = new Image<Gray, byte>(listOfImages[i].Size);
                CvInvoke.CopyMakeBorder(listOfImages[i], save, 100, 100, 100, 100, BorderType.Constant, valueScalar);
                //invert
                CvInvoke.BitwiseNot(save, save);
                //label image
                numberOfLabels = CvInvoke.ConnectedComponentsWithStats(save, labels, stats, centroids, LineType.FourConnected, DepthType.Cv32S);
                //
                Console.WriteLine("Symbol(" + i + ")| NumberOfLakes(" + (numberOfLabels - 2) + ")");
                save.Save(i + "-numberOfLabels" + (numberOfLabels - 2) + ".jpeg");
            }

            Console.WriteLine("RATIO OF WHITE");
            double whiteRatio;
            for (int i = 0; i < listOfImages.Count; i++)
            {
                listOfImages[i] = listOfImages[i].ThresholdBinary(new Gray(100), new Gray(255));
                whiteRatio = ((double)listOfImages[i].Height * (double)listOfImages[i].Width) / (double)listOfImages[i].CountNonzero().Max();
                save = listOfImages[i];
                Console.WriteLine("Symbol(" + i + ")| WhiteRatio(" + whiteRatio + ")");
                save.Save(i + "-WhiteRatio" + whiteRatio + ".jpeg");
            }

            Console.WriteLine("AVG number of changes in white/black steps");
            int stepsLine = 0;
            int allSteps = 0;
            int stepsLineMax = 0;
            double forAllLinesHeight = 0;
            for (int i = 0; i < listOfImages.Count; i++)// listOfImages.Count
            {
                allSteps = 0;
                forAllLinesHeight = 0;
                listOfImages[i] = listOfImages[i].ThresholdBinary(new Gray(100), new Gray(255));
                for (int j = 0; j < listOfImages[i].Height-1; j++)
                {
                    stepsLine = 0;
                    for (int k = 0; k < listOfImages[i].Width-1; k++)
                    {
                        //Console.Write(listOfImages[i].Data[j, k, 0] + "|");
                        if ((listOfImages[i].Data[j, k, 0] == 0 && listOfImages[i].Data[j, k + 1, 0] == 255) || (listOfImages[i].Data[j, k, 0] == 255 && listOfImages[i].Data[j, k + 1, 0] == 0)) {
                            stepsLine++;
                        }
                    }
                    if (stepsLine > stepsLineMax)
                    {
                        stepsLineMax = stepsLine;
                    }

                    allSteps += stepsLine;
                    //Console.WriteLine("For one line = " + stepsLine);
                }
                forAllLinesHeight = ((double)allSteps / listOfImages[i].Height);
                listOfImages[i].Save("BW" + i + "_" + forAllLinesHeight + "_MAX_" + stepsLineMax + ".jpeg");
            }
        }

        private Image<Gray, byte> makeNewImage(Image<Gray, byte> image, int size)
        {
            Image<Gray, byte> save;


            double h1 = size * (image.Height / (double)image.Width);
            double w2 = size * (image.Width / (double)image.Height);
            //wider
            if (h1 <= size)
            {
                save = image.Resize(size, (int)h1, Inter.Cubic);
            }
            //heigher
            else
            {
                save = image.Resize((int)w2, size, Inter.Cubic);
            }

            int top = (size - save.Height) / 2;
            int down = (size - save.Height + 1) / 2;
            int left = (size - save.Width) / 2;
            int right = (size - save.Width + 1) / 2;

            var valueScalar = new MCvScalar(0);
            CvInvoke.CopyMakeBorder(save, save, top, down, left, right, BorderType.Constant, valueScalar);

            return save;
        }

        private void btnTestChar_Click(object sender, EventArgs e)
        {
            DialogResult drChosenFile;
            drChosenFile = ofdOpenFile.ShowDialog();
            Mat imgOriginal;
            imgOriginal = new Mat(ofdOpenFile.FileName, LoadImageType.Color);
            //Mat invert = new Mat();
            //CvInvoke.BitwiseNot(imgOriginal, invert);
            //threshold binary
            Image<Gray, byte> BW = imgOriginal.ToImage<Gray, byte>();
            BW = BW.ThresholdBinary(new Gray(100), new Gray(255));

            Image<Gray, byte> save;
            int size = 35;
            save = makeNewImage(BW, size);
            save.Save("New35x35.jpeg");

        }
    }
}
