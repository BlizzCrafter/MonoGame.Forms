using static System.Net.Mime.MediaTypeNames;

namespace MonoGame.Forms.Samples
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
            tabControlEditorSwitch = new TabControl();
            tabPageWelcome = new TabPage();
            trackBarLogoFrames = new TrackBar();
            buttonEdit = new Button();
            welcomeControl = new Tests.Welcome();
            tabPageDrawControl = new TabPage();
            panelInvalidation = new Panel();
            buttonInvalidate = new Button();
            textBoxTestText = new TextBox();
            invalidationTestControl = new Tests.InvalidationTest();
            tabPageUpdateControl = new TabPage();
            buttonHelp = new Button();
            trackBarCamZoom = new TrackBar();
            checkBoxCam = new CheckBox();
            checkBoxCursor = new CheckBox();
            checkBoxFPS = new CheckBox();
            buttonResetCam = new Button();
            buttonMoveCam = new Button();
            monoGameTestControl = new Tests.MonoGameTest();
            tabPageAdvancedInput = new TabPage();
            buttonHelpInput = new Button();
            checkBoxShowHelp = new CheckBox();
            checkBoxShowStats = new CheckBox();
            buttonResetPlayer = new Button();
            advancedControlsTest = new Tests.AdvancedInputTest();
            tabPageMultipleControls = new TabPage();
            splitContainerMapHost = new SplitContainer();
            buttonHelpControls = new Button();
            multipleControls_First_Test1 = new Tests.MultipleControls_a_Test();
            multipleControls_Second_Test1 = new Tests.MultipleControls_b_Test();
            tabPageInfo = new TabPage();
            richTextBoxLicense = new RichTextBox();
            statusStrip = new StatusStrip();
            toolStripDropDownButtonGitHub = new ToolStripDropDownButton();
            toolStripDropDownButtonWiki = new ToolStripDropDownButton();
            tabControlEditorSwitch.SuspendLayout();
            tabPageWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLogoFrames).BeginInit();
            tabPageDrawControl.SuspendLayout();
            panelInvalidation.SuspendLayout();
            tabPageUpdateControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCamZoom).BeginInit();
            tabPageAdvancedInput.SuspendLayout();
            tabPageMultipleControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMapHost).BeginInit();
            splitContainerMapHost.Panel1.SuspendLayout();
            splitContainerMapHost.Panel2.SuspendLayout();
            splitContainerMapHost.SuspendLayout();
            tabPageInfo.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlEditorSwitch
            // 
            tabControlEditorSwitch.Controls.Add(tabPageWelcome);
            tabControlEditorSwitch.Controls.Add(tabPageDrawControl);
            tabControlEditorSwitch.Controls.Add(tabPageUpdateControl);
            tabControlEditorSwitch.Controls.Add(tabPageAdvancedInput);
            tabControlEditorSwitch.Controls.Add(tabPageMultipleControls);
            tabControlEditorSwitch.Controls.Add(tabPageInfo);
            tabControlEditorSwitch.Dock = DockStyle.Fill;
            tabControlEditorSwitch.Location = new Point(0, 0);
            tabControlEditorSwitch.Name = "tabControlEditorSwitch";
            tabControlEditorSwitch.SelectedIndex = 0;
            tabControlEditorSwitch.Size = new Size(736, 428);
            tabControlEditorSwitch.TabIndex = 0;
            // 
            // tabPageWelcome
            // 
            tabPageWelcome.Controls.Add(trackBarLogoFrames);
            tabPageWelcome.Controls.Add(buttonEdit);
            tabPageWelcome.Controls.Add(welcomeControl);
            tabPageWelcome.Location = new Point(4, 24);
            tabPageWelcome.Name = "tabPageWelcome";
            tabPageWelcome.Size = new Size(728, 400);
            tabPageWelcome.TabIndex = 2;
            tabPageWelcome.Text = "Welcome";
            tabPageWelcome.UseVisualStyleBackColor = true;
            // 
            // trackBarLogoFrames
            // 
            trackBarLogoFrames.Dock = DockStyle.Bottom;
            trackBarLogoFrames.LargeChange = 1;
            trackBarLogoFrames.Location = new Point(0, 355);
            trackBarLogoFrames.Maximum = 99;
            trackBarLogoFrames.Name = "trackBarLogoFrames";
            trackBarLogoFrames.Size = new Size(728, 45);
            trackBarLogoFrames.TabIndex = 2;
            trackBarLogoFrames.Visible = false;
            trackBarLogoFrames.Scroll += trackBarLogoFrames_Scroll;
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonEdit.Location = new Point(650, 3);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 1;
            buttonEdit.Text = "Edit";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // welcomeControl
            // 
            welcomeControl.Dock = DockStyle.Fill;
            welcomeControl.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            welcomeControl.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;
            welcomeControl.Location = new Point(0, 0);
            welcomeControl.MouseHoverUpdatesOnly = false;
            welcomeControl.Name = "welcomeControl";
            welcomeControl.Size = new Size(728, 400);
            welcomeControl.TabIndex = 3;
            welcomeControl.Text = "Welcome to MonoGame.Forms!";
            // 
            // tabPageDrawControl
            // 
            tabPageDrawControl.Controls.Add(panelInvalidation);
            tabPageDrawControl.Controls.Add(textBoxTestText);
            tabPageDrawControl.Controls.Add(invalidationTestControl);
            tabPageDrawControl.Location = new Point(4, 24);
            tabPageDrawControl.Name = "tabPageDrawControl";
            tabPageDrawControl.Padding = new Padding(3);
            tabPageDrawControl.Size = new Size(728, 400);
            tabPageDrawControl.TabIndex = 0;
            tabPageDrawControl.Text = "Invalidation Control";
            tabPageDrawControl.UseVisualStyleBackColor = true;
            // 
            // panelInvalidation
            // 
            panelInvalidation.BackColor = Color.CornflowerBlue;
            panelInvalidation.Controls.Add(buttonInvalidate);
            panelInvalidation.Dock = DockStyle.Bottom;
            panelInvalidation.Location = new Point(3, 313);
            panelInvalidation.Name = "panelInvalidation";
            panelInvalidation.Size = new Size(722, 34);
            panelInvalidation.TabIndex = 4;
            // 
            // buttonInvalidate
            // 
            buttonInvalidate.Dock = DockStyle.Left;
            buttonInvalidate.Location = new Point(0, 0);
            buttonInvalidate.Name = "buttonInvalidate";
            buttonInvalidate.Size = new Size(93, 34);
            buttonInvalidate.TabIndex = 2;
            buttonInvalidate.Text = "Invalidate";
            buttonInvalidate.UseVisualStyleBackColor = true;
            buttonInvalidate.Click += buttonInvalidate_Click;
            // 
            // textBoxTestText
            // 
            textBoxTestText.BackColor = Color.CornflowerBlue;
            textBoxTestText.Dock = DockStyle.Bottom;
            textBoxTestText.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxTestText.ForeColor = SystemColors.Control;
            textBoxTestText.Location = new Point(3, 347);
            textBoxTestText.Multiline = true;
            textBoxTestText.Name = "textBoxTestText";
            textBoxTestText.Size = new Size(722, 50);
            textBoxTestText.TabIndex = 2;
            textBoxTestText.Text = "Edit Me!";
            textBoxTestText.TextAlign = HorizontalAlignment.Center;
            textBoxTestText.TextChanged += textBoxTestText_TextChanged;
            // 
            // invalidationTestControl
            // 
            invalidationTestControl.BackColor = Color.CornflowerBlue;
            invalidationTestControl.Dock = DockStyle.Fill;
            invalidationTestControl.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            invalidationTestControl.ForeColor = Color.Yellow;
            invalidationTestControl.Location = new Point(3, 3);
            invalidationTestControl.Name = "invalidationTestControl";
            invalidationTestControl.Size = new Size(722, 394);
            invalidationTestControl.TabIndex = 5;
            invalidationTestControl.Text = "This 'InvalidationControl' has no game loop, but it's updated through invalidation!";
            // 
            // tabPageUpdateControl
            // 
            tabPageUpdateControl.Controls.Add(buttonHelp);
            tabPageUpdateControl.Controls.Add(trackBarCamZoom);
            tabPageUpdateControl.Controls.Add(checkBoxCam);
            tabPageUpdateControl.Controls.Add(checkBoxCursor);
            tabPageUpdateControl.Controls.Add(checkBoxFPS);
            tabPageUpdateControl.Controls.Add(buttonResetCam);
            tabPageUpdateControl.Controls.Add(buttonMoveCam);
            tabPageUpdateControl.Controls.Add(monoGameTestControl);
            tabPageUpdateControl.Location = new Point(4, 24);
            tabPageUpdateControl.Name = "tabPageUpdateControl";
            tabPageUpdateControl.Padding = new Padding(3);
            tabPageUpdateControl.Size = new Size(728, 400);
            tabPageUpdateControl.TabIndex = 1;
            tabPageUpdateControl.Text = "MonoGame Control";
            tabPageUpdateControl.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelp.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelp.Location = new Point(9, 339);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(28, 25);
            buttonHelp.TabIndex = 7;
            buttonHelp.Text = "?";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // trackBarCamZoom
            // 
            trackBarCamZoom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            trackBarCamZoom.LargeChange = 1;
            trackBarCamZoom.Location = new Point(664, 6);
            trackBarCamZoom.Maximum = 8;
            trackBarCamZoom.Name = "trackBarCamZoom";
            trackBarCamZoom.Orientation = Orientation.Vertical;
            trackBarCamZoom.Size = new Size(45, 297);
            trackBarCamZoom.TabIndex = 6;
            trackBarCamZoom.TickStyle = TickStyle.TopLeft;
            trackBarCamZoom.Scroll += trackBarCamZoom_Scroll;
            // 
            // checkBoxCam
            // 
            checkBoxCam.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxCam.AutoSize = true;
            checkBoxCam.Location = new Point(149, 372);
            checkBoxCam.Name = "checkBoxCam";
            checkBoxCam.Size = new Size(51, 19);
            checkBoxCam.TabIndex = 5;
            checkBoxCam.Text = "Cam";
            checkBoxCam.UseVisualStyleBackColor = true;
            checkBoxCam.CheckedChanged += checkBoxCam_CheckedChanged;
            // 
            // checkBoxCursor
            // 
            checkBoxCursor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxCursor.AutoSize = true;
            checkBoxCursor.Checked = true;
            checkBoxCursor.CheckState = CheckState.Checked;
            checkBoxCursor.Location = new Point(71, 372);
            checkBoxCursor.Name = "checkBoxCursor";
            checkBoxCursor.Size = new Size(61, 19);
            checkBoxCursor.TabIndex = 4;
            checkBoxCursor.Text = "Cursor";
            checkBoxCursor.UseVisualStyleBackColor = true;
            checkBoxCursor.CheckedChanged += checkBoxCursor_CheckedChanged;
            // 
            // checkBoxFPS
            // 
            checkBoxFPS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxFPS.AutoSize = true;
            checkBoxFPS.Checked = true;
            checkBoxFPS.CheckState = CheckState.Checked;
            checkBoxFPS.Location = new Point(9, 372);
            checkBoxFPS.Name = "checkBoxFPS";
            checkBoxFPS.Size = new Size(45, 19);
            checkBoxFPS.TabIndex = 3;
            checkBoxFPS.Text = "FPS";
            checkBoxFPS.UseVisualStyleBackColor = true;
            checkBoxFPS.CheckedChanged += checkBoxFPS_CheckedChanged;
            // 
            // buttonResetCam
            // 
            buttonResetCam.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonResetCam.Location = new Point(611, 309);
            buttonResetCam.Name = "buttonResetCam";
            buttonResetCam.Size = new Size(109, 30);
            buttonResetCam.TabIndex = 2;
            buttonResetCam.Text = "Reset Cam";
            buttonResetCam.UseVisualStyleBackColor = true;
            buttonResetCam.Click += buttonResetCam_Click;
            // 
            // buttonMoveCam
            // 
            buttonMoveCam.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonMoveCam.Cursor = Cursors.SizeAll;
            buttonMoveCam.Location = new Point(611, 345);
            buttonMoveCam.Name = "buttonMoveCam";
            buttonMoveCam.Size = new Size(109, 49);
            buttonMoveCam.TabIndex = 1;
            buttonMoveCam.Text = "Move Cam";
            buttonMoveCam.UseVisualStyleBackColor = true;
            buttonMoveCam.MouseDown += buttonMoveCam_MouseDown;
            buttonMoveCam.MouseMove += buttonMoveCam_MouseMove;
            buttonMoveCam.MouseUp += buttonMoveCam_MouseUp;
            // 
            // monoGameTestControl
            // 
            monoGameTestControl.BackColor = Color.PaleGoldenrod;
            monoGameTestControl.Dock = DockStyle.Fill;
            monoGameTestControl.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            monoGameTestControl.Location = new Point(3, 3);
            monoGameTestControl.MouseHoverUpdatesOnly = false;
            monoGameTestControl.Name = "monoGameTestControl";
            monoGameTestControl.Size = new Size(722, 394);
            monoGameTestControl.TabIndex = 8;
            monoGameTestControl.Text = "The 'MonoGameControl' has a game loop!";
            monoGameTestControl.VisibleChanged += monoGameTestControl_VisibleChanged;
            // 
            // tabPageAdvancedInput
            // 
            tabPageAdvancedInput.Controls.Add(buttonHelpInput);
            tabPageAdvancedInput.Controls.Add(checkBoxShowHelp);
            tabPageAdvancedInput.Controls.Add(checkBoxShowStats);
            tabPageAdvancedInput.Controls.Add(buttonResetPlayer);
            tabPageAdvancedInput.Controls.Add(advancedControlsTest);
            tabPageAdvancedInput.Location = new Point(4, 24);
            tabPageAdvancedInput.Name = "tabPageAdvancedInput";
            tabPageAdvancedInput.Size = new Size(728, 400);
            tabPageAdvancedInput.TabIndex = 4;
            tabPageAdvancedInput.Text = "Advanced Input";
            tabPageAdvancedInput.UseVisualStyleBackColor = true;
            // 
            // buttonHelpInput
            // 
            buttonHelpInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelpInput.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelpInput.Location = new Point(114, 361);
            buttonHelpInput.Name = "buttonHelpInput";
            buttonHelpInput.Size = new Size(59, 31);
            buttonHelpInput.TabIndex = 4;
            buttonHelpInput.Text = "Help";
            buttonHelpInput.UseVisualStyleBackColor = true;
            buttonHelpInput.Click += buttonHelpInput_Click;
            // 
            // checkBoxShowHelp
            // 
            checkBoxShowHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxShowHelp.AutoSize = true;
            checkBoxShowHelp.Checked = true;
            checkBoxShowHelp.CheckState = CheckState.Checked;
            checkBoxShowHelp.Location = new Point(8, 336);
            checkBoxShowHelp.Name = "checkBoxShowHelp";
            checkBoxShowHelp.Size = new Size(83, 19);
            checkBoxShowHelp.TabIndex = 3;
            checkBoxShowHelp.Text = "Show Help";
            checkBoxShowHelp.UseVisualStyleBackColor = true;
            checkBoxShowHelp.CheckedChanged += checkBoxShowHelp_CheckedChanged;
            // 
            // checkBoxShowStats
            // 
            checkBoxShowStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxShowStats.AutoSize = true;
            checkBoxShowStats.Checked = true;
            checkBoxShowStats.CheckState = CheckState.Checked;
            checkBoxShowStats.Location = new Point(8, 309);
            checkBoxShowStats.Name = "checkBoxShowStats";
            checkBoxShowStats.Size = new Size(83, 19);
            checkBoxShowStats.TabIndex = 2;
            checkBoxShowStats.Text = "Show Stats";
            checkBoxShowStats.UseVisualStyleBackColor = true;
            checkBoxShowStats.CheckedChanged += checkBoxShowStats_CheckedChanged;
            // 
            // buttonResetPlayer
            // 
            buttonResetPlayer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonResetPlayer.Location = new Point(8, 361);
            buttonResetPlayer.Name = "buttonResetPlayer";
            buttonResetPlayer.Size = new Size(100, 31);
            buttonResetPlayer.TabIndex = 1;
            buttonResetPlayer.Text = "Reset Player";
            buttonResetPlayer.UseVisualStyleBackColor = true;
            buttonResetPlayer.Click += buttonResetPlayer_Click;
            // 
            // advancedControlsTest
            // 
            advancedControlsTest.BackColor = Color.DarkSeaGreen;
            advancedControlsTest.Dock = DockStyle.Fill;
            advancedControlsTest.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            advancedControlsTest.ForeColor = Color.FloralWhite;
            advancedControlsTest.Location = new Point(0, 0);
            advancedControlsTest.MouseHoverUpdatesOnly = false;
            advancedControlsTest.Name = "advancedControlsTest";
            advancedControlsTest.Size = new Size(728, 400);
            advancedControlsTest.TabIndex = 0;
            advancedControlsTest.Text = "Test Keyboard, Mouse and GamePad input here!";
            // 
            // tabPageMultipleControls
            // 
            tabPageMultipleControls.Controls.Add(splitContainerMapHost);
            tabPageMultipleControls.Location = new Point(4, 24);
            tabPageMultipleControls.Name = "tabPageMultipleControls";
            tabPageMultipleControls.Size = new Size(728, 400);
            tabPageMultipleControls.TabIndex = 5;
            tabPageMultipleControls.Text = "Multiple Controls";
            tabPageMultipleControls.UseVisualStyleBackColor = true;
            // 
            // splitContainerMapHost
            // 
            splitContainerMapHost.Dock = DockStyle.Fill;
            splitContainerMapHost.Location = new Point(0, 0);
            splitContainerMapHost.Name = "splitContainerMapHost";
            // 
            // splitContainerMapHost.Panel1
            // 
            splitContainerMapHost.Panel1.Controls.Add(buttonHelpControls);
            splitContainerMapHost.Panel1.Controls.Add(multipleControls_First_Test1);
            // 
            // splitContainerMapHost.Panel2
            // 
            splitContainerMapHost.Panel2.Controls.Add(multipleControls_Second_Test1);
            splitContainerMapHost.Size = new Size(728, 399);
            splitContainerMapHost.SplitterDistance = 350;
            splitContainerMapHost.TabIndex = 3;
            splitContainerMapHost.VisibleChanged += splitContainerMapHost_VisibleChanged;
            // 
            // buttonHelpControls
            // 
            buttonHelpControls.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelpControls.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelpControls.Location = new Point(3, 372);
            buttonHelpControls.Name = "buttonHelpControls";
            buttonHelpControls.Size = new Size(75, 24);
            buttonHelpControls.TabIndex = 2;
            buttonHelpControls.Text = "Help";
            buttonHelpControls.UseVisualStyleBackColor = true;
            buttonHelpControls.Click += buttonHelpControls_Click;
            // 
            // multipleControls_First_Test1
            // 
            multipleControls_First_Test1.BackColor = Color.LightSeaGreen;
            multipleControls_First_Test1.Dock = DockStyle.Fill;
            multipleControls_First_Test1.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            multipleControls_First_Test1.ForeColor = Color.Lavender;
            multipleControls_First_Test1.Location = new Point(0, 0);
            multipleControls_First_Test1.MouseHoverUpdatesOnly = false;
            multipleControls_First_Test1.Name = "multipleControls_First_Test1";
            multipleControls_First_Test1.Size = new Size(350, 399);
            multipleControls_First_Test1.TabIndex = 0;
            multipleControls_First_Test1.Text = "Left Map";
            // 
            // multipleControls_Second_Test1
            // 
            multipleControls_Second_Test1.BackColor = Color.RoyalBlue;
            multipleControls_Second_Test1.Dock = DockStyle.Fill;
            multipleControls_Second_Test1.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            multipleControls_Second_Test1.ForeColor = Color.Lavender;
            multipleControls_Second_Test1.Location = new Point(0, 0);
            multipleControls_Second_Test1.MouseHoverUpdatesOnly = false;
            multipleControls_Second_Test1.Name = "multipleControls_Second_Test1";
            multipleControls_Second_Test1.Size = new Size(374, 399);
            multipleControls_Second_Test1.TabIndex = 1;
            multipleControls_Second_Test1.Text = "Right Map";
            // 
            // tabPageInfo
            // 
            tabPageInfo.Controls.Add(richTextBoxLicense);
            tabPageInfo.Controls.Add(statusStrip);
            tabPageInfo.Location = new Point(4, 24);
            tabPageInfo.Name = "tabPageInfo";
            tabPageInfo.Size = new Size(728, 400);
            tabPageInfo.TabIndex = 3;
            tabPageInfo.Text = "Info";
            tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLicense
            // 
            richTextBoxLicense.BorderStyle = BorderStyle.None;
            richTextBoxLicense.Dock = DockStyle.Fill;
            richTextBoxLicense.Location = new Point(0, 0);
            richTextBoxLicense.Name = "richTextBoxLicense";
            richTextBoxLicense.ReadOnly = true;
            richTextBoxLicense.Size = new Size(728, 369);
            richTextBoxLicense.TabIndex = 1;
            richTextBoxLicense.Text = resources.GetString("richTextBoxLicense.Text");
            richTextBoxLicense.ZoomFactor = 1.5F;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownButtonGitHub, toolStripDropDownButtonWiki });
            statusStrip.Location = new Point(0, 369);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(728, 31);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 0;
            // 
            // toolStripDropDownButtonGitHub
            // 
            toolStripDropDownButtonGitHub.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            toolStripDropDownButtonGitHub.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButtonGitHub.Name = "toolStripDropDownButtonGitHub";
            toolStripDropDownButtonGitHub.Size = new Size(190, 29);
            toolStripDropDownButtonGitHub.Text = "MonoGame.Forms";
            toolStripDropDownButtonGitHub.Click += toolStripDropDownButtonGitHub_Click;
            // 
            // toolStripDropDownButtonWiki
            // 
            toolStripDropDownButtonWiki.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            toolStripDropDownButtonWiki.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButtonWiki.Name = "toolStripDropDownButtonWiki";
            toolStripDropDownButtonWiki.Size = new Size(65, 29);
            toolStripDropDownButtonWiki.Text = "Wiki";
            toolStripDropDownButtonWiki.Click += toolStripDropDownButtonWiki_Click;
            // 
            // MainWindow
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(736, 428);
            Controls.Add(tabControlEditorSwitch);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MonoGame.Forms";
            tabControlEditorSwitch.ResumeLayout(false);
            tabPageWelcome.ResumeLayout(false);
            tabPageWelcome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLogoFrames).EndInit();
            tabPageDrawControl.ResumeLayout(false);
            tabPageDrawControl.PerformLayout();
            panelInvalidation.ResumeLayout(false);
            tabPageUpdateControl.ResumeLayout(false);
            tabPageUpdateControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCamZoom).EndInit();
            tabPageAdvancedInput.ResumeLayout(false);
            tabPageAdvancedInput.PerformLayout();
            tabPageMultipleControls.ResumeLayout(false);
            splitContainerMapHost.Panel1.ResumeLayout(false);
            splitContainerMapHost.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMapHost).EndInit();
            splitContainerMapHost.ResumeLayout(false);
            tabPageInfo.ResumeLayout(false);
            tabPageInfo.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlEditorSwitch;
        private TabPage tabPageDrawControl;
        private TabPage tabPageUpdateControl;
        private TextBox textBoxTestText;
        private Button buttonMoveCam;
        private Button buttonResetCam;
        private CheckBox checkBoxCam;
        private CheckBox checkBoxCursor;
        private CheckBox checkBoxFPS;
        private TrackBar trackBarCamZoom;
        private TabPage tabPageWelcome;
        private Button buttonEdit;
        private TrackBar trackBarLogoFrames;
        private TabPage tabPageInfo;
        private StatusStrip statusStrip;
        private ToolStripDropDownButton toolStripDropDownButtonGitHub;
        private RichTextBox richTextBoxLicense;
        private Button buttonHelp;
        private ToolStripDropDownButton toolStripDropDownButtonWiki;
        private Samples.Tests.Welcome welcomeControl;
        private TabPage tabPageAdvancedInput;
        private Samples.Tests.AdvancedInputTest advancedControlsTest;
        private Button buttonResetPlayer;
        private CheckBox checkBoxShowHelp;
        private CheckBox checkBoxShowStats;
        private TabPage tabPageMultipleControls;
        private Samples.Tests.MultipleControls_a_Test multipleControls_First_Test1;
        private Samples.Tests.MultipleControls_b_Test multipleControls_Second_Test1;
        private Button buttonHelpControls;
        private Button buttonHelpInput;
        private SplitContainer splitContainerMapHost;
        private Panel panelInvalidation;
        private Button buttonInvalidate;
        private Samples.Tests.InvalidationTest invalidationTestControl;
        private Samples.Tests.MonoGameTest monoGameTestControl;
    }
}