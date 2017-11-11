using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// Extend from <see cref="UpdateService"/> in your custom class to attach basic initializing, updating and drawing logic to it.
    /// Note: this class provides a game loop.
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of MonoGame.
    /// </summary>
    public class UpdateService : GFXService
    {
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public UpdateService() { }

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
        /// <param name="gameTime">The GameTime of the game loop.</param>
        /// <param name="relativeMousePosition">The mouse position relative to the dimensions of the control.</param>
        /// <param name="absoluteMousePosition">The absolute mouse position relative to the dimensions of the client area.</param>
        /// <param name="leftMouseButtonPressed">Set this value to false after using it to clear the state correctly! It checks if the left mouse button was pressed.</param>
        /// <param name="rightMouseButtonPressed">Set this value to false after using it to clear the state correctly! It checks if the right mouse button was pressed.</param>
        /// <param name="middleMouseButtonPressed">Set this value to false after using it to clear the state correctly! It checks if the middle mouse button was pressed.</param>
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
