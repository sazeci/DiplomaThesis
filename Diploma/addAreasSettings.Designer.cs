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
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.tb4 = new System.Windows.Forms.TextBox();
            this.tb5 = new System.Windows.Forms.TextBox();
            this.tb6 = new System.Windows.Forms.TextBox();
            this.tb7 = new System.Windows.Forms.TextBox();
            this.tb8 = new System.Windows.Forms.TextBox();
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            this.tableLayoutPanel2.Controls.Add(this.tb1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tb2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tb3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tb4, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tb5, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.tb6, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.tb7, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.tb8, 1, 7);
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
            this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
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
            this.btnSave2.Click += new System.EventHandler(this.btnSave2_Click);
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
            this.btnSave3.Click += new System.EventHandler(this.btnSave3_Click);
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
            this.btnSave4.Click += new System.EventHandler(this.btnSave4_Click);
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
            this.btnSave5.Click += new System.EventHandler(this.btnSave5_Click);
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
            this.btnSave6.Click += new System.EventHandler(this.btnSave6_Click);
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
            this.btnSave7.Click += new System.EventHandler(this.btnSave7_Click);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // tb1
            // 
            this.tb1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb1.Location = new System.Drawing.Point(146, 7);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(100, 20);
            this.tb1.TabIndex = 21;
            this.tb1.Visible = false;
            // 
            // tb2
            // 
            this.tb2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb2.Location = new System.Drawing.Point(146, 42);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(100, 20);
            this.tb2.TabIndex = 22;
            this.tb2.Visible = false;
            // 
            // tb3
            // 
            this.tb3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb3.Location = new System.Drawing.Point(146, 77);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(100, 20);
            this.tb3.TabIndex = 23;
            this.tb3.Visible = false;
            // 
            // tb4
            // 
            this.tb4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb4.Location = new System.Drawing.Point(146, 112);
            this.tb4.Name = "tb4";
            this.tb4.Size = new System.Drawing.Size(100, 20);
            this.tb4.TabIndex = 24;
            this.tb4.Visible = false;
            // 
            // tb5
            // 
            this.tb5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb5.Location = new System.Drawing.Point(146, 147);
            this.tb5.Name = "tb5";
            this.tb5.Size = new System.Drawing.Size(100, 20);
            this.tb5.TabIndex = 25;
            this.tb5.Visible = false;
            // 
            // tb6
            // 
            this.tb6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb6.Location = new System.Drawing.Point(146, 182);
            this.tb6.Name = "tb6";
            this.tb6.Size = new System.Drawing.Size(100, 20);
            this.tb6.TabIndex = 26;
            this.tb6.Visible = false;
            // 
            // tb7
            // 
            this.tb7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb7.Location = new System.Drawing.Point(146, 217);
            this.tb7.Name = "tb7";
            this.tb7.Size = new System.Drawing.Size(100, 20);
            this.tb7.TabIndex = 27;
            this.tb7.Visible = false;
            // 
            // tb8
            // 
            this.tb8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb8.Location = new System.Drawing.Point(146, 252);
            this.tb8.Name = "tb8";
            this.tb8.Size = new System.Drawing.Size(100, 20);
            this.tb8.TabIndex = 28;
            this.tb8.Visible = false;
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
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.TextBox tb4;
        private System.Windows.Forms.TextBox tb5;
        private System.Windows.Forms.TextBox tb6;
        private System.Windows.Forms.TextBox tb7;
        private System.Windows.Forms.TextBox tb8;
    }
}