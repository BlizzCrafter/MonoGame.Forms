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
    public abstract class UpdateWindow : GameControl
    {
        /// <summary>
        /// The <see cref="UpdateService"/> of the <see cref="UpdateWindow"/> draws and updates the actual content of the update control.
        /// </summary>
        [Browsable(false)]
        public UpdateService Editor { get { return _Editor; } }
        private UpdateService _Editor;

        /// <summary>
        /// Basic initializing.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            _Editor = new UpdateService(_graphicsDeviceService, SwapChainRenderTarget);
            _Editor.Initialize();

            UpdateSwapChainRenderTarget += UpdateWindow_UpdateSwapChainRenderTarget;
            UpdateMultiSampleCount += UpdateWindow_UpdateMultiSampleCount;
        }

        private void UpdateWindow_UpdateSwapChainRenderTarget(Microsoft.Xna.Framework.Graphics.SwapChainRenderTarget obj)
        {
            _Editor.SwapChainRenderTarget = obj;
        }

        private void UpdateWindow_UpdateMultiSampleCount(int obj)
        {
            _Editor.GetCurrentMultiSampleCount = obj;
        }

        /// <summary>
        /// Basic updating.
        /// It uses a real game loop, represented by <see cref="GameTime"/>.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (_Editor != null)
            {
                Editor.UpdateMousePositions(GetRelativeMousePosition, GetAbsoluteMousePosition);
                _Editor.Update(gameTime);
            }
        }

        /// <summary>
        /// Basic drawing.
        /// </summary>
        protected override void Draw()
        {
            if (_Editor != null) _Editor.Draw();
        }

        /// <summary>
        /// Updates the camera position according to the changed <see cref="System.Windows.Forms.Control.ClientSize"/>.
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

            if (Editor != null) Editor.CamHoldPosition(ClientSize);
        }

        /// <summary>
        /// Disposes the contents of the attached Editor.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _Editor?.Dispose();
            UpdateSwapChainRenderTarget -= UpdateWindow_UpdateSwapChainRenderTarget;
            UpdateMultiSampleCount -= UpdateWindow_UpdateMultiSampleCount;
        }
    }
}
