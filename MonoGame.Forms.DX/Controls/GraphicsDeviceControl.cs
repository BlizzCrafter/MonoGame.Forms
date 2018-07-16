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

#if GL
using MonoGame.Forms.GL;
using Microsoft.Xna.Framework.SDL;
#endif

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
        /// You shouldn't change this during runtime!
        /// </summary>
        [Browsable(true)]
        [Description("Set the GraphicsProfile on initialization. Please do not change this during runtime!")]
        [DefaultValue(GraphicsProfile.Reach)]
        public GraphicsProfile GraphicsProfile { get; set; } = GraphicsProfile.Reach;

        /// <summary>
        /// Set the background color of this Control in the designer.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(Color), "105, 105, 105")]
        new public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                if (designMode) Invalidate();
            }
        }
        private Color _BackColor = Color.DimGray;

        /// <summary>
        /// Set the foreground color of this Control in the designer.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(Color), "100, 149, 237")]
        new public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                if (designMode) Invalidate();
            }
        }
        private Color _ForeColor = Color.CornflowerBlue;
#if DX
        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// Note: When working with different <see cref="RenderTarget2D"/>, 
        /// you need to set the current render target back to the <see cref="SwapChainRenderTarget"/> as this is the real 'Back Buffer'. 
        /// 'GraphicsDevice.SetRenderTarget(null)' will NOT work as you are doing usally in MonoGame. Instead use 'GraphicsDevice.SetRenderTarget(SwapChainRenderTarget)'.
        /// Otherwise you will see only a black control window.
        /// </summary>
        [Browsable(false)]
        public SwapChainRenderTarget SwapChainRenderTarget { get { return _chain; } }
        private SwapChainRenderTarget _chain;

        /// <summary>
        /// Mainly transfers the new <see cref="SwapChainRenderTarget"/> to the editor service objects after resizing a custom control.
        /// </summary>
        internal event Action<SwapChainRenderTarget> SwapChainRenderTargetRefreshed = delegate { };

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
            MultiSampleCountRefreshed?.Invoke(GetClampedMultisampleCount(multiSampleCount));
        }
        /// <summary>
        /// Subscribe to this event to react to MultiSampleCount changes in your custom controls.
        /// </summary>
        public event Action<int> MultiSampleCountRefreshed = delegate { };

        private void RefreshDXWindow()
        {
            if (_chain != null)
            {
                _chain.Dispose();
                _chain = new SwapChainRenderTarget(_graphicsDeviceService.GraphicsDevice, Handle, ClientSize.Width,
                        ClientSize.Height);

                GraphicsDevice.PresentationParameters.BackBufferWidth = ClientSize.Width;
                GraphicsDevice.PresentationParameters.BackBufferHeight = ClientSize.Height;

                SwapChainRenderTargetRefreshed?.Invoke(_chain);
            }
        }
#elif GL
        /// <summary>
        /// A swap chain used for rendering to a secondary GameWindow.
        /// </summary>
        [Browsable(false)]
        public SwapChainRenderTarget_GL SwapChainRenderTarget { get { return _chain; } }
        private SwapChainRenderTarget_GL _chain;

        /// <summary>
        /// Mainly transfers the new <see cref="SwapChainRenderTarget"/> to the editor service objects after resizing a custom control.
        /// </summary>
        internal event Action<SwapChainRenderTarget_GL> SwapChainRenderTargetRefreshed = delegate { };

        [Browsable(true)]
        [Description("Define here the intervall in milliseconds of how often this control gets the BackBufferData of the GraphicsDevice. 1ms means realtime updates, which costs performance. Use values like 50ms or 100ms to get a better performance but not so frequent updates.")]
        [DefaultValue(1)]
        /// <summary>
        /// Define here the intervall in milliseconds of how often this control gets the BackBufferData of the GraphicsDevice. 
        /// 1ms means realtime updates, which costs performance. 
        /// Use values like 50ms or 100ms to get a better performance but not so frequent updates.
        /// </summary>
        public int DrawInterval
        {
            get { return _Intervall.Interval; }
            set { _Intervall.Interval = value; }
        }
        private Timer _Intervall = new Timer() { Interval = 1 };
        private bool _DrawThisFrame = true;

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (Visible) _DrawThisFrame = true;
        }

        internal void PresentDirty(bool forceInvalidation = false)
        {
            if (Visible)
            {
                if (forceInvalidation)
                {
                    RefreshGLWindow();
                    Invalidate();
                }
                else if (AutomaticInvalidation) Invalidate();
            }
        }

        private void RefreshGLWindow()
        {
            if (_chain != null)
            {
                _chain.Dispose();
                _chain = new SwapChainRenderTarget_GL(_graphicsDeviceService.GraphicsDevice, ClientSize.Width,
                        ClientSize.Height);

                GraphicsDevice.PresentationParameters.BackBufferWidth = ClientSize.Width;
                GraphicsDevice.PresentationParameters.BackBufferHeight = ClientSize.Height;
                GraphicsDevice.Viewport = new Viewport(0, 0, ClientSize.Width, ClientSize.Height);

                Sdl.Window.SetSize(_graphicsDeviceService.SDLPlatform.Window.Handle, ClientSize.Width, ClientSize.Height);

                SwapChainRenderTargetRefreshed?.Invoke(_chain);
            }
        }
#endif

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
        protected bool AutomaticInvalidation
        {
            get { return _AutomaticInvalidation; }
            set
            {
                _AutomaticInvalidation = value;
#if GL
                if (_chain != null) _Intervall.Enabled = value;
#endif
            }
        }
        private bool _AutomaticInvalidation;

#pragma warning disable 1591
        protected override void OnCreateControl()
        {
            if (!designMode)
            {
                _graphicsDeviceService = GraphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height, GraphicsProfile);
                Services.AddService<IGraphicsDeviceService>(_graphicsDeviceService);
#if DX
                _chain = new SwapChainRenderTarget(_graphicsDeviceService.GraphicsDevice, Handle, ClientSize.Width, ClientSize.Height);

                Microsoft.Xna.Framework.Input.Mouse.WindowHandle = Handle;
#elif GL
                _chain = new SwapChainRenderTarget_GL(_graphicsDeviceService.GraphicsDevice, ClientSize.Width, ClientSize.Height);

                _Intervall.Enabled = true;
                _Intervall.Start();
                _Intervall.Tick += (sender, e) => { PresentDirty(); };
#endif
                AutomaticInvalidation = true;
                Initialize();
            }
            base.OnCreateControl();
        }

        protected override void Dispose(bool disposing)
        {
            if (!designMode)
            {
                if (_graphicsDeviceService != null)
                {
#if GL
                    _graphicsDeviceService.SDLPlatform.Exit();
                    _graphicsDeviceService.SDLPlatform.Dispose();
#endif
                    _graphicsDeviceService.Release(disposing);
                    _graphicsDeviceService = null;
                }
#if GL
                _Intervall.Stop();
                _Intervall.Enabled = false;
                _Intervall.Dispose();

                if (_chain != null) _chain.Dispose();
#endif
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var beginDrawError = BeginDraw();
            if (string.IsNullOrEmpty(beginDrawError))
            {
                Draw();
                EndDraw(e);
            }
            else
            {
                PaintUsingSystemDrawing(e.Graphics, beginDrawError);
            }
        }

        private string BeginDraw()
        {
            if (_graphicsDeviceService == null)
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
#if DX
            _graphicsDeviceService.GraphicsDevice.SetRenderTarget(_chain);
#endif
            return null;
        }

        private void EndDraw(PaintEventArgs e)
        {
            try
            {
#if DX
                _chain.Present();
#elif GL
                if (_DrawThisFrame)
                {
                    _DrawThisFrame = false;
                    e.Graphics.DrawImage(_chain.Present(), 0, 0, _chain.Width, _chain.Height);
                }
                _graphicsDeviceService.SDLPlatform.RunLoop();
#endif
            }
            catch
            {
                // ignored
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (ClientSize.Width > 0 && 
                ClientSize.Height > 0)
            {
#if DX
                RefreshDXWindow();
#elif GL
                RefreshGLWindow();
#endif
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
            graphics.Clear(BackColor);

            try
            {
                Image logo;
#if DX
                logo = DX.Properties.Resources.MonoGame_Logo;
#elif GL
                logo = GL.Properties.Resources.MonoGame_Logo;
#endif
                graphics.DrawImage(
                    logo,
                    (Size.Width / 2) - (logo.Width / 2),
                    (Size.Height / 2) - (logo.Height / 2) - (Font.Height * 4.5f),
                    logo.Width,
                    logo.Height);
            }
            catch { }

            using (Brush brush = new SolidBrush(ForeColor))
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
        private bool _LockKeyboardInput = false;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!Focused) Focus();
#if DX
            SetKeyboardInput(true);
#elif GL
            _LockKeyboardInput = false;
#endif
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (Focused) Parent.Focus();

            if (!AlwaysEnableKeyboardInput)
            {
#if DX
                SetKeyboardInput(false);
#elif GL
                _LockKeyboardInput = true;
#endif
            }
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

#if DX
        private void SetKeyboardInput(bool enable)
        {
            var keyboardType = typeof(Microsoft.Xna.Framework.Input.Keyboard);
            var methodInfo = keyboardType.GetMethod("SetActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { enable });
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0) OnMouseWheelUpwards?.Invoke(e);
            else if (e.Delta < 0) OnMouseWheelDownwards?.Invoke(e);
        }
#elif GL
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!_LockKeyboardInput)
            {
                try
                {
                    if (!designMode)
                    {
                        Sdl.Event evt = GetKeyEvent((SDLK.Key)Enum.Parse(typeof(SDLK.Key), e.KeyCode.ToString()), (SDLK.ModifierKeys)e.Modifiers, true);
                        Sdl.PushEvent(out evt);
                    }
                }
                catch { }
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!_LockKeyboardInput)
            {
                try
                {
                    if (!designMode)
                    {
                        Sdl.Event evt = GetKeyEvent((SDLK.Key)Enum.Parse(typeof(SDLK.Key), e.KeyCode.ToString()), (SDLK.ModifierKeys)e.Modifiers, false);
                        Sdl.PushEvent(out evt);
                    }
                }
                catch { }
            }
            base.OnKeyUp(e);
        }

        public Sdl.Event GetKeyEvent(SDLK.Key key, SDLK.ModifierKeys modifierKeys, bool down)
        {
            Sdl.Event evt = new Sdl.Event();
            evt.Key.Keysym.Scancode = 0;
            evt.Key.Keysym.Sym = (int)key;
            evt.Key.Keysym.Mod = (int)modifierKeys;
            if (down)
            {
                evt.Key.State = (byte)SDLB.ButtonKeyState.Pressed;
                evt.Type = Sdl.EventType.KeyDown;
            }
            else
            {
                evt.Key.State = (byte)SDLB.ButtonKeyState.NotPressed;
                evt.Type = Sdl.EventType.KeyUp;
            }

            return evt;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (!designMode)
                {
                    Sdl.Event evt = GetButtonEvent(ConvertMouseButtons(e), true, (short)e.X, (short)e.Y);
                    Sdl.PushEvent(out evt);
                }
            }
            catch { }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                if (!designMode)
                {
                    Sdl.Event evt = GetButtonEvent(ConvertMouseButtons(e), false, (short)e.X, (short)e.Y);
                    Sdl.PushEvent(out evt);
                }
            }
            catch { }
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (!designMode)
                {
                    Sdl.Event evt = GetMotionEvent(ConvertMouseButtons(e), false, (short)e.X, (short)e.Y, (short)(e.X - lastX), (short)(e.Y - lastY));
                    Sdl.PushEvent(out evt);
                }
            }
            catch { }
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0) OnMouseWheelUpwards?.Invoke(e);
            else if (e.Delta < 0) OnMouseWheelDownwards?.Invoke(e);

            try
            {
                if (!designMode)
                {
                    Sdl.Event evt = GetWheelEvent(e.Delta);
                    Sdl.PushEvent(out evt);
                }
            }
            catch { }
        }

        private Sdl.Event GetWheelEvent(int delta)
        {
            Sdl.Event evt = new Sdl.Event();
            evt.Wheel.Which = 0;
            evt.Wheel.Type = Sdl.EventType.MouseWheel;
            evt.Wheel.Y = delta;
            evt.Type = Sdl.EventType.MouseWheel;
            return evt;
        }

        private Sdl.Event GetButtonEvent(SDLB.MouseButton button, bool buttonPressed, short positionX, short positionY)
        {
            Sdl.Event evt = new Sdl.Event();
            evt.Button.button = (byte)button;
            evt.Button.which = 0;
            evt.Button.x = positionX;
            evt.Button.y = positionY;
            if (buttonPressed)
            {
                evt.Button.state = (byte)SDLB.ButtonKeyState.Pressed;
                evt.Type = Sdl.EventType.MouseButtonDown;
            }
            else
            {
                evt.Button.state = (byte)SDLB.ButtonKeyState.NotPressed;
                evt.Type = Sdl.EventType.MouseButtonup;
            }
            return evt;
        }

        int lastX, lastY;
        private Sdl.Event GetMotionEvent(
            SDLB.MouseButton button,
            bool buttonPressed,
            short positionX, short positionY,
            short relativeX, short relativeY)
        {
            Sdl.Event evt = new Sdl.Event();
            evt.Motion.Xrel = relativeX;
            evt.Motion.Yrel = relativeY;
            evt.Motion.Which = (byte)button;
            evt.Motion.X = positionX;
            evt.Motion.Y = positionY;
            evt.Type = Sdl.EventType.MouseMotion;
            if (buttonPressed)
            {
                evt.Motion.State = (byte)SDLB.ButtonKeyState.Pressed;
            }
            else
            {
                evt.Motion.State = (byte)SDLB.ButtonKeyState.NotPressed;
            }
            return evt;
        }

        private static SDLB.MouseButton ConvertMouseButtons(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                return SDLB.MouseButton.PrimaryButton;
            }
            else if (e.Button == MouseButtons.Right)
            {
                return SDLB.MouseButton.SecondaryButton;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                return SDLB.MouseButton.MiddleButton;
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                return SDLB.MouseButton.WheelDown;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                return SDLB.MouseButton.WheelUp;
            }
            else
            {
                return SDLB.MouseButton.None;
            }
        }
#endif
#endregion
    }
}