using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameFormsPCLTest.Utils
{
    public class Animation
    {
        public float GetAnimationTime
        {
            get { return AnimationTime; }
            set
            {
                AnimationTime = value;
                AnimationTimeMax = value;
                CurrentAnimationTime = value;
            }
        }
        private float AnimationTime = 0.5f;
        private float AnimationTimeMax = 0.5f, CurrentAnimationTime = 0.5f;

        public bool GetLoop
        {
            get { return _Loop; }
            set { _Loop = value; }
        }
        private bool _Loop = false;

        public bool HorizontalAnimation { get; set; }

        public int GetVerticalParts
        {
            get { return _VerticalParts; }
            set
            {
                _VerticalParts = value;
                PartSizeY = Texture.Height / _VerticalParts;

                ResetAnimation(true);
            }
        }
        private int _VerticalParts;

        public int GetHorizontalParts
        {
            get { return _HorizontalParts; }
            set
            {
                _HorizontalParts = value;
                PartSizeX = Texture.Width / _HorizontalParts;

                ResetAnimation(true);
            }
        }
        private int _HorizontalParts;

        //CurrentRow
        public int GetCurrentH
        {
            get { return _CurrentH; }
            set { _CurrentH = value; }
        }
        private int _CurrentH = 1;

        //CurrentColumn
        public int GetCurrentV
        {
            get { return _CurrentV; }
            set { _CurrentV = value; }
        }
        private int _CurrentV = 1;

        public int PartSizeX { get; set; }
        public int PartSizeY { get; set; }

        public Vector2 GetOrigin
        {
            get { return new Vector2(PartSizeX / 2, PartSizeY / 2); }
            private set { _Origin = value; }
        }
        private Vector2 _Origin;

        public Vector2 TextureSize
        {
            get { return new Vector2(Texture.Width, Texture.Height); }
        }

        public Texture2D Texture { get; set; }

        public Color GetDrawingColor
        {
            get { return _DrawingColor; }
            set { _DrawingColor = value; }
        }
        private Color _DrawingColor;

        public int PositionX = 0, PositionY = 0, AnimLoops = 0;

        private bool LastRow = false;

        public bool EndOfAnimation = false;

        public Animation(
            Texture2D texture,
            int horizontalParts,
            int verticalParts,
            float animationTime,
            bool horizontalAnimation,
            bool loop,
            bool drawDeathAnimWithShadow = true)
        {
            Texture = texture;
            GetLoop = loop;
            GetHorizontalParts = horizontalParts;
            GetVerticalParts = verticalParts;
            GetAnimationTime = animationTime;
            HorizontalAnimation = horizontalAnimation;
            GetDrawingColor = Color.White;
        }

        public void UpdateDrawingFrame()
        {
            PartSizeX = Texture.Width / _HorizontalParts;
            PartSizeY = Texture.Height / _VerticalParts;

            ResetAnimation(true);
        }

        public void ResetAnimation(bool resetFrames)
        {
            if (resetFrames == true)
            {
                PositionX = 0;
                PositionY = 0;
                GetCurrentH = 1;
                GetCurrentV = 1;
            }
            AnimationTime = AnimationTimeMax;
            EndOfAnimation = false;
            LastRow = false;
        }

        public Rectangle GetLastFrame()
        {
            PositionX = Texture.Width - PartSizeX;
            PositionY = Texture.Height - PartSizeY;

            return new Rectangle(PositionX, PositionY, PartSizeX, PartSizeY);
        }
        public int GetLastFrameIndex()
        {
            int i = GetHorizontalParts * GetVerticalParts;
            return i;
        }
        public int GetCurrentFrameIndex()
        {
            int result = 0;
            int H = ((PositionX + PartSizeX) / PartSizeX);
            int V = ((PositionY + PartSizeY) / PartSizeY);

            if (HorizontalAnimation == false)
            {
                result = (H + V) - GetCurrentH;
                if (GetCurrentH > 1) result += (GetVerticalParts * (GetCurrentH - 1));
            }
            else
            {
                result = (H + V) - GetCurrentV;
                if (GetCurrentV > 1) result += (GetHorizontalParts * (GetCurrentV - 1));
            }
            return result;
        }
        public Rectangle GetCurrentFrame()
        {
            return new Rectangle(PositionX, PositionY, PartSizeX, PartSizeY);
        }

        //Interface
        public void OneFrameForward()
        {
            if (HorizontalAnimation == false)
            {
                if (PositionY < Texture.Height - PartSizeY)
                {
                    PositionY += PartSizeY;
                    GetCurrentV++;
                }
                else
                {
                    PositionY = 0;
                    GetCurrentV = 1;

                    if (PositionX < Texture.Width - PartSizeX)
                    {
                        PositionX += PartSizeX;
                        GetCurrentH++;
                    }
                    else
                    {
                        PositionX = 0;
                        GetCurrentH = 1;
                    }
                }
            }
            else
            {
                if (PositionX < Texture.Width - PartSizeX)
                {
                    PositionX += PartSizeX;
                    GetCurrentH++;
                }
                else
                {
                    PositionX = 0;
                    GetCurrentH = 1;

                    if (PositionY < Texture.Height - PartSizeY)
                    {
                        PositionY += PartSizeY;
                        GetCurrentV++;
                    }
                    else
                    {
                        PositionY = 0;
                        GetCurrentV = 1;
                    }
                }
            }

            if (PositionX >= Texture.Width ||
                PositionY >= Texture.Height)
            {
                PositionX = 0;
                PositionY = 0;

                GetCurrentH = 1;
                GetCurrentV = 1;
            }
        }
        public void OneFrameBackwards()
        {
            if (HorizontalAnimation == false)
            {
                if (PositionY > 0)
                {
                    PositionY -= PartSizeY;
                    GetCurrentV--;
                }
                else
                {
                    PositionY = Texture.Height - PartSizeY;
                    GetCurrentV = GetVerticalParts;

                    if (PositionX > 0)
                    {
                        PositionX -= PartSizeX;
                        GetCurrentH--;
                    }
                    else
                    {
                        PositionX = Texture.Width - PartSizeX;
                        PositionY = Texture.Height - PartSizeY;

                        GetCurrentH = GetHorizontalParts;
                        GetCurrentV = GetVerticalParts;
                    }
                }
            }
            else
            {
                if (PositionX > 0)
                {
                    PositionX -= PartSizeX;
                    GetCurrentH--;
                }
                else
                {
                    PositionX = Texture.Width - PartSizeX;
                    GetCurrentH = GetHorizontalParts;

                    if (PositionY > 0)
                    {
                        PositionY -= PartSizeY;
                        GetCurrentV--;
                    }
                    else
                    {
                        PositionX = Texture.Width - PartSizeX;
                        PositionY = Texture.Height - PartSizeY;

                        GetCurrentH = GetHorizontalParts;
                        GetCurrentV = GetVerticalParts;
                    }
                }
            }

            if (PositionX < 0 || PositionY < 0)
            {
                PositionX = Texture.Width - PartSizeX;
                PositionY = Texture.Height - PartSizeY;

                GetCurrentH = GetHorizontalParts;
                GetCurrentV = GetVerticalParts;
            }
        }

        public Rectangle DoAnimation()
        {
            if (CurrentAnimationTime > 0) CurrentAnimationTime -= 0.5f;
            else
            {
                if (HorizontalAnimation == false)
                {
                    if (PositionY < Texture.Height - PartSizeY)
                    {
                        PositionY += PartSizeY;
                        GetCurrentV++;
                    }
                    else
                    {
                        if (PositionX < Texture.Width - PartSizeX)
                        {
                            PositionX += PartSizeX;
                            PositionY = 0;
                            GetCurrentV = 1;
                            GetCurrentH++;
                        }
                        else LastRow = true;

                        if (LastRow == true && PositionY == Texture.Height - PartSizeY && GetLoop == true)
                        {
                            ResetAnimation(true);
                            AnimLoops++;
                        }
                        else if (LastRow == true && PositionY == Texture.Height - PartSizeY && GetLoop == false)
                        {
                            EndOfAnimation = true;
                            LastRow = false;
                        }
                    }
                }
                else if (HorizontalAnimation == true)
                {
                    if (PositionX < Texture.Width - PartSizeX)
                    {
                        PositionX += PartSizeX;
                        GetCurrentH++;
                    }
                    else
                    {
                        if (PositionY < Texture.Height - PartSizeY)
                        {
                            PositionX = 0;
                            PositionY += PartSizeY;
                            GetCurrentH = 1;
                            GetCurrentV++;
                        }
                        else LastRow = true;

                        if (LastRow == true && PositionX == Texture.Width - PartSizeX && GetLoop == true)
                        {
                            ResetAnimation(true);
                            AnimLoops++;
                        }
                        else if (LastRow == true && PositionX == Texture.Width - PartSizeX && GetLoop == false)
                        {
                            EndOfAnimation = true;
                            LastRow = false;
                        }
                    }
                }
                if (EndOfAnimation == false) CurrentAnimationTime = AnimationTimeMax;
            }
            return new Rectangle(PositionX, PositionY, PartSizeX, PartSizeY);
        }
        public Rectangle DoBackwardsAnimation()
        {
            if (CurrentAnimationTime > 0) CurrentAnimationTime -= 0.5f;
            else
            {
                if (HorizontalAnimation == false)
                {
                    if (PositionY > 0)
                    {
                        PositionY -= PartSizeY;
                        GetCurrentV--;
                    }
                    else
                    {
                        if (PositionX > 0)
                        {
                            PositionX -= PartSizeX;
                            PositionY = Texture.Height - PartSizeY;

                            GetCurrentV = GetVerticalParts;
                            GetCurrentH--;
                        }
                        else LastRow = true;

                        if (LastRow == true && PositionY == 0 && GetLoop == true)
                        {
                            ResetAnimation(true);
                            AnimLoops++;
                        }
                        else if (LastRow == true && PositionY == 0 && GetLoop == false)
                        {
                            EndOfAnimation = true;
                            LastRow = false;
                        }
                    }
                }
                else if (HorizontalAnimation == true)
                {
                    if (PositionX > 0)
                    {
                        PositionX -= PartSizeX;
                        GetCurrentH--;
                    }
                    else
                    {
                        if (PositionY > 0)
                        {
                            PositionX = Texture.Width - PartSizeX;
                            PositionY -= PartSizeY;
                            GetCurrentH = GetHorizontalParts;
                            GetCurrentV--;
                        }
                        else LastRow = true;

                        if (LastRow == true && PositionX == 0 && GetLoop == true)
                        {
                            PositionX = 0;
                            PositionY = 0;
                            GetCurrentH = 1;
                            GetCurrentV = 1;
                            LastRow = false;
                            AnimLoops++;
                        }
                        else if (LastRow == true && PositionX == 0 && GetLoop == false)
                        {
                            EndOfAnimation = true;
                            LastRow = false;
                        }
                    }
                }
                if (EndOfAnimation == false) CurrentAnimationTime = AnimationTimeMax;
            }
            return new Rectangle(PositionX, PositionY, PartSizeX, PartSizeY);
        }
    }
}