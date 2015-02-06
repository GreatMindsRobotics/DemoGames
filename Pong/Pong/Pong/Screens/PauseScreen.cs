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
    class PauseScreen : BaseScreen
    {
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button resumeBtn;
        Button mainMenuButton;
        Button optionsButton;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Pause"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Pause", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            resumeBtn = new Button(Content.Load<Texture2D>("Buttons//Resume"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            resumeBtn.Origin = new Vector2(resumeBtn.Texture.Width / 2, 169);
            resumeBtn.Position = new Vector2(960, 469 + resumeBtn.SourceRectangle.Value.Height / 2);

            mainMenuButton = new Button(Content.Load<Texture2D>("Buttons//Menu"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            mainMenuButton.Origin = new Vector2(mainMenuButton.Texture.Width / 2, 169);
            mainMenuButton.Position = new Vector2(960, 735 + mainMenuButton.SourceRectangle.Value.Height / 2);

            optionsButton = new Button(Content.Load<Texture2D>("Buttons//Settings"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            optionsButton.Origin = new Vector2(optionsButton.Texture.Width / 2, 169);
            optionsButton.Position = new Vector2(1743, 907 + optionsButton.SourceRectangle.Value.Height / 2);

            _sprites.Add(background);
            _sprites.Add(resumeBtn);
            //_sprites.Add(titleDropInFont);
            _sprites.Add(mainMenuButton);
            _sprites.Add(optionsButton);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (Global.UsingKeyboard)
            {
                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Back();
                }

                if (resumeBtn.IsClicked)
                {
                    ScreenManager.Change(ScreenState.Game);
                }
                else if (optionsButton.IsClicked)
                {
                    ScreenManager.Change(ScreenState.Options);
                }
                else if (mainMenuButton.IsClicked)
                {
                    ScreenManager.Change(ScreenState.MainMenu);
                }
            }
            else 
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    Global.Close = true;
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
                {
                    if (resumeBtn.IsPressed)
                    {
                        resumeBtn.IsPressed = false;
                        mainMenuButton.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
                {
                    if (mainMenuButton.IsPressed)
                    {
                        mainMenuButton.IsPressed = false;
                        resumeBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (resumeBtn.IsPressed)
                    {
                        resumeBtn.IsPressed = false;
                        optionsButton.IsPressed = true;
                    }
                    else if (mainMenuButton.IsPressed)
                    {
                        mainMenuButton.IsPressed = false;
                        optionsButton.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (optionsButton.IsPressed)
                    {
                        optionsButton.IsPressed = false;
                        resumeBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (resumeBtn.IsPressed)
                    {
                        Global.Mode = Mode.SinglePlayer;
                        ScreenManager.Change(ScreenState.GameMode);
                    }
                    else if (mainMenuButton.IsPressed)
                    {
                        Global.Mode = Mode.MultiPlayer;
                        ScreenManager.Change(ScreenState.TwoPlayerSelect);
                    }
                    else if (optionsButton.IsPressed)
                    {
                        ScreenManager.Change(ScreenState.Options);
                    }
                }
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
