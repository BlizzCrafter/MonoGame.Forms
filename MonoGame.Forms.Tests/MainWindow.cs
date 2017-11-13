using System.Windows.Forms;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MonoGame.Forms.Tests
{
    public partial class MainWindow : Form
    {
        #region Welcome

        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            welcomeControl.Logo.ResetAnimation(true);

            welcomeControl.EditMode = !welcomeControl.EditMode;
            trackBarLogoFrames.Visible = welcomeControl.EditMode;

            if (welcomeControl.EditMode) buttonEdit.Text = "Resume";
            else buttonEdit.Text = "Edit";
        }

        private void trackBarLogoFrames_Scroll(object sender, System.EventArgs e)
        {
            if (trackBarLogoFrames.Value > welcomeControl.LastFrame) welcomeControl.Logo.OneFrameForward();
            else welcomeControl.Logo.OneFrameBackwards();

            welcomeControl.LastFrame = trackBarLogoFrames.Value;
        }

        #endregion

        #region Draw Window

        private void textBoxTestText_TextChanged(object sender, System.EventArgs e)
        {
            drawTestControl.WelcomeMessage = textBoxTestText.Text;
        }

        #endregion

        #region Update Window
        
        bool CamButtonMouseDown = false;
        System.Drawing.Point CamButtonFirstMouseDownPosition;
        
        private void updateTestControl_VisibleChanged(object sender, EventArgs e)
        {
            trackBarCamZoom.Value = (int)updateTestControl.Editor.Cam.GetZoom;
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

                updateTestControl.Editor.MoveCam(new Vector2(xDiff, yDiff));

                CamButtonFirstMouseDownPosition.X = e.Location.X;
                CamButtonFirstMouseDownPosition.Y = e.Location.Y;
            }
        }

        private void buttonResetCam_Click(object sender, System.EventArgs e)
        {
            updateTestControl.Editor.ResetCam();
            trackBarCamZoom.Value = (int)updateTestControl.Editor.Cam.GetZoom;
        }
        
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please press a mouse button directly on the control to test if the different mouse events are working correctly." + Environment.NewLine + Environment.NewLine + "The mouse events are directly delivered to the corresponding classes, so it becomes very easy to work with them in your custom editor!", "Mouse Events 101", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            updateTestControl.Editor.Cam.GetZoom = 1 - ((float)trackBarCamZoom.Value / 10f);
        }

        #endregion

        #region Info

        private void toolStripDropDownButtonGitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sqrMin1/MonoGame.Forms");
        }

        private void toolStripDropDownButtonWiki_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sqrMin1/MonoGame.Forms/wiki");
        }

        private void toolStripDropDownButtonTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/sqrMin1");
        }

        private void toolStripDropDownButtonTwitterEngine_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/hashtag/RogueEngineEditor?src=hash");
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();            
        }
    }
}
