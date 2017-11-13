using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System.Windows.Forms;

namespace MonoGame.Forms.Tests.Tests
{
    public class UpdateTest : UpdateWindow
    {
        public string WelcomeMessage = "Everything in this world is magic and nothing can exist without magic!";

        Texture2D SmileyMap;
        SpriteFont DrawFont;
        
        protected override void Initialize()
        {
            base.Initialize();

            SmileyMap = Editor.Content.Load<Texture2D>("SmileyMap");
            DrawFont = Editor.Content.Load<SpriteFont>("DrawFont");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (LeftMouseButtonPressed)
            {
                LeftMouseButtonPressed = false;
                MessageBox.Show("[Left_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (RightMouseButtonPressed)
            {
                RightMouseButtonPressed = false;
                MessageBox.Show("[Right_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (MiddleMouseButtonPressed)
            {
                MiddleMouseButtonPressed = false;
                MessageBox.Show("[Middle_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.BeginCamera2D();

            Editor.spriteBatch.Draw(SmileyMap, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (SmileyMap.Width / 2), 
                (Editor.graphics.Viewport.Height / 2) - (SmileyMap.Height / 2)),
                Color.White);

            //Shadow
            Editor.spriteBatch.DrawString(DrawFont, WelcomeMessage, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2) + 1,
                (Editor.graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + SmileyMap.Height + 1),
                Color.Black);

            //Text
            Editor.spriteBatch.DrawString(DrawFont, WelcomeMessage, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2),
                (Editor.graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + SmileyMap.Height),
                Color.Yellow);

            Editor.EndCamera2D();

            Editor.DrawDisplay();
        }
    }
}
