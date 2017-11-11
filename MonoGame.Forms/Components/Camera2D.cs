using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Forms.Components
{
    /// <summary>
    /// A basic Camera2D component to move the view of an editor.
    /// </summary>
    public class Camera2D
    {
        #pragma warning disable 1591

        private float Zoom { get; set; }
        public Matrix Transform { get; set; }
        private Vector2 Position { get; set; }
        private Vector2 AbsolutPosition { get; set; }
        private float Rotation { get; set; }

        public Camera2D()
        {
            GetZoom = 1.0f;
            GetRotation = 0f;
            Position = Vector2.Zero;
        }

        // Sets and gets zoom
        public float GetZoom
        {
            get { return Zoom; }
            set { Zoom = value; if (Zoom < 0.1f) Zoom = 0.1f; } // Negative zoom will flip image
        }

        public float GetRotation
        {
            get { return Rotation; }
            set { Rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            Position += amount;
        }
        // Get set position
        public Vector2 GetPosition
        {
            get { return Position; }
            set { Position = value; }
        }
        // Get set absolut position
        public Vector2 GetAbsolutPosition
        {
            get { return AbsolutPosition; }
            set { AbsolutPosition = value; }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
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
