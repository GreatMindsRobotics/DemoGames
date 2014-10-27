using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FontEffectsLib.FontTypes;

namespace Pong.Screens
{
    class GameOverScreen : BaseScreen
    {
        FadingFont player1WinsFont;
        FadingFont player2WinsFont;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            player1WinsFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player 1 Wins"), Color.White, false);
            player1WinsFont.EnableShadow = false;
            player1WinsFont.SetCenterAsOrigin();

            player2WinsFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player 2 Wins"), Color.White, false);
            player2WinsFont.EnableShadow = false;
            player2WinsFont.SetCenterAsOrigin();

            player1WinsFont.IsVisible = false;
            player2WinsFont.IsVisible = false;

            _sprites.Add(player1WinsFont);
            _sprites.Add(player2WinsFont);
        }

        public override void Update(GameTime gameTime)
        {

            if (GameScreen.player1Won)
            {

                player1WinsFont.IsVisible = true;

            }
            else
            {

                player2WinsFont.IsVisible = true;
            
            }

            base.Update(gameTime);
        }

    }
}
