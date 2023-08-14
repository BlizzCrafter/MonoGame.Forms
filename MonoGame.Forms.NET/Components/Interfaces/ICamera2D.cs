using Microsoft.Xna.Framework;

namespace MonoGame.Forms.NET.Components.Interfaces
{
    /// <summary>
    /// Use this interface to create your own Camera2D component, which automatically works with the underlying editor.
    /// </summary>
    public interface ICamera2D : IGameComponent
    {
        abstract Vector2 Position { get; set; }

        abstract Vector2 GetAbsolutePosition();
        abstract Matrix GetTransform();
        abstract float GetZoom();
        abstract float GetRotation();

        abstract void Move(Vector2 amount);
        abstract void Zoom(float amount);
        abstract void Rotate(float amount);

        abstract void ResetPosition();
        abstract void ResetZoom();
        abstract void ResetRotation();

        internal void LockPosition(Size newClientSize)
        {
            Position = new Vector2(newClientSize.Width / 2, newClientSize.Height / 2);
            Move(Position);
        }
    }
}
