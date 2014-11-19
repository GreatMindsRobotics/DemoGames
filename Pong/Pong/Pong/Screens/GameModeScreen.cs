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
    public class GameModeScreen : BaseScreen
    {
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button classicalBtn;
        Button pingPongBtn;
        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Game Mode", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            classicalBtn = new Button(Content.Load<Texture2D>("temp classical button"), new Vector2(0, 0), Color.White);
            classicalBtn.SetCenterAsOrigin();
            classicalBtn.Position = new Vector2(_viewPort.Width / 2, titleDropInFont.Position.Y + classicalBtn.Origin.Y * 2.5f);

            pingPongBtn = new Button(Content.Load<Texture2D>("temp ping pong button"), new Vector2(0, 0), Color.White);
            pingPongBtn.SetCenterAsOrigin();
            pingPongBtn.Position = new Vector2(_viewPort.Width / 2, classicalBtn.Position.Y + pingPongBtn.Origin.Y * 3);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(titleDropInFont);
            _sprites.Add(classicalBtn);
            _sprites.Add(pingPongBtn);
            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if (classicalBtn.IsClicked)
            {
                Global.GameMode = GameMode.Classical;

                switch (Global.Mode)
                {
                    case Mode.SinglePlayer:
                        ScreenManager.Change(ScreenState.OnePlayerSelect);
                        break;

                    case Mode.MultiPlayer:
                        ScreenManager.Change(ScreenState.Game);
                        break;

                    default:
                        break;
                }
            }
            else if (pingPongBtn.IsClicked)
            {
                Global.GameMode = GameMode.PingPong;

                switch (Global.Mode)
                {
                    case Mode.SinglePlayer:
                        ScreenManager.Change(ScreenState.OnePlayerSelect);
                        break;

                    case Mode.MultiPlayer:
                        ScreenManager.Change(ScreenState.Game);
                        break;

                    default:
                        break;
                }
            }
            else if(backBtn.IsClicked)
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
