using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;

namespace MonoGame.Forms.Services
{
    public abstract class GFXService : IGFXInterface
    {
        public ContentManager Content { get; set; }
        private ContentManager InternContent { get; set; }
        public Vector2 GetMousePosition { get; set; }
        public GraphicsDevice graphics { get; set; }
        public GameServiceContainer services { get; set; }
        public SpriteBatch spriteBatch { get; set; }

        public Camera2D Cam { get; set; }
        public float CurrentWorldShiftX { get; set; }
        public float CurrentWorldShiftY { get; set; }

        public Color BackgroundColor { get; set; } = Color.CornflowerBlue;

        //Display
        public SpriteFont Font { get; set; }
        public NumberFormatInfo Format { get; set; }
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        public int FrameCounter { get; set; }
        public int FrameRate { get; set; }
        
        public void InitializeGFX(IGraphicsDeviceService graphics)
        {
            services = new GameServiceContainer();
            services.AddService<IGraphicsDeviceService>(graphics);

            this.graphics = graphics.GraphicsDevice;

            Content = new ContentManager(services, "Content");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            
            InternContent = new ResourceContentManager(services, Properties.Resources.ResourceManager);
            Font = InternContent.Load<SpriteFont>("Font");

            Format = new System.Globalization.NumberFormatInfo();
            Format.CurrencyDecimalSeparator = ".";

            Cam = new Camera2D();
            Cam.GetPosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        public void UpdateFrameCounter() => FrameCounter++;
        public void UpdateDisplay(GameTime gameTime, Vector2 mousePosition)
        {
            GetMousePosition = mousePosition;

            ElapsedTime += gameTime.ElapsedGameTime;
            if (ElapsedTime <= TimeSpan.FromSeconds(1)) return;
            ElapsedTime -= TimeSpan.FromSeconds(1);
            FrameRate = FrameCounter;
            FrameCounter = 0;
        }

        public void DrawDisplay()
        {
            if (Settings.ShowFPS || Settings.ShowCursorPosition)
            {
                spriteBatch.Begin();

                string fps = string.Format(Format, "{0} fps", FrameRate);

                if (Settings.ShowFPS)
                {
                    // Draw FPS display
                    spriteBatch.DrawString(Font, fps, new Vector2(5, 0), Color.White);
                }

                if (Settings.ShowCursorPosition)
                {
                    // Draw FPS display
                    spriteBatch.DrawString(Font, GetMousePosition.ToString(), new Vector2(
                        5, (!Settings.ShowFPS ? 0 : Font.MeasureString(fps).Y)), Color.White);
                }

                if (Settings.ShowCamPosition)
                {
                    // Draw FPS display
                    spriteBatch.DrawString(Font, Cam.GetAbsolutPosition.ToString(), new Vector2(
                        5, (!Settings.ShowFPS && !Settings.ShowCursorPosition ? 0 :
                        !Settings.ShowFPS || !Settings.ShowCursorPosition ? Font.MeasureString(fps).Y :
                        Font.MeasureString(fps).Y * 2)), Color.White);
                }

                spriteBatch.End();
            }
        }

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

        public void EndCamera2D()
        {
            spriteBatch.End();
        }

        public void MoveCam(Vector2 amount)
        {
            Cam.Move(new Vector2(amount.X, amount.Y));
            CurrentWorldShiftX += amount.X;
            CurrentWorldShiftY += amount.Y;
        }

        public void ResetCam()
        {
            Cam.Move(new Vector2(-CurrentWorldShiftX, -CurrentWorldShiftY));
            CurrentWorldShiftX = 0;
            CurrentWorldShiftY = 0;
        }
    }
}
