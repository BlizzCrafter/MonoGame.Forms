using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// This class mainly creates the <see cref="GraphicsDeviceControl.GraphicsDevice"/>, the <see cref="GraphicsDeviceControl.SwapChainRenderTarget"/> and the game loop.
    /// It inherits from <see cref="GraphicsDeviceControl"/>, which makes its childs available as a tool box control.
    /// </summary>
    public abstract class GameControl : GraphicsDeviceControl
    {
        internal GameTime _GameTime;
        internal Stopwatch _Timer;
        internal TimeSpan _Elapsed;

        /// <summary>
        /// Set this to 'true' to only update this control when the mouse cursor is inside (OnMouseHover).
        /// <remarks>
        /// This technique is useful when you only need to update this control temporarily or always on demand.
        /// Setting this property to 'true' will cause that this control will only consume CPU power to update its contents, when the mouse cursor is inside it
        /// or when you call 'RunOneFrame();' manually.
        /// </remarks>
        /// </summary>
        public bool MouseHoverUpdatesOnly { get; set; } = false;
        /// <summary>
        /// Runs exactly one frame by internally calling 'Invalidate();' one single time.
        /// This will run the game loop only once and immediately shows the result.
        /// </summary>
        public void RunOneFrame() => Invalidate();

        /// <summary>
        /// Basic initializing of the game control.
        /// It starts a <see cref="Stopwatch"/> and creates the mouse events.
        /// </summary>
        protected override void Initialize()
        {
            if (_Timer == null) _Timer = Stopwatch.StartNew();

            Application.Idle -= GameLoop;
            Application.Idle += GameLoop;
        }

        /// <summary>
        /// Basic drawing logic.
        /// </summary>
        protected override void Draw()
        {
            Draw();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (Visible)
            {
                _GameTime = new GameTime(_Timer.Elapsed, _Timer.Elapsed - _Elapsed);
                _Elapsed = _Timer.Elapsed;

                UpdateMousePositions();

                Update(_GameTime);
#if DX
                if (MouseHoverUpdatesOnly && IsMouseInsideControl) Invalidate();
                else if (!MouseHoverUpdatesOnly) Invalidate();
#endif
            }
        }

        /// <summary>
        /// Basic update logic.
        /// You must implement this to your custom class, so you can write your own update logic for the game loop.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> reflects the current time of the game loop</param>
        protected abstract void Update(GameTime gameTime);
    }
}