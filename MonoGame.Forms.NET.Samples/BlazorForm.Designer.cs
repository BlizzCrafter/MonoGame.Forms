namespace MonoGame.Forms.NET.Samples
{
    partial class BlazorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlazorForm));
            blazorWebView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            monoGameControlPanel = new Tests.Container.MonoGameControlPanel();
            SuspendLayout();
            // 
            // blazorWebView
            // 
            blazorWebView.Dock = DockStyle.Top;
            blazorWebView.Location = new Point(0, 0);
            blazorWebView.Name = "blazorWebView";
            blazorWebView.Size = new Size(1262, 137);
            blazorWebView.TabIndex = 0;
            blazorWebView.Text = "blazorWebView1";
            // 
            // monoGameControlPanel
            // 
            monoGameControlPanel.Dock = DockStyle.Fill;
            monoGameControlPanel.Location = new Point(0, 137);
            monoGameControlPanel.Name = "monoGameControlPanel";
            monoGameControlPanel.Size = new Size(1262, 536);
            monoGameControlPanel.TabIndex = 2;
            // 
            // BlazorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(monoGameControlPanel);
            Controls.Add(blazorWebView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BlazorForm";
            Text = "MonoGame.Forms.NET + Blazor";
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView;
        private Tests.Container.MonoGameControlPanel monoGameControlPanel;
    }
}