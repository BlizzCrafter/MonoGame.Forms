using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.NET.Controls;
using MonoGame.Forms.NET.Samples.Utils;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGame.Forms.NET.Samples.Tests
{
    public class AdvancedInputTest : MonoGameControl
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
                  Editor.GraphicsDevice.Viewport.Width / 2,
                  Editor.GraphicsDevice.Viewport.Height / 2);
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
        
        GamePadState NewGamePadState, OldGamePadState;

        public bool ShowStats = true, ShowControls = true;

        string ControlsHelpKeyboard = $@"Movement: [W,A,S,D]
Speed: [I, O]
Scale: Hold [E] + press [I, O]
Color (Increase): [7, 8, 9]
Color (Decrease): [4, 5, 6]";

        string ControlsHelpGamePad = $@"Movement: [L-Thumbstick]
Speed: [L-Trigger, R-Trigger]
Scale: [R-Thumbstick(Y)]
Color (Decrease): Hold [L-Shoulder] and press [B, A, X]
Color (Increase): Hold [R-Shoulder] and press [B, A, X]";

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
            
            NewGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

            if (NewGamePadState.IsConnected)
            {
                PlayerPosition.X += NewGamePadState.ThumbSticks.Left.X * (PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                PlayerPosition.Y -= NewGamePadState.ThumbSticks.Left.Y * (PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Player.SetAnimationTimeMax = 1 - Math.Abs((NewGamePadState.ThumbSticks.Left.X + NewGamePadState.ThumbSticks.Left.Y) / 2) + 0.5f;

                Vector2 angleVector = new Vector2(NewGamePadState.ThumbSticks.Left.X, -NewGamePadState.ThumbSticks.Left.Y);
                float angleFromVector = (float)Math.Atan2(angleVector.X, -angleVector.Y);

                if (Math.Abs(angleFromVector) > 0)
                {
                    PlayerRotation = angleFromVector;
                    Moving = true;
                }
                else Moving = false;

                if (NewGamePadState.IsButtonDown(Buttons.RightTrigger)) PlayerSpeed++;
                else if (NewGamePadState.IsButtonDown(Buttons.LeftTrigger)) PlayerSpeed--;

                PlayerScale = 1f + (NewGamePadState.ThumbSticks.Right.Y * 2);

                if (NewGamePadState.IsButtonDown(Buttons.RightShoulder))
                {
                    if (NewGamePadState.IsButtonDown(Buttons.B)) PlayerColorR++;
                    else if (NewGamePadState.IsButtonDown(Buttons.A)) PlayerColorG++;
                    else if (NewGamePadState.IsButtonDown(Buttons.X)) PlayerColorB++;

                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (NewGamePadState.IsButtonDown(Buttons.LeftShoulder))
                {
                    if (NewGamePadState.IsButtonDown(Buttons.B)) PlayerColorR--;
                    else if (NewGamePadState.IsButtonDown(Buttons.A)) PlayerColorG--;
                    else if (NewGamePadState.IsButtonDown(Buttons.X)) PlayerColorB--;

                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
            }
            else
            {
                #region Keyboard Controls

                #region Movement

                #region Up Movement

                //Up
                if (Keyboard.GetState().IsKeyDown(Keys.W) &&
                    Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    Moving = true;
                    PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(0);
                }

                //Up Right
                if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Moving = true;
                    PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(45);
                }

                //Up Left
                if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Moving = true;
                    PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerPosition.Y -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(-45);
                }

                #endregion

                #region Down Movement

                //Down
                if (Keyboard.GetState().IsKeyDown(Keys.S) &&
                    Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    Moving = true;
                    PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(180);
                }

                //Down Right
                if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Moving = true;
                    PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(135);
                }

                //Down Left
                if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Moving = true;
                    PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerPosition.Y += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(-135);
                }

                #endregion

                #region Left Movement

                //Left
                if (Keyboard.GetState().IsKeyDown(Keys.A) &&
                    Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    Moving = true;
                    PlayerPosition.X -= PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(-90);
                }

                #endregion

                #region Right Movement

                //Right
                if (Keyboard.GetState().IsKeyDown(Keys.D) &&
                    Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S))
                {
                    Moving = true;
                    PlayerPosition.X += PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    PlayerRotation = MathHelper.ToRadians(90);
                }

                #endregion

                if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) &&
                    Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D)) Moving = false;

                #endregion

                #region Stats

                if (Keyboard.GetState().IsKeyUp(Keys.E))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.O)) PlayerSpeed++;
                    else if (Keyboard.GetState().IsKeyDown(Keys.I)) PlayerSpeed--;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.O)) PlayerScale += 0.1f;
                    else if (Keyboard.GetState().IsKeyDown(Keys.I)) PlayerScale -= 0.1f;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D7))
                {
                    PlayerColorR++;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D8))
                {
                    PlayerColorG++;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D9))
                {
                    PlayerColorB++;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    PlayerColorR--;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D5))
                {
                    PlayerColorG--;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D6))
                {
                    PlayerColorB--;
                    Player.GetDrawingColor = new Color(PlayerColorR, PlayerColorG, PlayerColorB);
                }

                #endregion

                #endregion
            }

            OldGamePadState = GamePad.GetState(PlayerIndex.One);
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
                string controlsToDraw = (NewGamePadState.IsConnected ? ControlsHelpGamePad : ControlsHelpKeyboard);

                Vector2 controlsHelpPosition = new Vector2(
                    Editor.GraphicsDevice.Viewport.Width - DrawFont.MeasureString(controlsToDraw).X,
                    Editor.GraphicsDevice.Viewport.Height - DrawFont.MeasureString(controlsToDraw).Y);

                Editor.spriteBatch.DrawString(DrawFont, controlsToDraw, new Vector2(controlsHelpPosition.X - 1, controlsHelpPosition.Y - 1), Color.Black);
                Editor.spriteBatch.DrawString(DrawFont, controlsToDraw, controlsHelpPosition, Color.Lime);
            }

            #endregion

            Editor.spriteBatch.End();
        }
    }
}
