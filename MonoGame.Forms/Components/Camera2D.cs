using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Components
{
    /// <summary>
    /// A basic Camera2D component to move the view of an editor.
    /// </summary>
    public class Camera2D
    {
        private float Zoom { get; set; }

        /// <summary>
        /// The transformation matrix of the camera.
        /// </summary>
        public Matrix Transform { get; set; }
        private Vector2 Position { get; set; }
        private Vector2 AbsolutPosition { get; set; }
        private float Rotation { get; set; }

        /// <summary>
        /// The basic constructor.
        /// </summary>
        public Camera2D()
        {
            GetZoom = 1.0f;
            GetRotation = 0f;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Gets or Sets the Zoom value of the camera.
        /// </summary>
        public float GetZoom
        {
            get { return Zoom; }
            set { Zoom = value; if (Zoom < 0.1f) Zoom = 0.1f; } // Negative zoom will flip image
        }

        /// <summary>
        /// Gets or Sets the Rotation value of the camera.
        /// </summary>
        public float GetRotation
        {
            get { return Rotation; }
            set { Rotation = value; }
        }
        
        /// <summary>
        /// Auxiliary method to move the camera
        /// </summary>
        public void Move(Vector2 amount)
        {
            Position += amount;
        }
        /// <summary>
        /// Gets or Sets the Position value of the camera.
        /// </summary>
        public Vector2 GetPosition
        {
            get { return Position; }
            set { Position = value; }
        }
        /// <summary>
        /// Gets or Sets the absolute Position value of the camera.
        /// </summary>
        public Vector2 GetAbsolutPosition
        {
            get { return AbsolutPosition; }
            set { AbsolutPosition = value; }
        }

        /// <summary>
        /// Get the Transformation.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice.</param>
        /// <returns></returns>
        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Transform =
              Matrix.CreateTranslation(new Vector3(-GetPosition.X, -GetPosition.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(
                                             graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));

            GetAbsolutPosition = new Vector2(
                GetPosition.X - graphicsDevice.Viewport.Width / 2,
                GetPosition.Y - graphicsDevice.Viewport.Height / 2);

                return Transform;
        }
    }
}
