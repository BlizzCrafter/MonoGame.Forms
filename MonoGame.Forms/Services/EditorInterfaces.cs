using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;

namespace MonoGame.Forms.Services
{
    internal interface IGFXInterface
    {
        GameServiceContainer services { get; set; }
        GraphicsDevice graphics { get; set; }
        SpriteBatch spriteBatch { get; set; }
        ContentManager Content { get; set; }

        Camera2D Cam { get; set; }

        Color BackgroundColor { get; set; }

        // Display
        SpriteFont Font { get; set; }
        System.Drawing.Point GetRelativeMousePosition { get; set; }
        System.Drawing.Point GetAbsoluteMousePosition { get; set; }

        void MoveCam(Vector2 amount);
        void ResetCam();

        void InitializeGFX(IGraphicsDeviceService graphics, SwapChainRenderTarget swapChainRenderTarget);

        void UpdateFrameCounter();
        void UpdateDisplay(GameTime gameTime);

        void DrawDisplay();
        void BeginCamera2D(
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null);
        void EndCamera2D();
    }
}
