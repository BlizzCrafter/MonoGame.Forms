using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Components.Interfaces;

namespace MonoGame.Forms.NET.Components
{
    /// <summary>
    /// A basic Camera2D component to move the view of an editor.
    /// </summary>
    public class Camera2D : ICamera2D
    {
        private GraphicsDevice _graphics;

        /// <summary>
        /// The transformation matrix of the camera.
        /// </summary>
        private Matrix Transform { get; set; }

        /// <summary>
        /// The basic constructor.
        /// </summary>
        public Camera2D(GraphicsDevice graphics)
        {
            _graphics = graphics;

            CurrentZoom = 1.0f;
            CurrentRotation = 0f;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Initilizes the camera component.
        /// </summary>
        public void Initialize()
        {
            Position = new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);
        }

        /// <summary>
        /// Gets or Sets the Zoom value of the camera.
        /// </summary>
        public float CurrentZoom
        {
            get { return _CurrentZoom; }
            internal set { _CurrentZoom = value; if (_CurrentZoom < 0.1f) _CurrentZoom = 0.1f; } // Negative zoom will flip image
        }
        private float _CurrentZoom { get; set; }

        /// <summary>
        /// Gets or Sets the default Zoom value of the camera.
        /// </summary>
        public float DefaultZoom
        {
            get { return _DefaultZoom; }
            set { _DefaultZoom = value; if (_DefaultZoom < 0.1f) _DefaultZoom = 0.1f; } // Negative zoom will flip image
        }
        private float _DefaultZoom = 1f;

        /// <summary>
        /// Gets or Sets the Rotation value of the camera.
        /// </summary>
        public float CurrentRotation { get; internal set; } = 0f;

        /// <summary>
        /// Gets or Sets the default Rotation value of the camera.
        /// </summary>
        public float DefaultRotation { get; set; } = 0f;

        /// <summary>
        /// Auxiliary method to move the camera
        /// </summary>
        public void Move(Vector2 amount)
        {
            _Position += amount;
            UpdateAbsolutePosition();
        }

        /// <summary>
        /// Gets or Sets the Position value of the camera.
        /// </summary>
        public Vector2 Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
                UpdateAbsolutePosition();
            }
        }
        private Vector2 _Position { get; set; }

        /// <summary>
        /// Gets or Sets the default Position value of the camera.
        /// </summary>
        public Vector2 DefaultPosition { get; set; } = Vector2.Zero;

        /// <summary>
        /// Gets or Sets the absolute Position value of the camera.
        /// </summary>
        public Vector2 AbsolutPosition
        {
            get { return _AbsolutPosition; }
            private set { _AbsolutPosition = value; }
        }
        private Vector2 _AbsolutPosition { get; set; }
        /// <summary>
        /// Gets the absolute position of the camera.
        /// </summary>
        /// <returns>Absolute camera position.</returns>
        public Vector2 GetAbsolutePosition() => AbsolutPosition;

        /// <summary>
        /// Updates the absolute position of the camera based on the viewport
        /// </summary>
        private void UpdateAbsolutePosition()
        {
            AbsolutPosition = new Vector2(
                Position.X - _graphics.Viewport.Width / 2,
                Position.Y - _graphics.Viewport.Height / 2);
        }

        /// <summary>
        /// Set the default values of the camera (Position, Zoom, Rotation) to the current ones.
        /// </summary>
        public void SetDefaultsFromCurrent()
        {
            DefaultPosition = AbsolutPosition;
            DefaultZoom = CurrentZoom;
            DefaultRotation = CurrentRotation;
        }

        /// <summary>
        /// Get the Transformation.
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransform()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(CurrentRotation) *
                                         Matrix.CreateScale(new Vector3(CurrentZoom, CurrentZoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(
                                             _graphics.Viewport.Width * 0.5f, _graphics.Viewport.Height * 0.5f, 0));

            return Transform;
        }

        public void ResetPosition()
        {
            Move(-Position);

            if (DefaultPosition == Vector2.Zero) Position = new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);
            else Position = DefaultPosition;
        }

        public void ResetZoom()
        {
            CurrentZoom = DefaultZoom;
        }

        public void ResetRotation()
        {
            CurrentRotation = DefaultRotation;
        }

        public float GetZoom()
        {
            return CurrentZoom;
        }

        public float GetRotation()
        {
            return CurrentRotation;
        }

        public void Zoom(float amount)
        {
            CurrentZoom = amount;
        }

        public void Rotate(float amount)
        {
            CurrentRotation = amount;
        }
    }
}
