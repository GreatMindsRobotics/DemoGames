using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.CoreTypes;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;

namespace Pong.Screens
{
    class MainMenuScreen : BaseScreen
    {
        ScreenState screenState;

        DropInFont titleDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        Button playBtn;
        Button optionsBtn;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(400, -1000), new Vector2(400, 50), dropSpeed, "Main Menu", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            playBtn = new Button(Content.Load<Texture2D>("temp play button"), new Vector2(0,0), Color.White);
            playBtn.SetCenterAsOrigin();
            playBtn.Position = new Vector2(_viewPort.Width / 2, 150);

            optionsBtn = new Button(Content.Load<Texture2D>("temp options button"), new Vector2(0, 0), Color.White);
            optionsBtn.SetCenterAsOrigin();
            optionsBtn.Position = new Vector2(_viewPort.Width / 2, 300);

            _sprites.Add(titleDropInFont);
            _sprites.Add(playBtn);
            _sprites.Add(optionsBtn);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Change(ScreenState.Title);
            }

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

        public override void Reset()
        {
            titleDropInFont.Reset();

            base.Reset();
        }
    }
}
