using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;

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
        }

        /// <summary>
        /// Basic updating.
        /// It uses a real game loop, represented by <see cref="GameTime"/>.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (_Editor != null)
            {
                _Editor.Update(
                gameTime,
                GetRelativeMousePosition,
                GetAbsoluteMousePosition);
            }
        }

        /// <summary>
        /// Basic drawing.
        /// </summary>
        protected override void Draw()
        {
            if (_Editor != null) _Editor.Draw();
        }
    }
}
