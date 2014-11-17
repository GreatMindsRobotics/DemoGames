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
    class PauseScreen : BaseScreen
    {
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button resumeBtn;
        Button mainMenuButton;
        Button optionsButton;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Pause", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            resumeBtn = new Button(Content.Load<Texture2D>("temp resume button"), new Vector2(0, 0), Color.White);
            resumeBtn.SetCenterAsOrigin();
            resumeBtn.Position = new Vector2(_viewPort.Width / 2, titleDropInFont.Position.Y + resumeBtn.Origin.Y * 2.5f);

            mainMenuButton = new Button(Content.Load<Texture2D>("temp main menu button"), new Vector2(0, 0), Color.White);
            mainMenuButton.SetCenterAsOrigin();
            mainMenuButton.Position = new Vector2(_viewPort.Width / 2, resumeBtn.Position.Y + mainMenuButton.Origin.Y * 3);

            optionsButton = new Button(Content.Load<Texture2D>("temp options button"), new Vector2(0, 0), Color.White);
            optionsButton.SetCenterAsOrigin();
            optionsButton.Position = new Vector2(_viewPort.Width / 2, mainMenuButton.Position.Y + optionsButton.Origin.Y * 3);

            _sprites.Add(resumeBtn);
            _sprites.Add(titleDropInFont);
            _sprites.Add(mainMenuButton);
            _sprites.Add(optionsButton);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (resumeBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Game);
            }
            else if(optionsButton.IsClicked)
            {
                ScreenManager.Change(ScreenState.Options);
            }
            else if(mainMenuButton.IsClicked)
            {
                ScreenManager.Change(ScreenState.MainMenu);
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
