using Emgu.CV.DepthAI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraHumanDetection
{
    public partial class CMainPage : Form
    {
        /// <summary>
        /// bo góc tròn cho Form 
        /// </summary>
        /// <param name="nLeftRect"></param>
        /// <param name="nTopRect"></param>
        /// <param name="nRightRect"></param>
        /// <param name="nBottomRect"></param>
        /// <param name="nWidthEllipse"></param>
        /// <param name="nHeightEllipse"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        //form
        private Camera currentCameraForm;
        private ObjectCutterFromImage currentObjectCutterForm;
        private Form activeForm;
        public CMainPage()
        {
            this.InitializeComponent();
            this.CustomForm();
        }


        private void CustomForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void OpenChildForm(Form childForm, Panel parentPanel)
        {
            // Remove any existing controls from the parent panel
            parentPanel.Controls.Clear();

            // Set the child form properties
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the child form to the parent panel
            parentPanel.Controls.Add(childForm);
            parentPanel.Tag = childForm;

            // Show the child form
            childForm.BringToFront();
            childForm.Show();
        }


        #region button
        private void buttonCamera_Click(object sender, EventArgs e)
        {
            if (this.currentCameraForm == null)
                this.currentCameraForm = new Camera();
            if (this.activeForm != this.currentCameraForm)
                OpenChildForm(this.currentCameraForm, this.panelBodyMiddle);
            this.activeForm = this.currentCameraForm;
        }

        private void buttonRecog_Click(object sender, EventArgs e)
        {
			if (this.currentObjectCutterForm == null)
				this.currentObjectCutterForm = new ObjectCutterFromImage();
			if (this.activeForm != this.currentObjectCutterForm)
				OpenChildForm(this.currentObjectCutterForm, this.panelBodyMiddle);
			this.activeForm = this.currentObjectCutterForm;
		}

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion button 

        #region kéo thả form 
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion kéo thả form

    }
}
