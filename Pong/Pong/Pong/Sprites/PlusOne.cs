using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong.Sprites
{
    public class PlusOne : SlidingSprite 
    {
        private Color _originalColor;

        public PlusOne(Texture2D texture, Vector2 position, Color tintColor)
            : base(texture, position, tintColor)
        {
            _originalColor = tintColor;

            IsVisible = false;
            SlideUpdateCount = 200;
            SetCenterAsOrigin();

            SlideCompleted += new SlideCompletedState(PlusOne_SlideCompleted);
        }

        void PlusOne_SlideCompleted()
        {
            IsVisible = false;
            _scale = Vector2.One;
            _tintColor = _originalColor;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsVisible)
            {
                return;
            }

            _scale *= 0.99f;

            //TODO: Change to if statements; clamping will not work due to wrap around values. DO NOT decrement if value reaches 0 for each color componenet
           
            _tintColor = new Color(_tintColor.R = (_tintColor.R != 0) ? (byte)(_tintColor.R - 1) : _tintColor.R,
                                    _tintColor.G = (_tintColor.G != 0) ? (byte)(_tintColor.G - 1) : _tintColor.G,
                                    _tintColor.B = (_tintColor.B != 0) ? (byte)(_tintColor.B - 1) : _tintColor.B, 
                                    _tintColor.A = (_tintColor.A != 0) ? (byte)(_tintColor.A - 1) : _tintColor.A);

            base.Update(gameTime);
        }

    }
}
