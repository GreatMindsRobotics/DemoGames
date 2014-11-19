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

        Button singlaPlayerBtn;
        Button multiPlayerBtn;
        Button optionsBtn;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Main Menu", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            singlaPlayerBtn = new Button(Content.Load<Texture2D>("temp 1 player button"), new Vector2(0, 0), Color.White);
            singlaPlayerBtn.SetCenterAsOrigin();
            singlaPlayerBtn.Position = new Vector2(_viewPort.Width / 2, titleDropInFont.Position.Y + singlaPlayerBtn.Origin.Y * 2.5f);

            multiPlayerBtn = new Button(Content.Load<Texture2D>("temp 2 players button"), new Vector2(0, 0), Color.White);
            multiPlayerBtn.SetCenterAsOrigin();
            multiPlayerBtn.Position = new Vector2(_viewPort.Width / 2, singlaPlayerBtn.Position.Y + multiPlayerBtn.Origin.Y * 3);

            optionsBtn = new Button(Content.Load<Texture2D>("temp options button"), new Vector2(0, 0), Color.White);
            optionsBtn.SetCenterAsOrigin();
            optionsBtn.Position = new Vector2(_viewPort.Width / 2, multiPlayerBtn.Position.Y + optionsBtn.Origin.Y * 3);


            _sprites.Add(titleDropInFont);
            _sprites.Add(singlaPlayerBtn);
            _sprites.Add(multiPlayerBtn);
            _sprites.Add(optionsBtn);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Back();
            }
            else if (singlaPlayerBtn.IsClicked)
            {
                Global.Mode = Mode.SinglePlayer;
                ScreenManager.Change(ScreenState.GameMode);
            }
            else if (multiPlayerBtn.IsClicked)
            {
                Global.Mode = Mode.MultiPlayer;
                ScreenManager.Change(ScreenState.TwoPlayerSelect);
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
