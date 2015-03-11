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
using System.Xml.Linq;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace Pong.Screens
{
    class KeyboardScreen : BaseScreen
    {

        void loadkey(ContentManager content, string keyname, int x, int y)
        {
            TextButton name = new TextButton(content.Load<Texture2D>("Buttons//Key"), new Vector2(0, 0), Color.White, content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, keyname, new Rectangle(0, 117, 131, 137), new Rectangle(0, 0, 131, 117));
            name.Origin = new Vector2(name.Texture.Width / 2, 169);
            name.Position = new Vector2(x, y + name.SourceRectangle.Value.Height / 2);

            _keyboard.Add(keyname, name);
            _sprites.Add(name);
        }

        int col1 = 280;
        int colDiff = 150;
        int row1 = 420;
        int rowDiff = 140;

        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        
        GamePadMapper gamePad;

        Dictionary<string, Button> _keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            
            _keyboard = new Dictionary<string, Button>();

            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\EnterCode"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;
            _sprites.Add(background);

            loadkey(Content, "1", col1, row1);
            loadkey(Content, "2", col1 + colDiff, row1);
            loadkey(Content, "3", col1 + colDiff*2, row1);
            loadkey(Content, "4", col1 + colDiff*3, row1);
            loadkey(Content, "5", col1 + colDiff*4, row1);
            loadkey(Content, "6", col1 + colDiff*5, row1);
            loadkey(Content, "7", col1 + colDiff*6, row1);
            loadkey(Content, "8", col1 + colDiff*7, row1);
            loadkey(Content, "9", col1 + colDiff*8, row1);
            loadkey(Content, "0", col1 + colDiff*9, row1);

            loadkey(Content, "Q", col1, row1 + rowDiff);
            loadkey(Content, "W", col1 + colDiff, row1 + rowDiff);
            loadkey(Content, "E", col1 + colDiff * 2, row1 + rowDiff);
            loadkey(Content, "R", col1 + colDiff * 3, row1 + rowDiff);
            loadkey(Content, "T", col1 + colDiff * 4, row1 + rowDiff);
            loadkey(Content, "Y", col1 + colDiff * 5, row1 + rowDiff);
            loadkey(Content, "U", col1 + colDiff * 6, row1 + rowDiff);
            loadkey(Content, "I", col1 + colDiff * 7, row1 + rowDiff);
            loadkey(Content, "O", col1 + colDiff * 8, row1 + rowDiff);
            loadkey(Content, "P", col1 + colDiff * 9, row1 + rowDiff);

            loadkey(Content, "A", col1, row1 + rowDiff * 2);
            loadkey(Content, "S", col1 + colDiff, row1 + rowDiff * 2);
            loadkey(Content, "D", col1 + colDiff * 2, row1 + rowDiff * 2);
            loadkey(Content, "F", col1 + colDiff * 3, row1 + rowDiff * 2);
            loadkey(Content, "G", col1 + colDiff * 4, row1 + rowDiff * 2);
            loadkey(Content, "H", col1 + colDiff * 5, row1 + rowDiff * 2);
            loadkey(Content, "J", col1 + colDiff * 6, row1 + rowDiff * 2);
            loadkey(Content, "K", col1 + colDiff * 7, row1 + rowDiff * 2);
            loadkey(Content, "L", col1 + colDiff * 8, row1 + rowDiff * 2);
            loadkey(Content, ".", col1 + colDiff * 9, row1 + rowDiff * 2);



            loadkey(Content, "Z", col1, row1 + rowDiff * 3);
            loadkey(Content, "X", col1 + colDiff, row1 + rowDiff * 3);
            loadkey(Content, "C", col1 + colDiff * 2, row1 + rowDiff * 3);
            loadkey(Content, "V", col1 + colDiff * 3, row1 + rowDiff * 3);
            loadkey(Content, "B", col1 + colDiff * 4, row1 + rowDiff * 3);
            loadkey(Content, "N", col1 + colDiff * 5, row1 + rowDiff * 3);
            loadkey(Content, "M", col1 + colDiff * 6, row1 + rowDiff * 3);
            loadkey(Content, "-", col1 + colDiff * 7, row1 + rowDiff * 3);
            loadkey(Content, "_", col1 + colDiff * 8, row1 + rowDiff * 3);
            loadkey(Content, "!", col1 + colDiff * 9, row1 + rowDiff * 3);

                

         

            if (!Global.UsingKeyboard)
            {
                //classicalBtn.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {

            //if (Global.UsingKeyboard)
            //{
            //    if (InputManager.JustPressed(Keys.Escape))
            //    {
            //        ScreenManager.Back();
            //    }
            //}
            //else
            //{
            //    if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
            //    {
            //        ScreenManager.Back();
            //    }

            //    if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
            //    {
            //        if (classicalBtn.IsPressed)
            //        {
            //            classicalBtn.IsPressed = false;
            //            pingPongBtn.IsPressed = true;
            //        }
            //    }
            //    else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
            //    {
            //        if (pingPongBtn.IsPressed)
            //        {
            //            pingPongBtn.IsPressed = false;
            //            classicalBtn.IsPressed = true;
            //        }
            //    }
            //    else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
            //    {
            //        if (classicalBtn.IsPressed)
            //        {
            //            classicalBtn.IsPressed = false;
            //            backBtn.IsPressed = true;
            //        }
            //        else if (pingPongBtn.IsPressed)
            //        {
            //            pingPongBtn.IsPressed = false;
            //            backBtn.IsPressed = true;
            //        }
            //    }
            //    else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
            //    {
            //        if (backBtn.IsPressed)
            //        {
            //            backBtn.IsPressed = false;
            //            classicalBtn.IsPressed = true;
            //        }
            //    }
            //    else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
            //    {
            //        if (classicalBtn.IsPressed)
            //        {
            //            Global.GameMode = GameMode.Classical;
            //            if (Global.Mode == Mode.SinglePlayer)
            //            {
            //                ScreenManager.Change(ScreenState.OnePlayerSelect);
            //            }
            //            else
            //            {
            //                ScreenManager.Change(ScreenState.Game);
            //            }
            //        }
            //        else if (pingPongBtn.IsPressed)
            //        {
            //            Global.GameMode = GameMode.PingPong;
            //            ScreenManager.Change(ScreenState.TwoPlayerSelect);
            //            if (Global.Mode == Mode.SinglePlayer)
            //            {
            //                ScreenManager.Change(ScreenState.OnePlayerSelect);
            //            }
            //            else
            //            {
            //                ScreenManager.Change(ScreenState.Game);
            //            }
            //        }
            //        else if (backBtn.IsPressed)
            //        {
            //            ScreenManager.Back();
            //        }
            //    }
            //}

            //if (classicalBtn.IsClicked)
            //{
            //    Global.GameMode = GameMode.Classical;

            //    switch (Global.Mode)
            //    {
            //        case Mode.SinglePlayer:
            //            ScreenManager.Change(ScreenState.OnePlayerSelect);
            //            break;

            //        case Mode.MultiPlayer:
            //            ScreenManager.Change(ScreenState.Game);
            //            break;

            //        default:
            //            break;
            //    }
            //}
            //else if (pingPongBtn.IsClicked)
            //{
            //    Global.GameMode = GameMode.PingPong;

            //    switch (Global.Mode)
            //    {
            //        case Mode.SinglePlayer:
            //            ScreenManager.Change(ScreenState.OnePlayerSelect);
            //            break;

            //        case Mode.MultiPlayer:
            //            ScreenManager.Change(ScreenState.Game);
            //            break;

            //        default:
            //            break;
            //    }
            //}
            //else if (backBtn.IsClicked)
            //{
            //    ScreenManager.Back();
            //}

            Global.Reset = true;

            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //_keyboard["1"].Draw(spriteBatch);
        }
    }
}
