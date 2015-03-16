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

        TextButton[,] buttons = new TextButton[4, 11];

        //string displayedKeys;

        TextButton loadkey(ContentManager content, string keyname, int x, int y, float scale)
        {
            TextButton name = new TextButton(content.Load<Texture2D>("Buttons//Key"), new Vector2(0, 0), Color.White, content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, keyname, new Rectangle(0, 117, 131, 137), new Rectangle(0, 0, 131, 117));
            name.Scale *= scale;
            name.Origin = new Vector2(name.Texture.Width / 2, name.SourceRectangle.Value.Height);
            name.Position = new Vector2(x, y + name.SourceRectangle.Value.Height / 2);

            name.Clicked += new EventHandler(name_Clicked);



            _keyboard.Add(keyname, name);
            _sprites.Add(name);
            return name;
        }

        void name_Clicked(object sender, EventArgs e)
        {
            TextButton clickedButton = (TextButton)sender;

            Global.onlineCode += clickedButton.Text;
        }

        int col1 = 360;
        int colDiff = 120;
        int row1 = 470;
        int rowDiff = 120;
        float scale = 0.85f;

        int currentCol = 0;
        int currentRow = 0;
        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        FadingFont codeFont;

        TextButton back;
        TextButton space;
        TextButton done;
        Button backBtn;

        GamePadMapper gamePad;

        Dictionary<string, Button> _keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            _keyboard = new Dictionary<string, Button>();

            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\EnterCode"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;
            _sprites.Add(background);

            codeFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, string.Format(""), Color.White, false);
            codeFont.EnableShadow = false;
            codeFont.Position = new Vector2(col1 + colDiff * 5, row1 - 100);
            codeFont.SetCenterAsOrigin();


            buttons[0, 0] = loadkey(Content, "1", col1, row1, scale);
            buttons[0, 1] = loadkey(Content, "2", col1 + colDiff, row1, scale);
            buttons[0, 2] = loadkey(Content, "3", col1 + colDiff * 2, row1, scale);
            buttons[0, 3] = loadkey(Content, "4", col1 + colDiff * 3, row1, scale);
            buttons[0, 4] = loadkey(Content, "5", col1 + colDiff * 4, row1, scale);
            buttons[0, 5] = loadkey(Content, "6", col1 + colDiff * 5, row1, scale);
            buttons[0, 6] = loadkey(Content, "7", col1 + colDiff * 6, row1, scale);
            buttons[0, 7] = loadkey(Content, "8", col1 + colDiff * 7, row1, scale);
            buttons[0, 8] = loadkey(Content, "9", col1 + colDiff * 8, row1, scale);
            buttons[0, 9] = loadkey(Content, "0", col1 + colDiff * 9, row1, scale);
            buttons[0, 10] = loadkey(Content, "#", col1 + colDiff * 10, row1, scale);

            buttons[1, 0] = loadkey(Content, "Q", col1, row1 + rowDiff, scale);
            buttons[1, 1] = loadkey(Content, "W", col1 + colDiff, row1 + rowDiff, scale);
            buttons[1, 2] = loadkey(Content, "E", col1 + colDiff * 2, row1 + rowDiff, scale);
            buttons[1, 3] = loadkey(Content, "R", col1 + colDiff * 3, row1 + rowDiff, scale);
            buttons[1, 4] = loadkey(Content, "T", col1 + colDiff * 4, row1 + rowDiff, scale);
            buttons[1, 5] = loadkey(Content, "Y", col1 + colDiff * 5, row1 + rowDiff, scale);
            buttons[1, 6] = loadkey(Content, "U", col1 + colDiff * 6, row1 + rowDiff, scale);
            buttons[1, 7] = loadkey(Content, "I", col1 + colDiff * 7, row1 + rowDiff, scale);
            buttons[1, 8] = loadkey(Content, "O", col1 + colDiff * 8, row1 + rowDiff, scale);
            buttons[1, 9] = loadkey(Content, "P", col1 + colDiff * 9, row1 + rowDiff, scale);
            buttons[1, 10] = loadkey(Content, "*", col1 + colDiff * 10, row1 + rowDiff, scale);


            buttons[2, 0] = loadkey(Content, "A", col1, row1 + rowDiff * 2, scale);
            buttons[2, 1] = loadkey(Content, "S", col1 + colDiff, row1 + rowDiff * 2, scale);
            buttons[2, 2] = loadkey(Content, "D", col1 + colDiff * 2, row1 + rowDiff * 2, scale);
            buttons[2, 3] = loadkey(Content, "F", col1 + colDiff * 3, row1 + rowDiff * 2, scale);
            buttons[2, 4] = loadkey(Content, "G", col1 + colDiff * 4, row1 + rowDiff * 2, scale);
            buttons[2, 5] = loadkey(Content, "H", col1 + colDiff * 5, row1 + rowDiff * 2, scale);
            buttons[2, 6] = loadkey(Content, "J", col1 + colDiff * 6, row1 + rowDiff * 2, scale);
            buttons[2, 7] = loadkey(Content, "K", col1 + colDiff * 7, row1 + rowDiff * 2, scale);
            buttons[2, 8] = loadkey(Content, "L", col1 + colDiff * 8, row1 + rowDiff * 2, scale);
            buttons[2, 9] = loadkey(Content, ".", col1 + colDiff * 9, row1 + rowDiff * 2, scale);
            buttons[2, 10] = loadkey(Content, "&", col1 + colDiff * 10, row1 + rowDiff * 2, scale);

            buttons[3, 0] = loadkey(Content, "Z", col1, row1 + rowDiff * 3, scale);
            buttons[3, 1] = loadkey(Content, "X", col1 + colDiff, row1 + rowDiff * 3, scale);
            buttons[3, 2] = loadkey(Content, "C", col1 + colDiff * 2, row1 + rowDiff * 3, scale);
            buttons[3, 3] = loadkey(Content, "V", col1 + colDiff * 3, row1 + rowDiff * 3, scale);
            buttons[3, 4] = loadkey(Content, "B", col1 + colDiff * 4, row1 + rowDiff * 3, scale);
            buttons[3, 5] = loadkey(Content, "N", col1 + colDiff * 5, row1 + rowDiff * 3, scale);
            buttons[3, 6] = loadkey(Content, "M", col1 + colDiff * 6, row1 + rowDiff * 3, scale);
            buttons[3, 7] = loadkey(Content, "-", col1 + colDiff * 7, row1 + rowDiff * 3, scale);
            buttons[3, 8] = loadkey(Content, "_", col1 + colDiff * 8, row1 + rowDiff * 3, scale);
            buttons[3, 9] = loadkey(Content, "!", col1 + colDiff * 9, row1 + rowDiff * 3, scale);
            buttons[3, 10] = loadkey(Content, "?", col1 + colDiff * 10, row1 + rowDiff * 3, scale);


            back = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Back", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            back.Scale *= scale;
            back.Scale = new Vector2(back.Scale.X + 0.02f, back.Scale.Y);
            back.Origin = new Vector2(back.Texture.Width / 2, 137);
            back.Position = new Vector2(col1 + 120, row1 + rowDiff * 4 + back.SourceRectangle.Value.Height / 2);

            space = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, " ", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            space.Scale *= scale;
            space.Scale = new Vector2(space.Scale.X + 0.61f, space.Scale.Y);
            space.Origin = new Vector2(space.Texture.Width / 2, 137);
            space.Position = new Vector2(col1 + colDiff * 3 + 240, row1 + rowDiff * 4 + space.SourceRectangle.Value.Height / 2);

            done = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Done", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            done.Scale *= scale;
            done.Scale = new Vector2(done.Scale.X + 0.02f, done.Scale.Y);
            done.Origin = new Vector2(done.Texture.Width / 2, 137);
            done.Position = new Vector2(col1 + colDiff * 8 + 120, row1 + rowDiff * 4 + done.SourceRectangle.Value.Height / 2);

            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);

            _sprites.Add(codeFont);
            _sprites.Add(back);
            _sprites.Add(space);
            _sprites.Add(done);
            _sprites.Add(backBtn);


            if (!Global.UsingKeyboard)
            {
                //done.IsPressed = true;
                //_keyboard["2"].IsPressed = true;
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (back.IsClicked && Global.onlineCode.Length > 0)
            {
                Global.onlineCode = Global.onlineCode.Substring(0, Global.onlineCode.Length - 1);
            }
            else if (space.IsClicked)
            {
                Global.onlineCode += " ";
            }
            else if (done.IsClicked)
            {

            }


            if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
            {
                ScreenManager.Back();
            }


            if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
            {
                if (_keyboard["Z"].IsPressed)
                {
                    _keyboard["Z"].IsPressed = false;
                    back.IsPressed = true;
                }
                else if (_keyboard["X"].IsPressed)
                {
                    _keyboard["X"].IsPressed = false;
                    back.IsPressed = true;
                }
                else if (_keyboard["C"].IsPressed)
                {
                    _keyboard["C"].IsPressed = false;
                    back.IsPressed = true;
                }
                else if (_keyboard["V"].IsPressed)
                {
                    _keyboard["V"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["B"].IsPressed)
                {
                    _keyboard["B"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["N"].IsPressed)
                {
                    _keyboard["N"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["V"].IsPressed)
                {
                    _keyboard["V"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["M"].IsPressed)
                {
                    _keyboard["M"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["-"].IsPressed)
                {
                    _keyboard["-"].IsPressed = false;
                    space.IsPressed = true;
                }
                else if (_keyboard["_"].IsPressed)
                {
                    _keyboard["_"].IsPressed = false;
                    done.IsPressed = true;
                }
                else if (_keyboard["!"].IsPressed)
                {
                    _keyboard["!"].IsPressed = false;
                    done.IsPressed = true;
                }
                else if (_keyboard["?"].IsPressed)
                {
                    _keyboard["?"].IsPressed = false;
                    done.IsPressed = true;
                }
                else if (currentRow < 3 && !backBtn.IsPressed)
                {
                    buttons[currentRow, currentCol].IsPressed = false;
                    currentRow++;
                    buttons[currentRow, currentCol].IsPressed = true;
                }
            }
            else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
            {
                if (back.IsPressed)
                {
                    back.IsPressed = false;
                    _keyboard["X"].IsPressed = true;
                    currentCol = 1;
                    currentRow = 3;
                }
                else if (space.IsPressed)
                {
                    space.IsPressed = false;
                    _keyboard["N"].IsPressed = true;
                    currentCol = 5;
                    currentRow = 3;
                }
                else if (done.IsPressed)
                {
                    done.IsPressed = false;
                    _keyboard["!"].IsPressed = true;
                    currentCol = 9;
                    currentRow = 3;
                }
                else if (currentRow > 0 && !backBtn.IsPressed)
                {
                    buttons[currentRow, currentCol].IsPressed = false;
                    currentRow--;
                    buttons[currentRow, currentCol].IsPressed = true;
                }
            }
            else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
            {
                if (_keyboard["1"].IsPressed)
                {
                    _keyboard["1"].IsPressed = false;
                    backBtn.IsPressed = true;
                }
                else if (_keyboard["Q"].IsPressed)
                {
                    _keyboard["Q"].IsPressed = false;
                    backBtn.IsPressed = true;
                }
                else if (_keyboard["A"].IsPressed)
                {
                    _keyboard["A"].IsPressed = false;
                    backBtn.IsPressed = true;
                }
                else if (_keyboard["Z"].IsPressed)
                {
                    _keyboard["Z"].IsPressed = false;
                    backBtn.IsPressed = true;
                }
                else if (back.IsPressed)
                {
                    back.IsPressed = false;
                    backBtn.IsPressed = true;
                }
                else if (done.IsPressed)
                {
                    done.IsPressed = false;
                    space.IsPressed = true;
                }
                else if (space.IsPressed)
                {
                    space.IsPressed = false;
                    back.IsPressed = true;
                }
                else if (currentCol > 0)
                {
                    buttons[currentRow, currentCol].IsPressed = false;
                    currentCol--;
                    buttons[currentRow, currentCol].IsPressed = true;
                }
            }
            else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
            {
                if (backBtn.IsPressed)
                {
                    backBtn.IsPressed = false;
                    _keyboard["A"].IsPressed = true;
                    currentCol = 0;
                    currentRow = 2;
                }
                else if (back.IsPressed)
                {
                    back.IsPressed = false;
                    space.IsPressed = true;
                }
                else if (space.IsPressed)
                {
                    space.IsPressed = false;
                    done.IsPressed = true;
                }
                else if (currentCol < 10 && !done.IsPressed)
                {
                    buttons[currentRow, currentCol].IsPressed = false;
                    currentCol++;
                    buttons[currentRow, currentCol].IsPressed = true;
                }
            }
            else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
            {
                if (back.IsPressed && Global.onlineCode.Length > 0)
                {
                    Global.onlineCode = Global.onlineCode.Substring(0, Global.onlineCode.Length - 1);
                }
                else if (space.IsPressed)
                {
                    Global.onlineCode += " ";
                }
                else if (done.IsPressed)
                {
                    ScreenManager.Change(ScreenState.Game);
                }
                else if (backBtn.IsPressed)
                {
                    ScreenManager.Back();
                }
                else
                {
                    Global.onlineCode+=buttons[currentRow, currentCol].Text;
                }
            }


            codeFont.Text.Clear();
            codeFont.Text.Append(Global.onlineCode);
            codeFont.SetCenterAsOrigin();

            //Global.Reset = true;

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
