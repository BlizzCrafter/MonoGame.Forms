using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;

#if GL
using MonoGame.Forms.GL;
#elif DX
using System.Linq;
using System.Timers;
using System.Collections.Generic;
#endif

namespace MonoGame.Forms.Services
{
    /// <summary>
    /// The <see cref="GFXService"/> class provides basic functionality of MonoGame
    /// </summary>
    public abstract class GFXService : IDisposable
    {
#if DX
        /// <summary>
        /// This manager will manage all of your custom <see cref="RenderTarget2D"/>'s automatically - based on the current ClientSize and MultiSampleCount. 
        /// </summary>
        public class RenderTargetManager : IDisposable
        {
            /// <summary>
            /// This helper class helps to hold additional <see cref="RenderTarget2D"/> data.
            /// </summary>
            public class RenderTarget2DHelper
            {
                private GFXService GetGFXService;

                /// <summary>
                /// Get the actual <see cref="RenderTarget2D"/>.
                /// </summary>
                public RenderTarget2D GetRenderTarget2D { get; set; }

                internal bool Enabled { get; set; } = false;
                internal bool IsRefreshing { get; set; } = false;
                private bool UseMultiSampling { get; set; } = false;

                private void CreateNewRenderTarget2D(bool useMultiSampling)
                {
                    GetRenderTarget2D = new RenderTarget2D(
                        GetGFXService.graphics,
                        GetGFXService.graphics.PresentationParameters.BackBufferWidth,
                        GetGFXService.graphics.PresentationParameters.BackBufferHeight,
                        false,
                        SurfaceFormat.Color,
                        DepthFormat.Depth24,
                        useMultiSampling ? GetGFXService.GetCurrentMultiSampleCount : 0,
                        RenderTargetUsage.DiscardContents);
                }
                
                internal RenderTarget2DHelper(GFXService _GFXService, bool useMultiSampling)
                {
                    GetGFXService = _GFXService;
                    UseMultiSampling = useMultiSampling;

                    CreateNewRenderTarget2D(useMultiSampling);
                }

                internal void RefreshRenderTarget2D()
                {
                    if (GetRenderTarget2D == null) return;

                    IsRefreshing = true;

                    GetRenderTarget2D.Dispose();

                    CreateNewRenderTarget2D(UseMultiSampling);

                    IsRefreshing = false;
                }
            }

            internal Dictionary<string, RenderTarget2DHelper> RenderTargets { get; set; }

            private GFXService GetGFXService;

            internal void RefreshRenderTargets()
            {
                RenderTargets.ToList().ForEach(x => x.Value.RefreshRenderTarget2D());
            }

            internal RenderTargetManager(GFXService _GFXService)
            {
                GetGFXService = _GFXService;
                RenderTargets = new Dictionary<string, RenderTarget2DHelper>();
            }

            /// <summary>
            /// Use this function to create a <see cref="RenderTarget2D"/>, which is fully managed internally.
            /// </summary>
            /// <param name="key">Set a key (name) for the new <see cref="RenderTarget2D"/>.</param>
            /// <param name="useMultiSampling"><c>true</c> if you want to use multi sampling on this <see cref="RenderTarget2D"/>.</param>
            /// <returns>The freshly created <see cref="RenderTarget2D"/>.</returns>
            public RenderTarget2DHelper CreateNewRenderTarget2D(string key, bool useMultiSampling)
            {
                if (RenderTargets.ContainsKey(key)) return RenderTargets[key];

                RenderTargets.Add(key, new RenderTarget2DHelper(GetGFXService, useMultiSampling));

                return RenderTargets[key];
            }

            /// <summary>
            /// Get a <see cref="RenderTarget2D"/> out of the Manager's list.
            /// </summary>
            /// <param name="key">Your previously set key (name) for the <see cref="RenderTarget2D"/> you want to get.</param>
            /// <returns>Your desired <see cref="RenderTarget2D"/> or <c>null</c> if the key is not availablbe.</returns>
            public RenderTarget2D GetRenderTarget2D(string key)
            {
                if (RenderTargets.ContainsKey(key)) return RenderTargets[key].GetRenderTarget2D;

                return null;
            }

            /// <summary>
            /// Disposes the <see cref="RenderTarget2D"/>s of the <see cref="RenderTargetManager"/>.
            /// </summary>
            public void Dispose()
            {
                RenderTargets.ToList().ForEach(x => x.Value.GetRenderTarget2D.Dispose());
            }
        }
        
        /// <summary>
        /// Get the internal <see cref="RenderTargetManager"/>.
        /// <remarks>
        /// When working with custom <see cref="RenderTarget2D"/>'s, it's strongly recomended to create these render targets with this RenderTargetManager,
        /// because they will updated automatically when the client size or the multi sample count changes.
        /// </remarks>
        /// </summary>
        public RenderTargetManager GetRenderTargetManager { get; private set; }

        /// <summary>
        /// Disable all custom <see cref="RenderTarget2D"/>'s hold by the <see cref="RenderTargetManager"/>, before they becoming reactivated after 500 milliseconds.
        /// </summary>
        public void DisableRenderTargets()
        {
            if (RenderTargetTimer != null)
            {
                GetRenderTargetManager.RenderTargets.ToList().ForEach(x => x.Value.Enabled = false);
                RenderTargetTimer.Start();
            }
        }
        internal void OnRenderTargetTimeOutEnd()
        {
            RenderTargetTimer.Stop();

            GetRenderTargetManager.RefreshRenderTargets();
            GetRenderTargetManager.RenderTargets.ToList().ForEach(x => x.Value.Enabled = true);

            RenderTargetsRefreshed?.Invoke();
        }
        internal Timer RenderTargetTimer { get; private set; }
        /// <summary>
        /// Subscribe to this event in your custom control to get notified when <see cref="RenderTarget2D"/>'s, 
        /// hold by the <see cref="RenderTargetManager"/>, got refreshed.
        /// </summary>
        public event Action RenderTargetsRefreshed = delegate { };

        internal RenderTargetManager.RenderTarget2DHelper AntialisingRenderTarget { get; set; }
        /// <summary>
        /// Get the current active MultiSampleCount.
        /// </summary>
        public int GetCurrentMultiSampleCount
        {
            get { return _CurrentMultiSampleCount; }
            internal set
            {
                _CurrentMultiSampleCount = value;
                GetRenderTargetManager.RefreshRenderTargets();
            }
        }
        private int _CurrentMultiSampleCount = 0;

        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// Note: When working with different <see cref="RenderTarget2D"/>, 
        /// you need to set the current render target back to the <see cref="SwapChainRenderTarget"/> as this is the real 'Back Buffer'. 
        /// 'GraphicsDevice.SetRenderTarget(null)' will NOT work as you are doing usally in MonoGame. Instead use 'GraphicsDevice.SetRenderTarget(SwapChainRenderTarget)'.
        /// Otherwise you will see only a black control window.
        /// </summary>
        public SwapChainRenderTarget SwapChainRenderTarget { get; set; }
#elif GL
        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// Note: When working with different <see cref="RenderTarget2D"/>, 
        /// you need to set the current render target back to the <see cref="SwapChainRenderTarget"/> as this is the real 'Back Buffer'. 
        /// 'GraphicsDevice.SetRenderTarget(null)' will NOT work as you are doing usally in MonoGame. Instead use 'GraphicsDevice.SetRenderTarget(SwapChainRenderTarget)'.
        /// Otherwise you will see only a black control window.
        /// </summary>
        public SwapChainRenderTarget_GL SwapChainRenderTarget { get; set; }
#endif

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
        /// Returns true when the mouse cursor is inside the control.
        /// </summary>
        public bool IsMouseInsideControl { get; internal set; }

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

#if DX
        /// <summary>
        /// Initializes the GFX system, which contains basic MonoGame functionality.
        /// </summary>
        /// <param name="graphics">The graphics device service</param>
        /// <param name="swapChainRenderTarget">The swap chain render target</param>
        public void InitializeGFX_DX(
            IGraphicsDeviceService graphics,
            SwapChainRenderTarget swapChainRenderTarget)
        {
            InitializeGFX(graphics);

            SwapChainRenderTarget = swapChainRenderTarget;
        }
#elif GL
        /// <summary>
        /// Initializes the GFX system, which contains basic MonoGame functionality.
        /// </summary>
        /// <param name="graphics">The graphics device service</param>
        /// <param name="swapChainRenderTarget">The swap chain render target</param>
        public void InitializeGFX_GL(
            IGraphicsDeviceService graphics,
            SwapChainRenderTarget_GL swapChainRenderTarget)
        {
            InitializeGFX(graphics);

            SwapChainRenderTarget = swapChainRenderTarget;
        }
#endif

        private void InitializeGFX(IGraphicsDeviceService graphics)
        {
            services = new GameServiceContainer();
            services.AddService<IGraphicsDeviceService>(graphics);
            this.graphics = graphics.GraphicsDevice;

            Content = new ContentManager(services, "Content");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            Pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            Format = new System.Globalization.NumberFormatInfo();
            Format.CurrencyDecimalSeparator = ".";

            Cam = new Camera2D();
            Cam.GetPosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
#if DX
            InternContent = new ResourceContentManager(services, DX.Properties.Resources.ResourceManager);

            GetRenderTargetManager = new RenderTargetManager(this);
            AntialisingRenderTarget = GetRenderTargetManager.CreateNewRenderTarget2D("MSAA", true);

            RenderTargetTimer = new Timer();
            RenderTargetTimer.Interval = 500;
            RenderTargetTimer.Elapsed += (sender, e) => OnRenderTargetTimeOutEnd();
#elif GL
            InternContent = new ResourceContentManager(services, GL.Properties.Resources.ResourceManager);
#endif
            Font = InternContent.Load<SpriteFont>("Font");
            FontHeight = Font.MeasureString("A").Y;
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

#if DX
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
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="graphics"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="graphics"/> after setting the <see cref="RenderTarget2D"/>.</param>
        public void BeginAntialising(bool clearGraphics = true, Color? clearColor = null)
        {
            if (AntialisingRenderTarget.GetRenderTarget2D == null ||
                AntialisingRenderTarget.IsRefreshing || 
                !AntialisingRenderTarget.Enabled) return;

            graphics.SetRenderTarget(AntialisingRenderTarget.GetRenderTarget2D);
            if (clearGraphics) graphics.Clear(clearColor ?? BackgroundColor);
        }

        /// <summary>
        /// Everything between <c>BeginAntialising()</c> and <c>EndAntialising()</c> will be affected by MSAA.
        /// </summary>
        /// <param name="drawToSpriteBatch"><c>true</c> to automatically draw the result to the <see cref="SpriteBatch"/>.</param>
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="graphics"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="graphics"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <returns>The Antialising <see cref="RenderTarget2D"/>.</returns>
        public RenderTarget2D EndAntialising(bool drawToSpriteBatch = true, bool clearGraphics = true, Color? clearColor = null)
        {
            if (AntialisingRenderTarget.GetRenderTarget2D == null ||
                AntialisingRenderTarget.IsRefreshing ||
                !AntialisingRenderTarget.Enabled) return null;

            graphics.SetRenderTarget(SwapChainRenderTarget);

            if (clearGraphics) graphics.Clear(clearColor ?? BackgroundColor);

            if (drawToSpriteBatch)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(AntialisingRenderTarget.GetRenderTarget2D, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            return AntialisingRenderTarget.GetRenderTarget2D;
        }

        /// <summary>
        /// Everything between <c>BeginRenderTarget()</c> and <c>EndRenderTarget()</c> will be drawn to the <see cref="RenderTarget2D"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// protected override void Draw()
        /// {
        ///    base.Draw();
        ///    
        ///    Editor.BeginRenderTarget("MyRenderTarget");
        ///
        ///    Editor.spriteBatch.Begin();
        ///
        ///    //Your drawings
        ///
        ///    Editor.spriteBatch.End();
        ///
        ///    Editor.EndRenderTarget("MyRenderTarget", false);
        /// }
        /// </code>
        /// </example>
        /// <param name="key">Please enter a previously set key of the <see cref="RenderTarget2D"/> you want to begin with.</param>
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="graphics"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="graphics"/> after setting the <see cref="RenderTarget2D"/>.</param>
        public void BeginRenderTarget(string key, bool clearGraphics = true, Color? clearColor = null)
        {
            if (GetRenderTargetManager.GetRenderTarget2D(key) == null ||
                GetRenderTargetManager.RenderTargets[key].IsRefreshing ||
                !GetRenderTargetManager.RenderTargets[key].Enabled) return;

            graphics.SetRenderTarget(GetRenderTargetManager.GetRenderTarget2D(key));
            if (clearGraphics) graphics.Clear(clearColor ?? BackgroundColor);
        }

        /// <summary>
        /// Everything between <c>BeginRenderTarget()</c> and <c>EndRenderTarget()</c> will be drawn to the <see cref="RenderTarget2D"/>.
        /// </summary>
        /// <param name="key">Please enter a previously set key of the <see cref="RenderTarget2D"/> you want to end.</param>
        /// <param name="drawToSpriteBatch"><c>true</c> to automatically draw the result to the <see cref="SpriteBatch"/>.</param>
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="graphics"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="graphics"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <returns>The resulting <see cref="RenderTarget2D"/>.</returns>
        public RenderTarget2D EndRenderTarget(string key, bool drawToSpriteBatch = true, bool clearGraphics = true, Color? clearColor = null)
        {
            if (GetRenderTargetManager.GetRenderTarget2D(key) == null ||
                GetRenderTargetManager.RenderTargets[key].IsRefreshing ||
                !GetRenderTargetManager.RenderTargets[key].Enabled) return null;

            graphics.SetRenderTarget(SwapChainRenderTarget);

            if (clearGraphics) graphics.Clear(clearColor ?? BackgroundColor);

            if (drawToSpriteBatch)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(GetRenderTargetManager.GetRenderTarget2D(key), Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            return GetRenderTargetManager.GetRenderTarget2D(key);
        }
#endif

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
                        Cam.GetTransformation(graphics));
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

        /// <summary>
        /// Disposes the contents of this service.
        /// </summary>
        public void Dispose()
        {
            Content?.Dispose();
            InternContent?.Dispose();
            Pixel.Dispose();
            Font = null;
#if DX
            GetRenderTargetManager?.Dispose();
            RenderTargetTimer?.Dispose();
#endif
        }
    }
}
