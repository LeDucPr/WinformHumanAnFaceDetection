using System.Windows.Forms;

namespace CameraHumanDetection
{
    partial class Camera
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
			panelMain = new Panel();
			panelSSD = new Panel();
			comboBoxDevices = new ComboBox();
			comboBoxCamera = new ComboBox();
			buttonStart = new Button();
			buttonSave = new Button();
			tableLayoutPanel1 = new TableLayoutPanel();
			labelFPS = new Label();
			numericUpDownFPS = new NumericUpDown();
			labelFPSIndex = new Label();
			labelDelayFrame = new Label();
			tableLayoutPanel4 = new TableLayoutPanel();
			numericUpDownARC = new NumericUpDown();
			labelARCIndex = new Label();
			labelARC = new Label();
			labelAmountFrame = new Label();
			pictureBoxCameraPreview = new PictureBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			label1 = new Label();
			tableLayoutPanel3 = new TableLayoutPanel();
			label2 = new Label();
			panelMain.SuspendLayout();
			panelSSD.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownFPS).BeginInit();
			tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownARC).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxCameraPreview).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			// 
			// panelMain
			// 
			panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panelMain.BackColor = Color.FromArgb(148, 168, 168);
			panelMain.Controls.Add(panelSSD);
			panelMain.Controls.Add(pictureBoxCameraPreview);
			panelMain.Location = new Point(0, 0);
			panelMain.Name = "panelMain";
			panelMain.Size = new Size(1120, 549);
			panelMain.TabIndex = 0;
			// 
			// panelSSD
			// 
			panelSSD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			panelSSD.BackColor = Color.FromArgb(148, 168, 168);
			panelSSD.Controls.Add(comboBoxDevices);
			panelSSD.Controls.Add(comboBoxCamera);
			panelSSD.Controls.Add(buttonStart);
			panelSSD.Controls.Add(buttonSave);
			panelSSD.Controls.Add(tableLayoutPanel1);
			panelSSD.Controls.Add(labelDelayFrame);
			panelSSD.Controls.Add(tableLayoutPanel4);
			panelSSD.Controls.Add(labelAmountFrame);
			panelSSD.Location = new Point(947, 0);
			panelSSD.Name = "panelSSD";
			panelSSD.Size = new Size(173, 549);
			panelSSD.TabIndex = 14;
			// 
			// comboBoxDevices
			// 
			comboBoxDevices.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			comboBoxDevices.BackColor = Color.FromArgb(136, 153, 153);
			comboBoxDevices.Font = new Font("Segoe UI", 7.2F);
			comboBoxDevices.ForeColor = Color.FromArgb(206, 214, 214);
			comboBoxDevices.FormattingEnabled = true;
			comboBoxDevices.Location = new Point(6, 224);
			comboBoxDevices.Name = "comboBoxDevices";
			comboBoxDevices.Size = new Size(164, 23);
			comboBoxDevices.TabIndex = 14;
			comboBoxDevices.Text = "Cpu";
			comboBoxDevices.SelectedIndexChanged += comboBoxDevices_SelectedIndexChanged;
			// 
			// comboBoxCamera
			// 
			comboBoxCamera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			comboBoxCamera.BackColor = Color.FromArgb(136, 153, 153);
			comboBoxCamera.ForeColor = Color.FromArgb(206, 214, 214);
			comboBoxCamera.FormattingEnabled = true;
			comboBoxCamera.Location = new Point(6, 3);
			comboBoxCamera.Name = "comboBoxCamera";
			comboBoxCamera.Size = new Size(164, 28);
			comboBoxCamera.TabIndex = 1;
			// 
			// buttonStart
			// 
			buttonStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonStart.BackColor = Color.FromArgb(136, 153, 153);
			buttonStart.ForeColor = Color.FromArgb(206, 214, 214);
			buttonStart.Location = new Point(7, 37);
			buttonStart.Name = "buttonStart";
			buttonStart.Size = new Size(94, 29);
			buttonStart.TabIndex = 2;
			buttonStart.Text = "Start";
			buttonStart.UseVisualStyleBackColor = false;
			buttonStart.Click += buttonStart_Click;
			// 
			// buttonSave
			// 
			buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonSave.BackColor = Color.FromArgb(136, 153, 153);
			buttonSave.ForeColor = Color.FromArgb(206, 214, 214);
			buttonSave.Location = new Point(9, 343);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(61, 29);
			buttonSave.TabIndex = 12;
			buttonSave.Text = "Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Visible = false;
			buttonSave.Click += buttonSave_Click;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.63415F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.3658524F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49F));
			tableLayoutPanel1.Controls.Add(labelFPS, 0, 0);
			tableLayoutPanel1.Controls.Add(numericUpDownFPS, 1, 0);
			tableLayoutPanel1.Controls.Add(labelFPSIndex, 2, 0);
			tableLayoutPanel1.Location = new Point(9, 119);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.Size = new Size(133, 31);
			tableLayoutPanel1.TabIndex = 6;
			// 
			// labelFPS
			// 
			labelFPS.AutoSize = true;
			labelFPS.BackColor = Color.FromArgb(136, 153, 153);
			labelFPS.Font = new Font("Segoe UI", 11F);
			labelFPS.ForeColor = Color.FromArgb(206, 214, 214);
			labelFPS.Location = new Point(3, 0);
			labelFPS.Name = "labelFPS";
			labelFPS.Size = new Size(42, 25);
			labelFPS.TabIndex = 0;
			labelFPS.Text = "FPS";
			// 
			// numericUpDownFPS
			// 
			numericUpDownFPS.Anchor = AnchorStyles.None;
			numericUpDownFPS.BackColor = Color.FromArgb(50, 54, 54);
			numericUpDownFPS.Location = new Point(57, 3);
			numericUpDownFPS.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
			numericUpDownFPS.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
			numericUpDownFPS.Name = "numericUpDownFPS";
			numericUpDownFPS.Size = new Size(23, 27);
			numericUpDownFPS.TabIndex = 4;
			numericUpDownFPS.Value = new decimal(new int[] { 15, 0, 0, 0 });
			numericUpDownFPS.ValueChanged += NumericUpDownFPS_ValueChanged;
			// 
			// labelFPSIndex
			// 
			labelFPSIndex.AutoSize = true;
			labelFPSIndex.Font = new Font("Segoe UI", 11F);
			labelFPSIndex.ForeColor = Color.FromArgb(206, 214, 214);
			labelFPSIndex.Location = new Point(86, 0);
			labelFPSIndex.Name = "labelFPSIndex";
			labelFPSIndex.Size = new Size(32, 25);
			labelFPSIndex.TabIndex = 0;
			labelFPSIndex.Text = "15";
			// 
			// labelDelayFrame
			// 
			labelDelayFrame.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelDelayFrame.AutoSize = true;
			labelDelayFrame.BackColor = Color.FromArgb(136, 153, 153);
			labelDelayFrame.Font = new Font("Segoe UI", 8F);
			labelDelayFrame.ForeColor = Color.FromArgb(206, 214, 214);
			labelDelayFrame.Location = new Point(9, 197);
			labelDelayFrame.Name = "labelDelayFrame";
			labelDelayFrame.Size = new Size(92, 19);
			labelDelayFrame.TabIndex = 10;
			labelDelayFrame.Text = "DelayFrame:  ";
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			tableLayoutPanel4.ColumnCount = 3;
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.3333321F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.6666679F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 61F));
			tableLayoutPanel4.Controls.Add(numericUpDownARC, 1, 0);
			tableLayoutPanel4.Controls.Add(labelARCIndex, 2, 0);
			tableLayoutPanel4.Controls.Add(labelARC, 0, 0);
			tableLayoutPanel4.Location = new Point(9, 72);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 1;
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.Size = new Size(135, 33);
			tableLayoutPanel4.TabIndex = 7;
			// 
			// numericUpDownARC
			// 
			numericUpDownARC.Anchor = AnchorStyles.None;
			numericUpDownARC.BackColor = Color.FromArgb(50, 54, 54);
			numericUpDownARC.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
			numericUpDownARC.Location = new Point(48, 3);
			numericUpDownARC.Maximum = new decimal(new int[] { 99, 0, 0, 131072 });
			numericUpDownARC.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
			numericUpDownARC.Name = "numericUpDownARC";
			numericUpDownARC.Size = new Size(22, 27);
			numericUpDownARC.TabIndex = 8;
			numericUpDownARC.Value = new decimal(new int[] { 5, 0, 0, 65536 });
			numericUpDownARC.ValueChanged += NumericUpDownARC_ValueChanged;
			// 
			// labelARCIndex
			// 
			labelARCIndex.AutoSize = true;
			labelARCIndex.Font = new Font("Segoe UI", 11F);
			labelARCIndex.ForeColor = Color.FromArgb(206, 214, 214);
			labelARCIndex.Location = new Point(76, 0);
			labelARCIndex.Name = "labelARCIndex";
			labelARCIndex.Size = new Size(36, 25);
			labelARCIndex.TabIndex = 8;
			labelARCIndex.Text = "0.5";
			labelARCIndex.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// labelARC
			// 
			labelARC.AutoSize = true;
			labelARC.BackColor = Color.FromArgb(136, 153, 153);
			labelARC.Font = new Font("Segoe UI", 7F);
			labelARC.ForeColor = Color.FromArgb(206, 214, 214);
			labelARC.Location = new Point(3, 0);
			labelARC.Name = "labelARC";
			labelARC.Size = new Size(36, 30);
			labelARC.TabIndex = 8;
			labelARC.Text = " ARC (SSD)";
			// 
			// labelAmountFrame
			// 
			labelAmountFrame.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelAmountFrame.AutoSize = true;
			labelAmountFrame.BackColor = Color.FromArgb(136, 153, 153);
			labelAmountFrame.Font = new Font("Segoe UI", 8F);
			labelAmountFrame.ForeColor = Color.FromArgb(206, 214, 214);
			labelAmountFrame.Location = new Point(9, 165);
			labelAmountFrame.Name = "labelAmountFrame";
			labelAmountFrame.Size = new Size(60, 19);
			labelAmountFrame.TabIndex = 9;
			labelAmountFrame.Text = "Frames: ";
			// 
			// pictureBoxCameraPreview
			// 
			pictureBoxCameraPreview.BackColor = Color.FromArgb(148, 168, 168);
			pictureBoxCameraPreview.Location = new Point(0, 0);
			pictureBoxCameraPreview.Name = "pictureBoxCameraPreview";
			pictureBoxCameraPreview.Size = new Size(976, 549);
			pictureBoxCameraPreview.TabIndex = 0;
			pictureBoxCameraPreview.TabStop = false;
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
			label1.Location = new Point(22, 0);
			label1.Name = "label1";
			label1.Size = new Size(42, 25);
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
			label2.Location = new Point(22, 0);
			label2.Name = "label2";
			label2.Size = new Size(42, 25);
			label2.TabIndex = 0;
			label2.Text = "FPS";
			// 
			// Camera
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaptionText;
			ClientSize = new Size(1121, 549);
			Controls.Add(panelMain);
			FormBorderStyle = FormBorderStyle.None;
			Name = "Camera";
			Text = "Camera";
			FormClosing += Form_FormClosing;
			SizeChanged += Form_SizeChanged;
			panelMain.ResumeLayout(false);
			panelSSD.ResumeLayout(false);
			panelSSD.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownFPS).EndInit();
			tableLayoutPanel4.ResumeLayout(false);
			tableLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownARC).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxCameraPreview).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelMain;
        private Button buttonStart;
        private ComboBox comboBoxCamera;
        private PictureBox pictureBoxCameraPreview;
        private NumericUpDown numericUpDownFPS;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelFPS;
        private Label labelFPSIndex;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label2;
        private Label labelARCIndex;
        private NumericUpDown numericUpDownARC;
        private Label labelARC;
        private Label labelAmountFrame;
        private Label labelDelayFrame;
        private Button buttonSave;
        private Panel panelSSD;
		private ComboBox comboBoxDevices;
	}
}