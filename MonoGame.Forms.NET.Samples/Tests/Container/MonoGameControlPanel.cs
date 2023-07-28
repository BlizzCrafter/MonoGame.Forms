using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.NET.Samples.Tests.Container
{
    public class MonoGameControlPanel : Panel
    {
        public enum ControlKeys
        {
            Welcome,
            Invalidation,
            MonoGame,
            AdvancedInput,
            Multiple,
            Info
        }

        public string WelcomeActive { get; set; } = "active";
        public string InvalidationActive { get; set; } = "";
        public string MonoGameActive { get; set; } = "";
        public string AdvancedInputActive { get; set; } = "";
        public string MultipleActive { get; set; } = "";
        public string InfoActive { get; set; } = "";

        public Control InitializeMonoGameControl(ControlKeys key)
        {
            ResetControls();

            var control = Controls[key.ToString()];
            if (control == null)
            {
                if (key == ControlKeys.Welcome)
                {
                    Controls.Add(control = new Welcome() { Name = key.ToString(), Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.HiDef, Width = Width, Height = Height });
                }
                else if (key == ControlKeys.Invalidation)
                {
                    Controls.Add(control = new InvalidationTest() { Name = key.ToString(), Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.Reach, Width = Width, Height = Height });
                }
                else if (key == ControlKeys.MonoGame)
                {
                    Controls.Add(control = new MonoGameTest() { Name = key.ToString(), Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.Reach, Width = Width, Height = Height });
                }
                else if (key == ControlKeys.AdvancedInput)
                {
                    Controls.Add(control = new AdvancedInputTest() { Name = key.ToString(), Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.Reach, Width = Width, Height = Height });
                }
                else if (key == ControlKeys.Multiple)
                {
                    Controls.Add(control = new SplitContainer() { Name = key.ToString(), Dock = DockStyle.Fill });
                    ((SplitContainer)control).Panel1.Controls.Add(new MultipleControls_a_Test() { Name = $"{key}_a", Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.Reach, Width = Width, Height = Height });
                    ((SplitContainer)control).Panel2.Controls.Add(new MultipleControls_b_Test() { Name = $"{key}_b", Dock = DockStyle.Fill, GraphicsProfile = GraphicsProfile.Reach, Width = Width, Height = Height });
                }
                else if (key == ControlKeys.Info)
                {
                    var root = Parent.Parent as SplitContainer;
                    root.Panel2Collapsed = true;
                }
            }
            else control.Visible = true;

            return control;
        }

        private void ResetControls()
        {
            var root = Parent.Parent as SplitContainer;
            root.Panel2Collapsed = false;

            foreach (Control control in Controls)
            {
                control.Visible = false;
            }
        }
    }
}
