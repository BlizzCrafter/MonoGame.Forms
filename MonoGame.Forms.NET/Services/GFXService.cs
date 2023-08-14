using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Components;

using Color = Microsoft.Xna.Framework.Color;
using Timer = System.Timers.Timer;
using MonoGame.Forms.NET.Components.Interfaces;

namespace MonoGame.Forms.NET.Services
{
    /// <summary>
    /// The <see cref="GFXService"/> class provides basic functionality of MonoGame
    /// </summary>
    public abstract class GFXService : IDisposable
    {
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

                internal bool Enabled { get; set; } = true;
                internal bool IsRefreshing { get; set; } = false;
                private bool UseMultiSampling { get; set; } = false;

                private void CreateNewRenderTarget2D(bool useMultiSampling)
                {
                    if (GetGFXService.GraphicsDevice.PresentationParameters.BackBufferWidth > 0 &&
                        GetGFXService.GraphicsDevice.PresentationParameters.BackBufferHeight > 0)
                    {
                        GetRenderTarget2D = new RenderTarget2D(
                            GetGFXService.GraphicsDevice,
                            GetGFXService.GraphicsDevice.PresentationParameters.BackBufferWidth,
                            GetGFXService.GraphicsDevice.PresentationParameters.BackBufferHeight,
                            false,
                            SurfaceFormat.Color,
                            DepthFormat.Depth24,
                            useMultiSampling ? GetGFXService.GetCurrentMultiSampleCount : 0,
                            RenderTargetUsage.DiscardContents);
                    }
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

        /// <summary>
        /// The <see cref="ContentManager"/> is for loading custom content from the content root.
        /// </summary>
        public ContentManager Content { get; set; }
        /// <summary>
        /// The <see cref="ResourceContentManager"/> is for loading custom content from a resource file.
        /// </summary>
        public ResourceContentManager ResourceContent { get; private set; }
        /// <summary>
        /// Initializes a custom ResourceContentManager.
        /// </summary>
        /// <param name="resourceFile">Specify a resource file here (.resources). Usually: Properties.Resources.ResourceManager</param>
        public void ResourceContentManagerInitialize(System.Resources.ResourceManager resourceFile)
        {
            ResourceContent = new ResourceContentManager(_Services, resourceFile);
        }
        /// <summary>
        /// The <see cref="ResourceContentManager"/> is for loading internal content from a resource file.
        /// </summary>
        private ResourceContentManager InternContent { get; set; }

        /// <summary>
        /// The <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/>.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }
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
        /// The Camera2D component.
        /// </summary>
        public ICamera2D? Camera => _Components.OfType<ICamera2D>().FirstOrDefault();
        /// <summary>
        /// The FPSCounter component.
        /// </summary>
        public FPSCounter? FPSCounter => _Components.OfType<FPSCounter>().FirstOrDefault();

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
        /// A plain white pixel mainly to draw the background of the integrated display, but you can also use it in your custom control.
        /// </summary>
        public Texture2D Pixel { get; set; }

        /// <summary>
        /// Height of the display Font - Cached in InitializeGFX().
        /// </summary>
        public float FontHeight { get; private set; }

        /// <summary>
        /// Get the connected service container.
        /// </summary>
        private GameServiceContainer _Services { get; set; }
        /// <summary>
        /// Get the connected game component container.
        /// </summary>
        private GameComponentCollection _Components { get; set; }

        /// <summary>
        /// Initializes the GFX system, which contains basic MonoGame functionality.
        /// </summary>
        /// <param name="graphics">The graphics device service</param>
        /// <param name="swapChainRenderTarget">The swap chain render target</param>
        public void InitializeService(
            GameServiceContainer services,
            GameComponentCollection components,
            SwapChainRenderTarget swapChainRenderTarget)
        {
            _Services = services;
            _Components = components;

            var graphicsService = services.GetService(typeof(IGraphicsDeviceService)) as GraphicsDeviceService;
            
            GraphicsDevice = graphicsService.GraphicsDevice;

            Content = new ContentManager(services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            InternContent = new ResourceContentManager(services, Resources.ResourceManager);
            Font = InternContent.Load<SpriteFont>("Font");
            FontHeight = Font.MeasureString("A").Y;

            GetRenderTargetManager = new RenderTargetManager(this);
            AntialisingRenderTarget = GetRenderTargetManager.CreateNewRenderTarget2D("MSAA", true);

            RenderTargetTimer = new Timer();
            RenderTargetTimer.Interval = 500;
            RenderTargetTimer.Elapsed += (sender, e) => OnRenderTargetTimeOutEnd();

            SwapChainRenderTarget = swapChainRenderTarget;

            FrameworkDispatcher.Update();

            components.Add(new Camera2D(GraphicsDevice));
            components.Add(new FPSCounter(this, Camera));
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
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="GraphicsDevice"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="GraphicsDevice"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearOptions">Define your custom <see cref="ClearOptions"/>.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="stencil">The stencil</param>
        public void BeginAntialising(
            bool clearGraphics = true, 
            Color? clearColor = null,
            ClearOptions clearOptions = ClearOptions.DepthBuffer | ClearOptions.Stencil | ClearOptions.Target,
            float depth = 1f,
            int stencil = 0)
        {
            if (AntialisingRenderTarget.GetRenderTarget2D == null ||
                AntialisingRenderTarget.IsRefreshing || 
                !AntialisingRenderTarget.Enabled) return;

            GraphicsDevice.SetRenderTarget(AntialisingRenderTarget.GetRenderTarget2D);
            if (clearGraphics) GraphicsDevice.Clear(clearOptions, clearColor ?? BackgroundColor, depth, stencil);
        }

        /// <summary>
        /// Everything between <c>BeginAntialising()</c> and <c>EndAntialising()</c> will be affected by MSAA.
        /// </summary>
        /// <param name="drawToSpriteBatch"><c>true</c> to automatically draw the result to the <see cref="SpriteBatch"/>.</param>
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="GraphicsDevice"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="GraphicsDevice"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearOptions">Define your custom <see cref="ClearOptions"/>.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="stencil">The stencil</param>
        /// <returns>The Antialising <see cref="RenderTarget2D"/>.</returns>
        public RenderTarget2D EndAntialising(
            bool drawToSpriteBatch = true, 
            bool clearGraphics = true, 
            Color? clearColor = null,
            ClearOptions clearOptions = ClearOptions.DepthBuffer | ClearOptions.Stencil | ClearOptions.Target,
            float depth = 1f,
            int stencil = 0)
        {
            if (AntialisingRenderTarget.GetRenderTarget2D == null ||
                AntialisingRenderTarget.IsRefreshing ||
                !AntialisingRenderTarget.Enabled) return null;

            GraphicsDevice.SetRenderTarget(SwapChainRenderTarget);

            if (clearGraphics) GraphicsDevice.Clear(clearOptions, clearColor ?? BackgroundColor, depth, stencil);

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
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="GraphicsDevice"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="GraphicsDevice"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearOptions">Define your custom <see cref="ClearOptions"/>.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="stencil">The stencil</param>
        public void BeginRenderTarget(
            string key, 
            bool clearGraphics = true, 
            Color? clearColor = null,
            ClearOptions clearOptions = ClearOptions.DepthBuffer | ClearOptions.Stencil | ClearOptions.Target,
            float depth = 1f,
            int stencil = 0)
        {
            if (GetRenderTargetManager.GetRenderTarget2D(key) == null ||
                GetRenderTargetManager.RenderTargets[key].IsRefreshing ||
                !GetRenderTargetManager.RenderTargets[key].Enabled) return;

            GraphicsDevice.SetRenderTarget(GetRenderTargetManager.GetRenderTarget2D(key));
            if (clearGraphics) GraphicsDevice.Clear(clearOptions, clearColor ?? BackgroundColor, depth, stencil);
        }

        /// <summary>
        /// Everything between <c>BeginRenderTarget()</c> and <c>EndRenderTarget()</c> will be drawn to the <see cref="RenderTarget2D"/>.
        /// </summary>
        /// <param name="key">Please enter a previously set key of the <see cref="RenderTarget2D"/> you want to end.</param>
        /// <param name="drawToSpriteBatch"><c>true</c> to automatically draw the result to the <see cref="SpriteBatch"/>.</param>
        /// <param name="clearGraphics"><c>false</c> if you don't want to to call <see cref="GraphicsDevice"/>.Clear() after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearColor">The <see cref="Color"/> to be used to clear the <see cref="GraphicsDevice"/> after setting the <see cref="RenderTarget2D"/>.</param>
        /// <param name="clearOptions">Define your custom <see cref="ClearOptions"/>.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="stencil">The stencil</param>
        /// <returns>The resulting <see cref="RenderTarget2D"/>.</returns>
        public RenderTarget2D EndRenderTarget(
            string key,
            bool drawToSpriteBatch = true, 
            bool clearGraphics = true, 
            Color? clearColor = null,
            ClearOptions clearOptions = ClearOptions.DepthBuffer | ClearOptions.Stencil | ClearOptions.Target,
            float depth = 1f,
            int stencil = 0)
        {
            if (GetRenderTargetManager.GetRenderTarget2D(key) == null ||
                GetRenderTargetManager.RenderTargets[key].IsRefreshing ||
                !GetRenderTargetManager.RenderTargets[key].Enabled) return null;

            GraphicsDevice.SetRenderTarget(SwapChainRenderTarget);

            if (clearGraphics) GraphicsDevice.Clear(clearOptions, clearColor ?? BackgroundColor, depth, stencil);

            if (drawToSpriteBatch)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(GetRenderTargetManager.GetRenderTarget2D(key), Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            return GetRenderTargetManager.GetRenderTarget2D(key);
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
            BlendState? blendState = null, 
            SamplerState? samplerState = null, 
            DepthStencilState? depthStencilState = null, 
            RasterizerState? rasterizerState = null, 
            Effect? effect = null)
        {
            spriteBatch.Begin(sortMode,
                        blendState,
                        samplerState,
                        depthStencilState,
                        rasterizerState,
                        effect,
                        Camera?.GetTransform());
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
        public void CamMove(Vector2 amount) => Camera?.Move(new Vector2(amount.X, amount.Y));
        /// <summary>
        /// Zoom the camera component.
        /// </summary>
        /// <param name="amount">Usual values: 0.1f - 1f</param>
        public void CamZoom(float amount) => Camera?.Zoom(amount);
        /// <summary>
        /// Rotate the camera component.
        /// </summary>
        /// <param name="amount"></param>
        public void CamRotate(float amount) => Camera?.Rotate(amount);
        /// <summary>
        /// Get the current camera zoom.
        /// </summary>
        /// <returns>Current Zoom or 1f (never null).</returns>
        public float? GetCamZoom() { return Camera?.GetZoom() ?? 1f; }
        /// <summary>
        /// Get the current camera rotation.
        /// </summary>
        /// <returns>Current Rotation or 0f (never null)</returns>
        public float? GetCamRotation() { return Camera?.GetRotation() ?? 0f; }

        /// <summary>
        /// Resets all or specific values from the camera component to their defaults.
        /// </summary>
        public void ResetCam(bool resetPosition = true, bool resetZoom = true, bool resetRotation = true)
        {
            if (resetPosition) Camera?.ResetPosition();
            if (resetZoom) Camera?.ResetZoom();
            if (resetRotation) Camera?.ResetRotation();
        }
        
        internal void CamLockPosition(Size newClientSize)
        {
            Camera?.LockPosition(newClientSize);
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
            ResourceContent?.Dispose();
            Pixel.Dispose();
            Font = null;
#if DX
            GetRenderTargetManager?.Dispose();
            RenderTargetTimer?.Dispose();
#endif
        }
    }
}
