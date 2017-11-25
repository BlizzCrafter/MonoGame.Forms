using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of MonoGame.
    /// The <see cref="UpdateWindow"/> inherits from this class.
    /// <remarks>Note: this class provides a game loop. The <see cref="DrawService"/> is not using a game loop.</remarks>
    /// </summary>
    public sealed class UpdateService : GFXService
    {
        internal UpdateService(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX(graphics, swapChainRenderTarget);
        }

        /// <summary>
        /// Override this basic intitializing method in your custom class to create your own initializing logic.
        /// </summary>
        public override void Initialize() { }

        /// <summary>
        /// Override this basic updating method in your custom class to create your own initializing logic.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> of the game loop.</param>
        /// <param name="relativeMousePosition">The mouse position relative to the dimensions of the control.</param>
        /// <param name="absoluteMousePosition">The absolute mouse position relative to the dimensions of the client area.</param>
        public override void Update(
            GameTime gameTime,
            Vector2 relativeMousePosition,
            Vector2 absoluteMousePosition)
        {
            UpdateDisplay(gameTime, relativeMousePosition);
        }

        /// <summary>
        /// Override this basic drawing method in your custom class to create your own drawing logic.
        /// This basic implementation just clears the background color of the draw control in the predefined color: <see cref="GFXService.BackgroundColor"/>
        /// and updates the FrameCounter, which shows the current FPS of the window / control.
        /// </summary>
        public override void Draw()
        {
            UpdateFrameCounter();

            graphics.Clear(BackgroundColor);
        }
    }
}
