using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Tests.Utils;

namespace MonoGame.Forms.Tests.Tests
{
    public class AdvancedControlsTest : UpdateWindow
    {
        SpriteFont DrawFont;

        Animation Player;
        bool Moving = false;
        float PlayerRotation = 0f;        
        float PlayerSpeed = 100f;
        float PlayerScale = 1f;
        byte PlayerColorR = 255;
        byte PlayerColorG = 255;
        byte PlayerColorB = 255;

        Vector2 PlayerPosition;
        public void ResetPlayer()
        {
            PlayerPosition = new Vector2(
                  Editor.graphics.Viewport.Width / 2,
                  Editor.graphics.Viewport.Height / 2);
            PlayerRotation = 0f;
            PlayerSpeed = 100f;
            PlayerScale = 1f;
            Player.GetDrawingColor = Color.White;
            PlayerColorR = 255;
            PlayerColorG = 255;
            PlayerColorB = 255;
            Player.ResetAnimation(true);
            Moving = false;
        }

        public bool ShowStats = true, ShowControls = true;

        string ControlsHelp = $@"Movement: [W,A,S,D]
Speed: [OemPlus, OemMinus]
Scale: Hold [E] + press [OemPlus, OemMinus]
Color (Increase): [Num7, Num8, Num9]
Color (Decrease): [Num4, Num5, Num6]";

        protected override void Initialize()
        {
            base.Initialize();

            DrawFont = Editor.Content.Load<SpriteFont>("DrawFont");
            Player = new Animation(Editor.Content.Load<Texture2D>("Player_Sheet"), 8, 1, 0.5f, true, true);
            ResetPlayer();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            #region Keyboard Controls

            #region Movement

            #region Up Movement

            //Up
            if (GetKeyboardState.IsKeyDown(Keys.W) &&
                !GetKeyboardState.IsKeyDown(Keys.S) && !GetKeyboardState.IsKeyDown(Keys.A) && !GetKeyboardState.IsKeyDown(Keys.D))
            {
                Moving = true;
                PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(0);
            }

            //Up Right
            if (GetKeyboardState.IsKeyDown(Keys.W) && GetKeyboardState.IsKeyDown(Keys.D))
            {
                Moving = true;
                PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(45);
            }

            //Up Left
            if (GetKeyboardState.IsKeyDown(Keys.W) && GetKeyboardState.IsKeyDown(Keys.A))
            {
                Moving = true;
                PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(-45);
            }

            #endregion

            #region Down Movement

            //Down
            if (GetKeyboardState.IsKeyDown(Keys.S) &&
                !GetKeyboardState.IsKeyDown(Keys.W) && !GetKeyboardState.IsKeyDown(Keys.A) && !GetKeyboardState.IsKeyDown(Keys.D))
            {
                Moving = true;
                PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(180);
            }

            //Down Right
            if (GetKeyboardState.IsKeyDown(Keys.S) && GetKeyboardState.IsKeyDown(Keys.D))
            {
                Moving = true;
                PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(135);
            }

            //Down Left
            if (GetKeyboardState.IsKeyDown(Keys.S) && GetKeyboardState.IsKeyDown(Keys.A))
            {
                Moving = true;
                PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(-135);
            }

            #endregion

            #region Left Movement

            //Left
            if (GetKeyboardState.IsKeyDown(Keys.A) &&
                !GetKeyboardState.IsKeyDown(Keys.W) && !GetKeyboardState.IsKeyDown(Keys.S) && !GetKeyboardState.IsKeyDown(Keys.D))
            {
                Moving = true;
                PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(-90);
            }

            #endregion

            #region Right Movement

            //Right
            if (GetKeyboardState.IsKeyDown(Keys.D) &&
                !GetKeyboardState.IsKeyDown(Keys.A) && !GetKeyboardState.IsKeyDown(Keys.W) && !GetKeyboardState.IsKeyDown(Keys.S))
            {
                Moving = true;
                PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerRotation = MathHelper.ToRadians(90);
            }

            #endregion

            if (!GetKeyboardState.IsKeyDown(Keys.W) && !GetKeyboardState.IsKeyDown(Keys.S) &&
                !GetKeyboardState.IsKeyDown(Keys.A) && !GetKeyboardState.IsKeyDown(Keys.D)) Moving = false;

            #endregion

            #region Stats

            if (!GetKeyboardState.IsKeyDown(Keys.E))
            {
                if (GetKeyboardState.IsKeyDown(Keys.OemPlus)) PlayerSpeed++;
                else if (GetKeyboardState.IsKeyDown(Keys.OemMinus)) PlayerSpeed--;
            }
            else
            {
                if (GetKeyboardState.IsKeyDown(Keys.OemPlus)) PlayerScale += 0.1f;
                else if (GetKeyboardState.IsKeyDown(Keys.OemMinus)) PlayerScale -= 0.1f;
            }

            if (GetKeyboardState.IsKeyDown(Keys.NumPad7))
            {
                PlayerColorR++;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }
            else if (GetKeyboardState.IsKeyDown(Keys.NumPad8))
            {
                PlayerColorG++;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }
            else if (GetKeyboardState.IsKeyDown(Keys.NumPad9))
            {
                PlayerColorB++;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }
            else if (GetKeyboardState.IsKeyDown(Keys.NumPad4))
            {
                PlayerColorR--;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }
            else if (GetKeyboardState.IsKeyDown(Keys.NumPad5))
            {
                PlayerColorG--;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }
            else if (GetKeyboardState.IsKeyDown(Keys.NumPad6))
            {
                PlayerColorB--;
                Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
            }

            #endregion

            #endregion
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin();

            Editor.spriteBatch.Draw(Player.Texture, PlayerPosition,
                       (!Moving ? Player.GetFirstFrame() : Player.DoAnimation()),
                       Player.GetDrawingColor,
                       PlayerRotation,
                       Player.GetOrigin,
                       PlayerScale,
                       SpriteEffects.None, 0f);

            #region Stats

            if (ShowStats)
            {
                string Stats = $@"Speed: {PlayerSpeed}
Rotation: {MathHelper.ToDegrees(PlayerRotation)}
Scale: {PlayerScale}
Color R:{PlayerColorR}, G:{PlayerColorG}, B:{PlayerColorB}";

                Editor.spriteBatch.DrawString(DrawFont, Stats, new Vector2(1, 1), Color.Black);
                Editor.spriteBatch.DrawString(DrawFont, Stats, Vector2.Zero, Color.Yellow);
            }

            #endregion

            #region Controls

            if (ShowControls)
            {
                Vector2 controlsHelpPosition = new Vector2(
                    Editor.graphics.Viewport.Width - DrawFont.MeasureString(ControlsHelp).X,
                    Editor.graphics.Viewport.Height - DrawFont.MeasureString(ControlsHelp).Y);

                Editor.spriteBatch.DrawString(DrawFont, ControlsHelp, new Vector2(controlsHelpPosition.X - 1, controlsHelpPosition.Y - 1), Color.Black);
                Editor.spriteBatch.DrawString(DrawFont, ControlsHelp, controlsHelpPosition, Color.Lime);
            }

            #endregion

            Editor.spriteBatch.End();
        }
    }
}
