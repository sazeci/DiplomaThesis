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

namespace Diploma.camera
{
    class label
    {
        public string name;
        //ROI in regular image
        public int topRowRoi;
        public int leftCollumRoi;
        public int widthRoi;
        public int heightRoi;
        //selected Bounding box letter by letter in regular image
        public Point centroid;
        public int topRowBB;
        public int leftCollumBB;
        public int widthBB;
        public int heightBB;
        //first selected letter
        public Color colorRef;
        public int widthRef;
        public int heightRef;
        private int y;
        private int x;
        private int width;
        private int height;

        public label(int topRowRoi, int leftCollumRoi, int widthRoi, int heightRoi)
        {
            this.topRowRoi = topRowRoi;
            this.leftCollumRoi = leftCollumRoi;
            this.widthRoi = widthRoi;
            this.heightRoi = heightRoi;
        }
        //selected labels - TODO
    }
}
