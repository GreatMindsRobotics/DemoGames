using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    class Button : GameSprite
    {
        bool isClicked = false;

        public bool IsClicked
        {
            get
            {
                return isClicked;
            }
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


        public float VectorY
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }

        public float VectorX
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
            }
        }

        public Button(Texture2D image, Vector2 location, Color tint) :
            base(image, location, tint)
        {
            ls = Mouse.GetState();
        }

        MouseState ls;

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.X > Left && mouse.X < Right && mouse.Y > Top && mouse.Y < Bottom && mouse.LeftButton == ButtonState.Released && ls.LeftButton == ButtonState.Pressed)
            {
                isClicked = true;
            }
            else
            {
                isClicked = false;
            }


            ls = mouse;
            base.Update(gameTime);
        }

    }
}
