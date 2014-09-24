using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong.Sprites
{
    public class Paddle : GameSprite
    {

        public Paddle(Texture2D image, Vector2 location, Color tint):
            base(image, location, tint)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //todo: add moving functions
            base.Update(gameTime);
        }

    }
}
