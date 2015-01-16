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
    class ErrorScreen : BaseScreen
    {

        //DropInFont errorDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        //FadingFont errorInfoFadingFont;
        //String errorInfo = "This feature is not yet avaible.\nTry again when new updates have been added.";

        Button backButton;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Error"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;


            //errorDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(400, 50), new Vector2(400, 50), dropSpeed, "Error", Color.CornflowerBlue);
            //errorDropInFont.IsVisible = true;
            //errorDropInFont.SetCenterAsOrigin();
            //errorDropInFont.EnableShadow = false;
            //errorDropInFont.TintColor = Color.Black;
            //errorDropInFont.ShadowPosition = new Vector2(errorDropInFont.Position.X - 4, errorDropInFont.Position.Y + 4);
            //errorDropInFont.ShadowColor = Color.Gray;

            //errorInfoFadingFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 - 50), 0.1f, 1.0f, 0.01f, 1.0f, errorInfo, Color.White, false);
            //errorInfoFadingFont.SetCenterAsOrigin();
            //errorInfoFadingFont.EnableShadow = false;

            backButton = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backButton.Origin = new Vector2(backButton.Texture.Width / 2, 169);
            backButton.Position = new Vector2(177, 907 + backButton.SourceRectangle.Value.Height / 2);

            //_sprites.Add(errorDropInFont);
            //_sprites.Add(errorInfoFadingFont);
            _sprites.Add(background);
            _sprites.Add(backButton);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.JustPressed(Keys.Escape))
            {
                ScreenManager.Back();
            }

            if(backButton.IsClicked)
            {
                ScreenManager.Change(ScreenState.MainMenu);
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            //errorDropInFont.Reset();
            //errorInfoFadingFont.Reset();
            base.Reset();
        }
    }
}
