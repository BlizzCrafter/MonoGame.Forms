using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;
using System;
using System.ComponentModel;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// Inherit from this class in your custom class to create a draw control with a game loop, which is selectable from the ToolBox during design time.
    /// It provides a game loop and a place to draw.
    /// <remarks>This game loop control is useful as a window, which needs a classical game loop for complex <see cref="GameTime"/> based mechanics.</remarks>
    /// </summary>
    public abstract class MonoGameControl : GameControl
    {
        /// <summary>
        /// The <see cref="MonoGameService"/> of the <see cref="MonoGameControl"/> draws and updates the actual content of the update control.
        /// </summary>
        [Browsable(false)]
        public MonoGameService Editor { get; private set; }

        /// <summary>
        /// Basic initializing.
        /// </summary>
        internal override void InternalInitialize()
        {
            base.InternalInitialize();

            Editor = new MonoGameService(Services, SwapChainRenderTarget);

            SwapChainRenderTargetRefreshed -= UpdateWindow_UpdateSwapChainRenderTarget;
            SwapChainRenderTargetRefreshed += UpdateWindow_UpdateSwapChainRenderTarget;
            MultiSampleCountRefreshed -= UpdateWindow_UpdateMultiSampleCount;
            MultiSampleCountRefreshed += UpdateWindow_UpdateMultiSampleCount;

            Editor.InternalInitialize();
            Initialize();
        }

        /// <summary>
        /// Basic updating.
        /// It uses a real game loop, represented by <see cref="GameTime"/>.
        /// </summary>
        internal override void InternalUpdate(GameTime gameTime)
        {
            if (Editor != null)
            {
                Editor.UpdateMousePositions(GetRelativeMousePosition, GetAbsoluteMousePosition);
                Editor.InternalUpdate(gameTime);
                Update(gameTime);
            }
        }

        /// <summary>
        /// Basic drawing.
        /// </summary>
        internal override void InternalDraw()
        {
            if (Editor != null)
            {
                Editor.InternalDraw();
                Draw();
            }
        }

        private void UpdateWindow_UpdateSwapChainRenderTarget(Microsoft.Xna.Framework.Graphics.SwapChainRenderTarget obj)
        {
            if (Editor != null) Editor.SwapChainRenderTarget = obj;
        }

        private void UpdateWindow_UpdateMultiSampleCount(int obj)
        {
            if (Editor != null) Editor.GetCurrentMultiSampleCount = obj;
        }

        /// <summary>
        /// Updates related Editor services when the <see cref="System.Windows.Forms.Control.ClientSize"/> changes.
        /// </summary>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (Editor != null)
            {
                Editor.DisableRenderTargets();
                Editor.CamHoldPosition(ClientSize);
            }
        }

        /// <summary>
        /// In case the ClientSize was changed before activating the window, the cam position gets updated according to this changes.
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Editor != null)
            {
                Editor.CamHoldPosition(ClientSize);
            }
        }

        /// <summary>
        /// Disposes the contents of the attached Editor.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Editor?.Dispose();

                SwapChainRenderTargetRefreshed -= UpdateWindow_UpdateSwapChainRenderTarget;
                MultiSampleCountRefreshed -= UpdateWindow_UpdateMultiSampleCount;
            }
        }

        protected abstract void Initialize();
        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw();
    }
}
