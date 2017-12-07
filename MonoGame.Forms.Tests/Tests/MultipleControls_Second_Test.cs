using Microsoft.Xna.Framework;

namespace MonoGame.Forms.Tests.Tests
{
    public class MultipleControls_Second_Test : MapHost
    {
        protected override void Initialize()
        {
            base.Initialize();

            InitializeMap("b");
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.BeginCamera2D();

            DrawMap();

            Editor.EndCamera2D();
        }
    }
}
