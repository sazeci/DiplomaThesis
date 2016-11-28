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
                    ocr.Recognize(invert);
                    actualOCR = ocr.GetText();
                    if (actualOCR.Length == 0) {
                        actualOCR = "NaN";
                    }

                    newLine = newLine + actualOCR + "," ;
                }
                else {
                    newLine = newLine + "NaN,";
                }
            }
            newLine = newLine.Replace(System.Environment.NewLine, "");
            newLine = newLine.Replace(" ", "");
            Console.WriteLine(newLine);
            csv.AppendLine(newLine);
        }

        internal void closeCsv()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            File.WriteAllText("Monitor", csv.ToString());
        }
    }
}
