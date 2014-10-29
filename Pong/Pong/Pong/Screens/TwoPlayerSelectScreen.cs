﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.CoreTypes;
using Pong.Screens;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;

namespace Pong.Screens
{
    class TwoPlayerSelectScreen : BaseScreen
    {
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button onlineBtn;
        Button localBtn;
        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(400, 50), new Vector2(400, 50), dropSpeed, "Multiplayer", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            onlineBtn = new Button(Content.Load<Texture2D>("temp online button"), new Vector2(0, 0), Color.White);
            onlineBtn.SetCenterAsOrigin();
            onlineBtn.Position = new Vector2(_viewPort.Width / 2, 150);

            localBtn = new Button(Content.Load<Texture2D>("temp local button"), new Vector2(0, 0), Color.White);
            localBtn.SetCenterAsOrigin();
            localBtn.Position = new Vector2(_viewPort.Width / 2, 300);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(titleDropInFont);
            _sprites.Add(onlineBtn);
            _sprites.Add(localBtn);
            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if (onlineBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Game);
            }
            else if (localBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Game);
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
