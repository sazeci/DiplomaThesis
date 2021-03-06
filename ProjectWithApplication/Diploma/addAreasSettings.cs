﻿using System;
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

/// <summary>
/// windows form for selection all ROIs
/// </summary>
namespace Diploma
{
    public partial class addAreasSettings : Form
    {
        private additionalSettings additionalSettings;
        public Mat actualImage;
        Capture capWebcam;
        Image<Bgr, byte> actualCroppedImage;
        private Point roiStart;
        private Rectangle roi = new Rectangle();
        public bool isStreamEnabled = true;
        private int actualLabel = 1;
        private bool isStepThree = false;
        camera.backUpProcess backUpProcess;

        /////////////////////////////////////////////////////////////////////////////////////
        public addAreasSettings()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //constructor
        public addAreasSettings(additionalSettings additionalSettings)
        {
            InitializeComponent();
            this.additionalSettings = additionalSettings;
            ibCamera.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            actualImage = new Mat();
            backUpProcess = new camera.backUpProcess();

            //start camera
            startCamera(camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera]._CameraIndex);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void startCamera(int _CameraIndex)
        {
            //Capture capture;
            capWebcam = new Capture(_CameraIndex);

            Application.Idle += ProcessFrame;

        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (isStreamEnabled == true)
            {
                capWebcam.Grab();
                capWebcam.Retrieve(actualImage, 0);

                //crop
                actualCroppedImage = actualImage.ToImage<Bgr, byte>();
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;

                ibCamera.Image = actualCroppedImage;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void addAreasSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            additionalSettings.Show();
            this.Hide();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //processFrameAndUpdateGUI
        private void btnAddArea_Click(object sender, EventArgs e)
        {
            //enable IBcamera manipulation
            lblInstruction.Text = "2) Select area of interest in image";
            ibCamera.Enabled = true;
            //disable buttons
            btnAddArea.Enabled = false;
            btnNext.Enabled = false;
            btnBack.Enabled = false;

            //turn of stream
            //croppedImageInitial = actualCroppedImage;//DOESNT WORK
            isStreamEnabled = false;
            
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (isStepThree == false)
            {
                roiStart = e.Location;
            }
            else {
                Point regularRoiStart;
                coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, e.Location, e.Location, out regularRoiStart, out regularRoiStart);
                Point localPoint = regularRoiStart;
                regularRoiStart.X = regularRoiStart.X + camera.labelSettings.labelList[actualLabel - 1].roi.Location.X;
                regularRoiStart.Y = regularRoiStart.Y + camera.labelSettings.labelList[actualLabel - 1].roi.Location.Y;
                //set point color and others of label
                bool symbolWasFounded = camera.labelSettings.labelList[actualLabel - 1].addClickedPoint(regularRoiStart, localPoint, actualImage);

                if (symbolWasFounded == true)
                {
                    lblInstruction.Text = "4) Write down the name of the label and click save";
                    //visible TB
                    string dodo = "tb" + actualLabel;
                    TextBox tbx = this.Controls.Find(dodo, true).FirstOrDefault() as TextBox;
                    tbx.Visible = true;
                    //show cropped image
                    dodo = "ib" + actualLabel;
                    Image<Bgr, byte> selectedLabel = actualImage.ToImage<Bgr, byte>();
                    selectedLabel.ROI = camera.labelSettings.labelList[actualLabel - 1].roi;
                    ImageBox ibx = this.Controls.Find(dodo, true).FirstOrDefault() as ImageBox;
                    ibx.Image = selectedLabel;
                    //activate the button
                    //visible save button
                    dodo = "btnSave" + actualLabel;
                    Button btnx = this.Controls.Find(dodo, true).FirstOrDefault() as Button;
                    btnx.Visible = true;

                    ibCamera.Enabled = false;
                    isStepThree = false;
                }
                else {
                    lblInstruction.Text = "2) No symbol founded in this ROI please select the new ROI";
                    isStepThree = false;
                    camera.labelSettings.removeLastLabel();
                    ibCamera.Image = actualCroppedImage;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //actual point
                Point actualPoint = e.Location;

                //convert coordinates from ZOOM to regular image
                Point regularRoiStart;
                Point regularActualPoint;
                coordinates.coordinatesManipulation.ZoomToRegular(ibCamera, roiStart, actualPoint, out regularRoiStart, out regularActualPoint);
                Console.WriteLine(regularRoiStart + " | " + regularActualPoint);

                //rectangle
                roi.Location = new Point(Math.Min(regularRoiStart.X, regularActualPoint.X), Math.Min(regularRoiStart.Y, regularActualPoint.Y));
                roi.Size = new Size(Math.Abs(regularRoiStart.X - regularActualPoint.X), Math.Abs(regularRoiStart.Y - regularActualPoint.Y));

                //add roi to labels class
                //Mouse UP              

                //draw rectangle on image
                capWebcam.Grab();
                capWebcam.Retrieve(actualImage, 0);

                //crop
                actualCroppedImage = actualImage.ToImage<Bgr, byte>();
                actualCroppedImage.ROI = camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].roi;
                actualCroppedImage.Draw(roi, new Bgr(Color.Red));
                ibCamera.Image = actualCroppedImage;
            }
            else
            {
                return;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void ibCamera_MouseUp(object sender, MouseEventArgs e)
        {
            if (roi.Width * roi.Height > 400)
            {
                camera.labelSettings.addLabel(roi);
                lblInstruction.Text = "3) Click at required symbol/as close as possible";
                isStepThree = true;

                Image<Bgr, byte> selectedLabel = actualImage.ToImage<Bgr, byte>();
                selectedLabel.ROI = camera.labelSettings.labelList[actualLabel - 1].roi;
                ibCamera.Image = selectedLabel;
            }
            else {
                lblInstruction.Text = "2.5) Selected area is too small. Please repeat the selection or set camera better in space";
                ibCamera.Image = actualCroppedImage;
            }

            //camera.labelSettings.addLabel(roi);
            //lblInstruction.Text = "3) Write down the name of the label and click save";
            ////visible TB
            //string dodo = "tb" + actualLabel;
            //TextBox tbx = this.Controls.Find(dodo, true).FirstOrDefault() as TextBox;
            //tbx.Visible = true;
            ////show cropped image
            //dodo = "ib" + actualLabel;
            //Image<Bgr, byte> selectedLabel = actualImage.ToImage<Bgr, byte>();
            //selectedLabel.ROI = camera.labelSettings.labelList[actualLabel - 1].roi;
            //ImageBox ibx = this.Controls.Find(dodo, true).FirstOrDefault() as ImageBox;
            //ibx.Image = selectedLabel;
            ////activate the button
            ////visible save button
            //dodo = "btnSave" + actualLabel;
            //Button btnx = this.Controls.Find(dodo, true).FirstOrDefault() as Button;
            //btnx.Visible = true;

            //ibCamera.Enabled = false;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void saveAction()
        {
            string dodo = "tb" + actualLabel;
            TextBox tbx = this.Controls.Find(dodo, true).FirstOrDefault() as TextBox;

            if (tbx.Text.Length > 0)
            {
                dodo = "btnSave" + actualLabel;
                Button btnx = this.Controls.Find(dodo, true).FirstOrDefault() as Button;

                camera.labelSettings.labelList[actualLabel - 1].name = tbx.Text;
                btnx.Enabled = false;
                tbx.Enabled = false;
                //enable add
                btnAddArea.Enabled = true;
                btnNext.Enabled = true;
                lblInstruction.Text = "1) Click on Add area";
                actualLabel++;
                //turn on camera
                isStreamEnabled = true;
                //check
                Console.WriteLine("topRowBB " + camera.labelSettings.labelList[actualLabel - 2].topRowBB + " leftCollumBB " + camera.labelSettings.labelList[actualLabel - 2].leftCollumBB + " widthBB " + camera.labelSettings.labelList[actualLabel - 2].widthBB + " heightBB " + camera.labelSettings.labelList[actualLabel - 2].heightBB);

                //save actual contain of BB
                camera.labelSettings.labelList[actualLabel - 2].saveBBFill(actualImage);
            }
            else
            {
                lblInstruction.Text = "4) Write down the name of the label BEFORE click save";
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        private void btnSave1_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave3_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave4_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave5_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave6_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave7_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //define which one is the chosen label for backup
            bool isBeforeBackUp = true;
            if (camera.cameraSettings.cameraList[camera.cameraSettings.ActiveCamera].isBackUpEnabled == true)
            {
                backUpProcess.defineBeforeBackUp(actualImage, isBeforeBackUp);
            }

            //open new form
            isStreamEnabled = false;
            checkTrackingSettings checkTrackingSettings = new checkTrackingSettings(this);
            checkTrackingSettings.Show();
            this.Hide();

        }
    }
}
