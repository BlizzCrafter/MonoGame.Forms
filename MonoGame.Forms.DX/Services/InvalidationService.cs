using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// This class inherits from <see cref="EditorService"/>, which provides basic functionality of the MonoGame.Framework.
    /// The <see cref="MonoGame.Forms.Controls.InvalidationControl"/> inherits from this class.
    /// <remarks>Note: this class provides no game loop. Only the <see cref="MonoGameService"/> deliveres one.</remarks>
    /// </summary>
    public sealed class InvalidationService : EditorService
    {
        internal InvalidationService(GameServiceContainer services, SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize Service-System
            InitializeService(services, swapChainRenderTarget);
        }

        /// <summary>
        /// Override this basic intitializing method in your custom class to create your own initializing logic.
        /// </summary>
        internal override void InternalInitialize() { }

        /// <summary>
        /// Throws a <see cref="NotImplementedException"/>, because a <see cref="InvalidationService"/> class doesn't contain a game loop.
        /// This is a basic implementation of the corresponding abstract method from the <see cref="EditorService"/> class.
        /// </summary>
        internal override void InternalUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Override this basic drawing method in your custom class to create your own drawing logic.
        /// This basic implementation just clears the background color of the draw control in the predefined color: <see cref="EditorService.BackgroundColor"/>
        /// </summary>
        internal override void InternalDraw()
        {
            GraphicsDevice.Clear(BackgroundColor);
        }
    }
}
