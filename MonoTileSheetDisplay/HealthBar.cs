using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoTileSheetDisplay
{
    class HealthBar
    {
        public int health;
        private Texture2D txHealthBar; // hold the texture
        Vector2 position; // Position on the screen
        public Rectangle HealthRect
        {
            get
            {
                return new Rectangle((int)position.X , 
                                (int)position.Y , health, 10);
            }
        }

        public HealthBar(GraphicsDevice dev, Vector2 pos)
        {
            txHealthBar = new Texture2D(dev, 1, 1);
            txHealthBar.SetData(new[] { Color.White });
            position = pos;

        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (health > 60)
                spriteBatch.Draw(txHealthBar, HealthRect, Color.Green);
            else if (health > 30 && health <= 60)
                spriteBatch.Draw(txHealthBar, HealthRect, Color.Orange);
            else if (health > 0 && health < 30)
                spriteBatch.Draw(txHealthBar, HealthRect, Color.Red);

        }

    }
}
