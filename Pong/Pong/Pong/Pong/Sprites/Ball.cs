﻿using System;
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

        public float Left
        {
            get
            {
                return _position.X - _origin.X * _scale.X;

            }
        }

        public float Right
        {
            get
            {
                return _position.X + _origin.X * _scale.X;

            }
        }

        public float Top
        {
            get
            {
                return _position.Y - _origin.Y * _scale.Y;

            }
        }

        public float Bottom
        {
            get
            {
                return _position.Y + _origin.Y * _scale.Y;

            }
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

        private bool _isPaused = false;

        public bool IsPaused
        {
            set
            {
                _isPaused = value;
            }
            get
            {
                return _isPaused;
            }
        }

        public BallState BallState { get; set; }

        private Rectangle _boundingBox;

        public Ball (Texture2D image, Vector2 location,Color tint):
        base(image, location, tint)
        {
            _speed = Vector2.Zero; 
        }

        public Ball(Texture2D image, Vector2 location, Color tint, Vector2 speed) :
            base(image, location, tint)
        {
            _speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            if (!_isPaused)
            {
                _position += _speed;
                base.Update(gameTime);
            }
        }

    }
}
