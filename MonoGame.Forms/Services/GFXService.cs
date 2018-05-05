﻿using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;
using System.Timers;

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// The <see cref="GFXService"/> class provides basic functionality of MonoGame
    /// </summary>
    public abstract class GFXService
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
        public GameServiceContainer services { get; private set; }
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
                
        internal RenderTarget2D AntialisingRenderTarget { get; set; }
        private void CreateAntialisingRenderTarget(Vector2 size)
        {
            AntialisingRenderTarget = new RenderTarget2D(graphics,
                    (int)size.X, 
                    (int)size.Y,
                    false, SurfaceFormat.Color, DepthFormat.Depth24, _CurrentMultiSampleCount > 0 ? _CurrentMultiSampleCount : 0, RenderTargetUsage.DiscardContents);
        }
        internal void RefreshAntiAlisingRenderTarget(SwapChainRenderTarget obj, int multiSampleCount = -1)
        {
            if (AntialisingRenderTarget == null) return;

            IsRefreshingAntialisingRenderTarget = !IsRefreshingAntialisingRenderTarget;

            AntialisingRenderTarget.Dispose();

            if (multiSampleCount > 0) _CurrentMultiSampleCount = multiSampleCount;

            CreateAntialisingRenderTarget(new Vector2(obj.Width, obj.Height));

            IsRefreshingAntialisingRenderTarget = !IsRefreshingAntialisingRenderTarget;
        }
        private bool IsRefreshingAntialisingRenderTarget = false;
        /// <summary>
        /// Get the current active MultiSampleCount.
        /// </summary>
        public int GetCurrentMultiSampleCount { get { return _CurrentMultiSampleCount; } }
        private int _CurrentMultiSampleCount = -1;

        /// <summary>
        /// Subscribe to this event in your custom control to get <see cref="ResizeStart"/> events.
        /// <remarks>
        /// Note: This event doesn't work like the resizing event of a Form. 
        /// It can trigger multiple times and is maybe not the right choice for you - depending on what you are trying to achieve.
        /// It can be useful if you don't want to trigger some events very often, but from time to time in the <see cref="ResizeStart"/> event block for example.
        /// </remarks>
        /// </summary>
        public event Action<SwapChainRenderTarget> ResizeStart = delegate { };
        internal void InvokeResizeStart()
        {
            ResizeStart?.Invoke(SwapChainRenderTarget);
        }
        /// <summary>
        /// Subscribe to this event in your custom control to get <see cref="ResizeEnd"/> events.
        /// <remarks>
        /// Note: This event doesn't work like the resizing event of a Form. 
        /// It can trigger multiple times and is maybe not the right choice for you - depending on what you are trying to achieve.
        /// It can be useful if you don't want to trigger some events very often, but from time to time in the <see cref="ResizeEnd"/> event block for example.
        /// </remarks>
        /// </summary>
        public event Action<SwapChainRenderTarget> ResizeEnd = delegate { };
        internal void InvokeResizeEnd()
        {
            ResizeEnd?.Invoke(SwapChainRenderTarget);
        }
        internal void OnResizeEnd()
        {
            Timer.Stop();

            ResizeStarted = false;

            RefreshAntiAlisingRenderTarget(SwapChainRenderTarget);
            InvokeResizeEnd();
        }
        internal bool ResizeStarted = false, ResizeEnded = false;
        internal Timer Timer { get; private set; }

        /// <summary>
        /// Get the current mouse position in the control.
        /// </summary>
        public System.Drawing.Point GetRelativeMousePosition { get; private set; }

        /// <summary>
        /// Get the current mouse position.
        /// </summary>
        public System.Drawing.Point GetAbsoluteMousePosition { get; private set; }
        
        internal void UpdateMousePositions(System.Drawing.Point relativeMousePosition, System.Drawing.Point absoluteMousePosition)
        {
            GetRelativeMousePosition = relativeMousePosition;
            GetAbsoluteMousePosition = absoluteMousePosition;
        }

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
        /// Get the current frames per second (FPS).
        /// </summary>
        public int GetFrameRate { get; private set; }

        /// <summary>
        /// A plain white pixel mainly to draw the background of the integrated display, but you can also use it in your custom control.
        /// </summary>
        public Texture2D Pixel { get; set; }

        /// <summary>
        /// Set the back color of the integrated display.
        /// </summary>
        public Color DisplayBackColor { get; set; } = new Color(0, 0, 0, 100);

        /// <summary>
        /// Set the font color of the integrated display.
        /// </summary>
        public Color DisplayForeColor { get; set; } = Color.White;

        /// <summary>
        /// Height of the display Font - Cached in InitializeGFX().
        /// </summary>
        public float FontHeight { get; private set; }

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

            CreateAntialisingRenderTarget(new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height));

            Content = new ContentManager(services, "Content");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            
            InternContent = new ResourceContentManager(services, Properties.Resources.ResourceManager);
            Font = InternContent.Load<SpriteFont>("Font");
            FontHeight = Font.MeasureString("A").Y;

            Pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            Format = new System.Globalization.NumberFormatInfo();
            Format.CurrencyDecimalSeparator = ".";

            Cam = new Camera2D();
            Cam.GetPosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            Timer = new Timer();
            Timer.Interval = 500;
            Timer.Elapsed += (sender, e) => OnResizeEnd();
        }

        /// <summary>
        /// Updates the frame counter (FPS).
        /// </summary>
        internal void UpdateFrameCounter() => FrameCounter++;

        /// <summary>
        /// Updates the integrated display.
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> from the game loop.</param>
        internal void UpdateDisplay(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime;
            if (ElapsedTime <= TimeSpan.FromSeconds(1)) return;
            ElapsedTime -= TimeSpan.FromSeconds(1);
            GetFrameRate = FrameCounter;
            FrameCounter = 0;
        }

        /// <summary>
        /// Draws the integrated display.
        /// </summary>
        public void DrawDisplay()
        {
            if (ShowFPS || ShowCursorPosition || ShowCamPosition)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront);

                float MaxHeight = -FontHeight;

                float FPSWidth = 0;
                float MouseWidth = 0;
                float CamWidth = 0;

                if (ShowFPS)
                {
                    FPSWidth = Font.MeasureString(string.Format(Format, "{0} fps", GetFrameRate)).X;
                    MaxHeight += FontHeight;

                    spriteBatch.DrawString(Font, string.Format(Format, "{0} fps", GetFrameRate), SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, 0) :
                        new Vector2(graphics.Viewport.Width - FPSWidth - 10, 0), DisplayForeColor,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                else FPSWidth = 0;

                if (ShowCursorPosition)
                {
                    MouseWidth = Font.MeasureString($"X:{GetRelativeMousePosition.X} Y:{GetRelativeMousePosition.Y}").X;
                    MaxHeight += FontHeight;

                    spriteBatch.DrawString(Font, $"X:{GetRelativeMousePosition.X} Y:{GetRelativeMousePosition.Y}", SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, MaxHeight) :
                        new Vector2(graphics.Viewport.Width - MouseWidth - 10, MaxHeight), DisplayForeColor,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                else MouseWidth = 0;

                if (ShowCamPosition)
                {
                    CamWidth = Font.MeasureString($"X:{Cam.GetAbsolutPosition.X} Y:{Cam.GetAbsolutPosition.Y}").X;
                    MaxHeight += FontHeight;

                    spriteBatch.DrawString(Font, $"X:{Cam.GetAbsolutPosition.X} Y:{Cam.GetAbsolutPosition.Y}", SetDisplayStyle == DisplayStyle.TopLeft ? new Vector2(10, MaxHeight) :
                        new Vector2(graphics.Viewport.Width - CamWidth - 10, MaxHeight), DisplayForeColor,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                else CamWidth = 0;

                MaxHeight += FontHeight;
                
                float MaxWidth = Math.Max(FPSWidth, Math.Max(MouseWidth, CamWidth));

                spriteBatch.Draw(Pixel, SetDisplayStyle == DisplayStyle.TopLeft ?
                    new Rectangle(0, 0, (int)MaxWidth + 20, (int)MaxHeight + 5) :
                    new Rectangle(graphics.Viewport.Width - (int)MaxWidth - 20, 0, (int)MaxWidth + 20, (int)MaxHeight + 5), 
                    null, DisplayBackColor, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

                spriteBatch.End();
            }
        }

        /// <summary>
        /// Everything between <c>BeginAntialising()</c> and <c>EndAntialising()</c> will be affected by MSAA.
        /// </summary>
        /// <example>
        /// <code>
        /// protected override void Draw()
        /// {
        ///    base.Draw();
        ///    
        ///    Editor.BeginAntialising();
        ///
        ///    Editor.spriteBatch.Begin();
        ///
        ///    //Your drawings
        ///
        ///    Editor.spriteBatch.End();
        ///
        ///    Editor.EndAntialising();
        /// }
        /// </code>
        /// </example>
        public void BeginAntialising()
        {
            if (AntialisingRenderTarget == null || IsRefreshingAntialisingRenderTarget) return;

            graphics.SetRenderTarget(AntialisingRenderTarget);
            graphics.Clear(BackgroundColor);
        }

        /// <summary>
        /// Everything between <c>BeginAntialising()</c> and <c>EndAntialising()</c> will be affected by MSAA.
        /// <remarks>Ending the Antialising will automatically draw the result to the <see cref="SpriteBatch"/>.</remarks>
        /// </summary>
        public void EndAntialising()
        {
            if (AntialisingRenderTarget == null || IsRefreshingAntialisingRenderTarget) return;

            graphics.SetRenderTarget(SwapChainRenderTarget);
            graphics.Clear(BackgroundColor);

            spriteBatch.Begin();
            spriteBatch.Draw(AntialisingRenderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
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
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Basic drawing service.
        /// </summary>
        public abstract void Draw();
    }
}
