using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Controls;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public class InvalidationTest : InvalidationControl
    {
        public string WelcomeMessage = "This is an InvalidationControl without a real game loop,\nbut it's updated through invalidation.\n\nTry it with the text box below and press the button to invalidate the rendering!";
        
        SpriteFont DrawFont;

        protected override void Initialize()
        {
            base.Initialize();

            DrawFont = Editor.Content.Load<SpriteFont>("DrawFont");
        }

        protected override void Draw()
        {
            base.Draw();

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
