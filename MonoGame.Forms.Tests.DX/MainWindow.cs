﻿using System.Windows.Forms;
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

        private void buttonInvalidate_Click(object sender, EventArgs e)
        {
            drawTestControl.Invalidate();
        }

        private void radioButtonAutoInvalidateOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAutoInvalidateOn.Checked)
            {
                drawTestControl.GetAutoInvalidation = true;
                drawTestControl.Invalidate();
            }
        }

        private void radioButtonAutoInvalidateOff_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAutoInvalidateOff.Checked) drawTestControl.GetAutoInvalidation = false;
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
            updateTestControl.Editor.ShowFPS = checkBoxFPS.Checked;
        }

        private void checkBoxCursor_CheckedChanged(object sender, System.EventArgs e)
        {
            updateTestControl.Editor.ShowCursorPosition = checkBoxCursor.Checked;
        }

        private void checkBoxCam_CheckedChanged(object sender, System.EventArgs e)
        {
            updateTestControl.Editor.ShowCamPosition = checkBoxCam.Checked;
        }

        private void trackBarCamZoom_Scroll(object sender, System.EventArgs e)
        {
            updateTestControl.Editor.Cam.GetZoom = 1 - ((float)trackBarCamZoom.Value / 10f);
        }

        #endregion

        #region Multiple Controls
        
        private void buttonHelpControls_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Left Mouse Button] Move Cam\n[Right Mouse Button] Debug Display\n[Middle Mouse Button] Reset Cam\n[XButton1] Previous Map\n[XButton2] Next Map\n[Mouse Wheel] Zoom Cam\n\nImages copyright (c) by FinTerra\nTile Art copyright (c) by Pixel32\n\nOpenGameArt.org\n\nAttribution 3.0 Unported (CC BY 3.0)", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void splitContainerMapHost_VisibleChanged(object sender, EventArgs e)
        {
            splitContainerMapHost.SplitterDistance = (int)(splitContainerMapHost.ClientSize.Width * 0.5f);
        }

        #endregion

        #region Advanced Input

        private void buttonResetPlayer_Click(object sender, EventArgs e)
        {
            advancedControlsTest.ResetPlayer();
        }

        private void buttonHelpInput_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please plug in your GamePad if you want to switch from Keyboard to GamePad input.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxShowStats_CheckedChanged(object sender, EventArgs e)
        {
            advancedControlsTest.ShowStats = checkBoxShowStats.Checked;
        }

        private void checkBoxShowHelp_CheckedChanged(object sender, EventArgs e)
        {
            advancedControlsTest.ShowControls = checkBoxShowHelp.Checked;
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
