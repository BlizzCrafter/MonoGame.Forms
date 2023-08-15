using MonoGame.Forms.Services;
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// Inherit from this class in your custom class to create an invalidation control, which is selectable from the ToolBox during design time.
    /// It provides 'NO' game loop, but it's updated through invalidation (<see cref="System.Windows.Forms.Control.Invalidate()"/>).
    /// You need to call 'Invalidate()' on a custom control by yourself to update its contents.
    /// <remarks>This control is useful as a simple CPU gentle control, which doesn't need a classical game loop like a preview window for textures.</remarks>
    /// </summary>
    public abstract class InvalidationControl : GraphicsDeviceControl
    {
        /// <summary>
        /// The <see cref="InvalidationService"/> of the <see cref="InvalidationControl"/> draws the actual content of the draw control.
        /// </summary>
        [Browsable(false)]
        public InvalidationService Editor { get; private set; }

        /// <summary>
        /// Basic initializing.
        /// </summary>
        internal override void InternalInitialize()
        {
            Editor = new InvalidationService(Services, SwapChainRenderTarget);

            SwapChainRenderTargetRefreshed -= DrawWindow_UpdateSwapChainRenderTarget;
            SwapChainRenderTargetRefreshed += DrawWindow_UpdateSwapChainRenderTarget;
            MultiSampleCountRefreshed -= DrawWindow_UpdateMultiSampleCount;
            MultiSampleCountRefreshed += DrawWindow_UpdateMultiSampleCount;

            Editor.InternalInitialize();
            Initialize();
        }

        /// <summary>
        /// Basic drawing.
        /// This control becomes updated though invalidation: <see cref="System.Windows.Forms.Control.Invalidate()"/>
        /// </summary>
        internal override void InternalDraw()
        {
            if (Editor != null)
            {
                UpdateMousePositions();
                Editor.UpdateMousePositions(GetRelativeMousePosition, GetAbsoluteMousePosition);
                Editor.InternalDraw();
                Draw();
                DrawComponents(new GameTime());
            }
        }

        private void DrawWindow_UpdateSwapChainRenderTarget(SwapChainRenderTarget obj)
        {
            if (Editor != null) Editor.SwapChainRenderTarget = obj;
        }

        private void DrawWindow_UpdateMultiSampleCount(int obj)
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
        /// In case the ClientSize was changed before activating the control, the cam position gets updated according to this changes.
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

                SwapChainRenderTargetRefreshed -= DrawWindow_UpdateSwapChainRenderTarget;
                MultiSampleCountRefreshed -= DrawWindow_UpdateMultiSampleCount;
            }
        }

        protected abstract void Initialize();
        protected abstract void Draw();
    }
}
