using System;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Services;

namespace MonoGame.Forms.NET.Controls
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
        public InvalidationService Editor { get { return _Editor; } }
        private InvalidationService _Editor;

        /// <summary>
        /// Basic initializing.
        /// </summary>
        protected override void Initialize()
        {
            _Editor = new InvalidationService(Services, SwapChainRenderTarget);

            SwapChainRenderTargetRefreshed -= DrawWindow_UpdateSwapChainRenderTarget;
            SwapChainRenderTargetRefreshed += DrawWindow_UpdateSwapChainRenderTarget;
            MultiSampleCountRefreshed -= DrawWindow_UpdateMultiSampleCount;
            MultiSampleCountRefreshed += DrawWindow_UpdateMultiSampleCount;

            _Editor.Initialize();
        }

        private void DrawWindow_UpdateSwapChainRenderTarget(SwapChainRenderTarget obj)
        {
            if (_Editor != null) _Editor.SwapChainRenderTarget = obj;
        }

        private void DrawWindow_UpdateMultiSampleCount(int obj)
        {
            if (_Editor != null) _Editor.GetCurrentMultiSampleCount = obj;
        }

        /// <summary>
        /// Basic drawing.
        /// This control becomes updated though invalidation: <see cref="System.Windows.Forms.Control.Invalidate()"/>
        /// </summary>
        protected override void Draw()
        {
            if (_Editor != null)
            {
                UpdateMousePositions();
                Editor.UpdateMousePositions(GetRelativeMousePosition, GetAbsoluteMousePosition);

                _Editor.Draw();
            }
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
                _Editor?.Dispose();

                SwapChainRenderTargetRefreshed -= DrawWindow_UpdateSwapChainRenderTarget;
                MultiSampleCountRefreshed -= DrawWindow_UpdateMultiSampleCount;
            }
        }
    }
}
