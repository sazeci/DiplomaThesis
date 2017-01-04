using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.UI;

namespace Diploma.coordinates
{
    class coordinatesManipulation
    {
        /////////////////////////////////////////////////////////////////////////////////////
        internal static void ZoomToRegular(ImageBox ibCamera, Point roiStart, Point actualPoint, out Point regularRoiStart, out Point regularActualPoint)
        {
            regularRoiStart = new Point();
            regularActualPoint = new Point();

            if (ImageBoxRatioIsBigger(ibCamera))
            {
                //borders are on side (Y is same) //trojclenka
                //Console.WriteLine("roiStart.Y = " + roiStart.Y + " ibCamera.Image.Size.Height = " + ibCamera.Image.Size.Height + " ibCamera.ClientSize.Height = " + ibCamera.ClientSize.Height);
                regularRoiStart.Y = (int)(ibCamera.Image.Size.Height * roiStart.Y / (double)ibCamera.ClientSize.Height);
                regularActualPoint.Y = (int)(ibCamera.Image.Size.Height * actualPoint.Y / (double)ibCamera.ClientSize.Height);

                //x must be recalculated
                regularRoiStart.X = newX(ibCamera, roiStart);
                regularActualPoint.X = newX(ibCamera, actualPoint);
            }
            else {
                //borders are on the top and bottom (X is same) //trojclenka
                regularRoiStart.X = (int)(ibCamera.Image.Size.Width * roiStart.X / (double)ibCamera.ClientSize.Width);
                regularActualPoint.X = (int)(ibCamera.Image.Size.Width * actualPoint.X / (double)ibCamera.ClientSize.Width);

                //y must be recalculated
                regularRoiStart.Y = newY(ibCamera, roiStart);
                regularActualPoint.Y = newY(ibCamera, actualPoint);
            }

            Console.WriteLine("actualPoint = " + actualPoint + " regularActualPoint = " + regularActualPoint);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private static int newY(ImageBox ibCamera, Point point)
        {
            //ratio against background
            double scaleHeight = ibCamera.Image.Size.Height * ibCamera.ClientSize.Width / ibCamera.Image.Size.Width;
            //differnce
            double dy = (ibCamera.ClientSize.Height - scaleHeight) / 2;
            return((int)((point.Y - dy) * ibCamera.Image.Size.Width / (double)ibCamera.ClientSize.Width));
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private static int newX(ImageBox ibCamera, Point point)
        {
            //Console.WriteLine("ibCamera.Image.Size.Height = " + ibCamera.Image.Size.Height + " ibCamera.ClientSize.Height = " + ibCamera.ClientSize.Height + "ibCamera.Image.Size.Width = " + ibCamera.Image.Size.Width + " ibCamera.ClientSize.Width = " + ibCamera.ClientSize.Width);
            //width of shown image, exactly on screen
            double scaleWidth = ibCamera.Image.Size.Width * ibCamera.ClientSize.Height / ibCamera.Image.Size.Height;
            //na kolik px je ve scene pred samotnym obrazkem
            double dx = (ibCamera.ClientSize.Width - scaleWidth)/2;
            //position of X depend on ratio between exact image and shown image
            return((int)((point.X - dx) * ibCamera.Image.Size.Height/(double)ibCamera.ClientSize.Height));
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private static bool ImageBoxRatioIsBigger(ImageBox ibCamera)
        {
            double imageBoxRatio = ibCamera.ClientSize.Width / (double)ibCamera.ClientSize.Height;
            double CaptureRatio = ibCamera.Image.Size.Width / (double)ibCamera.Image.Size.Height;

            if (imageBoxRatio > CaptureRatio)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
