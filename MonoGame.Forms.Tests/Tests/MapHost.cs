using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System.Windows.Forms;

namespace MonoGame.Forms.Tests.Tests
{
    public abstract class MapHost : UpdateWindow
    {
        protected Texture2D[] HexMaps;
        int CurrentMap { get; set; } = 0;

        bool CamMouseDown = false;
        System.Drawing.Point CamFirstMouseDownPosition;

        #region Mouse Input Events
        
        private void MapHost_OnMouseWheelUpwards(MouseEventArgs e)
        {
            Editor.Cam.GetZoom += 0.1f;
        }

        private void MapHost_OnMouseWheelDownwards(MouseEventArgs e)
        {
            if (Editor.Cam.GetZoom > 0.7f) Editor.Cam.GetZoom -= 0.1f;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Middle) Editor.ResetCam();
            else if (e.Button == MouseButtons.XButton1) CurrentMap--;
            else if (e.Button == MouseButtons.XButton2) CurrentMap++;

            if (CurrentMap < 0) CurrentMap = HexMaps.Length - 1;
            else if (CurrentMap > HexMaps.Length - 1) CurrentMap = 0;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            CamMouseDown = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                CamFirstMouseDownPosition = e.Location;
                CamMouseDown = true;
            }
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

        #endregion

        protected void InitializeMap(string side)
        {
            HexMaps = new Texture2D[5];
            HexMaps[0] = Editor.Content.Load<Texture2D>("Maps/0" + side);
            HexMaps[1] = Editor.Content.Load<Texture2D>("Maps/1" + side);
            HexMaps[2] = Editor.Content.Load<Texture2D>("Maps/2" + side);
            HexMaps[3] = Editor.Content.Load<Texture2D>("Maps/3" + side);
            HexMaps[4] = Editor.Content.Load<Texture2D>("Maps/4" + side);

            OnMouseWheelUpwards += MapHost_OnMouseWheelUpwards;
            OnMouseWheelDownwards += MapHost_OnMouseWheelDownwards;

            Editor.BackgroundColor = Color.Black;
        }

        protected void DrawMap()
        {
            Editor.spriteBatch.Draw(HexMaps[CurrentMap], new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (HexMaps[CurrentMap].Width / 2),
                (Editor.graphics.Viewport.Height / 2) - (HexMaps[CurrentMap].Height / 2)),
                Color.White);
        }
    }
}
