using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System.Windows.Forms;

namespace MonoGame.Forms.Tests.Tests
{
    public class MultipleControls_Second_Test : UpdateWindow
    {
        Texture2D HexMap;
        bool CamMouseDown = false;
        System.Drawing.Point CamFirstMouseDownPosition;

        protected override void Initialize()
        {
            base.Initialize();

            HexMap = Editor.Content.Load<Texture2D>("Map_Second");

            OnMouseWheelUpwards += MultipleControls_First_Test_OnMouseWheelUpwards;
            OnMouseWheelDownwards += MultipleControls_First_Test_OnMouseWheelDownwards;

            Editor.BackgroundColor = Color.Black;
        }

        private void MultipleControls_First_Test_OnMouseWheelUpwards(System.Windows.Forms.MouseEventArgs e)
        {
            Editor.Cam.GetZoom += 0.1f;
        }
        private void MultipleControls_First_Test_OnMouseWheelDownwards(System.Windows.Forms.MouseEventArgs e)
        {
            if (Editor.Cam.GetZoom > 0.7f) Editor.Cam.GetZoom -= 0.1f;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Middle) Editor.ResetCam();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            CamMouseDown = false;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            CamFirstMouseDownPosition = e.Location;
            CamMouseDown = true;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (CamMouseDown)
            {
                int xDiff = CamFirstMouseDownPosition.X - e.Location.X;
                int yDiff = CamFirstMouseDownPosition.Y - e.Location.Y;

                Editor.MoveCam(new Vector2(xDiff, yDiff));

                CamFirstMouseDownPosition.X = e.Location.X;
                CamFirstMouseDownPosition.Y = e.Location.Y;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.BeginCamera2D();

            Editor.spriteBatch.Draw(HexMap, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (HexMap.Width / 2),
                (Editor.graphics.Viewport.Height / 2) - (HexMap.Height / 2)),
                Color.White);

            Editor.EndCamera2D();
        }
    }
}
