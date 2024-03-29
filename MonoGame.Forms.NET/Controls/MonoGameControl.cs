﻿using System.ComponentModel;
using Microsoft.Xna.Framework;
using MonoGame.Forms.NET.Services;

namespace MonoGame.Forms.NET.Controls
{
    /// <summary>
    /// Inherit from this class in your custom class to create a draw control with a game loop, which is selectable from the ToolBox during design time.
    /// It provides a game loop and a place to draw.
    /// <remarks>This game loop control is useful as a window, which needs a classical game loop for complex <see cref="GameTime"/> based mechanics.</remarks>
    /// </summary>
    public abstract class MonoGameControl : GameControl
    {
        /// <summary>
        /// The <see cref="MonoGameService"/> of the <see cref="MonoGameControl"/> draws and updates the actual content of the update control.
        /// </summary>
        [Browsable(false)]
        public MonoGameService Editor { get; private set; }

        /// <summary>
        /// Subscribe to get Update and Draw event info for this MonoGameControl.
        /// </summary>
        public event EventHandler<ControlStateEventArgs> ControlState;

        /// <summary>
        /// Basic initializing.
        /// </summary>
        internal override void InternalInitialize()
        {
            base.InternalInitialize();

            Editor = new MonoGameService(Services, Components, SwapChainRenderTarget);

            SwapChainRenderTargetRefreshed -= UpdateWindow_UpdateSwapChainRenderTarget;
            SwapChainRenderTargetRefreshed += UpdateWindow_UpdateSwapChainRenderTarget;
            MultiSampleCountRefreshed -= UpdateWindow_UpdateMultiSampleCount;
            MultiSampleCountRefreshed += UpdateWindow_UpdateMultiSampleCount;

            Editor.InternalInitialize();
            Initialize();
        }

        /// <summary>
        /// Basic updating.
        /// It uses a real game loop, represented by <see cref="GameTime"/>.
        /// </summary>
        internal override void InternalUpdate(GameTime gameTime)
        {
            if (Editor != null)
            {
                Editor.UpdateMousePositions(GetRelativeMousePosition, GetAbsoluteMousePosition);

                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.StartUpdate));
                Editor.InternalUpdate(gameTime);
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.BeforeComponentUpdate));
                UpdateComponents(gameTime);
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.AfterComponentUpdate));
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.BeforeUpdate));
                Update(gameTime);
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.AfterUpdate));
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.EndUpdate));
            }
        }

        /// <summary>
        /// Basic drawing.
        /// </summary>
        internal override void InternalDraw()
        {
            if (Editor != null)
            {
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.StartDraw));
                Editor.InternalDraw();
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.BeforeDraw));
                Draw();
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.AfterDraw));
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.BeforeComponentDraw));
                DrawComponents(_GameTime);
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.AfterComponentDraw));
                ControlState?.Invoke(this, new ControlStateEventArgs(NET.ControlState.EndDraw));
            }
        }

        private void UpdateWindow_UpdateSwapChainRenderTarget(Microsoft.Xna.Framework.Graphics.SwapChainRenderTarget obj)
        {
            if (Editor != null) Editor.SwapChainRenderTarget = obj;
        }

        private void UpdateWindow_UpdateMultiSampleCount(int obj)
        {
            if (Editor != null) Editor.GetCurrentMultiSampleCount = obj;
        }

        /// <summary>
        /// Updates related Editor services when the <see cref="System.Windows.Forms.Control.ClientSize"/> changes.
        /// </summary>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (Editor != null)
            {
                Editor.DisableRenderTargets();
                Editor.CamLockPosition(ClientSize);
            }
        }

        /// <summary>
        /// In case the ClientSize was changed before activating the window, the cam position gets updated according to this changes.
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Editor != null)
            {
                Editor.CamLockPosition(ClientSize);
            }
        }

        /// <summary>
        /// Disposes the contents of the attached Editor.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Editor?.Dispose();

                SwapChainRenderTargetRefreshed -= UpdateWindow_UpdateSwapChainRenderTarget;
                MultiSampleCountRefreshed -= UpdateWindow_UpdateMultiSampleCount;
                ControlState = null;
            }
        }

        /// <summary>
        /// Override to implement Initialization logic in your custom MonoGame.Forms.Control.
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// Override to implement Update (Game Loop) logic in your custom MonoGame.Forms.Control.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Update(GameTime gameTime);
        /// <summary>
        /// Override to implement Drawing logic in your custom MonoGame.Forms.Control.
        /// </summary>
        protected abstract void Draw();
    }
}
