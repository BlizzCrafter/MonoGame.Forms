using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using MonoGameFormsPCLTest.Utils;

namespace MonoGameFormsPCLTest.Tests
{
    public class Welcome : UpdateService
    {
        string WelcomeMessage = "Welcome to MonoGame.Forms!";
        public Animation Logo;
        public bool EditMode = false;
        public int LastFrame = 0;

        public override void Initialize()
        {
            base.Initialize();

            Logo = new Animation(Content.Load<Texture2D>("Logo_Sheet"), 10, 10, 0.5f, true, true);
            BackgroundColor = new Color(20, 19, 40);
        }

        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition, ref bool leftMouseButtonPressed, ref bool rightMouseButtonPressed, ref bool middleMouseButtonPressed)
        {
            base.Update(gameTime, relativeMousePosition, absoluteMousePosition, ref leftMouseButtonPressed, ref rightMouseButtonPressed, ref middleMouseButtonPressed);
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin();

            spriteBatch.Draw(Logo.Texture, new Rectangle(
                       graphics.Viewport.Width / 2, 
                       graphics.Viewport.Height / 2,
                       Logo.PartSizeX,
                       Logo.PartSizeY),
                       (EditMode ? Logo.GetCurrentFrame() : Logo.DoAnimation()),
                       Logo.GetDrawingColor, 
                       0f, 
                       Logo.GetOrigin, 
                       SpriteEffects.None, 0f);

            spriteBatch.DrawString(Font, WelcomeMessage, new Microsoft.Xna.Framework.Vector2(
                (graphics.Viewport.Width / 2) - (Font.MeasureString(WelcomeMessage).X / 2),
                (graphics.Viewport.Height / 2) - (Font.MeasureString(WelcomeMessage).Y / 2) + Logo.GetOrigin.Y + 10),
                Color.White);

            spriteBatch.End();

            DrawDisplay();
        }
    }
}
