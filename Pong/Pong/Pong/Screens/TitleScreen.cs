using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.CoreTypes;

namespace Pong.Screens
{
    public class TitleScreen : BaseScreen
    {
        GameSprite titleLogoSprite;

        ScreenState screenState;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleLogoSprite = new GameSprite(Content.Load<Texture2D>("titleLogo"), new Vector2(_viewPort.Width / 2 - 25, _viewPort.Height / 2), Color.White);

            _sprites.Add(titleLogoSprite);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Enter))
            {
                ScreenManager.Change(ScreenState.MainMenu);
            }

            base.Update(gameTime);
        }
    }
}
