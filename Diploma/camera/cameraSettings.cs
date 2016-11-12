using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DirectShowLib;            //camera

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using System.Windows.Forms;

namespace Diploma.camera
{
    class cameraSettings
    {
        public static int _DeviceIndex;
        public static int ActiveCamera;//which camere is now in setting mode
        public static List<camera> cameraList = new List<camera>();

        /////////////////////////////////////////////////////////////////////////////////////
        internal List<KeyValuePair<int, string>> getListOfCameras()
        {
            //get the list of the instaled cameras
            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();
            //finding of system cameras
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }

            return ListCamerasData;
        }

        internal static void addCamera(int _CameraIndex)
        {
            ActiveCamera = _CameraIndex;
            camera newCamera = new camera(_CameraIndex);
            cameraList.Add(newCamera);
        }
    }
}
