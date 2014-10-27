using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Screens;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.CoreTypes;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;

namespace Pong.Screens
{
    class PlayerSelectScreen : BaseScreen
    {
        DropInFont titleDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        Button onePlayerBtn;
        Button twoPlayersBtn;

        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(400, -1000), new Vector2(400, 50), dropSpeed, "Game Mode", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            onePlayerBtn = new Button(Content.Load<Texture2D>("temp 1 player button"), new Vector2(0, 0), Color.White);
            onePlayerBtn.SetCenterAsOrigin();
            onePlayerBtn.Position = new Vector2(_viewPort.Width / 2, 150);

            twoPlayersBtn = new Button(Content.Load<Texture2D>("temp 2 players button"), new Vector2(0, 0), Color.White);
            twoPlayersBtn.SetCenterAsOrigin();
            twoPlayersBtn.Position = new Vector2(_viewPort.Width / 2, 300);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(titleDropInFont);
            _sprites.Add(onePlayerBtn);
            _sprites.Add(twoPlayersBtn);
            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if(onePlayerBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.OnePlayerSelect);
            }
            if (twoPlayersBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.TwoPlayerSelect);
            }
            else if (backBtn.IsClicked)
            {
                ScreenManager.Back();
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
