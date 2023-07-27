#region License Information

//-----------------------------------------------------------------------------
//This file is based on:
//-----------------------------------------------------------------------------
// GraphicsDeviceControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

//Microsoft Public License (MS-PL)

//This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

//1. Definitions

//The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.

//A "contribution" is the original software, or any additions or changes to the software.

//A "contributor" is any person that distributes its contribution under this license.

//"Licensed patents" are a contributor's patent claims that read directly on its contribution.

//2. Grant of Rights

//(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

//(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

//3. Conditions and Limitations

//(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

//(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

//(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

//(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

//(E) The software is licensed "as-is." You bear the risk of using it.The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement

#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Services;
using MonoGame.Forms.NET;

namespace MonoGame.Forms.NET.Controls
{
    /// <summary>
    /// This class mainly creates the <see cref="GraphicsDevice"/> and the <see cref="SwapChainRenderTarget"/>.
    /// It inherits from <see cref="System.Windows.Forms.Control"/>, which makes its childs available as a tool box control.
    /// </summary>
    public abstract class GraphicsDeviceControl : System.Windows.Forms.Control
    {
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
                if (DesignMode) Invalidate();
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
                if (DesignMode) Invalidate();
            }
        }
        private Color _ForeColor = Color.CornflowerBlue;

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

#pragma warning disable 1591
        protected override void OnCreateControl()
        {
            if (!DesignMode)
            {
                if (ClientSize.Width == 0 || ClientSize.Height == 0) ClientSize = new Size(1, 1);

                _graphicsDeviceService = GraphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height, GraphicsProfile);
                Services.AddService<IGraphicsDeviceService>(_graphicsDeviceService);

                _chain = new SwapChainRenderTarget(_graphicsDeviceService.GraphicsDevice, Handle, ClientSize.Width, ClientSize.Height);

                Microsoft.Xna.Framework.Input.Mouse.WindowHandle = Handle;

                Initialize();
            }
            base.OnCreateControl();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!DesignMode)
                {
                    if (_graphicsDeviceService != null)
                    {
                        _graphicsDeviceService.Release(disposing);
                        _graphicsDeviceService = null;
                    }
                }
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

            if (DesignMode) return Text + "\n\n" + GetType();

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

        private void EndDraw(PaintEventArgs e)
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

            if (ClientSize.Width > 0 && 
                ClientSize.Height > 0)
            {
                RefreshDXWindow();
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

                logo = Resources.MonoGame_Logo;

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
        protected bool AlwaysEnableKeyboardInput
        {
            get { return _AlwaysEnableKeyboardInput; }
            set
            {
                _AlwaysEnableKeyboardInput = value;

                SetKeyboardInput(value);
            }
        }
        private bool _AlwaysEnableKeyboardInput = false;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!Focused) Focus();

            Microsoft.Xna.Framework.Input.Mouse.WindowHandle = Handle;
            SetKeyboardInput(true);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (Focused) Parent.Focus();

            if (!AlwaysEnableKeyboardInput)
            {
                SetKeyboardInput(false);
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

        /// <summary>
        /// Returns true when the mouse cursor is inside the specific area.
        /// </summary>
        protected bool IsMouseInsideControlArea(int positionX, int positionY, int width, int height)
        {
            Rectangle areaRec = new Rectangle(positionX, positionY, width, height);

            if (areaRec.Contains(PointToClient(Cursor.Position))) return true;
            else return false;
        }

        public delegate void MouseWheelUpwardsEvent(MouseEventArgs e);
        [DisplayName("MouseWheelUp")]
        [Description("Scroll the mouse wheel upwards to trigger this event.")]
        public event MouseWheelUpwardsEvent OnMouseWheelUpwards;

        public delegate void MouseWheelDownwardsEvent(MouseEventArgs e);
        [DisplayName("MouseWheelDown")]
        [Description("Scroll the mouse wheel downwards to trigger this event.")]
        public event MouseWheelDownwardsEvent OnMouseWheelDownwards;

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

#endregion
    }
}