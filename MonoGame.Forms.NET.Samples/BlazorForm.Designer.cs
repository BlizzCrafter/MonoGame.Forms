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
            splitContainer = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // blazorWebView
            // 
            blazorWebView.Dock = DockStyle.Fill;
            blazorWebView.Location = new Point(0, 0);
            blazorWebView.Name = "blazorWebView";
            blazorWebView.Size = new Size(1262, 124);
            blazorWebView.TabIndex = 0;
            blazorWebView.Text = "blazorWebView1";
            // 
            // monoGameControlPanel
            // 
            monoGameControlPanel.AdvancedInputActive = "";
            monoGameControlPanel.Dock = DockStyle.Fill;
            monoGameControlPanel.InfoActive = "";
            monoGameControlPanel.InvalidationActive = "";
            monoGameControlPanel.Location = new Point(0, 0);
            monoGameControlPanel.MonoGameActive = "";
            monoGameControlPanel.MultipleActive = "";
            monoGameControlPanel.Name = "monoGameControlPanel";
            monoGameControlPanel.Size = new Size(1262, 548);
            monoGameControlPanel.TabIndex = 2;
            monoGameControlPanel.WelcomeActive = "active";
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.FixedPanel = FixedPanel.Panel1;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(blazorWebView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(monoGameControlPanel);
            splitContainer.Size = new Size(1262, 673);
            splitContainer.SplitterDistance = 124;
            splitContainer.SplitterWidth = 1;
            splitContainer.TabIndex = 3;
            // 
            // BlazorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(splitContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BlazorForm";
            Text = "MonoGame.Forms.NET + Blazor";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView;
        private Tests.Container.MonoGameControlPanel monoGameControlPanel;
        private SplitContainer splitContainer;
    }
}