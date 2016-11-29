using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using System.Collections;
using Emgu.CV.OCR;
using Emgu.CV.Util;

namespace Diploma.camera
{
    class backUpProcess
    {

        Image<Gray, Int16> labelsImg;
        Image<Gray, Int16> statsImg;
        Image<Gray, Int16> centroidsImg;
        int pairsCount;
        private int[,] findedMatches;

        public int backUpLabel(Image<Rgb, byte> imgOriginalColor, int numberOfLabels, Image<Gray, short> labelsImg, Image<Gray, short> statsImg, int redRef, int greenRef, int blueRef, int refHeight, int refWidth, out int bbCol, out int bbRow, out int bbWidth, out int bbHeight)
        {
            Rectangle oneChar = new Rectangle();
            int redAvg;
            int greenAvg;
            int blueAvg;
            //Image<Rgb, byte> imgOriginalColor = imgOriginal.ToImage<Rgb, byte>();

            for (int i = 0; i < numberOfLabels; i++)
            {
                //Console.WriteLine(i);
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
                //Console.WriteLine("After Basic = " + i);
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
            return 0;
        }

        public void getRefColor(Image<Gray, Int16> labelsImg, Image<Rgb, byte> cropNew, int labelI, out int redAvg, out int greenAvg, out int blueAvg, Rectangle oneChar)
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
            //Console.WriteLine(" red = " + redAvg + " green = " + greenAvg + "blue = " + blueAvg + " COUNTER = " + counter);
        }

        internal void defineBeforeBackUp(Mat actualImage, bool isBeforeBackUp)
        {
            Image<Rgb, byte> colorWhole = actualImage.ToImage<Rgb, byte>();
            int redAvg;
            int greenAvg;
            int blueAvg;
            int background;
            int positionInFindedMatches = 0;
            findedMatches = new int[labelSettings.labelList.Count,100];

            //BW for markAreas
            Image<Gray, byte> bwImgOriginal = actualImage.ToImage<Gray, byte>();
            bwImgOriginal = bwImgOriginal.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 101, new Gray(0));
            bwImgOriginal.SmoothMedian(11);
            //bwImgOriginal.Save("BlackAndWhite.jpeg");

            //label image
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int numberOfLabels;
            numberOfLabels = CvInvoke.ConnectedComponentsWithStats(bwImgOriginal, labels, stats, centroids, LineType.FourConnected, DepthType.Cv32S);
            labelsImg = labels.ToImage<Gray, Int16>();
            statsImg = stats.ToImage<Gray, Int16>();
            centroidsImg = centroids.ToImage<Gray, Int16>();

            //go throught all labels and define which one of same parameters is it
            for (int i = 0; i < labelSettings.labelList.Count; i++){
                positionInFindedMatches = 0;
                //for all areas
                for (int j = 0; j < numberOfLabels; j++) {
                    //too small
                    if (statsImg.Data[j, 4, 0] < 20)
                    {
                        //Console.WriteLine("TOO SMALL");
                        continue;
                    }
                    //too big
                    if (statsImg.Data[j, 4, 0] > (int)((colorWhole.Size.Width * colorWhole.Size.Height) / 16))//for whole image 16
                    {
                        //Console.WriteLine("TOO BIG");
                        continue;
                    }
                    //wider than higher
                    if (statsImg.Data[j, 2, 0] > statsImg.Data[i, 3, 0])
                    {
                        //Console.WriteLine("WIDER THAN HEIGHER");
                        continue;
                    }
                    //aspect ratio Check
                    if ((((double)statsImg.Data[j, 3, 0] / (double)statsImg.Data[j, 2, 0]) < 1.1) || (((double)statsImg.Data[j, 3, 0] / (double)statsImg.Data[j, 2, 0]) > 4))
                    {
                        //Console.WriteLine("BAD ASPECT RATIO");
                        continue;
                    }
                    //check white ratio
                    Rectangle roicek = new Rectangle();
                    roicek.Location = new Point(statsImg.Data[j, 0, 0], statsImg.Data[j, 1, 0]);//left top
                    roicek.Size = new Size(statsImg.Data[j, 2, 0], statsImg.Data[j, 3, 0]);//width height
                    Image<Gray, byte> save = bwImgOriginal.Clone();
                    save.ROI = roicek;
                    if (((((double)statsImg.Data[j, 3, 0] * (double)statsImg.Data[j, 2, 0]) / (double)save.CountNonzero().Max()) < 1.1) || ((((double)statsImg.Data[j, 3, 0] * (double)statsImg.Data[j, 2, 0]) / (double)save.CountNonzero().Max()) > 2.5))
                    {
                        //Console.WriteLine("BAD WHITE RATIO");
                        continue;
                    }
                    double helpic = (((double)statsImg.Data[j, 3, 0] * (double)statsImg.Data[j, 2, 0]) / (double)save.CountNonzero().Max());

                    //chcek size and color
                    if (statsImg.Data[j, 3, 0] > (int)(labelSettings.labelList[i].heightRef * 0.75) && statsImg.Data[j, 3, 0] < (int)(labelSettings.labelList[i].heightRef * 1.3) && statsImg.Data[j, 2, 0] > (int)(labelSettings.labelList[i].widthRef * 0.4) && statsImg.Data[j, 2, 0] < (int)(labelSettings.labelList[i].widthRef * 1.7))
                    {
                    //Console.WriteLine("SIZE OK");
                        Image<Rgb, byte> cropNew = colorWhole.Clone();
                        cropNew.ROI = roicek;
                        getRefColorBeforeBackUp(labelsImg, cropNew, j, out redAvg, out greenAvg, out blueAvg, roicek, out background);
                        //labelSettings.labelList[i].actualBBFill.Save("Candidate" + i + "R=" + labelSettings.labelList[i].redRef + "G=" + labelSettings.labelList[i].greenRef + "B=" + labelSettings.labelList[i].blueRef + ".jpeg");
                        if (redAvg >= labelSettings.labelList[i].redRef - 50 && redAvg <= labelSettings.labelList[i].redRef + 50 && greenAvg >= labelSettings.labelList[i].greenRef - 50 && greenAvg <= labelSettings.labelList[i].greenRef + 50 && blueAvg >= labelSettings.labelList[i].blueRef - 50 && blueAvg <= labelSettings.labelList[i].blueRef + 50)
                        {
                            //Console.WriteLine("COLOR OK");
                            //chceck color of back
                            if (background < 65)
                            {
                                findedMatches[i, positionInFindedMatches] = j;
                                positionInFindedMatches++;
                                //Console.WriteLine("BACKGROUND OK");
                                //cropNew.Save("Candidate(" + j + ").jpeg");
                                cropNew.Save("Candidate" + i + "R=" + redAvg + "G=" + greenAvg + "B=" + blueAvg + "Background=" + background + ".jpeg");
                            }
                        }
                    }

                }
            }
            //for (int i = 0; i < findedMatches.GetLength(0); i++)
            //{
            //    for (int j = 0; j < findedMatches.GetLength(1); j++)
            //    {
            //        Console.Write(findedMatches[i, j] + "|");
            //    }
            //    Console.WriteLine();
            //}

            foundLines(findedMatches, isBeforeBackUp, actualImage);
        }

        private void foundLines(int[,] findedMatches, bool isBeforeBackUp, Mat actualImage)
        {
            int distanceX = 0;
            int[,] matchesForOneLabel;
            int matchesForOneLabelCounter = 0;

            //all labels
            for (int i = 0; i < findedMatches.GetLength(0); i++)
            {
                matchesForOneLabel = new int[2, 100];
                matchesForOneLabelCounter = 0;
                pairsCount = 0;
                //all candidates
                for (int j = 0; j < 100; j++)
                {
                    //end of candidates
                    if (findedMatches[i, j] == 0) {
                        break;
                    }
                    //compare with all other candidates
                    for (int k = 0; k < 100; k++)
                    {
                        //comparing same with same
                        if (k == j) {
                            continue;
                        }
                        //end of candidates
                        if (findedMatches[i, k] == 0)
                        {
                            break;
                        }
                        //calculate distance in X
                        distanceX = Math.Abs(centroidsImg.Data[findedMatches[i, j], 0, 0] - centroidsImg.Data[findedMatches[i, k], 0, 0]);
                        //Console.WriteLine("Distence between|" + findedMatches[i, j] + "|" + findedMatches[i, k] + "| is " + distanceX + " should be smaller than: " + (statsImg.Data[findedMatches[i, j], 2, 0] * 2));

                        //symbols are in distance < 2*width of symbol
                        if (distanceX < (statsImg.Data[findedMatches[i, j], 2, 0] * 2)) {
                            //centroid of K is in heigh of J (in Y axis)
                            if (centroidsImg.Data[findedMatches[i, k], 1, 0] > statsImg.Data[findedMatches[i, j], 1, 0] && centroidsImg.Data[findedMatches[i, k], 1, 0] < (statsImg.Data[findedMatches[i, j], 1, 0] + statsImg.Data[findedMatches[i, j], 3, 0])) {
                                matchesForOneLabel[0, matchesForOneLabelCounter] = findedMatches[i, j];
                                matchesForOneLabel[1, matchesForOneLabelCounter] = findedMatches[i, k];
                                matchesForOneLabelCounter++;
                                //Console.WriteLine(findedMatches[i, j] + "||" + findedMatches[i, k]);
                            }
                        }
                    }
                }
                connectPairsOneLabel(matchesForOneLabel, matchesForOneLabelCounter, i, isBeforeBackUp, actualImage);//i = label
            }
        }

        private void connectPairsOneLabel(int[,] matchesForOneLabel, int matchesForOneLabelCounter, int label, bool isBeforeBackUp, Mat actualImage)
        {
            List<List<int>> candidatesInGroups = new List<List<int>>(); ;
            candidatesInGroups.Add(new List<int>());
            candidatesInGroups[0].Add(matchesForOneLabel[0, 0]);
            candidatesInGroups[0].Add(matchesForOneLabel[1, 0]);
            int numberOfLists = 1;
            bool pairWasFounded = false;

            //through all pairs
            for (int i = 1; i < matchesForOneLabelCounter; i++)
            {
                pairWasFounded = false;
                //try to found match
                for (int j = 0; j < candidatesInGroups.Count; j++)
                {
                    if (candidatesInGroups[j].Contains(matchesForOneLabel[0, i])) {
                        if (candidatesInGroups[j].Contains(matchesForOneLabel[1, i])) {
                            pairWasFounded = true;
                            break;
                        }
                        candidatesInGroups[j].Add(matchesForOneLabel[1, i]);
                        pairWasFounded = true;
                        break;
                    }
                    if (candidatesInGroups[j].Contains(matchesForOneLabel[1, i]))
                    {
                        if (candidatesInGroups[j].Contains(matchesForOneLabel[0, i]))
                        {
                            pairWasFounded = true;
                            break;
                        }
                        candidatesInGroups[j].Add(matchesForOneLabel[0, i]);
                        pairWasFounded = true;
                        break;
                    }
                }
                //no match with pair
                if (pairWasFounded == false) {
                    candidatesInGroups.Add(new List<int>());
                    candidatesInGroups[numberOfLists].Add(matchesForOneLabel[0, i]);
                    candidatesInGroups[numberOfLists].Add(matchesForOneLabel[1, i]);
                    numberOfLists++;
                }
            }


            //check
            //for (int i = 0; i < candidatesInGroups.Count; i++)
            //{
            //    for (int j = 0; j < candidatesInGroups[i].Count; j++)
            //    {
            //        Console.Write(candidatesInGroups[i][j] + "|");
            //    }
            //    Console.WriteLine();
            //}

            //find centroids
            findCentroids(candidatesInGroups, label, isBeforeBackUp, actualImage);
        }

        private void findCentroids(List<List<int>> candidatesInGroups, int label, bool isBeforeBackUp, Mat actualImage)
        {
            int centroidX;
            int centroidY;
            Point[] centroids = new Point[candidatesInGroups.Count];


            //one label
            for (int i = 0; i < candidatesInGroups.Count; i++)
            {
                centroidX = 0;
                centroidY = 0;
                //conected candidates in lines
                for (int j = 0; j < candidatesInGroups[i].Count; j++)
                {
                    centroidX += centroidsImg.Data[candidatesInGroups[i][j], 0, 0];
                    centroidY += centroidsImg.Data[candidatesInGroups[i][j], 1, 0];
                }
                centroids[i].X = centroidX / candidatesInGroups[i].Count;
                centroids[i].Y = centroidY / candidatesInGroups[i].Count;
                //Console.WriteLine("Centroid = " + centroids[i]);
            }
            if (isBeforeBackUp == true)
            {
                findWhichOneIsLabelOfInterest(centroids, label);
            }
            else {
                findDistanceAndPosition(centroids, label, actualImage);
            }
        }

        private void findDistanceAndPosition(Point[] centroids, int label, Mat actualImage)
        {
            int[] distanceX = new int[centroids.GetLength(0)];
            int[] distanceXI = new int[centroids.GetLength(0)];
            int[] distanceY = new int[centroids.GetLength(0)];
            int[] distanceYI = new int[centroids.GetLength(0)];

            //calculate the distance to the regular label in X and Y
            for (int i = 0; i < centroids.GetLength(0); i++)
            {
                //add info to XY axis
                distanceXI[i] = i;
                distanceX[i] = centroids[i].X;
                distanceYI[i] = i;
                distanceY[i] = centroids[i].Y;
            }

            //sort by X and Y
            int left = 0;
            int top = 0;

            //order X by x[1]
            Array.Sort(distanceX, distanceXI);
            //order by Y[1]
            Array.Sort(distanceY, distanceYI);

            //point 
            Point localPoint = new Point();
            if (distanceX.GetLength(0) <= labelSettings.labelList[label].left || distanceY.GetLength(0) <= labelSettings.labelList[label].top)
            {

            }
            else {
                localPoint.X = distanceX[labelSettings.labelList[label].left];
                localPoint.Y = distanceY[labelSettings.labelList[label].top];

                labelSettings.labelList[label].actualizeAfterBackUp(localPoint, actualImage);
            }
        }

        private void findWhichOneIsLabelOfInterest(Point[] centroids, int label)
        {
            int distance = int.MaxValue;
            int helpDistance;
            int whichIsClosest = 0;
            int[] distanceX = new int[centroids.GetLength(0)];
            int[] distanceXI = new int[centroids.GetLength(0)];
            int[] distanceY = new int[centroids.GetLength(0)];
            int[] distanceYI = new int[centroids.GetLength(0)];

            //calculate the distance to the regular label from all of candidates to obtain which one is correct
            for (int i = 0; i < centroids.GetLength(0); i++)
            {
                //distance = Math.Abs(centroids[i].X - (labelSettings.labelList[label].centroidBB.X + labelSettings.labelList[label].roi.Location.X)) + Math.Abs(centroids[i].Y - (labelSettings.labelList[label].centroidBB.Y + labelSettings.labelList[label].roi.Location.Y));
                //Console.WriteLine(centroids[i].X + " ---- " + centroids[i].Y);
                //Console.WriteLine((labelSettings.labelList[label].centroidBB.X) + " ---- " + (labelSettings.labelList[label].centroidBB.Y));
                //Console.WriteLine("Distance: " + (Math.Abs(centroids[i].X - (labelSettings.labelList[label].centroidBB.X)) + Math.Abs(centroids[i].Y - (labelSettings.labelList[label].centroidBB.Y))));
                helpDistance = Math.Abs(centroids[i].X - (labelSettings.labelList[label].centroidBB.X)) + Math.Abs(centroids[i].Y - (labelSettings.labelList[label].centroidBB.Y));
                //add info to XY axis
                distanceXI[i] = i;
                distanceX[i] = centroids[i].X;
                distanceYI[i] = i;
                distanceY[i] = centroids[i].Y;

                if (helpDistance < distance) {
                    distance = helpDistance;
                    whichIsClosest = i;
                }
            }

            findPositionInScene(distanceX, distanceXI, distanceY, distanceYI, whichIsClosest, label);
        }

        private void findPositionInScene(int[] distanceX, int[] distanceXI, int[] distanceY, int[] distanceYI, int whichIsClosest, int label)
        {
            int left = 0;
            int top = 0;

            //order X by x[1]
            Array.Sort(distanceX, distanceXI);
            //order by Y[1]
            Array.Sort(distanceY, distanceYI);

            //find on which place is my label
            //left
            for (int i = 0; i < distanceXI.GetLength(0); i++)
            {
                if (distanceXI[i] == whichIsClosest) {
                    left = i;
                }
            }

            //top
            for (int i = 0; i < distanceYI.GetLength(0); i++)
            {
                if (distanceYI[i] == whichIsClosest)
                {
                    top = i;
                }
            }

            labelSettings.labelList[label].top = top;
            labelSettings.labelList[label].left = left;

            Console.WriteLine("My label is " + left + " from left and " + top + " from top");
        }

        private void getRefColorBeforeBackUp(Image<Gray, short> labelsImg, Image<Rgb, byte> cropNew, int labelI, out int redAvg, out int greenAvg, out int blueAvg, Rectangle roicek, out int background)
        {
            int actualLabel = labelI;
            int blue = 0;
            int green = 0;
            int red = 0;
            int counterColor = 0;
            //background
            int redBack = 0;
            int greenBack = 0;
            int blueBack = 0;
            int counterBack = 0;
            background = 0;

            //Console.WriteLine("Finded label: " + actualLabel);
            Image<Gray, Byte>[] channels = cropNew.Split();
            for (int i = 0; i < cropNew.Height; i++)
            {
                for (int j = 0; j < cropNew.Width; j++)
                {
                    //Console.Write(labelsImg.Data[i + roicek.Location.Y, j + roicek.Location.X, 0] + "|");
                    if (labelsImg.Data[i + roicek.Location.Y, j + roicek.Location.X, 0] == actualLabel)
                    {
                        counterColor++;
                        red += channels[0].Data[i, j, 0];
                        green += channels[1].Data[i, j, 0];
                        blue += channels[2].Data[i, j, 0];
                    }
                    else {
                        counterBack++;
                        redBack += channels[0].Data[i, j, 0];
                        greenBack += channels[1].Data[i, j, 0];
                        blueBack += channels[2].Data[i, j, 0];
                    }
                }
                //Console.WriteLine("");
            }
            //Console.WriteLine("-----------------------------------------------------------------------------------------");

            redAvg = red / counterColor;
            greenAvg = green / counterColor;
            blueAvg = blue / counterColor;
            //Console.WriteLine(" red = " + redAvg + " green = " + greenAvg + "blue = " + blueAvg + " COUNTER = " + counter);

            //chceck if back is dark
            background = (redBack + greenBack + blueBack) / (3 * counterBack);
        }

        internal void backUpWhole(Mat actualImage)
        {
            bool isBeforeBackUp = false;
            //find candidates
            defineBeforeBackUp(actualImage, isBeforeBackUp);
            //
        }
    }

}
