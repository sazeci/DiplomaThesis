using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Diploma.camera
{
    class backUpProcess
    {
        public int backUpLabel(Image<Rgb, byte> imgOriginalColor, int numberOfLabels, Image<Gray, short> labelsImg, Image<Gray, short> statsImg, int redRef, int greenRef, int blueRef, int refHeight, int refWidth, out int bbCol, out int bbRow, out int bbWidth, out int bbHeight)
        {
            Rectangle oneChar = new Rectangle();
            int redAvg;
            int greenAvg;
            int blueAvg;
            //Image<Rgb, byte> imgOriginalColor = imgOriginal.ToImage<Rgb, byte>();

            for (int i = 0; i < numberOfLabels; i++)
            {
                Console.WriteLine(i);
                //too small
                if (statsImg.Data[i, 4, 0] < 20)
                {
                    continue;
                }
                //too big
                if (statsImg.Data[i, 4, 0] > (int)((imgOriginalColor.Size.Width * imgOriginalColor.Size.Height) / 4))//for label only
                {
                    continue;
                }
                //wider than higher
                if (statsImg.Data[i, 2, 0] > statsImg.Data[i, 3, 0])
                {
                    continue;
                }
                Console.WriteLine("After Basic = " + i);
                //similar size of reference
                if (statsImg.Data[i, 3, 0] > (int)(refHeight * 0.75) && statsImg.Data[i, 3, 0] < (int)(refHeight * 1.3) && statsImg.Data[i, 2, 0] > (int)(refWidth * 0.4) && statsImg.Data[i, 2, 0] < (int)(refWidth * 1.7))
                {
                    oneChar.Location = new Point(statsImg.Data[i, 0, 0], statsImg.Data[i, 1, 0]);
                    oneChar.Size = new Size(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0]);
                    Image<Rgb, byte> cropNew = imgOriginalColor.Clone();
                    cropNew.ROI = oneChar;
                    getRefColor(labelsImg, cropNew, i, out redAvg, out greenAvg, out blueAvg, oneChar);
                    if (redAvg >= redRef - 50 && redAvg <= redRef + 50 && greenAvg >= greenRef - 50 && greenAvg <= greenRef + 50 && blueAvg >= blueRef - 50 && blueAvg <= blueRef + 50)
                    {
                        cropNew.Save("Candidate" + i + "R=" + redAvg + "G=" + greenAvg + "B=" + blueAvg + ".jpeg");
                        //maybe check if char
                        bbCol = statsImg.Data[i, 0, 0];
                        bbRow = statsImg.Data[i, 1, 0];
                        bbWidth = statsImg.Data[i, 2, 0];
                        bbHeight = statsImg.Data[i, 3, 0];
                        return i;
                    }
                }
            }

            bbCol = 0;
            bbRow = 0;
            bbWidth = 0;
            bbHeight = 0;
            return -1;
        }

        private void getRefColor(Image<Gray, Int16> labelsImg, Image<Rgb, byte> cropNew, int labelI, out int redAvg, out int greenAvg, out int blueAvg, Rectangle oneChar)
        {
            int actualLabel = labelI;
            int blue = 0;
            int green = 0;
            int red = 0;
            int counter = 0;

            Image<Gray, Byte>[] channels = cropNew.Split();
            for (int i = 0; i < cropNew.Height; i++)
            {
                for (int j = 0; j < cropNew.Width; j++)
                {
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
            Console.WriteLine(" red = " + redAvg + " green = " + greenAvg + "blue = " + blueAvg + " COUNTER = " + counter);
        }
    }
}
