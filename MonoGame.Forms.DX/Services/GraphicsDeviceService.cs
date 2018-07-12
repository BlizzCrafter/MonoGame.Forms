#region File Description

//-----------------------------------------------------------------------------
// GraphicsDeviceService.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

#if GL
using MonoGame.Forms.GL;
using Microsoft.Xna.Framework;
#endif

namespace MonoGame.Forms.Services
{
#pragma warning disable 1591
    internal class GraphicsDeviceService : IGraphicsDeviceService
    {
        private static GraphicsDeviceService _singletonInstance;
        private static int _referenceCount;
        private readonly PresentationParameters _parameters;

        private GraphicsDeviceService(IntPtr windowHandle, int width, int height, GraphicsProfile graphicsProfile)
        {
            _parameters = new PresentationParameters
            {
                BackBufferWidth = Math.Max(width, 1),
                BackBufferHeight = Math.Max(height, 1),
                BackBufferFormat = SurfaceFormat.Color,
                DepthStencilFormat = DepthFormat.Depth24,
                DeviceWindowHandle = windowHandle,
                PresentationInterval = PresentInterval.Immediate,
                IsFullScreen = false
            };
#if GL
            PreferredBackBufferFormat = SurfaceFormat.Color;
            PreferredDepthStencilFormat = DepthFormat.Depth24;

            SDLPlatform = new SdlGamePlatform();
            PlatformInitialize(_parameters);
            SDLPlatform.IsActive = true;
#endif

            GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                         graphicsProfile,
                                                         _parameters);
#if DX
            MaxMultiSampleCount = GetMaxMultiSampleCount(GraphicsDevice);
#elif GL
            ((SdlGameWindow)SdlGameWindow.Instance).GetGraphicsDevice = GraphicsDevice;
#endif
        }
#if DX
        internal int MaxMultiSampleCount;

        private int GetMaxMultiSampleCount(GraphicsDevice device)
        {
            var format = SharpDXHelper.ToFormat(device.PresentationParameters.BackBufferFormat);
            var qualityLevels = 0;
            var maxLevel = 32;
            while (maxLevel > 0)
            {
                qualityLevels = ((SharpDX.Direct3D11.Device)device.Handle).CheckMultisampleQualityLevels(format, maxLevel);
                if (qualityLevels > 0)
                    break;
                maxLevel /= 2;
            }
            return maxLevel;
        }
#elif GL
        internal SdlGamePlatform SDLPlatform { get; set; }

        public void PlatformInitialize(PresentationParameters presentationParameters)
        {
            var surfaceFormat = PreferredBackBufferFormat.GetColorFormat();
            var depthStencilFormat = PreferredDepthStencilFormat;
            
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.RedSize, surfaceFormat.R);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.GreenSize, surfaceFormat.G);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.BlueSize, surfaceFormat.B);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.AlphaSize, surfaceFormat.A);

            switch (depthStencilFormat)
            {
                case DepthFormat.None:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 0);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth16:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 16);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24Stencil8:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 8);
                    break;
            }

            Sdl.GL.SetAttribute(Sdl.GL.Attribute.DoubleBuffer, 1);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMajorVersion, 2);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMinorVersion, 1);

            ((SdlGameWindow)SdlGameWindow.Instance).CreateWindow();
        }

        public SurfaceFormat PreferredBackBufferFormat
        {
            get
            {
                return _preferredBackBufferFormat;
            }
            set
            {
                _preferredBackBufferFormat = value;
            }
        }
        private SurfaceFormat _preferredBackBufferFormat;

        public DepthFormat PreferredDepthStencilFormat
        {
            get
            {
                return _preferredDepthStencilFormat;
            }
            set
            {
                _preferredDepthStencilFormat = value;
            }
        }
        private DepthFormat _preferredDepthStencilFormat;
#endif
        public GraphicsDevice GraphicsDevice { get; private set; }

#pragma warning disable 67
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        public static GraphicsDeviceService AddRef(IntPtr windowHandle, int width, int height, GraphicsProfile graphicsProfile)
        {
            if (Interlocked.Increment(ref _referenceCount) == 1)
            {
                _singletonInstance = new GraphicsDeviceService(windowHandle, width, height, graphicsProfile);
            }
            return _singletonInstance;
        }

        public void Release(bool disposing)
        {
            if (Interlocked.Decrement(ref _referenceCount) != 0)
                return;
            if (disposing)
            {
                DeviceDisposing?.Invoke(this, EventArgs.Empty);
                GraphicsDevice.Dispose();
            }
            GraphicsDevice = null;
        }

        public void ResetDevice(int width, int height)
        {
            DeviceResetting?.Invoke(this, EventArgs.Empty);
            _parameters.BackBufferWidth = Math.Max(_parameters.BackBufferWidth, width);
            _parameters.BackBufferHeight = Math.Max(_parameters.BackBufferHeight, height);
            GraphicsDevice.Reset(_parameters);
            DeviceReset?.Invoke(this, EventArgs.Empty);
        }
    }
}