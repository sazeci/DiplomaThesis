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
    class bwThresholding
    {
        Image<Gray, byte> actualCroppedImage;
        Image<Gray, byte> imgBlurred;
        int thresholdValue;

        public bwThresholding()
        {
            if (camera.cameraSettings.cameraList.Count > 0)
            {
                imgBlurred = new Image<Gray, byte>(camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi.Size);
            }
        }

        internal Mat globalBwCrop(Mat imgOriginal, int globalThreshold)
        {
            //treshold value 1-100
            //grayscale
            actualCroppedImage = imgOriginal.ToImage<Gray, byte>();
            //equalize hist
            actualCroppedImage._EqualizeHist();
            //crop
            if (camera.cameraSettings.cameraList.Count > 0)
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            //blue gaussian
            CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //bwGlobal
            thresholdValue = globalThreshold * 255 / 100;
            //Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + globalThreshold);
            imgBlurred = imgBlurred.ThresholdBinary(new Gray(thresholdValue), new Gray(255));

            return (imgBlurred.Mat);
        }

        //TODO oddelit crop
        internal Mat adaptiveBwCrop(Mat imgOriginal, int adaptiveThreshold)
        {
            //TODO

            //treshold value 1-100
            //grayscale
            actualCroppedImage = imgOriginal.ToImage<Gray, byte>();//TODO tady to haze errory
            //equalize hist
            actualCroppedImage._EqualizeHist();
            //crop
            if (camera.cameraSettings.cameraList.Count > 0)
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
            //imgBlurred = actualCroppedImage.SmoothGaussian(5);
            //CvInvoke.GaussianBlur(actualCroppedImage, imgBlurred, new Size(5, 5), 1.5);
            //imgBlurred = actualCroppedImage.SmoothMedian(3);
            imgBlurred = actualCroppedImage;

            //bwGlobal
            thresholdValue = adaptiveThreshold * 255 / 100;
            Console.WriteLine("thresholdValue " + thresholdValue + " globalTreshold = " + adaptiveThreshold);
            imgBlurred = imgBlurred.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(thresholdValue));    //23               

            //CvInvoke.GaussianBlur(imgBlurred, imgBlurred, new Size(5, 5), 1.5);

                return (imgBlurred.Mat);
        }
    }
}
