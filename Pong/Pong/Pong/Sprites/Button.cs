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
    public class Button : GameSprite
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
                float blah = Left;
                if (_sourceRectangle.HasValue)
                {
                    blah += _sourceRectangle.Value.Width * _scale.X;
                }
                else
                {
                    blah += _texture.Width * _scale.X;
                }
                return blah;
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
                float blah = Top;
                if (_sourceRectangle.HasValue)
                {
                    blah += _sourceRectangle.Value.Height * _scale.Y;
                }
                else
                {
                    blah += _texture.Height * _scale.Y;
                }
                return blah;
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

        private Rectangle[] _rectangles;

        public Button(Texture2D image, Vector2 location, Color tint) :
            base(image, location, tint)
        {
            ls = Mouse.GetState();
        }

        public Button(Texture2D releasedImage, Vector2 location, Color tint, params Rectangle[] rectangles) :
            base(releasedImage, location, tint)
        {

            _rectangles = rectangles;
            SourceRectangle = _rectangles[0];

            ls = Mouse.GetState();
        }

        MouseState ls;
        bool isPressed = false;

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            
            

            if (_rectangles != null)
            {

                _origin.Y = SourceRectangle.Value.Height;

                if (mouse.X > Left && mouse.X < Right && mouse.Y > Top && mouse.Y < Bottom)
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (!isPressed)
                        {
                            Mouse.SetPosition(mouse.X, mouse.Y + _rectangles[0].Height - _rectangles[1].Height);
                        }
                        isPressed = true;
                        SourceRectangle = _rectangles[1];
                    }
                    else
                    {
                        isPressed = false;
                        SourceRectangle = _rectangles[0];
                    }
                }
                else
                {
                    isPressed = false;
                    SourceRectangle = _rectangles[0];
                }
            }


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
