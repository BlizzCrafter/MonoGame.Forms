using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    public class UpdateService : GFXService
    {
        public UpdateService() { }

        internal UpdateService(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX(graphics, swapChainRenderTarget);
        }

        public override void Initialize() { }

        public override void Update(
            GameTime gameTime,
            Vector2 relativeMousePosition,
            Vector2 absoluteMousePosition,
            ref bool leftMouseButtonPressed,
            ref bool rightMouseButtonPressed,
            ref bool middleMouseButtonPressed)
        {
            UpdateDisplay(gameTime, relativeMousePosition);
        }

        public override void Draw()
        {
            UpdateFrameCounter();

            graphics.Clear(BackgroundColor);
        }
    }
}
