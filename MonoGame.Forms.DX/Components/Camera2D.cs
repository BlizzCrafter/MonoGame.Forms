using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Components
{
    /// <summary>
    /// A basic Camera2D component to move the view of an editor.
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// The transformation matrix of the camera.
        /// </summary>
        public Matrix Transform { get; set; }

        /// <summary>
        /// The basic constructor.
        /// </summary>
        public Camera2D()
        {
            Zoom = 1.0f;
            Rotation = 0f;
            _Position = Vector2.Zero;
        }

        /// <summary>
        /// Gets or Sets the Zoom value of the camera.
        /// </summary>
        public float Zoom
        {
            get { return _Zoom; }
            set { _Zoom = value; if (_Zoom < 0.1f) _Zoom = 0.1f; } // Negative zoom will flip image
        }
        private float _Zoom { get; set; }

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
        public float Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }
        private float _Rotation { get; set; }

        /// <summary>
        /// Gets or Sets the default Rotation value of the camera.
        /// </summary>
        public float DefaultRotation
        {
            get { return _DefaultRotation; }
            set { _DefaultRotation = value; }
        }
        private float _DefaultRotation = 0f;

        /// <summary>
        /// Auxiliary method to move the camera
        /// </summary>
        public void Move(Vector2 amount)
        {
            _Position += amount;
        }
        /// <summary>
        /// Gets or Sets the Position value of the camera.
        /// </summary>
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        private Vector2 _Position { get; set; }

        /// <summary>
        /// Gets or Sets the default Position value of the camera.
        /// </summary>
        public Vector2 DefaultPosition
        {
            get { return _DefaultPosition; }
            set { _DefaultPosition = value; }
        }
        private Vector2 _DefaultPosition = Vector2.Zero;

        /// <summary>
        /// Gets or Sets the absolute Position value of the camera.
        /// </summary>
        public Vector2 AbsolutPosition
        {
            get { return _AbsolutPosition; }
            set { _AbsolutPosition = value; }
        }
        private Vector2 _AbsolutPosition { get; set; }

        /// <summary>
        /// Set the default values of the camera (Position, Zoom, Rotation) to the current ones.
        /// </summary>
        public void SetDefaultsFromCurrent()
        {
            DefaultPosition = AbsolutPosition;
            DefaultZoom = Zoom;
            DefaultRotation = Rotation;
        }

        /// <summary>
        /// Get the Transformation.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice.</param>
        /// <returns></returns>
        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Transform =
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(_Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(
                                             graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));

            AbsolutPosition = new Vector2(
                Position.X - graphicsDevice.Viewport.Width / 2,
                Position.Y - graphicsDevice.Viewport.Height / 2);

                return Transform;
        }
    }
}
