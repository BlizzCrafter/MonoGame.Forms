using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;

namespace MonoGameFormsPCLTest.Tests
{
    public class DrawTest : DrawService
    {
        public string WelcomeMessage = "Hello MonoGame.Forms!";

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin();

            spriteBatch.DrawString(Font, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (Font.MeasureString(WelcomeMessage).X / 2),
                (graphics.Viewport.Height / 2) - (Font.MeasureString(WelcomeMessage).Y / 2)),
                Color.White);

            spriteBatch.End();
        }
    }
}
