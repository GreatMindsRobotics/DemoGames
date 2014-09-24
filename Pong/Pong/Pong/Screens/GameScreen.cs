using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong.Screens
{
    public class GameScreen : BaseScreen
    {
        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            Paddle leftPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), Vector2.Zero, Color.White);

            _sprites.Add(leftPaddle);
        }
    }
}
