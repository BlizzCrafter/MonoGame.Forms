using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace MonoGame.Forms.Tests.Tests
{
    public class DrawTest : DrawWindow
    {
        public string WelcomeMessage = "This is a draw window without a real game loop,\nbut it's updated through invalidation.\n\nTry it with the text box below!";
        
        SpriteFont DrawFont;
        
        public bool GetAutoInvalidation
        {
            get { return AutomaticInvalidation; }
            set { AutomaticInvalidation = value; }
        }

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
                (Editor.graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2) + 1,
                (Editor.graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + 1),
                Color.Black);

            //Text
            Editor.spriteBatch.DrawString(DrawFont, WelcomeMessage, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2),
                (Editor.graphics.Viewport.Height / 2) - (Editor.FontHeight / 2)),
                Color.Yellow);

            Editor.spriteBatch.End();
        }
    }
}
