using Microsoft.Xna.Framework;

namespace MonoGame.Forms.Samples.Tests
{
    public class MultipleControls_b_Test : MapHost
    {
        protected override void Initialize()
        {
            InitializeMap("b");

            Editor.SetDisplayStyle = Forms.Services.EditorService.DisplayStyle.TopRight;
        }

        protected override void Update(GameTime gameTime) { }

        protected override void Draw()
        {
            DrawMap();
        }
    }
}
