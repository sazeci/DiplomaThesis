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
        public Point clickedPonint;//clicked point
        public string name;
        //ROI in regular image
        public Rectangle roi;
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

        public label(Rectangle roi)
        {
            this.roi = roi;
        }
        //selected labels - TODO
    }
}
