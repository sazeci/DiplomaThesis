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
using System.IO;
using System.Drawing;

namespace Diploma.save
{
    class saveToFile
    {
        private Tesseract ocr;
        private StringBuilder csv;
        private Image<Rgb, byte> colorImage;
        int counter = 0;

        private int counterHelpa = 0;

        public saveToFile()
        {
            ocr = new Tesseract("", "eng", OcrEngineMode.TesseractOnly);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789/()");
        }

        internal void openCsv()
        {
            csv = new StringBuilder();
            string newLine = "";

            for (int i = 0; i < camera.labelSettings.labelList.Count; i++)
            {
                newLine += camera.labelSettings.labelList[i].name + ",";
            }
            csv.AppendLine(newLine);
        }

        internal void saveToCsv()
        {
            string newLine = "";
            string actualOCR = "";
            Mat invert;
            invert = camera.labelSettings.labelList[0].actualBBFill.Mat;

            for (int i = 0; i < camera.labelSettings.labelList.Count; i++) {
                if (camera.labelSettings.labelList[i].noText == false)
                {
                    invert = camera.labelSettings.labelList[i].actualBBFill.Mat;
                    CvInvoke.CopyMakeBorder(invert, invert, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
                    //test
                    //Image<Gray, byte> invertGray = invert.ToImage<Gray, byte>();
                    //invertGray = invertGray.ThresholdBinary(new Gray(100), new Gray(255));
                    //invertGray.Resize(5, Inter.Cubic);
                    //CvInvoke.CopyMakeBorder(invertGray, invertGray, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
                    //invertGray = invertGray.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                    //invertGray.Save("Invert" + counterHelpa + ".jpeg");
                    //test - end
                    counterHelpa++;
                    ocr.Recognize(invert);
                    //invert.Save("Invert" + counterHelpa + ".jpeg");
                    actualOCR = ocr.GetText();
                    if (actualOCR.Length == 0) {//actualOCR.Length == 0
                        actualOCR = testConvolution(invert);
                        //TODO konvoluce s maskou
                        Console.WriteLine("Text length = 0");
                    }
                    newLine = newLine + actualOCR + "," ;
                }
                else {
                    Console.WriteLine("No text == true");
                    newLine = newLine + "NaN,";
                }
            }
            newLine = newLine.Replace(System.Environment.NewLine, "");
            newLine = newLine.Replace(" ", "");
            //Console.WriteLine(newLine);
            csv.AppendLine(newLine);
        }



        private string testConvolution(Mat invert)
        {
            ////load all templates to array
            //number of files in directory
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("TemplatesTestMonitor");
            int count = dir.GetFiles().Length;
            Console.WriteLine(count);
            //load all templates
            Mat imgOriginal;
            List<Image<Gray, byte>> listOfImages = new List<Image<Gray, byte>>();
            for (int i = 0; i < count; i++)
            {
                imgOriginal = new Mat("./TemplatesTestMonitor/" + (i + 1) + ".jpeg", LoadImageType.Color);
                listOfImages.Add(imgOriginal.ToImage<Gray, byte>());
            }


            Image<Gray, byte> actualCroppedImage = invert.ToImage<Gray, byte>();
            actualCroppedImage = actualCroppedImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));

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

            int size = 35;
            List<int> resultWithHighAccuracy = new List<int>();


            Console.WriteLine("numberOfLabels " + numberOfLabels);
            for (int i = 0; i < numberOfLabels; i++)
            {
                //Console.WriteLine("statsImg.Data[i, 0, 0] " + statsImg.Data[i, 0, 0] + "  " + statsImg.Data[i, 1, 0] + "|||[" + statsImg.Data[i, 2, 0] + "," + statsImg.Data[i, 3, 0] + "]");
                if (statsImg.Data[i, 4, 0] > 50)
                {
                    cropped = actualCroppedImage;
                    //define new roi
                    roicek.Location = new Point(statsImg.Data[i, 0, 0], statsImg.Data[i, 1, 0]);//left top
                    roicek.Size = new Size(statsImg.Data[i, 2, 0], statsImg.Data[i, 3, 0]);//width height
                    cropped.ROI = roicek;
                    //cropped.Save("candidateBeforeRoi_" + i + "_" + counter + ".jpeg");
                    counter++;

                    //convert to square size x size
                    cropped = makeNewImage(cropped, size);
                    //cropped.Save("Acandidate_" + i + ".jpeg");
                    //compare to candidates
                    double percentMin = double.MaxValue;
                    int minChar = -1;
                    for (int j = 0; j < listOfImages.Count; j++)
                    {
                        template = makeNewImage(listOfImages[j], size);
                        //template.Save("Bcandidate_" + i + "_template_" + j + ".jpeg");
                        var a = cropped.AbsDiff(template);
                        //a.Save("C" + i + " " + j + ".jpeg");
                        //morphological open
                        Mat kernel1 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(2, 2), new Point(1, 1));
                        CvInvoke.MorphologyEx(a, a, MorphOp.Open, kernel1, new Point(0, 0), 2, BorderType.Default, new MCvScalar());
                        a = a.ThresholdBinary(new Gray(100), new Gray(255));
                        int nonZero = CvInvoke.CountNonZero(a);
                        double percent = (double)(nonZero * 100) / (size * size);
                        //Console.WriteLine("Percent = " + percent);
                        if (percent < percentMin)
                        {
                            percentMin = percent;
                            minChar = j;
                            //from right
                        }
                    }
                    if (percentMin < 2)
                    {
                        resultWithHighAccuracy.Add(minChar);
                    }
                }
            }

            //test
            //for (int i = 0; i < resultWithHighAccuracy.Count; i++)
            //{
            //    //from right
            //    Console.WriteLine(resultWithHighAccuracy[i]);
            //}
            string returnString = "";
            if (resultWithHighAccuracy.Count <= 0)
            {
                return "NaN";
            }
            else {
                for (int i = resultWithHighAccuracy.Count-1; i >= 0; i--)
                {
                    returnString += decodeConvolution(resultWithHighAccuracy[i]);
                }
            }
            return returnString;
        }

        private string decodeConvolution(int codeValueOfMask)
        {
            List<string> decode = new List<string>();
            decode.Add("1");
            decode.Add("2");
            decode.Add("3");
            decode.Add("4");
            decode.Add("5");
            decode.Add("6");
            decode.Add("7");
            decode.Add("8");
            decode.Add("9");
            decode.Add("0");
            decode.Add("(");
            decode.Add(")");
            decode.Add("/");

            return decode[codeValueOfMask];
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

        internal void closeCsv()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            File.WriteAllText("Monitor", csv.ToString());
        }
    }
}
