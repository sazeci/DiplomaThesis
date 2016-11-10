using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //

namespace Diploma
{
    public partial class roiSettings : Form
    {

        private setCameraInSpaceSettings setCameraInSpaceSettings;

        private Point roiStart;
        private Rectangle roi = new Rectangle();

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public roiSettings(setCameraInSpaceSettings setCameraInSpaceSettings)
        {
            InitializeComponent();
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            this.setCameraInSpaceSettings = setCameraInSpaceSettings;

            //showImage
            ibCamera.Image = setCameraInSpaceSettings.actualImage;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            setCameraInSpaceSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void roiSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseDown(object sender, MouseEventArgs e)
        {
            roiStart = e.Location;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //actual point
                Point actualPoint = e.Location;

                //rectangle
                roi.Location = new Point(Math.Min(roiStart.X, actualPoint.X), Math.Min(roiStart.Y, actualPoint.Y));
                roi.Size = new Size(Math.Abs(roiStart.X - actualPoint.X), Math.Abs(roiStart.Y - actualPoint.Y));

                //draw rectangle on image
                Image<Bgr, byte> imgEntrada = setCameraInSpaceSettings.actualImage.ToImage<Bgr, byte>();
                imgEntrada.Draw(roi, new Bgr(Color.Red));
                ibCamera.Image = imgEntrada;

                ((PictureBox)sender).Invalidate();

            }
            else {
                return;
            }
        }
    }
}
