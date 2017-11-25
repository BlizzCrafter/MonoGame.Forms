using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of MonoGame.
    /// The <see cref="DrawWindow"/> inherits from this class.
    /// <remarks>Note: this class provides no game loop. Only the <see cref="UpdateService"/> deliveres one.</remarks>
    /// </summary>
    public sealed class DrawService : GFXService
    {
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
        public override void Update(GameTime gameTime, Vector2 relativeMousePosition, Vector2 absoluteMousePosition)
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
