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

namespace Diploma.imageManipulation
{
    class bwTreshold
    {
        internal static Mat globalBwCrop(Mat imgOriginal, int globalThreshold)
        {
            //treshold value 1-100
            //grayscale
            Image<Gray, byte> actualCroppedImage = imgOriginal.ToImage<Gray, byte>();
            //crop
            actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            //gausian
            Image<Gray, byte> imgBlurred = new Image<Gray, byte>(actualCroppedImage.Size);
            CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //bwGlobal
            int thresholdValue = globalThreshold * 255 / 100;
            //Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + globalThreshold);
            imgBlurred = imgBlurred.ThresholdBinary(new Gray(thresholdValue), new Gray(255));

            //dispose things
            imgOriginal.Dispose();
            actualCroppedImage.Dispose();

            return (imgBlurred.Mat);
        }

        internal static Mat adaptiveBwCrop(Mat imgOriginal, int adaptiveThreshold)
        {
            //treshold value 1-100
            //grayscale
            Image<Gray, byte> actualCroppedImage = imgOriginal.ToImage<Gray, byte>();
            //crop
            actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            //gausian
            Image<Gray, byte> imgBlurred = new Image<Gray, byte>(actualCroppedImage.Size);
            CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //bwGlobal
            int thresholdValue = adaptiveThreshold * 255 / 100;
            Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + adaptiveThreshold);
            //imgBlurred.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Otsu, 10, new Gray(thresholdValue));
            imgBlurred = imgBlurred.ThresholdBinary(new Gray(thresholdValue), new Gray(255));

            //dispose things
            imgOriginal.Dispose();
            actualCroppedImage.Dispose();

            return (imgBlurred.Mat);
        }
    }
}
