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
using Emgu.CV.OCR;

namespace Diploma
{
    public partial class checkTrackingSettings : Form
    {
        private addAreasSettings addAreasSettings;
        public Mat actualImage;
        Capture capWebcam;
        Image<Gray, byte> actualCroppedImage;
        private bool isStreamEnabled = true;
        private Tesseract ocr;
        private save.saveToFile saveToFile;
        private bool isCsvOpened = false;
        private camera.backUpProcess backUpProcess;

        private int counter = 0;

        private int timerTicker = 0;

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

            //tessarect
            ocr = new Tesseract("", "eng", OcrEngineMode.TesseractOnly);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789/()");

            //save to file
            saveToFile = new save.saveToFile();

            //backup
            backUpProcess = new camera.backUpProcess();
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
            //pause for testing
            //isStreamEnabled = false;
            Image<Gray, byte> save; 
            for (int i = 0; i < camera.labelSettings.labelList.Count; i++) {
                //get roi from image
                actualCroppedImage = actualImage.ToImage<Gray, byte>();
                actualCroppedImage.ROI = camera.labelSettings.labelList[i].BB;
                //binarize image
                actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                //compare to old one
                //Console.WriteLine("camera.labelSettings.labelList[i].actualBBFill " + camera.labelSettings.labelList[i].actualBBFill.Size + " | " + camera.labelSettings.labelList[i].actualBBFill.NumberOfChannels);
                //Console.WriteLine("actualCroppedImage " + actualCroppedImage.Size + " | " + actualCroppedImage.NumberOfChannels);
                //a = new Image<Gray, byte>(actualCroppedImage.Size);
                //CvInvoke.AbsDiff(actualCroppedImage, camera.labelSettings.labelList[i].actualBBFill, a);
                save = camera.labelSettings.labelList[i].actualBBFill.Clone();
                save = save.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                var a = save.AbsDiff(actualCroppedImage);
                int[] nonZeroPixels = a.CountNonzero();
                double percent = (nonZeroPixels.Max() * 100) / (actualCroppedImage.Width * actualCroppedImage.Height);
                //nonZeroPixels.Max() / (actualCroppedImage.Width * actualCroppedImage.Height);
                if (percent > 3)
                {
                    Console.WriteLine("nonZeroPixels.Max() " + nonZeroPixels.Max() + " number of pixels " + (actualCroppedImage.Width * actualCroppedImage.Height) + " CHANGE CHANGE percent: " + percent);
                    camera.labelSettings.labelList[i].actualizeLabel(actualImage);
                    //actualCroppedImage.Save(camera.labelSettings.labelList[i].name + counter + ".jpeg");
                    counter++;
                    if (actualCroppedImage.Size.Height * actualCroppedImage.Size.Width > 100)
                    {
                        //ibCamera.Image = actualCroppedImage;
                    }
                }

                

                //roi
                Image<Gray, byte> roiImage;
                roiImage = actualImage.ToImage<Gray, byte>();
                roiImage.ROI = camera.labelSettings.labelList[0].roi;
                //binarize image
                roiImage = roiImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                ibCamera2.Image = roiImage;

                //bounding
                Image<Gray, byte> boundingImage;
                boundingImage = actualImage.ToImage<Gray, byte>();
                boundingImage.ROI = camera.labelSettings.labelList[0].BB;
                //binarize image
                boundingImage = boundingImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                ibCamera.Image = boundingImage;
            }
        }

        private void checkTrackingSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveToFile.closeCsv();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCsvOpened == false) {
                saveToFile.openCsv();
                isCsvOpened = true;
            }
            saveToFile.saveToCsv();

            //timerTicker++;
            //if (timerTicker == 5)
            //{
            //    //BackUp
            //    backUpProcess.backUpWhole(actualImage);
            //    //reicinalize
            //    timerTicker = 0;
            //}
            //Mat invert = new Mat();
            //CvInvoke.BitwiseNot(camera.labelSettings.labelList[0].actualBBFill, invert);

            //Mat invert = camera.labelSettings.labelList[0].actualBBFill.Mat;
            //add border to better rocognition
            //CvInvoke.CopyMakeBorder(invert, invert, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
            ////rescale
            //Image<Gray, byte> cropped = invert.ToImage<Gray, byte>();
            ////make it bigger
            //cropped.Resize(5, Inter.Cubic);

            //ocr.Recognize(invert);
            //ibCamera.Image = camera.labelSettings.labelList[0].actualBBFill;
            //Console.WriteLine("Text = " + ocr.GetText());
        }
    }
}
