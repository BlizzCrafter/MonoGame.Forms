namespace MonoGame.Forms.Tests
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControlEditorSwitch = new System.Windows.Forms.TabControl();
            this.tabPageWelcome = new System.Windows.Forms.TabPage();
            this.trackBarLogoFrames = new System.Windows.Forms.TrackBar();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.welcomeControl = new MonoGame.Forms.Tests.Tests.Welcome();
            this.tabPageDrawForm = new System.Windows.Forms.TabPage();
            this.textBoxTestText = new System.Windows.Forms.TextBox();
            this.drawTestControl = new MonoGame.Forms.Tests.Tests.DrawTest();
            this.tabPageUpdateForm = new System.Windows.Forms.TabPage();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.trackBarCamZoom = new System.Windows.Forms.TrackBar();
            this.checkBoxCam = new System.Windows.Forms.CheckBox();
            this.checkBoxCursor = new System.Windows.Forms.CheckBox();
            this.checkBoxFPS = new System.Windows.Forms.CheckBox();
            this.buttonResetCam = new System.Windows.Forms.Button();
            this.buttonMoveCam = new System.Windows.Forms.Button();
            this.updateTestControl = new MonoGame.Forms.Tests.Tests.UpdateTest();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButtonGitHub = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonWiki = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonTwitter = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonTwitterEngine = new System.Windows.Forms.ToolStripDropDownButton();
            this.tabControlEditorSwitch.SuspendLayout();
            this.tabPageWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLogoFrames)).BeginInit();
            this.tabPageDrawForm.SuspendLayout();
            this.tabPageUpdateForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamZoom)).BeginInit();
            this.tabPageInfo.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlEditorSwitch
            // 
            this.tabControlEditorSwitch.Controls.Add(this.tabPageWelcome);
            this.tabControlEditorSwitch.Controls.Add(this.tabPageDrawForm);
            this.tabControlEditorSwitch.Controls.Add(this.tabPageUpdateForm);
            this.tabControlEditorSwitch.Controls.Add(this.tabPageInfo);
            this.tabControlEditorSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEditorSwitch.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditorSwitch.Name = "tabControlEditorSwitch";
            this.tabControlEditorSwitch.SelectedIndex = 0;
            this.tabControlEditorSwitch.Size = new System.Drawing.Size(736, 428);
            this.tabControlEditorSwitch.TabIndex = 0;
            // 
            // tabPageWelcome
            // 
            this.tabPageWelcome.Controls.Add(this.trackBarLogoFrames);
            this.tabPageWelcome.Controls.Add(this.buttonEdit);
            this.tabPageWelcome.Controls.Add(this.welcomeControl);
            this.tabPageWelcome.Location = new System.Drawing.Point(4, 25);
            this.tabPageWelcome.Name = "tabPageWelcome";
            this.tabPageWelcome.Size = new System.Drawing.Size(728, 399);
            this.tabPageWelcome.TabIndex = 2;
            this.tabPageWelcome.Text = "Welcome";
            this.tabPageWelcome.UseVisualStyleBackColor = true;
            // 
            // trackBarLogoFrames
            // 
            this.trackBarLogoFrames.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarLogoFrames.LargeChange = 1;
            this.trackBarLogoFrames.Location = new System.Drawing.Point(0, 343);
            this.trackBarLogoFrames.Maximum = 99;
            this.trackBarLogoFrames.Name = "trackBarLogoFrames";
            this.trackBarLogoFrames.Size = new System.Drawing.Size(728, 56);
            this.trackBarLogoFrames.TabIndex = 2;
            this.trackBarLogoFrames.Visible = false;
            this.trackBarLogoFrames.Scroll += new System.EventHandler(this.trackBarLogoFrames_Scroll);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(650, 3);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // welcomeControl
            // 
            this.welcomeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomeControl.Location = new System.Drawing.Point(0, 0);
            this.welcomeControl.Name = "welcomeControl";
            this.welcomeControl.Size = new System.Drawing.Size(728, 399);
            this.welcomeControl.TabIndex = 3;
            this.welcomeControl.Text = "Welcome to MonoGame.Forms!";
            // 
            // tabPageDrawForm
            // 
            this.tabPageDrawForm.Controls.Add(this.textBoxTestText);
            this.tabPageDrawForm.Controls.Add(this.drawTestControl);
            this.tabPageDrawForm.Location = new System.Drawing.Point(4, 25);
            this.tabPageDrawForm.Name = "tabPageDrawForm";
            this.tabPageDrawForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDrawForm.Size = new System.Drawing.Size(728, 399);
            this.tabPageDrawForm.TabIndex = 0;
            this.tabPageDrawForm.Text = "Draw Form";
            this.tabPageDrawForm.UseVisualStyleBackColor = true;
            // 
            // textBoxTestText
            // 
            this.textBoxTestText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxTestText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTestText.Location = new System.Drawing.Point(3, 346);
            this.textBoxTestText.Multiline = true;
            this.textBoxTestText.Name = "textBoxTestText";
            this.textBoxTestText.Size = new System.Drawing.Size(722, 50);
            this.textBoxTestText.TabIndex = 2;
            this.textBoxTestText.Text = "Edit Me!";
            this.textBoxTestText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxTestText.TextChanged += new System.EventHandler(this.textBoxTestText_TextChanged);
            // 
            // drawTestControl
            // 
            this.drawTestControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawTestControl.Location = new System.Drawing.Point(3, 3);
            this.drawTestControl.Name = "drawTestControl";
            this.drawTestControl.Size = new System.Drawing.Size(722, 393);
            this.drawTestControl.TabIndex = 3;
            this.drawTestControl.Text = "This \'DrawWindow\' has no game loop, but it\'s updated through invalidation!";
            // 
            // tabPageUpdateForm
            // 
            this.tabPageUpdateForm.Controls.Add(this.buttonHelp);
            this.tabPageUpdateForm.Controls.Add(this.trackBarCamZoom);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxCam);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxCursor);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxFPS);
            this.tabPageUpdateForm.Controls.Add(this.buttonResetCam);
            this.tabPageUpdateForm.Controls.Add(this.buttonMoveCam);
            this.tabPageUpdateForm.Controls.Add(this.updateTestControl);
            this.tabPageUpdateForm.Location = new System.Drawing.Point(4, 25);
            this.tabPageUpdateForm.Name = "tabPageUpdateForm";
            this.tabPageUpdateForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpdateForm.Size = new System.Drawing.Size(728, 399);
            this.tabPageUpdateForm.TabIndex = 1;
            this.tabPageUpdateForm.Text = "Update Form";
            this.tabPageUpdateForm.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHelp.Location = new System.Drawing.Point(9, 338);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(28, 25);
            this.buttonHelp.TabIndex = 7;
            this.buttonHelp.Text = "?";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // trackBarCamZoom
            // 
            this.trackBarCamZoom.LargeChange = 1;
            this.trackBarCamZoom.Location = new System.Drawing.Point(664, 6);
            this.trackBarCamZoom.Maximum = 8;
            this.trackBarCamZoom.Name = "trackBarCamZoom";
            this.trackBarCamZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarCamZoom.Size = new System.Drawing.Size(56, 296);
            this.trackBarCamZoom.TabIndex = 6;
            this.trackBarCamZoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarCamZoom.Scroll += new System.EventHandler(this.trackBarCamZoom_Scroll);
            // 
            // checkBoxCam
            // 
            this.checkBoxCam.AutoSize = true;
            this.checkBoxCam.Location = new System.Drawing.Point(149, 369);
            this.checkBoxCam.Name = "checkBoxCam";
            this.checkBoxCam.Size = new System.Drawing.Size(58, 21);
            this.checkBoxCam.TabIndex = 5;
            this.checkBoxCam.Text = "Cam";
            this.checkBoxCam.UseVisualStyleBackColor = true;
            this.checkBoxCam.CheckedChanged += new System.EventHandler(this.checkBoxCam_CheckedChanged);
            // 
            // checkBoxCursor
            // 
            this.checkBoxCursor.AutoSize = true;
            this.checkBoxCursor.Checked = true;
            this.checkBoxCursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCursor.Location = new System.Drawing.Point(71, 369);
            this.checkBoxCursor.Name = "checkBoxCursor";
            this.checkBoxCursor.Size = new System.Drawing.Size(72, 21);
            this.checkBoxCursor.TabIndex = 4;
            this.checkBoxCursor.Text = "Cursor";
            this.checkBoxCursor.UseVisualStyleBackColor = true;
            this.checkBoxCursor.CheckedChanged += new System.EventHandler(this.checkBoxCursor_CheckedChanged);
            // 
            // checkBoxFPS
            // 
            this.checkBoxFPS.AutoSize = true;
            this.checkBoxFPS.Checked = true;
            this.checkBoxFPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFPS.Location = new System.Drawing.Point(9, 369);
            this.checkBoxFPS.Name = "checkBoxFPS";
            this.checkBoxFPS.Size = new System.Drawing.Size(56, 21);
            this.checkBoxFPS.TabIndex = 3;
            this.checkBoxFPS.Text = "FPS";
            this.checkBoxFPS.UseVisualStyleBackColor = true;
            this.checkBoxFPS.CheckedChanged += new System.EventHandler(this.checkBoxFPS_CheckedChanged);
            // 
            // buttonResetCam
            // 
            this.buttonResetCam.Location = new System.Drawing.Point(611, 308);
            this.buttonResetCam.Name = "buttonResetCam";
            this.buttonResetCam.Size = new System.Drawing.Size(109, 30);
            this.buttonResetCam.TabIndex = 2;
            this.buttonResetCam.Text = "Reset Cam";
            this.buttonResetCam.UseVisualStyleBackColor = true;
            this.buttonResetCam.Click += new System.EventHandler(this.buttonResetCam_Click);
            // 
            // buttonMoveCam
            // 
            this.buttonMoveCam.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.buttonMoveCam.Location = new System.Drawing.Point(611, 344);
            this.buttonMoveCam.Name = "buttonMoveCam";
            this.buttonMoveCam.Size = new System.Drawing.Size(109, 49);
            this.buttonMoveCam.TabIndex = 1;
            this.buttonMoveCam.Text = "Move Cam";
            this.buttonMoveCam.UseVisualStyleBackColor = true;
            this.buttonMoveCam.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseDown);
            this.buttonMoveCam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseMove);
            this.buttonMoveCam.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseUp);
            // 
            // updateTestControl
            // 
            this.updateTestControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateTestControl.Location = new System.Drawing.Point(3, 3);
            this.updateTestControl.Name = "updateTestControl";
            this.updateTestControl.Size = new System.Drawing.Size(722, 393);
            this.updateTestControl.TabIndex = 8;
            this.updateTestControl.Text = "This \'UpdateWindow\' has a game loop! Use it for complex \'GameTime\' based mechanic" +
    "s.";
            this.updateTestControl.VisibleChanged += new System.EventHandler(this.updateTestControl_VisibleChanged);
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.richTextBoxLicense);
            this.tabPageInfo.Controls.Add(this.statusStrip);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(728, 399);
            this.tabPageInfo.TabIndex = 3;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLicense
            // 
            this.richTextBoxLicense.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLicense.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLicense.Name = "richTextBoxLicense";
            this.richTextBoxLicense.Size = new System.Drawing.Size(728, 361);
            this.richTextBoxLicense.TabIndex = 1;
            this.richTextBoxLicense.Text = resources.GetString("richTextBoxLicense.Text");
            this.richTextBoxLicense.ZoomFactor = 1.5F;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonGitHub,
            this.toolStripDropDownButtonWiki,
            this.toolStripDropDownButtonTwitter,
            this.toolStripDropDownButtonTwitterEngine});
            this.statusStrip.Location = new System.Drawing.Point(0, 361);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(728, 38);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // toolStripDropDownButtonGitHub
            // 
            this.toolStripDropDownButtonGitHub.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButtonGitHub.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonGitHub.Image")));
            this.toolStripDropDownButtonGitHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonGitHub.Name = "toolStripDropDownButtonGitHub";
            this.toolStripDropDownButtonGitHub.Size = new System.Drawing.Size(259, 36);
            this.toolStripDropDownButtonGitHub.Text = "MonoGame.Forms";
            this.toolStripDropDownButtonGitHub.Click += new System.EventHandler(this.toolStripDropDownButtonGitHub_Click);
            // 
            // toolStripDropDownButtonWiki
            // 
            this.toolStripDropDownButtonWiki.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButtonWiki.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonWiki.Image")));
            this.toolStripDropDownButtonWiki.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonWiki.Name = "toolStripDropDownButtonWiki";
            this.toolStripDropDownButtonWiki.Size = new System.Drawing.Size(100, 36);
            this.toolStripDropDownButtonWiki.Text = "Wiki";
            this.toolStripDropDownButtonWiki.Click += new System.EventHandler(this.toolStripDropDownButtonWiki_Click);
            // 
            // toolStripDropDownButtonTwitter
            // 
            this.toolStripDropDownButtonTwitter.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButtonTwitter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonTwitter.Image")));
            this.toolStripDropDownButtonTwitter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonTwitter.Name = "toolStripDropDownButtonTwitter";
            this.toolStripDropDownButtonTwitter.Size = new System.Drawing.Size(158, 36);
            this.toolStripDropDownButtonTwitter.Text = "#sqrMin1";
            this.toolStripDropDownButtonTwitter.Click += new System.EventHandler(this.toolStripDropDownButtonTwitter_Click);
            // 
            // toolStripDropDownButtonTwitterEngine
            // 
            this.toolStripDropDownButtonTwitterEngine.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButtonTwitterEngine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonTwitterEngine.Image")));
            this.toolStripDropDownButtonTwitterEngine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonTwitterEngine.Name = "toolStripDropDownButtonTwitterEngine";
            this.toolStripDropDownButtonTwitterEngine.Size = new System.Drawing.Size(283, 36);
            this.toolStripDropDownButtonTwitterEngine.Text = "#RogueEngineEditor";
            this.toolStripDropDownButtonTwitterEngine.Click += new System.EventHandler(this.toolStripDropDownButtonTwitterEngine_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(736, 428);
            this.Controls.Add(this.tabControlEditorSwitch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonoGame.Forms";
            this.tabControlEditorSwitch.ResumeLayout(false);
            this.tabPageWelcome.ResumeLayout(false);
            this.tabPageWelcome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLogoFrames)).EndInit();
            this.tabPageDrawForm.ResumeLayout(false);
            this.tabPageDrawForm.PerformLayout();
            this.tabPageUpdateForm.ResumeLayout(false);
            this.tabPageUpdateForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamZoom)).EndInit();
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlEditorSwitch;
        private System.Windows.Forms.TabPage tabPageDrawForm;
        private System.Windows.Forms.TabPage tabPageUpdateForm;
        private System.Windows.Forms.TextBox textBoxTestText;
        private System.Windows.Forms.Button buttonMoveCam;
        private System.Windows.Forms.Button buttonResetCam;
        private System.Windows.Forms.CheckBox checkBoxCam;
        private System.Windows.Forms.CheckBox checkBoxCursor;
        private System.Windows.Forms.CheckBox checkBoxFPS;
        private System.Windows.Forms.TrackBar trackBarCamZoom;
        private System.Windows.Forms.TabPage tabPageWelcome;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.TrackBar trackBarLogoFrames;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonGitHub;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonTwitter;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonTwitterEngine;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonWiki;
        private Tests.Welcome welcomeControl;
        private Tests.DrawTest drawTestControl;
        private Tests.UpdateTest updateTestControl;
    }
}

