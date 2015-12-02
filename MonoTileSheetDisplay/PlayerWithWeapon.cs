using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TileManagerNS;
using MonoTileSheetDisplay;
using Engine.Engines;

namespace AnimatedSprite
{
    public enum DIRECTION {LEFT,RIGHT,UP,DOWN }
    public enum STATE { MOVING,STILL}

        class PlayerWithWeapon : AnimateSheetSprite
        {
            
            private Projectile myProjectile;
            protected CrossHair Site;
            private Tile currentTilePostion;
            private Rectangle _drawRectangle;
            private DIRECTION _direction;
            private List<List<TileRef>> _directionFrames = new List<List<TileRef>>();
            public Vector2 CentrePos
            {
                get { return PixelPosition + new Vector2(FrameWidth/ 2, FrameHeight/ 2); }
                
            }
            public float speed = 0.1f;
            public Vector2 TargetTilePos;
            public STATE MovingState = STATE.STILL;

        public Projectile MyProjectile
        {
            get
            {
                return myProjectile;
            }

            set
            {
                myProjectile = value;
            }
        }

        public Tile CurrentPlayerTile
        {
            get
            {
                return currentTilePostion;
            }

            set
            {
                currentTilePostion = value;
            }
        }

        public Rectangle DrawRectangle
        {
            get
            {
                return new Rectangle(currentTilePostion.X,
                    currentTilePostion.Y, 
                    currentTilePostion.TileWidth,
                    currentTilePostion.TileHeight);
            }

            set
            {
                _drawRectangle = value;
            }
        }

        public DIRECTION Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
            }
        }

        private Vector2 TileBound;

        public PlayerWithWeapon(Vector2 userPosition, Vector2 tileBounds,
            List<TileRef> InitialSheetRefs, 
                int frameWidth, int frameHeight, float layerDepth)
            : base(userPosition, InitialSheetRefs, frameWidth, frameHeight, layerDepth)
        {

            _directionFrames.Add(InitialSheetRefs); // Stopped
            _directionFrames.Add(new List<TileRef>()); // LEFT
            _directionFrames.Add(new List<TileRef>()); // RIGHT
            _directionFrames.Add(new List<TileRef>()); // UP
            _directionFrames.Add(new List<TileRef>()); // Down All to be set by setFrameSet
            TileBound = tileBounds;
            }

        public void loadProjectile(Projectile r)
            {
                MyProjectile = r;
            }

        public void setFrameSet(DIRECTION d, List<TileRef> sheetRefs)
        {
            _directionFrames[(int)d] = sheetRefs;
        }

        public void checkforMovement()
        {
            DIRECTION oldDirection = Direction;
            if (MovingState != STATE.MOVING)
            {
                if(InputEngine.IsKeyHeld(Keys.Right))
                
                {
                    TargetTilePos = Tileposition + new Vector2(1, 0);
                    _direction = DIRECTION.RIGHT;
                    MovingState = STATE.MOVING;
                }
                if (InputEngine.IsKeyHeld(Keys.Left))
                {
                    TargetTilePos = Tileposition + new Vector2(-1, 0);
                    _direction = DIRECTION.LEFT;
                    MovingState = STATE.MOVING;
                }
                if (InputEngine.IsKeyHeld(Keys.Up))
                {
                    TargetTilePos = Tileposition + new Vector2( 0, -1);
                    _direction = DIRECTION.UP;
                    MovingState = STATE.MOVING;
                }
                if (InputEngine.IsKeyHeld(Keys.Down))
                {
                    TargetTilePos = Tileposition + new Vector2(0, 1);
                    _direction = DIRECTION.DOWN;
                    MovingState = STATE.MOVING;
                }
                // Make sure the player stays in the bounds 
                TargetTilePos = Vector2.Clamp(TargetTilePos, Vector2.Zero,
                                     new Vector2(TileBound.X, TileBound.Y) - new Vector2(1, 1));
            }
            else 
            {
                Vector2 targetDirection = TargetTilePos - Tileposition;
                Vector2 nTarget = Vector2.Normalize(targetDirection);
                float distance = Vector2.DistanceSquared(Tileposition, TargetTilePos);
                if (distance > speed)
                {
                    Tileposition += nTarget * speed;
                }
                else
                {
                    MovingState = STATE.STILL;
                    Tileposition = TargetTilePos;
                    CurrentPlayerTile.X = (int)Tileposition.X;
                    CurrentPlayerTile.Y = (int)Tileposition.Y;
                }

            }
            // Change the image set based on a change of direction
            if (Direction != oldDirection && _directionFrames[(int)_direction].Count() > 0)
            {
                Frames = _directionFrames[(int)_direction];
                CurrentFrame = 0;
            }


            //return moved;
        }
        public override void Update(GameTime gameTime)
        {

            // check for site change
            //checkforMovement();
            //Site.Update(gameTime);
            // Whenever the rocket is still and loaded it follows the player posiion
            if (MyProjectile != null && MyProjectile.ProjectileState == Projectile.PROJECTILE_STATE.STILL)
                MyProjectile.Tileposition = this.Tileposition;
            // if a roecket is loaded
            if (MyProjectile != null)
            {
                // fire the rocket and it looks for the target
                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                    MyProjectile.fire(Site.PixelPosition);
            }


            // Update the Camera with respect to the players new position
            //Vector2 delta = cam.Pos - this.position;
            //cam.Pos += delta;

            if (MyProjectile != null)
                MyProjectile.Update(gameTime);
            // Update the players site
            //Site.Update(gameTime);
            // call Sprite Update to get it to animated 

            base.Update(gameTime);
        }
            
        public override void Draw(SpriteBatch spriteBatch,Texture2D tx)
        {
            base.Draw(spriteBatch,tx);
            //Site.Draw(spriteBatch,tx);
            if (MyProjectile != null && MyProjectile.ProjectileState != Projectile.PROJECTILE_STATE.STILL)
                    MyProjectile.Draw(spriteBatch,tx);
            
        }

    }
}
