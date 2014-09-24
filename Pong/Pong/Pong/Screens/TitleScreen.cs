using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong.Screens
{
    public class TitleScreen : BaseScreen
    {


        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            GameSprite titleLogoSprite = new GameSprite(Content.Load<Texture2D>("titleLogo"), Vector2.Zero, Color.White);


            _sprites.Add(titleLogoSprite);
        }
    }
}
