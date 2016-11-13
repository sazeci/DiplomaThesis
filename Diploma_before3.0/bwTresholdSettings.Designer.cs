namespace Diploma
{
    partial class bwTresholdSettings
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbGlobal = new System.Windows.Forms.RadioButton();
            this.rdbAdaptive = new System.Windows.Forms.RadioButton();
            this.tbGlobal = new System.Windows.Forms.TrackBar();
            this.tbAdaptive = new System.Windows.Forms.TrackBar();
            this.lblText = new System.Windows.Forms.Label();
            this.ibCamera = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGlobal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAdaptive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(958, 505);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnBack, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNext, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 438);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(952, 64);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBack.Location = new System.Drawing.Point(200, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.Location = new System.Drawing.Point(676, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTitle.Location = new System.Drawing.Point(376, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(205, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Setting - BW tresholding";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.ibCamera, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(952, 359);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.rdbGlobal, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.rdbAdaptive, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.tbGlobal, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tbAdaptive, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.lblText, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(279, 353);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // rdbGlobal
            // 
            this.rdbGlobal.AutoSize = true;
            this.rdbGlobal.Checked = true;
            this.rdbGlobal.Location = new System.Drawing.Point(5, 198);
            this.rdbGlobal.Margin = new System.Windows.Forms.Padding(5);
            this.rdbGlobal.Name = "rdbGlobal";
            this.rdbGlobal.Size = new System.Drawing.Size(109, 17);
            this.rdbGlobal.TabIndex = 0;
            this.rdbGlobal.TabStop = true;
            this.rdbGlobal.Text = "Global tresholding";
            this.rdbGlobal.UseVisualStyleBackColor = true;
            this.rdbGlobal.CheckedChanged += new System.EventHandler(this.rdbGlobal_CheckedChanged);
            this.rdbGlobal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rdbGlobal_MouseDown);
            // 
            // rdbAdaptive
            // 
            this.rdbAdaptive.AutoSize = true;
            this.rdbAdaptive.Location = new System.Drawing.Point(5, 278);
            this.rdbAdaptive.Margin = new System.Windows.Forms.Padding(5);
            this.rdbAdaptive.Name = "rdbAdaptive";
            this.rdbAdaptive.Size = new System.Drawing.Size(121, 17);
            this.rdbAdaptive.TabIndex = 1;
            this.rdbAdaptive.Text = "Adaptive tresholding";
            this.rdbAdaptive.UseVisualStyleBackColor = true;
            this.rdbAdaptive.CheckedChanged += new System.EventHandler(this.rdbAdaptive_CheckedChanged);
            this.rdbAdaptive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rdbAdaptive_MouseDown);
            // 
            // tbGlobal
            // 
            this.tbGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbGlobal.Location = new System.Drawing.Point(5, 238);
            this.tbGlobal.Margin = new System.Windows.Forms.Padding(5);
            this.tbGlobal.Maximum = 100;
            this.tbGlobal.Name = "tbGlobal";
            this.tbGlobal.Size = new System.Drawing.Size(269, 30);
            this.tbGlobal.TabIndex = 2;
            // 
            // tbAdaptive
            // 
            this.tbAdaptive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAdaptive.Location = new System.Drawing.Point(5, 318);
            this.tbAdaptive.Margin = new System.Windows.Forms.Padding(5);
            this.tbAdaptive.Maximum = 100;
            this.tbAdaptive.Name = "tbAdaptive";
            this.tbAdaptive.Size = new System.Drawing.Size(269, 30);
            this.tbAdaptive.TabIndex = 3;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(5, 5);
            this.lblText.Margin = new System.Windows.Forms.Padding(5);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(269, 39);
            this.lblText.TabIndex = 4;
            this.lblText.Text = "Please select thresholding method and its value, so that the patient monitor scre" +
    "en is clearly separated from its environment by uninterrupted border";
            // 
            // ibCamera
            // 
            this.ibCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibCamera.Location = new System.Drawing.Point(288, 3);
            this.ibCamera.Name = "ibCamera";
            this.ibCamera.Size = new System.Drawing.Size(661, 353);
            this.ibCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibCamera.TabIndex = 2;
            this.ibCamera.TabStop = false;
            // 
            // bwTresholdSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 505);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "bwTresholdSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "bwTresholdSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.bwTresholdSettings_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGlobal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAdaptive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RadioButton rdbGlobal;
        private System.Windows.Forms.RadioButton rdbAdaptive;
        private System.Windows.Forms.TrackBar tbGlobal;
        private System.Windows.Forms.TrackBar tbAdaptive;
        private System.Windows.Forms.Label lblText;
        private Emgu.CV.UI.ImageBox ibCamera;
    }
}