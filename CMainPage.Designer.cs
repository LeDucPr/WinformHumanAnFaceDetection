namespace CameraHumanDetection
{
    partial class CMainPage
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
			panelHeader = new Panel();
			labelHeader = new Label();
			buttonMinimize = new Button();
			buttonClose = new Button();
			panelBodyLeft = new Panel();
			buttonSetting = new Button();
			buttonRecog = new Button();
			buttonCamera = new Button();
			panelBodyMiddle = new Panel();
			panelHeader.SuspendLayout();
			panelBodyLeft.SuspendLayout();
			SuspendLayout();
			// 
			// panelHeader
			// 
			panelHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			panelHeader.BackColor = Color.FromArgb(148, 168, 168);
			panelHeader.Controls.Add(labelHeader);
			panelHeader.Controls.Add(buttonMinimize);
			panelHeader.Controls.Add(buttonClose);
			panelHeader.Location = new Point(3, 2);
			panelHeader.Margin = new Padding(3, 2, 3, 2);
			panelHeader.Name = "panelHeader";
			panelHeader.Size = new Size(1058, 26);
			panelHeader.TabIndex = 0;
			panelHeader.MouseDown += panelHeader_MouseDown;
			panelHeader.MouseMove += panelHeader_MouseMove;
			panelHeader.MouseUp += panelHeader_MouseUp;
			// 
			// labelHeader
			// 
			labelHeader.AutoSize = true;
			labelHeader.Font = new Font("Segoe UI Variable Display", 10.2F, FontStyle.Bold);
			labelHeader.ForeColor = SystemColors.ButtonFace;
			labelHeader.Location = new Point(3, 2);
			labelHeader.Name = "labelHeader";
			labelHeader.Size = new Size(58, 19);
			labelHeader.TabIndex = 2;
			labelHeader.Text = "MyApp";
			// 
			// buttonMinimize
			// 
			buttonMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			buttonMinimize.BackColor = SystemColors.ActiveCaption;
			buttonMinimize.ForeColor = Color.FromArgb(206, 214, 214);
			buttonMinimize.ImageAlign = ContentAlignment.TopCenter;
			buttonMinimize.Location = new Point(997, 2);
			buttonMinimize.Margin = new Padding(3, 2, 3, 2);
			buttonMinimize.Name = "buttonMinimize";
			buttonMinimize.Size = new Size(24, 20);
			buttonMinimize.TabIndex = 1;
			buttonMinimize.Text = "▃";
			buttonMinimize.TextAlign = ContentAlignment.TopCenter;
			buttonMinimize.UseVisualStyleBackColor = false;
			buttonMinimize.Click += buttonMinimize_Click;
			// 
			// buttonClose
			// 
			buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			buttonClose.BackColor = SystemColors.ActiveCaption;
			buttonClose.ForeColor = Color.FromArgb(206, 214, 214);
			buttonClose.Location = new Point(1026, 2);
			buttonClose.Margin = new Padding(3, 2, 3, 2);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new Size(24, 20);
			buttonClose.TabIndex = 0;
			buttonClose.Text = "✖";
			buttonClose.TextAlign = ContentAlignment.TopCenter;
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += buttonClose_Click;
			// 
			// panelBodyLeft
			// 
			panelBodyLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			panelBodyLeft.BackColor = Color.FromArgb(148, 168, 168);
			panelBodyLeft.Controls.Add(buttonSetting);
			panelBodyLeft.Controls.Add(buttonRecog);
			panelBodyLeft.Controls.Add(buttonCamera);
			panelBodyLeft.Location = new Point(0, 26);
			panelBodyLeft.Margin = new Padding(3, 2, 3, 2);
			panelBodyLeft.Name = "panelBodyLeft";
			panelBodyLeft.Size = new Size(52, 415);
			panelBodyLeft.TabIndex = 1;
			// 
			// buttonSetting
			// 
			buttonSetting.BackColor = Color.FromArgb(148, 168, 168);
			buttonSetting.Font = new Font("Segoe UI", 16.2F);
			buttonSetting.ForeColor = Color.FromArgb(206, 214, 214);
			buttonSetting.Location = new Point(3, 95);
			buttonSetting.Margin = new Padding(3, 2, 3, 2);
			buttonSetting.Name = "buttonSetting";
			buttonSetting.Size = new Size(46, 42);
			buttonSetting.TabIndex = 2;
			buttonSetting.Text = "⚙️";
			buttonSetting.UseVisualStyleBackColor = false;
			buttonSetting.Click += buttonSetting_Click;
			// 
			// buttonRecog
			// 
			buttonRecog.BackColor = Color.FromArgb(148, 168, 168);
			buttonRecog.Font = new Font("Segoe UI", 16.2F);
			buttonRecog.ForeColor = Color.FromArgb(206, 214, 214);
			buttonRecog.Location = new Point(4, 49);
			buttonRecog.Margin = new Padding(3, 2, 3, 2);
			buttonRecog.Name = "buttonRecog";
			buttonRecog.Size = new Size(46, 42);
			buttonRecog.TabIndex = 1;
			buttonRecog.Text = "👤";
			buttonRecog.UseVisualStyleBackColor = false;
			buttonRecog.Click += buttonRecog_Click;
			// 
			// buttonCamera
			// 
			buttonCamera.BackColor = Color.FromArgb(148, 168, 168);
			buttonCamera.Font = new Font("Segoe UI", 16.2F);
			buttonCamera.ForeColor = Color.FromArgb(206, 214, 214);
			buttonCamera.Location = new Point(4, 2);
			buttonCamera.Margin = new Padding(3, 2, 3, 2);
			buttonCamera.Name = "buttonCamera";
			buttonCamera.Size = new Size(46, 42);
			buttonCamera.TabIndex = 0;
			buttonCamera.Text = "📷";
			buttonCamera.UseVisualStyleBackColor = false;
			buttonCamera.Click += buttonCamera_Click;
			// 
			// panelBodyMiddle
			// 
			panelBodyMiddle.BackColor = Color.FromArgb(148, 168, 168);
			panelBodyMiddle.Location = new Point(54, 29);
			panelBodyMiddle.Margin = new Padding(3, 2, 3, 2);
			panelBodyMiddle.Name = "panelBodyMiddle";
			panelBodyMiddle.Size = new Size(1005, 412);
			panelBodyMiddle.TabIndex = 2;
			// 
			// CMainPage
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(148, 168, 168);
			ClientSize = new Size(1063, 441);
			Controls.Add(panelBodyMiddle);
			Controls.Add(panelBodyLeft);
			Controls.Add(panelHeader);
			FormBorderStyle = FormBorderStyle.None;
			Margin = new Padding(3, 2, 3, 2);
			Name = "CMainPage";
			Text = "CMainPage";
			panelHeader.ResumeLayout(false);
			panelHeader.PerformLayout();
			panelBodyLeft.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Panel panelHeader;
        private Button buttonMinimize;
        private Button buttonClose;
        private Label labelHeader;
        private Panel panelBodyLeft;
        private Button buttonCamera;
        private Button buttonRecog;
        private Button buttonSetting;
        private Panel panelBodyMiddle;
    }
}