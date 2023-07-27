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

        public string WelcomeActive { get; set; } = "";
        public string InvalidationActive { get; set; } = "";
        public string MonoGameActive { get; set; } = "";
        public string AdvancedInputActive { get; set; } = "";
        public string MultipleActive { get; set; } = "";
        public string InfoActive { get; set; } = "";

        public Control InitializeMonoGameControl(ControlKeys key)
        {
            HideMonoGameControls();

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
            }
            else control.Visible = true;
            return control;
        }

        private void HideMonoGameControls()
        {
            foreach (Control control in Controls)
            {
                control.Visible = false;
            }
        }
    }
}
