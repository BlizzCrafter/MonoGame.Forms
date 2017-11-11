using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;

namespace MonoGameFormsPCLTest.Tests
{
    public class DrawTest : DrawService
    {
        public string WelcomeMessage = "This is a draw window without a real game loop,\nbut it's updated through invalidation.\n\nTry it with the text box below!";

        SpriteFont DrawFont;

        public override void Initialize()
        {
            base.Initialize();

            DrawFont = Content.Load<SpriteFont>("DrawFont");
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin();

            //Shadow
            spriteBatch.DrawString(DrawFont, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2) + 1,
                (graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + 1),
                Color.Black);

            //Text
            spriteBatch.DrawString(DrawFont, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2),
                (graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2)),
                Color.Yellow);

            spriteBatch.End();
        }
    }
}
