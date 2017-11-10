using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    public class DrawService : GFXService
    {
        public DrawService() { }

        internal DrawService(IGraphicsDeviceService graphics)
        {
            // Initialize GFX-System
            InitializeGFX(graphics);
        }

        public virtual void Initialize() { }

        public virtual void Draw()
        {
            graphics.Clear(BackgroundColor);
        }
    }
}
