using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL;
using System;

namespace MonoGame.Forms.GL
{
    internal static class Extensions
    {
        public static ColorFormat GetColorFormat(this SurfaceFormat format)
        {
            switch (format)
            {
                case SurfaceFormat.Alpha8:
                    return new ColorFormat(0, 0, 0, 8);
                case SurfaceFormat.Bgr565:
                    return new ColorFormat(5, 6, 5, 0);
                case SurfaceFormat.Bgra4444:
                    return new ColorFormat(4, 4, 4, 4);
                case SurfaceFormat.Bgra5551:
                    return new ColorFormat(5, 5, 5, 1);
                case SurfaceFormat.Bgr32:
                    return new ColorFormat(8, 8, 8, 0);
                case SurfaceFormat.Bgra32:
                case SurfaceFormat.Color:
                case SurfaceFormat.ColorSRgb:
                    return new ColorFormat(8, 8, 8, 8);
                case SurfaceFormat.Rgba1010102:
                    return new ColorFormat(10, 10, 10, 2);
                default:
                    // Floating point backbuffers formats could be implemented
                    // but they are not typically used on the backbuffer. In
                    // those cases it is better to create a render target instead.
                    throw new NotSupportedException();
            }
        }
    }
}
