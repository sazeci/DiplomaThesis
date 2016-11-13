﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using System.Drawing;

namespace Diploma.imageManipulation
{
    class bwThresholding
    {
        Image<Gray, byte> actualCroppedImage;
        Image<Gray, byte> imgBlurred = new Image<Gray, byte>(camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi.Size);
        int thresholdValue;

        internal Mat globalBwCrop(Mat imgOriginal, int globalThreshold)
        {
            //treshold value 1-100
            //grayscale
            actualCroppedImage = imgOriginal.ToImage<Gray, byte>();//TODO tady to haze errory
            //crop
            actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //bwGlobal
            thresholdValue = globalThreshold * 255 / 100;
            //Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + globalThreshold);
            imgBlurred = imgBlurred.ThresholdBinary(new Gray(thresholdValue), new Gray(255));

            return (imgBlurred.Mat);
        }

        internal Mat adaptiveBwCrop(Mat imgOriginal, int adaptiveThreshold)
        {
            //treshold value 1-100
            //grayscale
            actualCroppedImage = imgOriginal.ToImage<Gray, byte>();//TODO tady to haze errory
            //crop
            actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //bwGlobal
            thresholdValue = adaptiveThreshold * 255 / 100;
            //Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + globalThreshold);
            //imgBlurred = imgBlurred.ThresholdBinary(new Gray(thresholdValue), new Gray(255));
            imgBlurred.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.MeanC, ThresholdType.Binary, 10, new Gray(thresholdValue));

            return (imgBlurred.Mat);
        }
    }
}
