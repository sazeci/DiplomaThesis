namespace Diploma
{
    partial class testImageBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ibCamera = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewCapture = new System.Windows.Forms.Button();
            this.btnMarkers = new System.Windows.Forms.Button();
            this.ibMarker = new Emgu.CV.UI.ImageBox();
            this.btnSelectText = new System.Windows.Forms.Button();
            this.btnOCR = new System.Windows.Forms.Button();
            this.btnTemplates = new System.Windows.Forms.Button();
            this.btnDiff = new System.Windows.Forms.Button();
            this.btnFindText = new System.Windows.Forms.Button();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnBackUpRoi = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibMarker)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.ibCamera, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // ibCamera
            // 
            this.ibCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibCamera.Location = new System.Drawing.Point(5, 75);
            this.ibCamera.Margin = new System.Windows.Forms.Padding(5);
            this.ibCamera.Name = "ibCamera";
            this.ibCamera.Size = new System.Drawing.Size(774, 381);
            this.ibCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibCamera.TabIndex = 2;
            this.ibCamera.TabStop = false;
            this.ibCamera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ibCamera_MouseDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.87275F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57029F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.080414F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel2.Controls.Add(this.btnNewCapture, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMarkers, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ibMarker, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSelectText, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOCR, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnTemplates, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDiff, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFindText, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBackUpRoi, 8, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 64);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btnNewCapture
            // 
            this.btnNewCapture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNewCapture.Location = new System.Drawing.Point(7, 20);
            this.btnNewCapture.Name = "btnNewCapture";
            this.btnNewCapture.Size = new System.Drawing.Size(75, 23);
            this.btnNewCapture.TabIndex = 0;
            this.btnNewCapture.Text = "newCapture";
            this.btnNewCapture.UseVisualStyleBackColor = true;
            this.btnNewCapture.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMarkers
            // 
            this.btnMarkers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMarkers.Location = new System.Drawing.Point(97, 20);
            this.btnMarkers.Name = "btnMarkers";
            this.btnMarkers.Size = new System.Drawing.Size(75, 23);
            this.btnMarkers.TabIndex = 1;
            this.btnMarkers.Text = "Markers";
            this.btnMarkers.UseVisualStyleBackColor = true;
            this.btnMarkers.Click += new System.EventHandler(this.btnMarkers_Click);
            // 
            // ibMarker
            // 
            this.ibMarker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibMarker.Location = new System.Drawing.Point(185, 5);
            this.ibMarker.Margin = new System.Windows.Forms.Padding(5);
            this.ibMarker.Name = "ibMarker";
            this.ibMarker.Size = new System.Drawing.Size(80, 54);
            this.ibMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibMarker.TabIndex = 2;
            this.ibMarker.TabStop = false;
            // 
            // btnSelectText
            // 
            this.btnSelectText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectText.Location = new System.Drawing.Point(277, 20);
            this.btnSelectText.Name = "btnSelectText";
            this.btnSelectText.Size = new System.Drawing.Size(75, 23);
            this.btnSelectText.TabIndex = 3;
            this.btnSelectText.Text = "select text";
            this.btnSelectText.UseVisualStyleBackColor = true;
            this.btnSelectText.Click += new System.EventHandler(this.btnSelectText_Click);
            // 
            // btnOCR
            // 
            this.btnOCR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOCR.Location = new System.Drawing.Point(367, 20);
            this.btnOCR.Name = "btnOCR";
            this.btnOCR.Size = new System.Drawing.Size(75, 23);
            this.btnOCR.TabIndex = 4;
            this.btnOCR.Text = "btnOCR";
            this.btnOCR.UseVisualStyleBackColor = true;
            this.btnOCR.Click += new System.EventHandler(this.btnOCR_Click);
            // 
            // btnTemplates
            // 
            this.btnTemplates.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTemplates.Location = new System.Drawing.Point(458, 20);
            this.btnTemplates.Name = "btnTemplates";
            this.btnTemplates.Size = new System.Drawing.Size(75, 23);
            this.btnTemplates.TabIndex = 5;
            this.btnTemplates.Text = "obtainTemplates";
            this.btnTemplates.UseVisualStyleBackColor = true;
            this.btnTemplates.Click += new System.EventHandler(this.btnTemplates_Click);
            // 
            // btnDiff
            // 
            this.btnDiff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDiff.Location = new System.Drawing.Point(557, 20);
            this.btnDiff.Name = "btnDiff";
            this.btnDiff.Size = new System.Drawing.Size(75, 23);
            this.btnDiff.TabIndex = 6;
            this.btnDiff.Text = "DiffTemplate";
            this.btnDiff.UseVisualStyleBackColor = true;
            this.btnDiff.Click += new System.EventHandler(this.btnDiff_Click);
            // 
            // btnFindText
            // 
            this.btnFindText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFindText.Location = new System.Drawing.Point(650, 20);
            this.btnFindText.Name = "btnFindText";
            this.btnFindText.Size = new System.Drawing.Size(49, 23);
            this.btnFindText.TabIndex = 7;
            this.btnFindText.Text = "findText";
            this.btnFindText.UseVisualStyleBackColor = true;
            this.btnFindText.Click += new System.EventHandler(this.btnFindText_Click);
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "ofdOpenFile";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnBackUpRoi
            // 
            this.btnBackUpRoi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBackUpRoi.Location = new System.Drawing.Point(705, 20);
            this.btnBackUpRoi.Name = "btnBackUpRoi";
            this.btnBackUpRoi.Size = new System.Drawing.Size(70, 23);
            this.btnBackUpRoi.TabIndex = 8;
            this.btnBackUpRoi.Text = "backUpRoi";
            this.btnBackUpRoi.UseVisualStyleBackColor = true;
            this.btnBackUpRoi.Click += new System.EventHandler(this.btnBackUpRoi_Click);
            // 
            // testImageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "testImageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "testImageBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.testImageBox_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibMarker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Emgu.CV.UI.ImageBox ibCamera;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNewCapture;
        private System.Windows.Forms.Button btnMarkers;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private Emgu.CV.UI.ImageBox ibMarker;
        private System.Windows.Forms.Button btnSelectText;
        private System.Windows.Forms.Button btnOCR;
        private System.Windows.Forms.Button btnTemplates;
        private System.Windows.Forms.Button btnDiff;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnFindText;
        private System.Windows.Forms.Button btnBackUpRoi;
    }
}