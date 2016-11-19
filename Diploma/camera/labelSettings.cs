using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diploma.camera
{
    class labelSettings
    {
        public static List<label> labelList = new List<label>();

        /////////////////////////////////////////////////////////////////////////////////////
        internal static void addLabel(Rectangle roi)
        {
            Rectangle bigRoi = cameraSettings.cameraList[cameraSettings.ActiveCamera].roi;
            Point newLeftTop = new Point();
            newLeftTop.X = bigRoi.Location.X + roi.Location.X;
            newLeftTop.Y = bigRoi.Location.Y + roi.Location.Y;
            roi.Location = newLeftTop;

            label newCamera = new label(roi);
            labelList.Add(newCamera);
        }


    }
}
