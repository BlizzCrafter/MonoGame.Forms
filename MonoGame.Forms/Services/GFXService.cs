using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// The <see cref="GFXService"/> class provides basic functionality of MonoGame
    /// </summary>
    public abstract class GFXService : IGFXInterface
    {
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
        /// The <see cref="ContentManager"/> is for loading custom content from the content root.
        /// </summary>
        public ContentManager Content { get; set; }
        private ContentManager InternContent { get; set; }

        /// <summary>
        /// The <see cref="GraphicsDevice"/>.
        /// </summary>
        public GraphicsDevice graphics { get; set; }
        /// <summary>
        /// The <see cref="GameServiceContainer"/>.
        /// </summary>
        public GameServiceContainer services { get; set; }
        /// <summary>
        /// The <see cref="SpriteBatch"/>.
        /// </summary>
        public SpriteBatch spriteBatch { get; set; }
        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// Note: When working with different <see cref="RenderTarget2D"/>, 
        /// you need to set the current render target back to the <see cref="SwapChainRenderTarget"/> as this is the real 'Back Buffer'. 
        /// 'GraphicsDevice.SetRenderTarget(null)' will NOT work as you are doing usally in MonoGame. Instead use 'GraphicsDevice.SetRenderTarget(SwapChainRenderTarget)'.
        /// Otherwise you will see only a black control window.
        /// <remarks>This is an extension and not part of stock XNA. It is currently implemented for Windows and DirectX only.</remarks>
        /// </summary>
        public SwapChainRenderTarget SwapChainRenderTarget { get; set; }

        /// <summary>
        /// Gets the current mouse position in the control.
        /// </summary>
        public System.Drawing.Point GetMousePosition { get; set; }

        /// <summary>
        /// The Camera2D component.
        /// </summary>
        public Camera2D Cam { get; set; }
        private float CurrentWorldShiftX { get; set; }
        private float CurrentWorldShiftY { get; set; }

        /// <summary>
        /// The color used to clear the screen / control with <see cref="GraphicsDevice.Clear(Color)"/>
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.CornflowerBlue;

        //Display
        /// <summary>
        /// A built-in font, which is used by the integrated display. You can also use it as debugging font for example.
        /// </summary>
        public SpriteFont Font { get; set; }
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
        /// The actual frames per second (FPS).
        /// </summary>
        private int FrameRate { get; set; }

        /// <summary>
        /// Set the font color of the integrated display.
        /// </summary>
        public Color DisplayColor { get; set; } = Color.White;

        /// <summary>
        /// Height of the display Font - Cached in InitializeGFX().
        /// </summary>
        private float FontHeight;

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
        /// Initializes the GFX system, which contains basic MonoGame functionality.
        /// </summary>
        /// <param name="graphics">The graphics device service</param>
        /// <param name="swapChainRenderTarget">The swap chain render target</param>
        public void InitializeGFX(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget)
        {
            services = new GameServiceContainer();
            services.AddService<IGraphicsDeviceService>(graphics);

            this.graphics = graphics.GraphicsDevice;
            SwapChainRenderTarget = swapChainRenderTarget;

            Content = new ContentManager(services, "Content");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            
            InternContent = new ResourceContentManager(services, Properties.Resources.ResourceManager);
            Font = InternContent.Load<SpriteFont>("Font");
            FontHeight = Font.MeasureString("A").Y;

            Format = new System.Globalization.NumberFormatInfo();
            Format.CurrencyDecimalSeparator = ".";

            Cam = new Camera2D();
            Cam.GetPosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// Updates the frame counter (FPS).
        /// </summary>
        public void UpdateFrameCounter() => FrameCounter++;

        /// <summary>
        /// Updates the integrated display.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> from the game loop.</param>
        /// <param name="mousePosition">The mouse position.</param>
        public void UpdateDisplay(GameTime gameTime, System.Drawing.Point mousePosition)
        {
            GetMousePosition = mousePosition;

            ElapsedTime += gameTime.ElapsedGameTime;
            if (ElapsedTime <= TimeSpan.FromSeconds(1)) return;
            ElapsedTime -= TimeSpan.FromSeconds(1);
            FrameRate = FrameCounter;
            FrameCounter = 0;
        }

        /// <summary>
        /// Draws the integrated display in the upper left corner.
        /// </summary>
        public void DrawDisplay()
        {
            if (ShowFPS || ShowCursorPosition || ShowCamPosition)
            {
                spriteBatch.Begin();

                // Draw FPS display
                //FPS
                if (ShowFPS) spriteBatch.DrawString(Font, string.Format(Format, "{0} fps", FrameRate), SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(5, 0) : 
                    new Vector2(graphics.Viewport.Width - Font.MeasureString(string.Format(Format, "{0} fps", FrameRate)).X - 5, 0), DisplayColor);
                //Cursor Position
                if (ShowCursorPosition) spriteBatch.DrawString(Font, GetMousePosition.ToString(), SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(5, ShowFPS ? FontHeight : 0) : 
                    new Vector2(graphics.Viewport.Width - Font.MeasureString(GetMousePosition.ToString()).X - 5, ShowFPS ? FontHeight : 0), DisplayColor);
                //Cam Position
                if (ShowCamPosition) spriteBatch.DrawString(Font, Cam.GetAbsolutPosition.ToString(), SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(5, ShowFPS && !ShowCursorPosition ? FontHeight : ShowFPS && ShowCursorPosition ? FontHeight * 2 : 0) : 
                    new Vector2(graphics.Viewport.Width - Font.MeasureString(Cam.GetAbsolutPosition.ToString()).X - 5, ShowFPS && !ShowCursorPosition ? FontHeight : ShowFPS && ShowCursorPosition ? FontHeight * 2 : 0), DisplayColor);

                spriteBatch.End();
            }
        }

        /// <summary>
        /// Use 'BeginCamera2D' as a replacement of <see cref="SpriteBatch"/>.Begin(<see cref="SpriteSortMode"/>, <see cref="BlendState"/>, <see cref="SamplerState"/>, <see cref="DepthStencilState"/>, <see cref="RasterizerState"/>, <see cref="Effect"/>, <see cref="Matrix"/>?).
        /// <remarks>Automatically uses the <see cref="Matrix"/> of the Camera2D component!</remarks>
        /// </summary>
        /// <param name="sortMode">Defines sprite sort rendering options.</param>
        /// <param name="blendState">The blend state.</param>
        /// <param name="samplerState">The sampler state.</param>
        /// <param name="depthStencilState">The depth stencil state.</param>
        /// <param name="rasterizerState">The rasterizer state.</param>
        /// <param name="effect">The effect.</param>
        public void BeginCamera2D(
            SpriteSortMode sortMode = SpriteSortMode.Deferred, 
            BlendState blendState = null, 
            SamplerState samplerState = null, 
            DepthStencilState depthStencilState = null, 
            RasterizerState rasterizerState = null, 
            Effect effect = null)
        {
            spriteBatch.Begin(sortMode,
                        blendState,
                        samplerState,
                        depthStencilState,
                        rasterizerState,
                        effect,
                        Cam.get_transformation(graphics));
        }

        /// <summary>
        /// Use this to end the <see cref="SpriteBatch"/>, previously opened by <see cref="BeginCamera2D"/>.
        /// </summary>
        public void EndCamera2D()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// Move the camera by the value defined in the parameter amount.
        /// </summary>
        /// <param name="amount">How much should the camera move?</param>
        public void MoveCam(Vector2 amount)
        {
            Cam.Move(new Vector2(amount.X, amount.Y));
            CurrentWorldShiftX += amount.X;
            CurrentWorldShiftY += amount.Y;
        }

        /// <summary>
        /// Resets all the values from the camera component to their defaults.
        /// </summary>
        public void ResetCam()
        {
            Cam.Move(new Vector2(-CurrentWorldShiftX, -CurrentWorldShiftY));
            CurrentWorldShiftX = 0;
            CurrentWorldShiftY = 0;
            Cam.GetZoom = 1f;
            Cam.GetRotation = 0f;

            Cam.GetPosition = new Vector2(
                graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
        }
        
        internal void CamHoldPosition(System.Drawing.Size newClientSize)
        {
            if (Cam != null && graphics != null)
            {
                Cam.GetPosition = new Vector2(newClientSize.Width / 2, newClientSize.Height / 2);

                float oldCamPoxX = CurrentWorldShiftX;
                float oldCamPoxY = CurrentWorldShiftY;

                CurrentWorldShiftX = 0;
                CurrentWorldShiftY = 0;

                MoveCam(new Vector2(oldCamPoxX, oldCamPoxY));
            }
        }

        /// <summary>
        /// Basic initializing service.
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// Basic updating service.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> from the game loop.</param>
        /// <param name="relativeMousePosition">The mouse position relative to the dimensions of the control.</param>
        /// <param name="absoluteMousePosition">The absolute mouse position relative to the dimensions of the client area.</param>
        public abstract void Update(
            GameTime gameTime,
            System.Drawing.Point relativeMousePosition,
            System.Drawing.Point absoluteMousePosition);
        /// <summary>
        /// Basic drawing service.
        /// </summary>
        public abstract void Draw();
    }
}
