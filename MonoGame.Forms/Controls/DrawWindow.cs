using MonoGame.Forms.Services;
using System.ComponentModel;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// This control is selectable in the tool box of the designer.
    /// It just provides a place to draw. It has no game loop, but it's updated through invalidation.
    /// You need to place this control onto a <see cref="System.Windows.Forms.Form"/>.
    /// </summary>
    [DesignTimeVisible(true)]
    public class DrawWindow : GraphicsDeviceControl
    {
        /// <summary>
        /// The <see cref="DrawService"/> of the <see cref="DrawWindow"/> draws the actual content of the draw control.
        /// Attach here your custom 'Editor', which should inherit from <see cref="DrawService"/> to be attachable.
        /// </summary>
        public DrawService Editor
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
                Invalidate();
            }
        }
    }
}
