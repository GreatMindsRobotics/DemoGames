using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong.Sprites
{
    class Ball : GameSprite
    {

        private Vector2 _speed;

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float SpeedY
        {
            get
            {
                return _speed.Y;
            }
            set
            {
                _speed.Y = value;
            }
        }

        public float SpeedX
        {
            get
            {
                return _speed.X;
            }
            set
            {
                _speed.X = value;
            }
        }

        private Vector2 _vector;

        public Vector2 Vector
        {
            get { return _vector; }
            set { _vector = value; }
        }

        public float VectorY
        {
            get
            {
                return _vector.Y;
            }
            set
            {
                _vector.Y = value;
            }
        }

        public float VectorX
        {
            get
            {
                return _vector.X;
            }
            set
            {
                _speed.X = value;
            }
        }


        public Ball (Texture2D image, Vector2 location,Color tint):
        base(image, location, tint)
        {
            _speed = Vector2.One;
        }

        public Ball(Texture2D image, Vector2 location, Color tint, Vector2 speed) :
            base(image, location, tint)
        {
            _speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            _position += _speed;
            base.Update(gameTime);
        }

    }
}
