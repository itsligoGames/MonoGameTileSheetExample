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
    class CrossHair : AnimateSheetSprite
    {

        public CrossHair(Vector2 userPosition, List<TileRef> cursor, int frameWidth, int frameHeight, float layerDepth) : base(userPosition, cursor, frameWidth, frameHeight, layerDepth)
            {
        
            }

        public override void Update(GameTime gametime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                this.Tileposition += new Vector2(1, 0) ;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                this.Tileposition += new Vector2(-1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                this.Tileposition += new Vector2(0, -1);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                this.Tileposition += new Vector2(0, 1) ;
            
            // Make sure the Cross Hair stays in the bounds see previous lab for details
            //position = Vector2.Clamp(position, Vector2.Zero,
            //                                new Vector2(gameScreen.Width - spriteWidth,
            //                                            gameScreen.Height - spriteHeight));
            
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch,Texture2D tx)
        {

            base.Draw(spriteBatch,tx);
        }
    }
}
