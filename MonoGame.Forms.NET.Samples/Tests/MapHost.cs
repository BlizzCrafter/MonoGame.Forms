using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using MonoGame.Forms.NET.Controls;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public abstract class MapHost : MonoGameControl
    {
        private Texture2D[] HexMaps;
        private int CurrentMap { get; set; } = 0;
        private bool ShowDebugDisplay { get; set; } = false;

        private bool CamMouseDown = false;
        private System.Drawing.Point CamFirstMouseDownPosition;

        #region Mouse Input Events
        
        private void MapHost_OnMouseWheelUpwards(MouseEventArgs e)
        {
            Editor.Cam.Zoom += 0.1f;
        }

        private void MapHost_OnMouseWheelDownwards(MouseEventArgs e)
        {
            if (Editor.Cam.Zoom > 0.7f) Editor.Cam.Zoom -= 0.1f;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Middle) Editor.ResetCam();
            else if (e.Button == MouseButtons.XButton1) CurrentMap--;
            else if (e.Button == MouseButtons.XButton2) CurrentMap++;
            else if (e.Button == MouseButtons.Right) ShowDebugDisplay = !ShowDebugDisplay;

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

            Editor.BackgroundColor = new Color(20, 19, 40);
            Editor.ShowCamPosition = true;
        }

        protected void DrawMap()
        {
            Editor.BeginCamera2D();

            Editor.spriteBatch.Draw(HexMaps[CurrentMap], new Vector2(
                (Editor.GraphicsDevice.Viewport.Width / 2) - (HexMaps[CurrentMap].Width / 2),
                (Editor.GraphicsDevice.Viewport.Height / 2) - (HexMaps[CurrentMap].Height / 2)),
                Color.White);

            Editor.EndCamera2D();

            if (ShowDebugDisplay) Editor.DrawDisplay();
        }

        protected override void Dispose(bool disposing)
        {
            OnMouseWheelUpwards -= MapHost_OnMouseWheelUpwards;
            OnMouseWheelDownwards -= MapHost_OnMouseWheelDownwards;

            base.Dispose(disposing);
        }
    }
}
