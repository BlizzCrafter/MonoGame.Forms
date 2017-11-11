using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// This control is selectable in the tool box of the designer.
    /// It provides a game loop and a place to draw.
    /// You need to place this control onto a <see cref="System.Windows.Forms.Form"/>.
    /// </summary>
    public class UpdateWindow : GameControl
    {
        /// <summary>
        /// The <see cref="UpdateService"/> of the <see cref="UpdateWindow"/> draws and updates the actual content of the update control.
        /// Attach here your custom 'Editor', which should inherit from <see cref="UpdateService"/> to be attachable.
        /// </summary>
        public UpdateService Editor
        {
            get { return _Editor; }
            set
            {
                if (value != null)
                {
                    _Editor = value;
                    _Editor.InitializeGFX(_graphicsDeviceService, SwapChainRenderTarget);
                    _Editor.Initialize();
                }
            }
        }
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
                GetAbsoluteMousePosition,
                ref LeftMouseButtonPressed,
                ref RightMouseButtonPressed,
                ref MiddleMouseButtonPressed);
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
