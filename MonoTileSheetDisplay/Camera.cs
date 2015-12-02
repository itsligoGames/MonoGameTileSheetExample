using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileManagerNS
{
    class Camera
    {
        Vector2 _camPos = Vector2.Zero;
        Vector2 _worldBound;
        Viewport _view;
        public Matrix CurrentCameraTranslation { get
            {
                return Matrix.CreateTranslation(new Vector3(-_camPos, 0));
            } }


        public Camera(Vector2 startPos, Vector2 bound)
        {
            _camPos = startPos;
            _worldBound = bound;
        }

        public void move(Vector2 delta, Viewport v)
        {
            _camPos += delta;
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

        public void follow(Vector2 followPos, Viewport v)
        {
            _camPos = followPos - new Vector2(v.Width/2,v.Height/2);
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

    }
}
