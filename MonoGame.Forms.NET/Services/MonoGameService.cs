﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.NET.Services
{
    /// <summary>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of the MonoGame.Framework.
    /// The <see cref="MonoGame.Forms.Controls.MonoGameControl"/> inherits from this class.
    /// <remarks>Note: this class provides a game loop. The <see cref="InvalidationService"/> is not using a game loop.</remarks>
    /// </summary>
    public sealed class MonoGameService : GFXService
    {
        /// <summary>
        /// Get the current <see cref="GameTime"/> of the game loop.
        /// </summary>
        public GameTime GameTime { get; private set; }

        internal MonoGameService(
            GameServiceContainer services, 
            GameComponentCollection components, 
            SwapChainRenderTarget swapChainRenderTarget)
        {
            // Initialize Service-System
            InitializeService(services, components, swapChainRenderTarget);
        }

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
            FrameworkDispatcher.Update();
            GameTime = gameTime;
        }

        /// <summary>
        /// Override this basic drawing method in your custom class to create your own drawing logic.
        /// This basic implementation just clears the background color of the draw control in the predefined color: <see cref="GFXService.BackgroundColor"/>
        /// and updates the FrameCounter, which shows the current FPS of the window / control.
        /// </summary>
        public override void Draw()
        {
            FPSCounter?.UpdateFrameCounter();

            GraphicsDevice.Clear(BackgroundColor);
        }
    }
}
