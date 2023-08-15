using Microsoft.Xna.Framework;
using static MonoGame.Forms.NET.Components.FPSCounter;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public class MultipleControls_b_Test : MapHost
    {
        protected override void Initialize()
        {
            InitializeMap("b");

            Editor.FPSCounter.SetDisplayStyle = DisplayStyle.TopRight;
        }

        protected override void Update(GameTime gameTime) { }

        protected override void Draw()
        {
            DrawMap();
        }
    }
}
