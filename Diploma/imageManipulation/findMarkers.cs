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
    class findMarkers
    {
        Image<Gray, Byte> actualCroppedImage;
        Image<Gray, Byte> actualMarker;


        public Mat findTemplate(Mat actualImage, Mat marker)
        {
            //images
            actualCroppedImage = marker.ToImage<Gray, byte>();
            actualMarker = marker.ToImage<Gray, byte>();

            //using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(actualCroppedImage, TemplateMatchingType.CcoeffNormed));

            return marker;
        }

        public Mat hsvTransform(Mat imgOriginal)
        {
            Image<Bgr, Byte> bgrOriginal = imgOriginal.ToImage<Bgr, byte>();
            Image<Hsv, Byte> hsvImage = new Image<Hsv, byte>(bgrOriginal.Size);

            CvInvoke.CvtColor(imgOriginal, hsvImage, ColorConversion.Bgr2Hsv, 0);


            return hsvImage.Mat;
        }

        internal IImage findColor(Mat actualImage)
        {
            Image<Bgr, Byte> bgrOriginal = actualImage.ToImage<Bgr, byte>();
            Image<Gray, Byte> bgrOutput = new Image<Gray, byte>(bgrOriginal.Size);

            bgrOutput = bgrOriginal.InRange(new Bgr(0, 0, 100), new Bgr(100, 100, 255));

            return bgrOutput;
        }
    }
}
