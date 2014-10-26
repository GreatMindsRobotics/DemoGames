using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.CoreTypes;

namespace Pong.Screens
{
    class MainMenuScreen : BaseScreen
    {
        ScreenState screenState;

        Button playBtn;
        Button optionsBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            playBtn = new Button(Content.Load<Texture2D>("temp play button"), new Vector2(0,0), Color.White);
            playBtn.SetCenterAsOrigin();
            playBtn.Position = new Vector2(_viewPort.Width / 2, 150);

            optionsBtn = new Button(Content.Load<Texture2D>("temp options button"), new Vector2(0, 0), Color.White);
            optionsBtn.SetCenterAsOrigin();
            optionsBtn.Position = new Vector2(_viewPort.Width / 2, 300);

            _sprites.Add(playBtn);
            _sprites.Add(optionsBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if(playBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.PlayerSelect);
            }
            else if (optionsBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Options);
            }

            base.Update(gameTime);
        }
    }
}
