using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using System.Windows.Forms;

namespace MonoGame.Forms.Tests.Tests
{
    public class UpdateTest : UpdateService
    {
        public string WelcomeMessage = "Everything in this world is magic and nothing can exist without magic!";

        Texture2D SmileyMap;
        SpriteFont DrawFont;
        
        public override void Initialize()
        {
            base.Initialize();

            SmileyMap = Content.Load<Texture2D>("SmileyMap");
            DrawFont = Content.Load<SpriteFont>("DrawFont");
        }

        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition, ref bool leftMouseButtonPressed, ref bool rightMouseButtonPressed, ref bool middleMouseButtonPressed)
        {
            base.Update(gameTime, relativeMousePosition, absoluteMousePosition, ref leftMouseButtonPressed, ref rightMouseButtonPressed, ref middleMouseButtonPressed);

            if (leftMouseButtonPressed)
            {
                leftMouseButtonPressed = false;
                MessageBox.Show("[Left_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (rightMouseButtonPressed)
            {
                rightMouseButtonPressed = false;
                MessageBox.Show("[Right_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (middleMouseButtonPressed)
            {
                middleMouseButtonPressed = false;
                MessageBox.Show("[Middle_Mouse_Button] pressed on control!", "Test_Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public override void Draw()
        {
            base.Draw();

            BeginCamera2D();

            spriteBatch.Draw(SmileyMap, new Vector2(
                (graphics.Viewport.Width / 2) - (SmileyMap.Width / 2), 
                (graphics.Viewport.Height / 2) - (SmileyMap.Height / 2)),
                Color.White);

            //Shadow
            spriteBatch.DrawString(DrawFont, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2) + 1,
                (graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + SmileyMap.Height + 1),
                Color.Black);

            //Text
            spriteBatch.DrawString(DrawFont, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (DrawFont.MeasureString(WelcomeMessage).X / 2),
                (graphics.Viewport.Height / 2) - (DrawFont.MeasureString(WelcomeMessage).Y / 2) + SmileyMap.Height),
                Color.Yellow);

            EndCamera2D();

            DrawDisplay();
        }
    }
}
