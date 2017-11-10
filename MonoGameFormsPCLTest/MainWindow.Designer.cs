namespace MonoGameFormsPCLTest
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
            this.tabControlEditorSwitch = new System.Windows.Forms.TabControl();
            this.tabPageDrawForm = new System.Windows.Forms.TabPage();
            this.textBoxTestText = new System.Windows.Forms.TextBox();
            this.tabPageUpdateForm = new System.Windows.Forms.TabPage();
            this.trackBarCamZoom = new System.Windows.Forms.TrackBar();
            this.checkBoxCam = new System.Windows.Forms.CheckBox();
            this.checkBoxCursor = new System.Windows.Forms.CheckBox();
            this.checkBoxFPS = new System.Windows.Forms.CheckBox();
            this.buttonResetCam = new System.Windows.Forms.Button();
            this.buttonMoveCam = new System.Windows.Forms.Button();
            this.drawWindow = new MonoGame.Forms.Controls.DrawWindow();
            this.updateWindow = new MonoGame.Forms.Controls.UpdateWindow();
            this.tabControlEditorSwitch.SuspendLayout();
            this.tabPageDrawForm.SuspendLayout();
            this.tabPageUpdateForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlEditorSwitch
            // 
            this.tabControlEditorSwitch.Controls.Add(this.tabPageDrawForm);
            this.tabControlEditorSwitch.Controls.Add(this.tabPageUpdateForm);
            this.tabControlEditorSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEditorSwitch.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditorSwitch.Name = "tabControlEditorSwitch";
            this.tabControlEditorSwitch.SelectedIndex = 0;
            this.tabControlEditorSwitch.Size = new System.Drawing.Size(736, 428);
            this.tabControlEditorSwitch.TabIndex = 0;
            // 
            // tabPageDrawForm
            // 
            this.tabPageDrawForm.Controls.Add(this.textBoxTestText);
            this.tabPageDrawForm.Controls.Add(this.drawWindow);
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
            this.textBoxTestText.Location = new System.Drawing.Point(3, 374);
            this.textBoxTestText.Name = "textBoxTestText";
            this.textBoxTestText.Size = new System.Drawing.Size(722, 22);
            this.textBoxTestText.TabIndex = 2;
            this.textBoxTestText.Text = "Edit Me!";
            this.textBoxTestText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxTestText.TextChanged += new System.EventHandler(this.textBoxTestText_TextChanged);
            // 
            // tabPageUpdateForm
            // 
            this.tabPageUpdateForm.Controls.Add(this.trackBarCamZoom);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxCam);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxCursor);
            this.tabPageUpdateForm.Controls.Add(this.checkBoxFPS);
            this.tabPageUpdateForm.Controls.Add(this.buttonResetCam);
            this.tabPageUpdateForm.Controls.Add(this.buttonMoveCam);
            this.tabPageUpdateForm.Controls.Add(this.updateWindow);
            this.tabPageUpdateForm.Location = new System.Drawing.Point(4, 25);
            this.tabPageUpdateForm.Name = "tabPageUpdateForm";
            this.tabPageUpdateForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpdateForm.Size = new System.Drawing.Size(728, 399);
            this.tabPageUpdateForm.TabIndex = 1;
            this.tabPageUpdateForm.Text = "Update Form";
            this.tabPageUpdateForm.UseVisualStyleBackColor = true;
            // 
            // trackBarCamZoom
            // 
            this.trackBarCamZoom.LargeChange = 1;
            this.trackBarCamZoom.Location = new System.Drawing.Point(664, 6);
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
            this.checkBoxCam.Checked = true;
            this.checkBoxCam.CheckState = System.Windows.Forms.CheckState.Checked;
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
            // drawWindow
            // 
            this.drawWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawWindow.Editor = null;
            this.drawWindow.Location = new System.Drawing.Point(3, 3);
            this.drawWindow.Name = "drawWindow";
            this.drawWindow.Size = new System.Drawing.Size(722, 393);
            this.drawWindow.TabIndex = 0;
            this.drawWindow.Text = "This is a simple draw test. It uses the following control:";
            this.drawWindow.VisibleChanged += new System.EventHandler(this.drawWindow_VisibleChanged);
            // 
            // updateWindow
            // 
            this.updateWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateWindow.Editor = null;
            this.updateWindow.Location = new System.Drawing.Point(3, 3);
            this.updateWindow.Name = "updateWindow";
            this.updateWindow.Size = new System.Drawing.Size(722, 393);
            this.updateWindow.TabIndex = 0;
            this.updateWindow.Text = "This is a simple update test. It uses the following control:";
            this.updateWindow.VisibleChanged += new System.EventHandler(this.updateWindow_VisibleChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(736, 428);
            this.Controls.Add(this.tabControlEditorSwitch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonoGame.Forms";
            this.tabControlEditorSwitch.ResumeLayout(false);
            this.tabPageDrawForm.ResumeLayout(false);
            this.tabPageDrawForm.PerformLayout();
            this.tabPageUpdateForm.ResumeLayout(false);
            this.tabPageUpdateForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlEditorSwitch;
        private System.Windows.Forms.TabPage tabPageDrawForm;
        private System.Windows.Forms.TabPage tabPageUpdateForm;
        private MonoGame.Forms.Controls.DrawWindow drawWindow;
        private System.Windows.Forms.TextBox textBoxTestText;
        private MonoGame.Forms.Controls.UpdateWindow updateWindow;
        private System.Windows.Forms.Button buttonMoveCam;
        private System.Windows.Forms.Button buttonResetCam;
        private System.Windows.Forms.CheckBox checkBoxCam;
        private System.Windows.Forms.CheckBox checkBoxCursor;
        private System.Windows.Forms.CheckBox checkBoxFPS;
        private System.Windows.Forms.TrackBar trackBarCamZoom;
    }
}

