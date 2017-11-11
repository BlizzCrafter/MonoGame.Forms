using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// This class mainly creates the <see cref="GraphicsDeviceControl.GraphicsDevice"/>, the <see cref="GraphicsDeviceControl.SwapChainRenderTarget"/> and the game loop.
    /// It inherits from <see cref="GraphicsDeviceControl"/>, which makes it available as a tool box control.
    /// You can drag and drop the control directly from the tool box in the designer.
    /// </summary>
    public abstract class GameControl : GraphicsDeviceControl
    {
        GameTime gameTime;
        Stopwatch timer;
        TimeSpan elapsed;

        /// <summary>
        /// Shows if the left mouse button was pressed.
        /// </summary>
        protected bool LeftMouseButtonPressed = false;
        /// <summary>
        /// Shows if the right mouse button was pressed.
        /// </summary>
        protected bool RightMouseButtonPressed = false;
        /// <summary>
        /// Shows if the middle mouse button was pressed.
        /// </summary>
        protected bool MiddleMouseButtonPressed = false;

        /// <summary>
        /// Get the relative mouse position as a <see cref="Vector2"/>
        /// </summary>
        protected Vector2 GetRelativeMousePosition { get; set; }
        /// <summary>
        /// Get the absolute mouse position as a <see cref="Vector2"/>
        /// </summary>
        protected Vector2 GetAbsoluteMousePosition { get; set; }

        /// <summary>
        /// Basic initializing of the game control.
        /// It starts a <see cref="Stopwatch"/> and creates the mouse events.
        /// </summary>
        protected override void Initialize()
        {
            timer = Stopwatch.StartNew();

            Application.Idle += delegate { GameLoop(); };

            MouseDown += GameControl_MouseDown;
            MouseUp += GameControl_MouseUp;
        }

        private void GameControl_MouseUp(object sender, MouseEventArgs e)
        {
            LeftMouseButtonPressed = false;
            RightMouseButtonPressed = false;
            MiddleMouseButtonPressed = false;
        }

        private void GameControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) LeftMouseButtonPressed = true;
            else if (e.Button == MouseButtons.Right) RightMouseButtonPressed = true;
            else if (e.Button == MouseButtons.Middle) MiddleMouseButtonPressed = true;
        }

        /// <summary>
        /// Basic drawing logic.
        /// </summary>
        protected override void Draw()
        {
            Draw();
        }

        private void GameLoop()
        {
            if (Visible)
            {
                gameTime = new GameTime(timer.Elapsed, timer.Elapsed - elapsed);
                elapsed = timer.Elapsed;

                System.Drawing.Point p = this.PointToClient(
                    new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y));

                GetAbsoluteMousePosition = new Vector2(Cursor.Position.X, Cursor.Position.Y);

                if (ClientRectangle.Contains(p))
                {
                    GetRelativeMousePosition = new Vector2(
                        MathHelper.Clamp(p.X, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Width),
                        MathHelper.Clamp(p.Y, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Height));
                }

                Update(gameTime);
                Invalidate();
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