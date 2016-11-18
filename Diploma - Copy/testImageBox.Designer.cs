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
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectText = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnNewCapture, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMarkers, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ibMarker, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSelectText, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 64);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btnNewCapture
            // 
            this.btnNewCapture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNewCapture.Location = new System.Drawing.Point(40, 20);
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
            this.btnMarkers.Location = new System.Drawing.Point(195, 20);
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
            this.ibMarker.Location = new System.Drawing.Point(315, 5);
            this.ibMarker.Margin = new System.Windows.Forms.Padding(5);
            this.ibMarker.Name = "ibMarker";
            this.ibMarker.Size = new System.Drawing.Size(145, 54);
            this.ibMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibMarker.TabIndex = 2;
            this.ibMarker.TabStop = false;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "ofdOpenFile";
            // 
            // btnSelectText
            // 
            this.btnSelectText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectText.Location = new System.Drawing.Point(505, 20);
            this.btnSelectText.Name = "btnSelectText";
            this.btnSelectText.Size = new System.Drawing.Size(75, 23);
            this.btnSelectText.TabIndex = 3;
            this.btnSelectText.Text = "select text";
            this.btnSelectText.UseVisualStyleBackColor = true;
            this.btnSelectText.Click += new System.EventHandler(this.btnSelectText_Click);
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
    }
}