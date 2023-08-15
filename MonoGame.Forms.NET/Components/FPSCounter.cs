using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Components.Interfaces;
using MonoGame.Forms.NET.Services;
using System.Globalization;

using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGame.Forms.NET.Components
{
    public class FPSCounter : IGameComponent, IUpdateable, IDrawable
    {
        public bool Visible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public int DrawOrder { get; } = int.MaxValue;
        public int UpdateOrder { get; } = 0;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        /// DisplayStyle enumerations for the integrated display.
        /// </summary>
        public enum DisplayStyle
        {
            /// <summary>
            /// Draws the integrated display in the top left corner of the custom control.
            /// </summary>
            TopLeft,
            /// <summary>
            /// Draws the integrated display in the top right corner of the custom control.
            /// </summary>
            TopRight
        }
        /// <summary>
        /// Directly sets the <see cref="DisplayStyle"/> of the integrated display.
        /// </summary>
        public DisplayStyle SetDisplayStyle { get; set; } = DisplayStyle.TopLeft;

        /// <summary>
        /// Show or hide the 'FPS' (frames per second) of the corresponding control / window.
        /// </summary>
        public bool ShowFPS { get; set; } = true;

        /// <summary>
        /// Show or hide the 'cursor position' of the corresponding control / window.
        /// </summary>
        public bool ShowCursorPosition { get; set; } = true;

        /// <summary>
        /// Show or hide the 'cam position' of the corresponding control / window.
        /// </summary>
        public bool ShowCamPosition { get; set; } = false;

        /// <summary>
        /// This formats the fps style.
        /// </summary>
        private NumberFormatInfo Format { get; set; }
        /// <summary>
        /// The elapsed <see cref="GameTime"/>.
        /// </summary>
        private TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// The frame counter used by the fps display.
        /// </summary>
        private int FrameCounter { get; set; }
        /// <summary>
        /// Get the current frames per second (FPS).
        /// </summary>
        public int GetFrameRate { get; private set; }

        /// <summary>
        /// Set the back color of the integrated display.
        /// </summary>
        public Color DisplayBackColor { get; set; } = new Color(0, 0, 0, 100);

        /// <summary>
        /// Set the font color of the integrated display.
        /// </summary>
        public Color DisplayForeColor { get; set; } = Color.White;

        /// <summary>
        /// Updates the frame counter (FPS).
        /// </summary>
        internal void UpdateFrameCounter() => FrameCounter++;

        private EditorService _Editor;
        private ICamera2D _Camera;

        public FPSCounter(EditorService editor, ICamera2D camera) 
        {
            _Editor = editor;
            _Camera = camera;
        }

        public void Initialize()
        {
            Format = new NumberFormatInfo();
            Format.CurrencyDecimalSeparator = ".";
        }

        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                ElapsedTime += gameTime.ElapsedGameTime;
                if (ElapsedTime <= TimeSpan.FromSeconds(1)) return;
                ElapsedTime -= TimeSpan.FromSeconds(1);
                GetFrameRate = FrameCounter;
                FrameCounter = 0;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                if (ShowFPS || ShowCursorPosition || ShowCamPosition)
                {
                    _Editor.spriteBatch.Begin(SpriteSortMode.BackToFront);

                    float MaxHeight = -_Editor.FontHeight;

                    float FPSWidth = 0;
                    float MouseWidth = 0;
                    float CamWidth = 0;

                    if (ShowFPS)
                    {
                        FPSWidth = _Editor.Font.MeasureString(string.Format(Format, "{0} fps", GetFrameRate)).X;
                        MaxHeight += _Editor.FontHeight;

                        _Editor.spriteBatch.DrawString(_Editor.Font, string.Format(Format, "{0} fps", GetFrameRate), SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, 0) :
                            new Vector2(_Editor.GraphicsDevice.Viewport.Width - FPSWidth - 10, 0), DisplayForeColor,
                            0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else FPSWidth = 0;

                    if (ShowCursorPosition)
                    {
                        MouseWidth = _Editor.Font.MeasureString($"X:{_Editor.GetRelativeMousePosition.X} Y:{_Editor.GetRelativeMousePosition.Y}").X;
                        MaxHeight += _Editor.FontHeight;

                        _Editor.spriteBatch.DrawString(_Editor.Font, $"X:{_Editor.GetRelativeMousePosition.X} Y:{_Editor.GetRelativeMousePosition.Y}", SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, MaxHeight) :
                            new Vector2(_Editor.GraphicsDevice.Viewport.Width - MouseWidth - 10, MaxHeight), DisplayForeColor,
                            0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else MouseWidth = 0;

                    if (ShowCamPosition && _Camera != null)
                    {
                        CamWidth = _Editor.Font.MeasureString($"X:{_Camera.GetAbsolutePosition().X} Y:{_Camera.GetAbsolutePosition().Y}").X;
                        MaxHeight += _Editor.FontHeight;

                        _Editor.spriteBatch.DrawString(_Editor.Font, $"X:{_Camera.GetAbsolutePosition().X} Y:{_Camera.GetAbsolutePosition().Y}", SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, MaxHeight) :
                            new Vector2(_Editor.GraphicsDevice.Viewport.Width - CamWidth - 10, MaxHeight), DisplayForeColor,
                            0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else CamWidth = 0;
                    MaxHeight += _Editor.FontHeight;

                    float MaxWidth = Math.Max(FPSWidth, Math.Max(MouseWidth, CamWidth));

                    _Editor.spriteBatch.Draw(_Editor.Pixel, SetDisplayStyle == DisplayStyle.TopLeft ?
                        new Rectangle(0, 0, (int)MaxWidth + 20, (int)MaxHeight + 5) :
                        new Rectangle(_Editor.GraphicsDevice.Viewport.Width - (int)MaxWidth - 20, 0, (int)MaxWidth + 20, (int)MaxHeight + 5),
                        null, DisplayBackColor, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

                    _Editor.spriteBatch.End();
                }
            }
        }
    }
}
