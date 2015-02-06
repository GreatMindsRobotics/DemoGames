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
    class OnePlayerSelectScreen : BaseScreen
    {

        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button easyBtn;
        Button mediumBtn;
        Button hardBtn;
        Button backBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\MainMenu"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Difficulty", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            easyBtn = new Button(Content.Load<Texture2D>("Buttons//Easy"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 520, 169), new Rectangle(0, 0, 520, 149));
            easyBtn.Origin = new Vector2(easyBtn.Texture.Width / 2, 169);
            easyBtn.Position = new Vector2(960, 469 + easyBtn.SourceRectangle.Value.Height / 2);

            mediumBtn = new Button(Content.Load<Texture2D>("Buttons//Medium"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 520, 169), new Rectangle(0, 0, 520, 149));
            mediumBtn.Origin = new Vector2(mediumBtn.Texture.Width / 2, 169);
            mediumBtn.Position = new Vector2(960, 684 + mediumBtn.SourceRectangle.Value.Height / 2);

            hardBtn = new Button(Content.Load<Texture2D>("Buttons//Hard"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 520, 169), new Rectangle(0, 0, 520, 149));
            hardBtn.Origin = new Vector2(hardBtn.Texture.Width / 2, 169);
            hardBtn.Position = new Vector2(960, 899 + hardBtn.SourceRectangle.Value.Height / 2);


            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);
            _sprites.Add(background);
            //_sprites.Add(titleDropInFont);
            _sprites.Add(easyBtn);
            _sprites.Add(mediumBtn);
            _sprites.Add(hardBtn);
            _sprites.Add(backBtn);

            if (!Global.UsingKeyboard)
            {
                easyBtn.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {

            if (Global.UsingKeyboard)
            {
                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Back();
                }

                if (easyBtn.IsClicked)
                {
                    Global.Difficulty = Difficulty.Easy;
                    ScreenManager.Change(ScreenState.Game);
                }
                else if (mediumBtn.IsClicked)
                {
                    Global.Difficulty = Difficulty.Medium;
                    ScreenManager.Change(ScreenState.Game);
                }
                else if (hardBtn.IsClicked)
                {
                    Global.Difficulty = Difficulty.Hard;
                    ScreenManager.Change(ScreenState.Game);
                }
                else if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                }
            }
            else
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
                {
                    if (easyBtn.IsPressed)
                    {
                        easyBtn.IsPressed = false;
                        mediumBtn.IsPressed = true;
                    }
                    else if (mediumBtn.IsPressed)
                    {
                        mediumBtn.IsPressed = false;
                        hardBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
                {
                    if (mediumBtn.IsPressed)
                    {
                        mediumBtn.IsPressed = false;
                        easyBtn.IsPressed = true;
                    }
                    else if (hardBtn.IsPressed)
                    {
                        hardBtn.IsPressed = false;
                        mediumBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (easyBtn.IsPressed)
                    {
                        easyBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    else if (mediumBtn.IsPressed)
                    {
                        mediumBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    else if (hardBtn.IsPressed)
                    {
                        hardBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;
                        easyBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (easyBtn.IsPressed)
                    {
                        Global.Difficulty = Difficulty.Easy;
                        ScreenManager.Change(ScreenState.Game);
                    }
                    else if (mediumBtn.IsPressed)
                    {
                        Global.Difficulty = Difficulty.Medium;
                        ScreenManager.Change(ScreenState.Game);
                    }
                    else if (hardBtn.IsPressed)
                    {
                        Global.Difficulty = Difficulty.Hard;
                        ScreenManager.Change(ScreenState.Game);
                    }
                    else if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
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
