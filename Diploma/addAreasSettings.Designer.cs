namespace Diploma
{
    partial class addAreasSettings
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ibCamera = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ib1 = new Emgu.CV.UI.ImageBox();
            this.ib2 = new Emgu.CV.UI.ImageBox();
            this.ib3 = new Emgu.CV.UI.ImageBox();
            this.ib4 = new Emgu.CV.UI.ImageBox();
            this.ib5 = new Emgu.CV.UI.ImageBox();
            this.ib6 = new Emgu.CV.UI.ImageBox();
            this.ib7 = new Emgu.CV.UI.ImageBox();
            this.ib8 = new Emgu.CV.UI.ImageBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl8 = new System.Windows.Forms.Label();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.btnSave1 = new System.Windows.Forms.Button();
            this.btnSave2 = new System.Windows.Forms.Button();
            this.btnSave3 = new System.Windows.Forms.Button();
            this.btnSave4 = new System.Windows.Forms.Button();
            this.btnSave5 = new System.Windows.Forms.Button();
            this.btnSave6 = new System.Windows.Forms.Button();
            this.btnSave7 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ib1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib8)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnNext, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ibCamera, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBack.Location = new System.Drawing.Point(158, 414);
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
            this.btnNext.Location = new System.Drawing.Point(550, 414);
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
            this.tableLayoutPanel1.SetColumnSpan(this.lblTitle, 2);
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTitle.Location = new System.Drawing.Point(253, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(277, 20);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Settings - select areas of interest";
            // 
            // ibCamera
            // 
            this.ibCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibCamera.Enabled = false;
            this.ibCamera.Location = new System.Drawing.Point(402, 80);
            this.ibCamera.Margin = new System.Windows.Forms.Padding(10);
            this.ibCamera.Name = "ibCamera";
            this.ibCamera.Size = new System.Drawing.Size(372, 301);
            this.ibCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibCamera.TabIndex = 2;
            this.ibCamera.TabStop = false;
            this.ibCamera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ibCamera_MouseDown_1);
            this.ibCamera.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ibCamera_MouseMove_1);
            this.ibCamera.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ibCamera_MouseUp);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.48276F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.48276F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.51724F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.51724F));
            this.tableLayoutPanel2.Controls.Add(this.ib1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ib2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ib3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.ib4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.ib5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.ib6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.ib7, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.ib8, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lbl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl4, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lbl5, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbl6, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lbl7, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lbl8, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.btnAddArea, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.btnSave1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave2, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSave3, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSave4, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnSave5, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnSave6, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnSave7, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblInstruction, 1, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 75);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(382, 311);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // ib1
            // 
            this.ib1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib1.Location = new System.Drawing.Point(3, 3);
            this.ib1.Name = "ib1";
            this.ib1.Size = new System.Drawing.Size(125, 29);
            this.ib1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib1.TabIndex = 2;
            this.ib1.TabStop = false;
            // 
            // ib2
            // 
            this.ib2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib2.Location = new System.Drawing.Point(3, 38);
            this.ib2.Name = "ib2";
            this.ib2.Size = new System.Drawing.Size(125, 29);
            this.ib2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib2.TabIndex = 2;
            this.ib2.TabStop = false;
            // 
            // ib3
            // 
            this.ib3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib3.Location = new System.Drawing.Point(3, 73);
            this.ib3.Name = "ib3";
            this.ib3.Size = new System.Drawing.Size(125, 29);
            this.ib3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib3.TabIndex = 2;
            this.ib3.TabStop = false;
            // 
            // ib4
            // 
            this.ib4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib4.Location = new System.Drawing.Point(3, 108);
            this.ib4.Name = "ib4";
            this.ib4.Size = new System.Drawing.Size(125, 29);
            this.ib4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib4.TabIndex = 2;
            this.ib4.TabStop = false;
            // 
            // ib5
            // 
            this.ib5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib5.Location = new System.Drawing.Point(3, 143);
            this.ib5.Name = "ib5";
            this.ib5.Size = new System.Drawing.Size(125, 29);
            this.ib5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib5.TabIndex = 2;
            this.ib5.TabStop = false;
            // 
            // ib6
            // 
            this.ib6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib6.Location = new System.Drawing.Point(3, 178);
            this.ib6.Name = "ib6";
            this.ib6.Size = new System.Drawing.Size(125, 29);
            this.ib6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib6.TabIndex = 2;
            this.ib6.TabStop = false;
            // 
            // ib7
            // 
            this.ib7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib7.Location = new System.Drawing.Point(3, 213);
            this.ib7.Name = "ib7";
            this.ib7.Size = new System.Drawing.Size(125, 29);
            this.ib7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib7.TabIndex = 2;
            this.ib7.TabStop = false;
            // 
            // ib8
            // 
            this.ib8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ib8.Location = new System.Drawing.Point(3, 248);
            this.ib8.Name = "ib8";
            this.ib8.Size = new System.Drawing.Size(125, 29);
            this.ib8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib8.TabIndex = 2;
            this.ib8.TabStop = false;
            // 
            // lbl1
            // 
            this.lbl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(196, 11);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(0, 13);
            this.lbl1.TabIndex = 3;
            // 
            // lbl2
            // 
            this.lbl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(196, 46);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(0, 13);
            this.lbl2.TabIndex = 4;
            // 
            // lbl3
            // 
            this.lbl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(196, 81);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(0, 13);
            this.lbl3.TabIndex = 5;
            // 
            // lbl4
            // 
            this.lbl4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(196, 116);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(0, 13);
            this.lbl4.TabIndex = 6;
            // 
            // lbl5
            // 
            this.lbl5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(196, 151);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(0, 13);
            this.lbl5.TabIndex = 7;
            // 
            // lbl6
            // 
            this.lbl6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(196, 186);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(0, 13);
            this.lbl6.TabIndex = 8;
            // 
            // lbl7
            // 
            this.lbl7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl7.AutoSize = true;
            this.lbl7.Location = new System.Drawing.Point(196, 221);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(0, 13);
            this.lbl7.TabIndex = 9;
            // 
            // lbl8
            // 
            this.lbl8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl8.AutoSize = true;
            this.lbl8.Location = new System.Drawing.Point(196, 256);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(0, 13);
            this.lbl8.TabIndex = 10;
            // 
            // btnAddArea
            // 
            this.btnAddArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddArea.Location = new System.Drawing.Point(28, 284);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(75, 23);
            this.btnAddArea.TabIndex = 11;
            this.btnAddArea.Text = "Add area";
            this.btnAddArea.UseVisualStyleBackColor = true;
            this.btnAddArea.Click += new System.EventHandler(this.btnAddArea_Click);
            // 
            // btnSave1
            // 
            this.btnSave1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave1.Location = new System.Drawing.Point(265, 6);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.Size = new System.Drawing.Size(53, 23);
            this.btnSave1.TabIndex = 12;
            this.btnSave1.Text = "save";
            this.btnSave1.UseVisualStyleBackColor = true;
            this.btnSave1.Visible = false;
            // 
            // btnSave2
            // 
            this.btnSave2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave2.Location = new System.Drawing.Point(265, 41);
            this.btnSave2.Name = "btnSave2";
            this.btnSave2.Size = new System.Drawing.Size(53, 23);
            this.btnSave2.TabIndex = 13;
            this.btnSave2.Text = "save";
            this.btnSave2.UseVisualStyleBackColor = true;
            this.btnSave2.Visible = false;
            // 
            // btnSave3
            // 
            this.btnSave3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave3.Location = new System.Drawing.Point(265, 76);
            this.btnSave3.Name = "btnSave3";
            this.btnSave3.Size = new System.Drawing.Size(53, 23);
            this.btnSave3.TabIndex = 14;
            this.btnSave3.Text = "save";
            this.btnSave3.UseVisualStyleBackColor = true;
            this.btnSave3.Visible = false;
            // 
            // btnSave4
            // 
            this.btnSave4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave4.Location = new System.Drawing.Point(265, 111);
            this.btnSave4.Name = "btnSave4";
            this.btnSave4.Size = new System.Drawing.Size(53, 23);
            this.btnSave4.TabIndex = 15;
            this.btnSave4.Text = "save";
            this.btnSave4.UseVisualStyleBackColor = true;
            this.btnSave4.Visible = false;
            // 
            // btnSave5
            // 
            this.btnSave5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave5.Location = new System.Drawing.Point(265, 146);
            this.btnSave5.Name = "btnSave5";
            this.btnSave5.Size = new System.Drawing.Size(53, 23);
            this.btnSave5.TabIndex = 16;
            this.btnSave5.Text = "save";
            this.btnSave5.UseVisualStyleBackColor = true;
            this.btnSave5.Visible = false;
            // 
            // btnSave6
            // 
            this.btnSave6.Location = new System.Drawing.Point(265, 178);
            this.btnSave6.Name = "btnSave6";
            this.btnSave6.Size = new System.Drawing.Size(53, 23);
            this.btnSave6.TabIndex = 17;
            this.btnSave6.Text = "save";
            this.btnSave6.UseVisualStyleBackColor = true;
            this.btnSave6.Visible = false;
            // 
            // btnSave7
            // 
            this.btnSave7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave7.Location = new System.Drawing.Point(265, 216);
            this.btnSave7.Name = "btnSave7";
            this.btnSave7.Size = new System.Drawing.Size(53, 23);
            this.btnSave7.TabIndex = 18;
            this.btnSave7.Text = "save";
            this.btnSave7.UseVisualStyleBackColor = true;
            this.btnSave7.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(265, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(53, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            // 
            // lblInstruction
            // 
            this.lblInstruction.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInstruction.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblInstruction, 3);
            this.lblInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInstruction.Location = new System.Drawing.Point(134, 289);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(123, 13);
            this.lblInstruction.TabIndex = 20;
            this.lblInstruction.Text = "1) Click on Add area";
            // 
            // addAreasSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "addAreasSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings - select areas of interest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.addAreasSettings_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCamera)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ib1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTitle;
        private Emgu.CV.UI.ImageBox ibCamera;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Emgu.CV.UI.ImageBox ib1;
        private Emgu.CV.UI.ImageBox ib2;
        private Emgu.CV.UI.ImageBox ib3;
        private Emgu.CV.UI.ImageBox ib4;
        private Emgu.CV.UI.ImageBox ib5;
        private Emgu.CV.UI.ImageBox ib6;
        private Emgu.CV.UI.ImageBox ib7;
        private Emgu.CV.UI.ImageBox ib8;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Button btnAddArea;
        private System.Windows.Forms.Button btnSave1;
        private System.Windows.Forms.Button btnSave2;
        private System.Windows.Forms.Button btnSave3;
        private System.Windows.Forms.Button btnSave4;
        private System.Windows.Forms.Button btnSave5;
        private System.Windows.Forms.Button btnSave6;
        private System.Windows.Forms.Button btnSave7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblInstruction;
    }
}