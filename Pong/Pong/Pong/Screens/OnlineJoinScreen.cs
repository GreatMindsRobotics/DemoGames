using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Screens;
using FontEffectsLib.SpriteTypes;
using FontEffectsLib.FontTypes;

namespace Pong.CoreTypes
{
    class OnlineJoinScreen : BaseScreen
    {
        Button backBtn;
        Button connectBtn;

        FadingFont codeLable;

        GamePadMapper gamePad;

        string code;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\EnterCode"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //connectBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Join", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            //connectBtn.Origin = new Vector2(connectBtn.Texture.Width / 2, 169);
            //connectBtn.Position = new Vector2(960, 735 + connectBtn.SourceRectangle.Value.Height / 2);


            codeLable = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, code, Color.LightGoldenrodYellow, false);
            codeLable.EnableShadow = false;
            codeLable.SetCenterAsOrigin();


            connectBtn = new Button(Content.Load<Texture2D>("Buttons//Connect"), new Vector2(0, 0), Color.White, new Rectangle(0, 77, 356, 87), new Rectangle(0, 0, 356, 77));
            connectBtn.Origin = new Vector2(connectBtn.Texture.Width / 2, 169);
            connectBtn.Position = new Vector2(960, 735 + connectBtn.SourceRectangle.Value.Height / 2);

            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);

            _sprites.Add(background);
            _sprites.Add(codeLable);
            _sprites.Add(backBtn);
            _sprites.Add(connectBtn);

            if (!Global.UsingKeyboard)
            {
                connectBtn.IsPressed = true;
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

                if (connectBtn.IsClicked)
                {
                    //ScreenManager.Change(ScreenState.Game);
                }
                else if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                }
            }
            else
            {
                //IS NOT FOCUSED FOR SOME REASON


                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }


                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (connectBtn.IsPressed)
                    {
                        connectBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;
                        connectBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (connectBtn.IsPressed)
                    {
                        //ScreenManager.Change(ScreenState.Game);
                    }
                    else if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
                    }
                }


                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.X))
                {
                    code += "X";
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Y))
                {
                    code += "Y";
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.B))
                {
                    code += "B";
                }

                codeLable.Text.Clear();

                codeLable.Text.Append(code);
                codeLable.SetCenterAsOrigin();

            }
            base.Update(gameTime);
        }
    }
}
