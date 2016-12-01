using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Diploma.camera
{
    class camera
    {
        public int _CameraIndex;
        public Rectangle roi = new Rectangle();
        public string fileName;
        public int frame;
        public bool isBackUpEnabled;

        public camera(int _CameraIndex)
        {
            this._CameraIndex = _CameraIndex;
        }
    }
}
