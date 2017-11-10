using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MonoGame.Forms.Controls
{
    public abstract class GameControl : GraphicsDeviceControl
    {
        GameTime gameTime;
        Stopwatch timer;
        TimeSpan elapsed;

        protected bool LeftMouseButtonPressed = false;

        protected Vector2 _MousePosition { get; set; }

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
        }

        private void GameControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) LeftMouseButtonPressed = true;
        }

        protected override void Draw()
        {
            Draw(gameTime);
        }

        private void GameLoop()
        {
            if (Visible)
            {
                gameTime = new GameTime(timer.Elapsed, timer.Elapsed - elapsed);
                elapsed = timer.Elapsed;

                System.Drawing.Point p = this.PointToClient(
                    new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y));

                if (ClientRectangle.Contains(p))
                {
                    _MousePosition = new Vector2(
                        MathHelper.Clamp(p.X, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Width),
                        MathHelper.Clamp(p.Y, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Height));
                }

                Update(gameTime);
                Invalidate();
            }
        }
        
        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);
    }
}