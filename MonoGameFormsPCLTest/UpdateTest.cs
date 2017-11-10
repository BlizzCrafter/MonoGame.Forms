using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;

namespace MonoGameFormsPCLTest
{
    public class UpdateTest : UpdateService
    {
        Texture2D SmileyMap;
        
        public override void Initialize()
        {
            base.Initialize();

            SmileyMap = Content.Load<Texture2D>("SmileyMap");
        }

        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition, ref bool leftMouseButtonPressed, ref bool rightMouseButtonPressed, ref bool middleMouseButtonPressed)
        {
            base.Update(gameTime, relativeMousePosition, absoluteMousePosition, ref leftMouseButtonPressed, ref rightMouseButtonPressed, ref middleMouseButtonPressed);

            if (leftMouseButtonPressed)
            {
                leftMouseButtonPressed = false;
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

            EndCamera2D();

            DrawDisplay();
        }
    }
}
