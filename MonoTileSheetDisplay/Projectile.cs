using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoTileSheetDisplay;

namespace AnimatedSprite
{
    class Projectile : RotatingSprite
    {

            public enum PROJECTILE_STATE { STILL, FIRING, EXPOLODING };
            PROJECTILE_STATE projectileState = PROJECTILE_STATE.STILL;
            protected float RocketVelocity = 4.0f;
            Vector2 textureCenter;
            Vector2 Target;
            AnimateSheetSprite explosion;
            float ExplosionTimer = 0;
            float ExplosionVisibleLimit = 1000;
            Vector2 StartPosition;
           

            public PROJECTILE_STATE ProjectileState
            {
                get { return projectileState; }
                set { projectileState = value; }
            }



        public Projectile(Vector2 userPosition, List<TileRef> projectiletRefs, List<TileRef> explosionRef, int frameWidth, int frameHeight, float layerDepth) : base(userPosition, projectiletRefs, frameWidth, frameHeight, layerDepth)
        {
                Target = Vector2.Zero;
                StartPosition = userPosition;
                ProjectileState = PROJECTILE_STATE.STILL;
                
            }
            public override void Update(GameTime gametime)
            {
                switch (projectileState)
                {
                    case PROJECTILE_STATE.STILL:
                        this.Visible = false;
                        explosion.Visible = false;
                        break;
                    // Using Lerp here could use target - pos and normalise for direction and then apply
                    // Velocity
                    case PROJECTILE_STATE.FIRING:
                        this.Visible = true;                       
                        Tileposition = Vector2.Lerp(Tileposition, Target, 0.02f * RocketVelocity);
                         // rotate towards the Target
                        this.angleOfRotation = TurnToFace(PixelPosition,
                                                Target, angleOfRotation, 1f);
                    if (Vector2.Distance(PixelPosition, Target) < 2)
                        projectileState = PROJECTILE_STATE.EXPOLODING;
                        break;
                    case PROJECTILE_STATE.EXPOLODING:
                        explosion.Tileposition = Target;
                        explosion.Visible = true;
                        break;
                }
                // if the explosion is visible then just play the animation and count the timer
                if (explosion.Visible)
                {
                    explosion.Update(gametime);
                    ExplosionTimer += gametime.ElapsedGameTime.Milliseconds;
                }
                // if the timer goes off the explosion is finished
                if (ExplosionTimer > ExplosionVisibleLimit)
                {
                    explosion.Visible = false;
                    ExplosionTimer = 0;
                projectileState = PROJECTILE_STATE.STILL;
                }

                base.Update(gametime);
            }
            public void fire(Vector2 SiteTarget)
            {
            projectileState = PROJECTILE_STATE.FIRING;
                Target = SiteTarget;
            }   
            public override void Draw(SpriteBatch spriteBatch,Texture2D tx)
            {
                base.Draw(spriteBatch,tx);
                if (explosion.Visible)
                    explosion.Draw( spriteBatch,tx);
                

            }

    }
}
