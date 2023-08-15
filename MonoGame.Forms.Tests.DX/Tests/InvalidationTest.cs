using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace MonoGame.Forms.Samples.Tests
{
    public class InvalidationTest : InvalidationControl
    {
        public string WelcomeMessage = "This is an InvalidationControl without a real game loop (0 fps),\nbut it's updated through invalidation (i.e. Invalidate();).\n\nTry it with the text box below and press the button to invalidate the rendering!";
        
        SpriteFont DrawFont;

        protected override void Initialize()
        {
            DrawFont = Editor.Content.Load<SpriteFont>("DrawFont");

            Editor.ShowCursorPosition = false;
        }

        protected override void Draw()
        {
            Editor.spriteBatch.Begin();

            //Shadow
            Editor.spriteBatch.DrawString(DrawFont, WelcomeMessage, new Vector2(
                (Editor.GraphicsDevice.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2) + 1,
                (Editor.GraphicsDevice.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + 1),
                Color.Black);

            //Text
            Editor.spriteBatch.DrawString(DrawFont, WelcomeMessage, new Vector2(
                (Editor.GraphicsDevice.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2),
                (Editor.GraphicsDevice.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2)),
                Color.Yellow);

            Editor.spriteBatch.End();
        }
    }
}
