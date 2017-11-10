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
            this.drawWindow = new MonoGame.Forms.Controls.DrawWindow();
            this.tabPageUpdateForm = new System.Windows.Forms.TabPage();
            this.updateWindow = new MonoGame.Forms.Controls.UpdateWindow();
            this.buttonMoveCam = new System.Windows.Forms.Button();
            this.buttonResetCam = new System.Windows.Forms.Button();
            this.tabControlEditorSwitch.SuspendLayout();
            this.tabPageDrawForm.SuspendLayout();
            this.tabPageUpdateForm.SuspendLayout();
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
            // tabPageUpdateForm
            // 
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
            // buttonMoveCam
            // 
            this.buttonMoveCam.Location = new System.Drawing.Point(611, 342);
            this.buttonMoveCam.Name = "buttonMoveCam";
            this.buttonMoveCam.Size = new System.Drawing.Size(109, 49);
            this.buttonMoveCam.TabIndex = 1;
            this.buttonMoveCam.Text = "Move Cam";
            this.buttonMoveCam.UseVisualStyleBackColor = true;
            this.buttonMoveCam.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseDown);
            this.buttonMoveCam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseMove);
            this.buttonMoveCam.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonMoveCam_MouseUp);
            // 
            // buttonResetCam
            // 
            this.buttonResetCam.Location = new System.Drawing.Point(8, 342);
            this.buttonResetCam.Name = "buttonResetCam";
            this.buttonResetCam.Size = new System.Drawing.Size(109, 49);
            this.buttonResetCam.TabIndex = 2;
            this.buttonResetCam.Text = "Reset Cam";
            this.buttonResetCam.UseVisualStyleBackColor = true;
            this.buttonResetCam.Click += new System.EventHandler(this.buttonResetCam_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
    }
}

