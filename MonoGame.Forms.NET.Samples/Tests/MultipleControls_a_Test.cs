using Microsoft.Xna.Framework;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public class MultipleControls_a_Test : MapHost
    {
        protected override void Initialize()
        {
            InitializeMap("a");
        }

        protected override void Update(GameTime gameTime) { }

        protected override void Draw()
        {
            DrawMap();
        }
    }
}
