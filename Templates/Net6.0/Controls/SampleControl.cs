using Microsoft.Xna.Framework;
using MonoGame.Forms.NET.Controls;

using Color = Microsoft.Xna.Framework.Color;

namespace Editor.Controls
{
    public class SampleControl : MonoGameControl
    {
        // Fields & Properties here!
        private const string WelcomeMessage = "Welcome to MonoGame.Forms!";

        protected override void Initialize()
        {
            // Initialization & Content-Loading here!

            SetMultiSampleCount(8);

            //Editor.Content.Load<Texture2D>("");

            // Remove FPS-Panel:
            //Components.Remove(Editor.FPSCounter);

            // Remove Default (Built-In) components (this includes the Camera2D):
            //Editor.RemoveDefaultComponents();
        }

        protected override void Update(GameTime gameTime)
        {
            // Updating here!
        }

        protected override void Draw()
        {
            // Drawing here!

            Editor.BeginAntialising();
            Editor.spriteBatch.Begin();

            Editor.spriteBatch.DrawString(Editor.Font, WelcomeMessage, new Vector2(
                (Editor.GraphicsDevice.Viewport.Width / 2) - (Editor.Font.MeasureString(WelcomeMessage).X / 2),
                (Editor.GraphicsDevice.Viewport.Height / 2) - (Editor.FontHeight / 2)),
                Color.White);

            Editor.spriteBatch.End();
            Editor.EndAntialising();
        }
    }
}
