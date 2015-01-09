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
using FontEffectsLib.SpriteTypes;

namespace Pong.Screens
{
    class MainMenuScreen : BaseScreen
    {
        ScreenState screenState;

        //DropInFont titleDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        Button singlaPlayerBtn;
        Button multiPlayerBtn;
        Button optionsBtn;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\MainMenu"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;


            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Main Menu", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            singlaPlayerBtn = new Button(Content.Load<Texture2D>("Buttons//1Player"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            singlaPlayerBtn.Origin = new Vector2(singlaPlayerBtn.Texture.Width / 2, 169);
            singlaPlayerBtn.Position = new Vector2(960, 469 + singlaPlayerBtn.SourceRectangle.Value.Height / 2);


            multiPlayerBtn = new Button(Content.Load<Texture2D>("Buttons//2Players"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            multiPlayerBtn.Origin = new Vector2(multiPlayerBtn.Texture.Width / 2, 169);
            multiPlayerBtn.Position = new Vector2(960, 735 + multiPlayerBtn.SourceRectangle.Value.Height / 2);


            optionsBtn = new Button(Content.Load<Texture2D>("Buttons//Settings"), new Vector2(0, 0), Color.White, new Rectangle(0, 149,159, 169), new Rectangle(0, 0, 159, 149));
            optionsBtn.Origin = new Vector2(optionsBtn.Texture.Width / 2, 169);
            optionsBtn.Position = new Vector2(1743, 907 + optionsBtn.SourceRectangle.Value.Height / 2);

            _sprites.Add(background);


            //_sprites.Add(titleDropInFont);
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
            //titleDropInFont.Reset();
            base.Reset();
        }
    }
}
