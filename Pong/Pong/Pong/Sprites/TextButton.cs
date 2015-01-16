using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FontEffectsLib.FontTypes;
using Pong.CoreTypes;

namespace Pong.Sprites
{
    public class TextButton : Button
    {
        FadingFont fadingFont;
        public Color FontColor
        {
            get
            {
                return fadingFont.TintColor;
            }
            set
            {
                fadingFont.TintColor = value;
            }
        }

        public void Reset()
        {
            fadingFont.Reset();
        }


        public string Text
        {
            set
            {
                fadingFont.Text.Clear();
                fadingFont.Text.Append(value);
                //fadingFont.Position = new Vector2(Position.X - fadingFont.Size.X / 2, Top - fadingFont.Size.Y);
            }
        }

         public TextButton(Texture2D image, Vector2 location, Color tint, SpriteFont font, Color fontColor, string text) :
            base(image, location, tint)
        {
            fadingFont = new FadingFont(font, location - _origin + new Vector2(image.Width / 2, image.Height / 2)  * Scale, 0.1f, 1.0f, 0.1f, 1.0f, text, fontColor, true);
            fadingFont.EnableShadow = false;
            Text = text;
        }

        public TextButton(Texture2D releasedImage, Vector2 location, Color tint, SpriteFont font, Color fontColor, string text, params Rectangle[] rectangles) :
            base(releasedImage, location, tint, rectangles)
        {
            fadingFont = new FadingFont(font, location - _origin+ new Vector2(rectangles[0].Width / 2, rectangles[0].Height / 2)*Scale, 0.1f, 1.0f, 0.1f, 1.0f, text, fontColor, true);
            fadingFont.EnableShadow = false; 
            Text = text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(SourceRectangle.HasValue)
            {
                fadingFont.Position = new Vector2(Position.X, Top + 60) - fadingFont.Size/2 ;// - 120);
            }
            fadingFont.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            fadingFont.Draw(spriteBatch);
        }
    }
}
