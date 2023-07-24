using Microsoft.Xna.Framework;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public class MultipleControls_b_Test : MapHost
    {
        protected override void Initialize()
        {
            base.Initialize();

            InitializeMap("b");

            Editor.SetDisplayStyle = NET.Services.GFXService.DisplayStyle.TopRight;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            DrawMap();
        }
    }
}
