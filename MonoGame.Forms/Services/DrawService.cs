using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    public class DrawService : GFXService
    {
        public DrawService() { }

        internal DrawService(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX(graphics, swapChainRenderTarget);
        }

        public override void Initialize() { }

        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition, ref bool leftMouseButtonPressed, ref bool rightMouseButtonPressed, ref bool middleMouseButtonPressed)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            graphics.Clear(BackgroundColor);
        }
    }
}
