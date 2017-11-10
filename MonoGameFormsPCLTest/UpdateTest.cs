using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;

namespace MonoGameFormsPCLTest
{
    public class UpdateTest : UpdateService
    {
        public string WelcomeMessage = "Hello MonoGames.Forms (Updatable)!";

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime, Vector2 mousePosition)
        {
            base.Update(gameTime, mousePosition);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            BeginCamera2D();

            spriteBatch.DrawString(Font, WelcomeMessage, new Vector2(
                (graphics.Viewport.Width / 2) - (Font.MeasureString(WelcomeMessage).X / 2),
                (graphics.Viewport.Height / 2) - (Font.MeasureString(WelcomeMessage).Y / 2)),
                Color.White);

            EndCamera2D();
        }
    }
}
