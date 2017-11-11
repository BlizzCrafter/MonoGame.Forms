using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// Extend from <see cref="DrawService"/> in your custom class to attach basic initializing and drawing logic to it.
    /// Note: this class provides no game loop. If you need a real game loop, then please use the <see cref="UpdateService"/>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of MonoGame.
    /// </summary>
    public class DrawService : GFXService
    {
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public DrawService() { }

        internal DrawService(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX(graphics, swapChainRenderTarget);
        }

        /// <summary>
        /// Override this basic intitializing method in your custom class to create your own initializing logic.
        /// </summary>
        public override void Initialize() { }

        /// <summary>
        /// Throws a <see cref="NotImplementedException"/>, because a <see cref="DrawService"/> class doesn't contain a game loop.
        /// This is a basic implementation of the corresponding abstract method from the <see cref="GFXService"/> class.
        /// </summary>
        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition, ref bool leftMouseButtonPressed, ref bool rightMouseButtonPressed, ref bool middleMouseButtonPressed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Override this basic drawing method in your custom class to create your own drawing logic.
        /// This basic implementation just clears the background color of the draw control in the predefined color: <see cref="GFXService.BackgroundColor"/>
        /// </summary>
        public override void Draw()
        {
            graphics.Clear(BackgroundColor);
        }
    }
}
