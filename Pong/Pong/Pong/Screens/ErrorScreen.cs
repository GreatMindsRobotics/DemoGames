using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.CoreTypes;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;
using Microsoft.Xna.Framework.Input;

namespace Pong.Screens
{
    class ErrorScreen : BaseScreen
    {
        DropInFont errorDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        FadingFont errorInfoFadingFont;
        String errorInfo = "This feature is not yet avaible.\nTry again when new updates have been added.";

        Button backButton;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            errorDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(400, 50), new Vector2(400, 50), dropSpeed, "Error", Color.CornflowerBlue);
            errorDropInFont.IsVisible = true;
            errorDropInFont.SetCenterAsOrigin();
            errorDropInFont.EnableShadow = false;
            errorDropInFont.TintColor = Color.Black;
            errorDropInFont.ShadowPosition = new Vector2(errorDropInFont.Position.X - 4, errorDropInFont.Position.Y + 4);
            errorDropInFont.ShadowColor = Color.Gray;

            errorInfoFadingFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 - 50), 0.1f, 1.0f, 0.01f, 1.0f, errorInfo, Color.White, false);
            errorInfoFadingFont.SetCenterAsOrigin();
            errorInfoFadingFont.EnableShadow = false;

            backButton = new Button(Content.Load<Texture2D>("temp resume button"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 + 50), Color.White);
            backButton.SetCenterAsOrigin();

            _sprites.Add(errorDropInFont);
            _sprites.Add(errorInfoFadingFont);
            _sprites.Add(backButton);
        }

        public override void Update(GameTime gameTime)
        {
            if(backButton.IsClicked)
            {
                ScreenManager.Back();
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            errorDropInFont.Reset();
            errorInfoFadingFont.Reset();
            base.Reset();
        }
    }
}
