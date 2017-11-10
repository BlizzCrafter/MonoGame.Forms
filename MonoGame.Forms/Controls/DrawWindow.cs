using MonoGame.Forms.Services;

namespace MonoGame.Forms.Controls
{
    public class DrawWindow : GraphicsDeviceControl
    {
        public DrawService Editor
        {
            get { return _Editor; }
            set
            {
                if (value != null)
                {
                    _Editor = value;
                    _Editor.InitializeGFX(_graphicsDeviceService);
                    _Editor.Initialize();
                }
            }
        }
        private DrawService _Editor;

        protected override void Initialize()
        {
            _Editor = new DrawService(_graphicsDeviceService);
            _Editor.Initialize();
        }

        protected override void Draw()
        {
            if (_Editor != null)
            {
                _Editor.Draw();
                Invalidate();
            }
        }
    }
}
