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
    public class Paddle : GameSprite
    {
        public Keys UpKey { get; set; }
        public Keys DownKey { get; set; }
        public GamePadMapper.GamePadButtons UpButton { get; set; }
        public GamePadMapper.GamePadButtons DownButton { get; set; }

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

        public Paddle(Texture2D image, Vector2 location, Color tint):
            base(image, location, tint)
        {
            /*UpButton = GamePadMapper.GamePadButtons.LeftTrigger;
            GamePadMapper mapper = new GamePadMapper(PlayerIndex.One);
            mapper.IsButtonDown(UpButton);*/
        }

        public override void Update(GameTime gameTime)
        {
            //todo: add moving functions
            base.Update(gameTime);
        }

    }
}
