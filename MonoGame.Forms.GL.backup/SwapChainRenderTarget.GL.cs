using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MonoGame.Forms.GL
{
    /// <summary>
    /// A swap chain used for rendering to a secondary GameWindow.
    /// </summary>
    /// <remarks>
    /// This is an extension and not part of stock XNA.
    /// </remarks>
    public class SwapChainRenderTarget_GL : RenderTarget2D
    {
        internal Bitmap BackBuffer;

        public SwapChainRenderTarget_GL(GraphicsDevice graphicsDevice,
                                        int width,
                                        int height)
            : this(
                graphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24,
                0,
                RenderTargetUsage.DiscardContents,
                PresentInterval.Default)
        {
        }

        public SwapChainRenderTarget_GL(GraphicsDevice graphicsDevice,
                                        int width,
                                        int height,
                                        bool mipMap,
                                        SurfaceFormat surfaceFormat,
                                        DepthFormat depthFormat,
                                        int preferredMultiSampleCount,
                                        RenderTargetUsage usage,
                                        PresentInterval presentInterval)
            : base(
                graphicsDevice,
                width,
                height,
                mipMap,
                surfaceFormat,
                depthFormat,
                preferredMultiSampleCount,
                usage,
                SurfaceType.SwapChainRenderTarget)
        {
        }

        private bool GetBackBufferData()
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
                BackBuffer = null;
            }

            bool success = false;
            try
            {
                var bmp = new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
                var bmpData = bmp.LockBits(
                    new System.Drawing.Rectangle(0, 0, Width, Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppRgb);
                var pixelData = new int[Width * Height];
                GraphicsDevice.GetBackBufferData(pixelData);

                for (int i = 0; i < pixelData.Length; i++)
#pragma warning disable // Caused by bitwise function requiring uint to int conversion
                    pixelData[i] = (int)( // Swap bgra - rgba
                            (pixelData[i] & 0x000000ff) << 16 |
                            (pixelData[i] & 0x0000FF00) |
                            (pixelData[i] & 0x00FF0000) >> 16 |
                            (pixelData[i] & 0xFF000000));
#pragma warning disable

                Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);
                bmp.UnlockBits(bmpData);

                BackBuffer = bmp;

                success = true;
            }
            catch { success = false; }

            return success;
        }

        /// <summary>
        /// Displays the contents of the active back buffer to the screen.
        /// </summary>
        public Bitmap Present()
        {
            if (GetBackBufferData())
            {
                return BackBuffer;
            }
            else return new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (BackBuffer != null) BackBuffer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
