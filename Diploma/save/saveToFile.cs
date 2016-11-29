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

namespace Diploma.save
{
    class saveToFile
    {
        private Tesseract ocr;
        private StringBuilder csv;
        private Image<Rgb, byte> colorImage;

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
                    //CvInvoke.CopyMakeBorder(invert, invert, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
                    //test
                    Image<Gray, byte> invertGray = invert.ToImage<Gray, byte>();
                    invertGray = invertGray.ThresholdBinary(new Gray(100), new Gray(255));
                    invertGray.Resize(5, Inter.Cubic);
                    CvInvoke.CopyMakeBorder(invertGray, invertGray, 100, 100, 100, 100, BorderType.Constant, new MCvScalar(0));
                    invertGray = invertGray.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
                    //invertGray.Save("Invert" + counterHelpa + ".jpeg");
                    //test - end
                    counterHelpa++;
                    ocr.Recognize(invertGray);
                    actualOCR = ocr.GetText();
                    if (actualOCR.Length == 0) {
                        //TODO konvoluce s maskou
                        Console.WriteLine("Text length = 0");
                        actualOCR = "NaN";
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

        internal void closeCsv()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            File.WriteAllText("Monitor", csv.ToString());
        }
    }
}
