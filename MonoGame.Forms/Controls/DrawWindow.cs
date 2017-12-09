using MonoGame.Forms.Services;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// Inherit from this class in your custom class to create a draw control, which is selectable from the ToolBox during design time.
    /// It provides 'NO' game loop, but it's updated through invalidation (<see cref="System.Windows.Forms.Control.Invalidate()"/>).
    /// <remarks>This draw control is useful as a simple window, which doesn't need a classical game loop like a preview window for textures.</remarks>
    /// </summary>
    public abstract class DrawWindow : GraphicsDeviceControl
    {
        /// <summary>
        /// The <see cref="DrawService"/> of the <see cref="DrawWindow"/> draws the actual content of the draw control.
        /// </summary>
        public DrawService Editor { get { return _Editor; } }
        private DrawService _Editor;

        /// <summary>
        /// Basic initializing.
        /// </summary>
        protected override void Initialize()
        {
            _Editor = new DrawService(_graphicsDeviceService, SwapChainRenderTarget);
            _Editor.Initialize();
        }

        /// <summary>
        /// Basic drawing.
        /// The draw control becomes updated though invalidation: <see cref="System.Windows.Forms.Control.Invalidate()"/>
        /// </summary>
        protected override void Draw()
        {
            if (_Editor != null)
            {
                _Editor.Draw();
                GetKeyboardState = KeyboardService.GetState();

                Invalidate();
            }
        }
    }
}
