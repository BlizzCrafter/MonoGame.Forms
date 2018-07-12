using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if GL
using MonoGame.Forms.GL;
#endif

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of MonoGame.
    /// The <see cref="MonoGame.Forms.Controls.UpdateWindow"/> inherits from this class.
    /// <remarks>Note: this class provides a game loop. The <see cref="DrawService"/> is not using a game loop.</remarks>
    /// </summary>
    public sealed class UpdateService : GFXService
    {
#if DX
        internal UpdateService(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX_DX(graphics, swapChainRenderTarget);
        }
#elif GL
        internal UpdateService(IGraphicsDeviceService graphics, SwapChainRenderTarget_GL swapChainRenderTarget)
        {
            // Initialize GFX-System
            InitializeGFX_GL(graphics, swapChainRenderTarget);
        }
#endif

        /// <summary>
        /// Override this basic intitializing method in your custom class to create your own initializing logic.
        /// </summary>
        public override void Initialize() { }

        /// <summary>
        /// Override this basic updating method in your custom class to create your own initializing logic.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> of the game loop.</param>
        public override void Update(GameTime gameTime)
        {
            UpdateDisplay(gameTime);
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
