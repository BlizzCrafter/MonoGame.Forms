#region File Description

//-----------------------------------------------------------------------------
// GraphicsDeviceControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using System.ComponentModel;

namespace MonoGame.Forms.Controls
{
    /// <summary>
    /// This class mainly creates the <see cref="GraphicsDevice"/> and the <see cref="SwapChainRenderTarget"/>.
    /// It inherits from <see cref="System.Windows.Forms.Control"/>, which makes its childs available as a tool box control.
    /// </summary>
    public abstract class GraphicsDeviceControl : System.Windows.Forms.Control
    {
        private bool designMode
        {
            get
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
                bool res = process.ProcessName == "devenv";
                process.Dispose();
                return res;
            }
        }

        /// <summary>
        /// Set the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsProfile"/> in the property grid during Design-Time (HiDef or Reach).
        /// You shouldn't change this during Run-Time!
        /// </summary>
        [Browsable(true)]
        [DefaultValue(GraphicsProfile.Reach)]
        public GraphicsProfile GraphicsProfile { get; set; } = GraphicsProfile.Reach;

        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// Note: When working with different <see cref="RenderTarget2D"/>, 
        /// you need to set the current render target back to the <see cref="SwapChainRenderTarget"/> as this is the real 'Back Buffer'. 
        /// 'GraphicsDevice.SetRenderTarget(null)' will NOT work as you are doing usally in MonoGame. Instead use 'GraphicsDevice.SetRenderTarget(SwapChainRenderTarget)'.
        /// Otherwise you will see only a black control window.
        /// <remarks>This is an extension and not part of stock XNA. It is currently implemented for Windows and DirectX only.</remarks>
        /// </summary>
        [Browsable(false)]
        public SwapChainRenderTarget SwapChainRenderTarget { get { return _chain; } }
        private SwapChainRenderTarget _chain;

        /// <summary>
        /// Transfers the new <see cref="SwapChainRenderTarget"/> to the editor service object after resizing a custom control.
        /// <remarks>Make sure to subscribe to this event in your custom control to update your custom <see cref="RenderTarget2D"/>'s
        /// according to the recent changes in the back buffer (BackBufferWidth and BackBufferHeight).</remarks>
        /// </summary>
        protected event Action<SwapChainRenderTarget> UpdateSwapChainRenderTarget = delegate { };

        /// <summary>
        /// Get the MultiSampleCount (MSAA Antialising) to the nearest power of two in relation of what the users <see cref="GraphicsDevice"/> can handle.
        /// </summary>
        /// <param name="multiSampleCount">The desired multisample count (MSAA)</param>
        /// <returns>The power of two of the MultiSampleCount</returns>
        internal int GetClampedMultisampleCount(int multiSampleCount)
        {
            if (multiSampleCount > 1)
            {
                // Round down MultiSampleCount to the nearest power of two
                // hack from http://stackoverflow.com/a/2681094
                // Note: this will return an incorrect, but large value
                // for very large numbers. That doesn't matter because
                // the number will get clamped below anyway in this case.
                var msc = multiSampleCount;
                msc = msc | (msc >> 1);
                msc = msc | (msc >> 2);
                msc = msc | (msc >> 4);
                msc -= (msc >> 1);
                // and clamp it to what the device can handle
                if (msc > _graphicsDeviceService.MaxMultiSampleCount)
                    msc = _graphicsDeviceService.MaxMultiSampleCount;

                return msc;
            }
            else return 0;
        }
        /// <summary>
        /// Set the "MultiSampleCount" for Multi Sampled AntiAlising (MSAA).
        /// The input value will be automatically clamped to the nearest power of two in relation of what the users <see cref="GraphicsDevice"/> can handle.
        /// </summary>
        /// <param name="multiSampleCount">Usual numbers are 0, 2, 4, 8.</param>
        public void SetMultiSampleCount(int multiSampleCount)
        {
            UpdateMultiSampleCount?.Invoke(GetClampedMultisampleCount(multiSampleCount));
        }
        /// <summary>
        /// Subscribe to this event in your custom control to update your custom <see cref="RenderTarget2D"/>'s with MultiSampling.
        /// </summary>
        public event Action<int> UpdateMultiSampleCount = delegate { };

        /// <summary>
        /// Get the GraphicsDevice.
        /// </summary>
        [Browsable(false)]
        public GraphicsDevice GraphicsDevice => _graphicsDeviceService.GraphicsDevice;

        /// <summary>
        /// Get the GraphicsDeviceService.
        /// </summary>
        internal GraphicsDeviceService _graphicsDeviceService;

        /// <summary>
        /// Get the ServiceContainer.
        /// </summary>
        protected ServiceContainer Services { get; } = new ServiceContainer();

        /// <summary>
        /// "true" if you want the editor view automatically updates itself.
        /// Set this to "false" to update the editor view manually by calling "Invalidate()" on a custom control.
        /// <remarks>
        /// This option is useful when you are using a MonoGame.Forms render control inside a NodeGraphEditor for example and you don't want to block the
        /// whole NodeGraph with the invalidations taking place here.
        /// </remarks>
        /// </summary>
        protected bool AutomaticInvalidation { get; set; } = true;

        #pragma warning disable 1591
        protected override void OnCreateControl()
        {
            if (!designMode)
            {
                _graphicsDeviceService = GraphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height, GraphicsProfile);
                _chain = new SwapChainRenderTarget(_graphicsDeviceService.GraphicsDevice, Handle, ClientSize.Width,
                    ClientSize.Height);
                Services.AddService<IGraphicsDeviceService>(_graphicsDeviceService);
                Initialize();
                Microsoft.Xna.Framework.Input.Mouse.WindowHandle = Handle;
            }
            base.OnCreateControl();
        }

        protected override void Dispose(bool disposing)
        {
            if (_graphicsDeviceService != null)
            {
                _graphicsDeviceService.Release(disposing);
                _graphicsDeviceService = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var beginDrawError = BeginDraw();
            if (string.IsNullOrEmpty(beginDrawError))
            {
                Draw();
                EndDraw();
            }
            else
            {
                PaintUsingSystemDrawing(e.Graphics, beginDrawError);
            }
        }

        private string BeginDraw()
        {
            if (_graphicsDeviceService == null || _chain == null)
            {
                return Text + "\n\n" + GetType();
            }
            var deviceResetError = HandleDeviceReset();
            if (!string.IsNullOrEmpty(deviceResetError))
            {
                return deviceResetError;
            }

            var viewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = ClientSize.Width,
                Height = ClientSize.Height,
                MinDepth = 0,
                MaxDepth = 1
            };
            GraphicsDevice.Viewport = viewport;
            GraphicsDevice.PresentationParameters.BackBufferWidth = ClientSize.Width;
            GraphicsDevice.PresentationParameters.BackBufferHeight = ClientSize.Height;
            _graphicsDeviceService.GraphicsDevice.SetRenderTarget(_chain);
            return null;
        }

        private void EndDraw()
        {
            try
            {
                _chain.Present();
            }
            catch
            {
                // ignored
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (_chain != null)
            {
                if (ClientSize.Width > 0 && ClientSize.Height > 0)
                {
                    _chain.Dispose();
                    _chain = new SwapChainRenderTarget(_graphicsDeviceService.GraphicsDevice, Handle, ClientSize.Width,
                            ClientSize.Height);

                    GraphicsDevice.PresentationParameters.BackBufferWidth = ClientSize.Width;
                    GraphicsDevice.PresentationParameters.BackBufferHeight = ClientSize.Height;

                    UpdateSwapChainRenderTarget?.Invoke(_chain);
                }
            }
        }

        private string HandleDeviceReset()
        {
            var deviceNeedsReset = false;
            switch (GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    return "Graphics device lost";
                case GraphicsDeviceStatus.NotReset:
                    deviceNeedsReset = true;
                    break;
                case GraphicsDeviceStatus.Normal:
                    break;
                default:
                    var pp = GraphicsDevice.PresentationParameters;
                    deviceNeedsReset = (ClientSize.Width > pp.BackBufferWidth) ||
                                       (ClientSize.Height > pp.BackBufferHeight);
                    break;
            }
            if (!deviceNeedsReset) return null;
            try
            {
                _graphicsDeviceService.ResetDevice(ClientSize.Width,
                    ClientSize.Height);
            }
            catch (Exception e)
            {
                return "Graphics device reset failed\n\n" + e;
            }
            return null;
        }

        protected virtual void PaintUsingSystemDrawing(System.Drawing.Graphics graphics, string text)
        {
            graphics.Clear(System.Drawing.Color.DimGray);
            using (Brush brush = new SolidBrush(System.Drawing.Color.CornflowerBlue))
            {
                using (var format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    graphics.DrawString(text, Font, brush, ClientRectangle, format);
                }
            }
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected abstract void Initialize();
        protected abstract void Draw();

        #region Input

        /// <summary>
        /// If enabled the Keyboard input will work even if the current control has no focus (mouse cursor is outside of the control).
        /// </summary>
        protected bool AlwaysEnableKeyboardInput { get; set; } = false;

        private void SetKeyboardInput(bool enable)
        {
            var keyboardType = typeof(Microsoft.Xna.Framework.Input.Keyboard);
            var methodInfo = keyboardType.GetMethod("SetActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { enable });
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!Focused) Focus();

            SetKeyboardInput(true);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (Focused) Parent.Focus();

            if (!AlwaysEnableKeyboardInput) SetKeyboardInput(false);
        }
        
        internal Point GetRelativeMousePosition { get; set; }
        internal Point GetAbsoluteMousePosition { get; set; }
        internal void UpdateMousePositions()
        {
            GetAbsoluteMousePosition = new Point(Cursor.Position.X, Cursor.Position.Y);

            if (IsMouseInsideControl)
            {
                GetRelativeMousePosition = new Point(
                    Microsoft.Xna.Framework.MathHelper.Clamp(PointToClient(Cursor.Position).X, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Width),
                    Microsoft.Xna.Framework.MathHelper.Clamp(PointToClient(Cursor.Position).Y, 0, _graphicsDeviceService.GraphicsDevice.Viewport.Height));
            }
        }

        /// <summary>
        /// Returns true when the mouse cursor is inside the control.
        /// </summary>
        protected bool IsMouseInsideControl
        {
            get
            {
                if (ClientRectangle.Contains(PointToClient(Cursor.Position))) return true;
                else return false;
            }
        }

        public delegate void MouseWheelUpwardsEvent(MouseEventArgs e);
        [DisplayName("MouseWheelUp")]
        [Description("Scroll the mouse wheel upwards to trigger this event.")]
        public event MouseWheelUpwardsEvent OnMouseWheelUpwards;

        public delegate void MouseWheelDownwardsEvent(MouseEventArgs e);
        [DisplayName("MouseWheelDown")]
        [Description("Scroll the mouse wheel downwards to trigger this event.")]
        public event MouseWheelDownwardsEvent OnMouseWheelDownwards;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0) OnMouseWheelUpwards?.Invoke(e);
            else if (e.Delta < 0) OnMouseWheelDownwards?.Invoke(e);
        }

        #endregion
    }
}