using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTileSheetDisplay;

namespace AnimatedSprite
{
    public class AnimateSheetSprite
    {
        //sprite texture and position
        
        private bool visible;
        protected Vector2 origin;
        protected float angleOfRotation;
        protected float spriteDepth = 1f;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private Vector2 _tileposition;
        public Vector2 Tileposition
        {
            get
            {
                return _tileposition;
            }

            set
            {
                _tileposition = value;
            }
        }

        //the number of frames in the sprite sheet
        //the current fram in the animation
        //the time between frames
        int mililsecondsBetweenFrames = 100;
        float timer = 0f;

        //the width and height of our texture
        public int FrameWidth = 0;
        public int FrameHeight = 0;

        //the source of our image within the sprite sheet to draw
        Rectangle sourceRectangle;

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        public Vector2 PixelPosition
        {
            get
            {
                return new Vector2(Tileposition.X * FrameWidth, Tileposition.Y * FrameHeight);
            }
        }


        List<TileRef> Frames = new List<TileRef>();
        private int currentFrame;

        public AnimateSheetSprite(Vector2 userPosition, List<TileRef> sheetRefs, int frameWidth, int frameHeight, float layerDepth)
        {
            spriteDepth = layerDepth;
            Tileposition = userPosition;
            visible = true;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            Frames = sheetRefs;
            // added to allow sprites to rotate
            origin = new Vector2(FrameWidth / 2, FrameHeight/ 2);
            
            angleOfRotation = 0;
            currentFrame = 0;
        }


        public virtual void Update(GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.Milliseconds;

            //if the timer is greater then the time between frames, then animate
                    if (timer > mililsecondsBetweenFrames)
                    {
                        //moce to the next frame
                        currentFrame++;

                        //if we have exceed the number of frames
                        if (currentFrame > Frames.Count() - 1)
                        {
                            currentFrame = 0;
                        }
                        //reset our timer
                        timer = 0f;
                    }
            //set the source to be the current frame in our animation
                    sourceRectangle = new Rectangle(Frames[currentFrame]._sheetPosX * FrameWidth  , 
                        Frames[currentFrame]._sheetPosY * FrameHeight, 
                        FrameWidth, FrameHeight);
            
            }
        public bool collisionDetect(AnimateSheetSprite other)
        {
            Rectangle myBound = new Rectangle((int)this.Tileposition.X, (int)this.Tileposition.Y, this.FrameWidth, this.FrameHeight);
            Rectangle otherBound = new Rectangle((int)other.Tileposition.X, (int)other.Tileposition.Y, other.FrameWidth, other.FrameHeight);
            if (myBound.Intersects(otherBound))
                return true;
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D SpriteSheet)
        {
            if (visible)
            {
                //spriteBatch.Draw(SpriteSheet, new Rectangle((int)PixelPosition.X, (int)PixelPosition.Y, FrameWidth, FrameHeight), SourceRectangle, Color.White);
                spriteBatch.Draw(SpriteSheet,
                    PixelPosition + origin, sourceRectangle,
                    Color.White, angleOfRotation, origin,
                    1.0f, SpriteEffects.None, spriteDepth);
            }
        }       

    }
}
