using Microsoft.Xna.Framework;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Tests.Tests;

namespace MonoGame.Forms.Tests
{
    public partial class MainWindow : Form
    {
        Welcome welcome = new Welcome();
        DrawTest drawTest = new DrawTest();
        UpdateTest updateTest = new UpdateTest();

        bool CamButtonMouseDown = false;
        System.Drawing.Point CamButtonFirstMouseDownPosition;
        
        #region Welcome

        private void updateWindowWelcome_VisibleChanged(object sender, System.EventArgs e)
        {
            if (((UpdateWindow)sender).Editor != welcome) ((UpdateWindow)sender).Editor = welcome;
        }

        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            welcome.Logo.ResetAnimation(true);

            welcome.EditMode = !welcome.EditMode;
            trackBarLogoFrames.Visible = welcome.EditMode;

            if (welcome.EditMode) buttonEdit.Text = "Resume";
            else buttonEdit.Text = "Edit";
        }

        private void trackBarLogoFrames_Scroll(object sender, System.EventArgs e)
        {
            if (trackBarLogoFrames.Value > welcome.LastFrame) welcome.Logo.OneFrameForward();
            else welcome.Logo.OneFrameBackwards();

            welcome.LastFrame = trackBarLogoFrames.Value;
        }

        #endregion

        #region Draw Window

        private void drawWindow_VisibleChanged(object sender, System.EventArgs e)
        {
            if (((DrawWindow)sender).Editor != drawTest) ((DrawWindow)sender).Editor = drawTest;
        }

        private void textBoxTestText_TextChanged(object sender, System.EventArgs e)
        {
            drawTest.WelcomeMessage = textBoxTestText.Text;
        }

        #endregion
        
        #region Update Window

        private void updateWindow_VisibleChanged(object sender, System.EventArgs e)
        {
            if (((UpdateWindow)sender).Editor != updateTest) ((UpdateWindow)sender).Editor = updateTest;

            trackBarCamZoom.Value = (int)updateTest.Cam.GetZoom;
        }

        private void buttonMoveCam_MouseUp(object sender, MouseEventArgs e)
        {
            CamButtonMouseDown = false;
        }

        private void buttonMoveCam_MouseDown(object sender, MouseEventArgs e)
        {
            CamButtonFirstMouseDownPosition = e.Location;
            CamButtonMouseDown = true;
        }

        private void buttonMoveCam_MouseMove(object sender, MouseEventArgs e)
        {
            if (CamButtonMouseDown)
            {
                int xDiff = CamButtonFirstMouseDownPosition.X - e.Location.X;
                int yDiff = CamButtonFirstMouseDownPosition.Y - e.Location.Y;

                updateTest.MoveCam(new Vector2(xDiff, yDiff));

                CamButtonFirstMouseDownPosition.X = e.Location.X;
                CamButtonFirstMouseDownPosition.Y = e.Location.Y;
            }
        }

        private void buttonResetCam_Click(object sender, System.EventArgs e)
        {
            updateTest.ResetCam();
            trackBarCamZoom.Value = (int)updateTest.Cam.GetZoom;
        }

        private void checkBoxFPS_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.ShowFPS = checkBoxFPS.Checked;
        }

        private void checkBoxCursor_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.ShowCursorPosition = checkBoxCursor.Checked;
        }

        private void checkBoxCam_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.ShowCamPosition = checkBoxCam.Checked;
        }

        private void trackBarCamZoom_Scroll(object sender, System.EventArgs e)
        {
            updateTest.Cam.GetZoom = 1 - ((float)trackBarCamZoom.Value / 10f);
        }

        #endregion

        #region Info

        private void toolStripDropDownButtonGitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sqrMin1/MonoGame.Forms");
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();            
        }
    }
}
