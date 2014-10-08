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
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsVisible)
            {
                return;
            }

            _scale *= 0.99f;

            //TODO: Change to if statements; clamping will not work due to wrap around values. DO NOT decrement if value reaches 0 for each color componenet
            _tintColor = new Color(MathHelper.Clamp(--_tintColor.R, 0, 255), 
                                    MathHelper.Clamp(--_tintColor.G, 0, 255), 
                                    MathHelper.Clamp(--_tintColor.B, 0, 255), 
                                    MathHelper.Clamp(--_tintColor.A, 0, 255));

            if (Position == SlideTo)
            {
                IsVisible = false;
                _scale = Vector2.One;
                _tintColor = _originalColor;
            }

            base.Update(gameTime);
        }

    }
}
