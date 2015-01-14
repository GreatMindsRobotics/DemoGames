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
    class OptionsScreen : BaseScreen
    {
        KeyboardState keyboard;
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button editContBtn;

        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Options"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Options", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;


            editContBtn = new Button(Content.Load<Texture2D>("Buttons//Keys"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 520, 169), new Rectangle(0, 0, 520, 149));
            editContBtn.Origin = new Vector2(editContBtn.Texture.Width / 2, 169);
            editContBtn.Position = new Vector2(960, 469 + editContBtn.SourceRectangle.Value.Height / 2);

            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);


            _sprites.Add(background);
            //_sprites.Add(titleDropInFont);
            _sprites.Add(editContBtn);
            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Back();
            }

            if (editContBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.EditControls);
            }
            else if (backBtn.IsClicked)
            {
                ScreenManager.Back();
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
