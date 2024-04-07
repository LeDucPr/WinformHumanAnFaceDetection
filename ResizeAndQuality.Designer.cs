using System.Windows.Forms;

namespace CameraHumanDetection
{
    partial class ResizeAndQuality
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
		private async void InitializeComponent()
		{
			panelMain = new Panel();
			panel1 = new Panel();
			treeViewOutputFolder = new TreeView();
			treeViewInputFolder = new TreeView();
			checkBoxViewHandlingImages = new CheckBox();
			pictureBoxHandlerImage = new PictureBox();
			panelSSD = new Panel();
			panelSize = new Panel();
			textBoxQuality = new TextBox();
			labelQuality = new Label();
			textBoxHeight = new TextBox();
			textBoxWidth = new TextBox();
			checkBoxHeight = new CheckBox();
			checkBoxWidth = new CheckBox();
			checkBoxRatio = new CheckBox();
			button1 = new Button();
			buttonRefreshSaveFolder = new Button();
			buttonRefreshOpenFolder = new Button();
			buttonSaveFolder = new Button();
			textBoxSaveFolderPath = new TextBox();
			textBoxOpenFolderPath = new TextBox();
			buttonOpenFolder = new Button();
			labelRemainingImages = new Label();
			pictureBoxOriginImage = new PictureBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			label1 = new Label();
			tableLayoutPanel3 = new TableLayoutPanel();
			label2 = new Label();
			panelMain.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxHandlerImage).BeginInit();
			panelSSD.SuspendLayout();
			panelSize.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxOriginImage).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			// 
			// panelMain
			// 
			panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panelMain.BackColor = Color.FromArgb(148, 168, 168);
			panelMain.Controls.Add(panel1);
			panelMain.Controls.Add(checkBoxViewHandlingImages);
			panelMain.Controls.Add(pictureBoxHandlerImage);
			panelMain.Controls.Add(panelSSD);
			panelMain.Controls.Add(pictureBoxOriginImage);
			panelMain.Location = new Point(0, 0);
			panelMain.Margin = new Padding(3, 2, 3, 2);
			panelMain.Name = "panelMain";
			panelMain.Size = new Size(997, 412);
			panelMain.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(treeViewOutputFolder);
			panel1.Controls.Add(treeViewInputFolder);
			panel1.Location = new Point(316, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(334, 412);
			panel1.TabIndex = 17;
			// 
			// treeViewOutputFolder
			// 
			treeViewOutputFolder.BackColor = Color.FromArgb(148, 168, 168);
			treeViewOutputFolder.ForeColor = Color.FromArgb(206, 214, 214);
			treeViewOutputFolder.Location = new Point(3, 208);
			treeViewOutputFolder.Name = "treeViewOutputFolder";
			treeViewOutputFolder.Size = new Size(328, 201);
			treeViewOutputFolder.TabIndex = 1;
			// 
			// treeViewInputFolder
			// 
			treeViewInputFolder.BackColor = Color.FromArgb(148, 168, 168);
			treeViewInputFolder.ForeColor = Color.FromArgb(206, 214, 214);
			treeViewInputFolder.Location = new Point(3, 4);
			treeViewInputFolder.Name = "treeViewInputFolder";
			treeViewInputFolder.Size = new Size(328, 198);
			treeViewInputFolder.TabIndex = 0;
			// 
			// checkBoxViewHandlingImages
			// 
			checkBoxViewHandlingImages.AutoSize = true;
			checkBoxViewHandlingImages.ForeColor = Color.FromArgb(206, 214, 214);
			checkBoxViewHandlingImages.Location = new Point(94, 198);
			checkBoxViewHandlingImages.Name = "checkBoxViewHandlingImages";
			checkBoxViewHandlingImages.Size = new Size(138, 19);
			checkBoxViewHandlingImages.TabIndex = 16;
			checkBoxViewHandlingImages.Text = "ViewHandlingImages";
			checkBoxViewHandlingImages.UseVisualStyleBackColor = true;
			// 
			// pictureBoxHandlerImage
			// 
			pictureBoxHandlerImage.BackColor = Color.FromArgb(148, 168, 168);
			pictureBoxHandlerImage.Location = new Point(3, 222);
			pictureBoxHandlerImage.Margin = new Padding(3, 2, 3, 2);
			pictureBoxHandlerImage.Name = "pictureBoxHandlerImage";
			pictureBoxHandlerImage.Size = new Size(307, 188);
			pictureBoxHandlerImage.TabIndex = 15;
			pictureBoxHandlerImage.TabStop = false;
			// 
			// panelSSD
			// 
			panelSSD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			panelSSD.BackColor = Color.FromArgb(148, 168, 168);
			panelSSD.Controls.Add(panelSize);
			panelSSD.Controls.Add(button1);
			panelSSD.Controls.Add(buttonRefreshSaveFolder);
			panelSSD.Controls.Add(buttonRefreshOpenFolder);
			panelSSD.Controls.Add(buttonSaveFolder);
			panelSSD.Controls.Add(textBoxSaveFolderPath);
			panelSSD.Controls.Add(textBoxOpenFolderPath);
			panelSSD.Controls.Add(buttonOpenFolder);
			panelSSD.Controls.Add(labelRemainingImages);
			panelSSD.Location = new Point(656, 0);
			panelSSD.Margin = new Padding(3, 2, 3, 2);
			panelSSD.Name = "panelSSD";
			panelSSD.Size = new Size(341, 412);
			panelSSD.TabIndex = 14;
			// 
			// panelSize
			// 
			panelSize.BorderStyle = BorderStyle.FixedSingle;
			panelSize.Controls.Add(textBoxQuality);
			panelSize.Controls.Add(labelQuality);
			panelSize.Controls.Add(textBoxHeight);
			panelSize.Controls.Add(textBoxWidth);
			panelSize.Controls.Add(checkBoxHeight);
			panelSize.Controls.Add(checkBoxWidth);
			panelSize.Controls.Add(checkBoxRatio);
			panelSize.Location = new Point(33, 62);
			panelSize.Name = "panelSize";
			panelSize.Size = new Size(297, 83);
			panelSize.TabIndex = 22;
			// 
			// textBoxQuality
			// 
			textBoxQuality.BackColor = Color.FromArgb(148, 168, 168);
			textBoxQuality.ForeColor = Color.FromArgb(206, 214, 214);
			textBoxQuality.Location = new Point(138, 50);
			textBoxQuality.Name = "textBoxQuality";
			textBoxQuality.Size = new Size(157, 23);
			textBoxQuality.TabIndex = 27;
			textBoxQuality.Text = "80";
			textBoxQuality.KeyPress += textBoxQuality_KeyPress;
			// 
			// labelQuality
			// 
			labelQuality.AutoSize = true;
			labelQuality.ForeColor = Color.FromArgb(206, 214, 214);
			labelQuality.Location = new Point(66, 53);
			labelQuality.Name = "labelQuality";
			labelQuality.Size = new Size(66, 15);
			labelQuality.TabIndex = 26;
			labelQuality.Text = "Quality(%):";
			// 
			// textBoxHeight
			// 
			textBoxHeight.BackColor = Color.FromArgb(148, 168, 168);
			textBoxHeight.ForeColor = Color.FromArgb(206, 214, 214);
			textBoxHeight.Location = new Point(138, 24);
			textBoxHeight.Name = "textBoxHeight";
			textBoxHeight.Size = new Size(157, 23);
			textBoxHeight.TabIndex = 25;
			textBoxHeight.KeyPress += textBoxWidthHeight_KeyPress;
			// 
			// textBoxWidth
			// 
			textBoxWidth.BackColor = Color.FromArgb(148, 168, 168);
			textBoxWidth.ForeColor = Color.FromArgb(206, 214, 214);
			textBoxWidth.Location = new Point(138, -1);
			textBoxWidth.Name = "textBoxWidth";
			textBoxWidth.Size = new Size(157, 23);
			textBoxWidth.TabIndex = 24;
			textBoxWidth.KeyPress += textBoxWidthHeight_KeyPress;
			// 
			// checkBoxHeight
			// 
			checkBoxHeight.AutoSize = true;
			checkBoxHeight.ForeColor = Color.FromArgb(206, 214, 214);
			checkBoxHeight.Location = new Point(71, 28);
			checkBoxHeight.Name = "checkBoxHeight";
			checkBoxHeight.Size = new Size(65, 19);
			checkBoxHeight.TabIndex = 23;
			checkBoxHeight.Text = "Height:";
			checkBoxHeight.UseVisualStyleBackColor = true;
			checkBoxHeight.CheckedChanged += checkBoxHeight_CheckedChanged;
			// 
			// checkBoxWidth
			// 
			checkBoxWidth.AutoSize = true;
			checkBoxWidth.Checked = true;
			checkBoxWidth.CheckState = CheckState.Checked;
			checkBoxWidth.ForeColor = Color.FromArgb(206, 214, 214);
			checkBoxWidth.Location = new Point(71, 3);
			checkBoxWidth.Name = "checkBoxWidth";
			checkBoxWidth.Size = new Size(61, 19);
			checkBoxWidth.TabIndex = 22;
			checkBoxWidth.Text = "Width:";
			checkBoxWidth.UseVisualStyleBackColor = true;
			checkBoxWidth.CheckedChanged += checkBoxWidth_CheckedChanged;
			// 
			// checkBoxRatio
			// 
			checkBoxRatio.AutoSize = true;
			checkBoxRatio.Checked = true;
			checkBoxRatio.CheckState = CheckState.Checked;
			checkBoxRatio.ForeColor = Color.FromArgb(206, 214, 214);
			checkBoxRatio.Location = new Point(3, 3);
			checkBoxRatio.Name = "checkBoxRatio";
			checkBoxRatio.Size = new Size(53, 19);
			checkBoxRatio.TabIndex = 21;
			checkBoxRatio.Text = "Ratio";
			checkBoxRatio.UseVisualStyleBackColor = true;
			checkBoxRatio.CheckedChanged += checkBoxRatio_CheckedChanged;
			// 
			// button1
			// 
			button1.Location = new Point(248, 155);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 20;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// buttonRefreshSaveFolder
			// 
			buttonRefreshSaveFolder.BackColor = Color.FromArgb(136, 153, 153);
			buttonRefreshSaveFolder.Font = new Font("Segoe UI", 9F);
			buttonRefreshSaveFolder.ForeColor = Color.FromArgb(206, 214, 214);
			buttonRefreshSaveFolder.ImageAlign = ContentAlignment.BottomLeft;
			buttonRefreshSaveFolder.Location = new Point(6, 33);
			buttonRefreshSaveFolder.Name = "buttonRefreshSaveFolder";
			buttonRefreshSaveFolder.Size = new Size(23, 23);
			buttonRefreshSaveFolder.TabIndex = 19;
			buttonRefreshSaveFolder.Text = "⟳";
			buttonRefreshSaveFolder.UseVisualStyleBackColor = false;
			buttonRefreshSaveFolder.Click += buttonRefreshSaveFolder_Click;
			// 
			// buttonRefreshOpenFolder
			// 
			buttonRefreshOpenFolder.BackColor = Color.FromArgb(136, 153, 153);
			buttonRefreshOpenFolder.Font = new Font("Segoe UI", 9F);
			buttonRefreshOpenFolder.ForeColor = Color.FromArgb(206, 214, 214);
			buttonRefreshOpenFolder.ImageAlign = ContentAlignment.BottomLeft;
			buttonRefreshOpenFolder.Location = new Point(6, 4);
			buttonRefreshOpenFolder.Name = "buttonRefreshOpenFolder";
			buttonRefreshOpenFolder.Size = new Size(23, 23);
			buttonRefreshOpenFolder.TabIndex = 18;
			buttonRefreshOpenFolder.Text = "⟳";
			buttonRefreshOpenFolder.UseVisualStyleBackColor = false;
			buttonRefreshOpenFolder.Click += buttonRefreshOpenFolder_Click;
			// 
			// buttonSaveFolder
			// 
			buttonSaveFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonSaveFolder.BackColor = Color.FromArgb(136, 153, 153);
			buttonSaveFolder.ForeColor = Color.FromArgb(206, 214, 214);
			buttonSaveFolder.Location = new Point(249, 33);
			buttonSaveFolder.Margin = new Padding(3, 2, 3, 2);
			buttonSaveFolder.Name = "buttonSaveFolder";
			buttonSaveFolder.Size = new Size(82, 24);
			buttonSaveFolder.TabIndex = 17;
			buttonSaveFolder.Text = "SaveFolder";
			buttonSaveFolder.UseVisualStyleBackColor = false;
			// 
			// textBoxSaveFolderPath
			// 
			textBoxSaveFolderPath.BackColor = Color.FromArgb(136, 153, 153);
			textBoxSaveFolderPath.ForeColor = Color.FromArgb(206, 214, 214);
			textBoxSaveFolderPath.Location = new Point(34, 33);
			textBoxSaveFolderPath.Name = "textBoxSaveFolderPath";
			textBoxSaveFolderPath.ReadOnly = true;
			textBoxSaveFolderPath.Size = new Size(210, 23);
			textBoxSaveFolderPath.TabIndex = 16;
			// 
			// textBoxOpenFolderPath
			// 
			textBoxOpenFolderPath.BackColor = Color.FromArgb(136, 153, 153);
			textBoxOpenFolderPath.ForeColor = Color.FromArgb(206, 214, 214);
			textBoxOpenFolderPath.Location = new Point(34, 4);
			textBoxOpenFolderPath.Name = "textBoxOpenFolderPath";
			textBoxOpenFolderPath.ReadOnly = true;
			textBoxOpenFolderPath.Size = new Size(210, 23);
			textBoxOpenFolderPath.TabIndex = 15;
			// 
			// buttonOpenFolder
			// 
			buttonOpenFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonOpenFolder.BackColor = Color.FromArgb(136, 153, 153);
			buttonOpenFolder.ForeColor = Color.FromArgb(206, 214, 214);
			buttonOpenFolder.Location = new Point(249, 3);
			buttonOpenFolder.Margin = new Padding(3, 2, 3, 2);
			buttonOpenFolder.Name = "buttonOpenFolder";
			buttonOpenFolder.Size = new Size(82, 24);
			buttonOpenFolder.TabIndex = 2;
			buttonOpenFolder.Text = "OpenFolder";
			buttonOpenFolder.UseVisualStyleBackColor = false;
			buttonOpenFolder.Click += buttonOpenFolder_Click;
			// 
			// labelRemainingImages
			// 
			labelRemainingImages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelRemainingImages.AutoSize = true;
			labelRemainingImages.BackColor = Color.FromArgb(136, 153, 153);
			labelRemainingImages.Font = new Font("Segoe UI", 9F);
			labelRemainingImages.ForeColor = Color.FromArgb(206, 214, 214);
			labelRemainingImages.Location = new Point(33, 159);
			labelRemainingImages.Name = "labelRemainingImages";
			labelRemainingImages.Size = new Size(111, 15);
			labelRemainingImages.TabIndex = 10;
			labelRemainingImages.Text = "RemainingImages:  ";
			// 
			// pictureBoxOriginImage
			// 
			pictureBoxOriginImage.BackColor = Color.FromArgb(148, 168, 168);
			pictureBoxOriginImage.Location = new Point(3, 4);
			pictureBoxOriginImage.Margin = new Padding(3, 2, 3, 2);
			pictureBoxOriginImage.Name = "pictureBoxOriginImage";
			pictureBoxOriginImage.Size = new Size(307, 188);
			pictureBoxOriginImage.TabIndex = 0;
			pictureBoxOriginImage.TabStop = false;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.93507F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.0649338F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Location = new Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel2.Size = new Size(200, 100);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 11F);
			label1.Location = new Point(32, 0);
			label1.Name = "label1";
			label1.Size = new Size(32, 20);
			label1.TabIndex = 0;
			label1.Text = "FPS";
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			tableLayoutPanel3.ColumnCount = 3;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.93507F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.0649338F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
			tableLayoutPanel3.Controls.Add(label2, 0, 0);
			tableLayoutPanel3.Location = new Point(0, 0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel3.Size = new Size(200, 100);
			tableLayoutPanel3.TabIndex = 0;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 11F);
			label2.Location = new Point(32, 0);
			label2.Name = "label2";
			label2.Size = new Size(32, 20);
			label2.TabIndex = 0;
			label2.Text = "FPS";
			// 
			// ResizeAndQuality
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaptionText;
			ClientSize = new Size(998, 412);
			Controls.Add(panelMain);
			FormBorderStyle = FormBorderStyle.None;
			Margin = new Padding(3, 2, 3, 2);
			Name = "ResizeAndQuality";
			Text = "Camera";
			panelMain.ResumeLayout(false);
			panelMain.PerformLayout();
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBoxHandlerImage).EndInit();
			panelSSD.ResumeLayout(false);
			panelSSD.PerformLayout();
			panelSize.ResumeLayout(false);
			panelSize.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxOriginImage).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			ResumeLayout(false);
		}
		#endregion

		private Panel panelMain;
        private Button buttonOpenFolder;
        private PictureBox pictureBoxOriginImage;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label2;
        private Label labelRemainingImages;
        private Panel panelSSD;
		private TextBox textBoxOpenFolderPath;
		private Button buttonSaveFolder;
		private TextBox textBoxSaveFolderPath;
		private CheckBox checkBoxViewHandlingImages;
		private PictureBox pictureBoxHandlerImage;
		private Panel panel1;
		private TreeView treeViewOutputFolder;
		private TreeView treeViewInputFolder;
		private Button buttonRefreshOpenFolder;
		private Button buttonRefreshSaveFolder;
		private Button button1;
		private Panel panelSize;
		private CheckBox checkBoxRatio;
		private TextBox textBoxWidth;
		private CheckBox checkBoxHeight;
		private CheckBox checkBoxWidth;
		private TextBox textBoxHeight;
		private TextBox textBoxQuality;
		private Label labelQuality;
	}
}