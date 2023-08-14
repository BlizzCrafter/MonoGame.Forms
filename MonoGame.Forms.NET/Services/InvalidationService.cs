﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.NET.Services
{
    /// <summary>
    /// This class inherits from <see cref="GFXService"/>, which provides basic functionality of the MonoGame.Framework.
    /// The <see cref="MonoGame.Forms.Controls.InvalidationControl"/> inherits from this class.
    /// <remarks>Note: this class provides no game loop. Only the <see cref="MonoGameService"/> deliveres one.</remarks>
    /// </summary>
    public sealed class InvalidationService : GFXService
    {
        internal InvalidationService(
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
        /// Throws a <see cref="NotImplementedException"/>, because a <see cref="InvalidationService"/> class doesn't contain a game loop.
        /// This is a basic implementation of the corresponding abstract method from the <see cref="GFXService"/> class.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Override this basic drawing method in your custom class to create your own drawing logic.
        /// This basic implementation just clears the background color of the draw control in the predefined color: <see cref="GFXService.BackgroundColor"/>
        /// </summary>
        public override void Draw()
        {
            GraphicsDevice.Clear(BackgroundColor);
        }
    }
}
