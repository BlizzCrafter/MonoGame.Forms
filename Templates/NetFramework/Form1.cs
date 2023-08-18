using Editor.Controls;
using System;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // .NET-Framework notice:
            // We are adding MonoGameControls manually to avoid designer bugs.
            // If you want to work with the WindowsForms-Designer,
            // you should add the source code of MonoGame.Forms.DX to your
            // editor project to avoid such bugs.
            Controls.Add(new SampleControl() { Dock = DockStyle.Fill });
        }
    }
}
