using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    public class UpdateService : GFXService
    {
        public UpdateService() { }

        internal UpdateService(IGraphicsDeviceService graphics)
        {
            // Initialize GFX-System
            InitializeGFX(graphics);
        }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime, Vector2 mousePosition)
        {
            UpdateDisplay(gameTime, mousePosition);
        }

        public virtual void Draw(GameTime gameTime)
        {
            UpdateFrameCounter();

            graphics.Clear(BackgroundColor);

            DrawDisplay();
        }
    }
}
