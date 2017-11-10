using Microsoft.Xna.Framework;
using MonoGame.Forms.Services;

namespace MonoGame.Forms.Controls
{
    public class UpdateWindow : GameControl
    {
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

        protected override void Initialize()
        {
            base.Initialize();

            _Editor = new UpdateService(_graphicsDeviceService, SwapChainRenderTarget);
            _Editor.Initialize();
        }

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

        protected override void Draw()
        {
            if (_Editor != null) _Editor.Draw();
        }
    }
}
