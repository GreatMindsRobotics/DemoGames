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
        //string displayedKeys;

        void loadkey(ContentManager content, string keyname, int x, int y, float scale)
        {
            TextButton name = new TextButton(content.Load<Texture2D>("Buttons//Key"), new Vector2(0, 0), Color.White, content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, keyname, new Rectangle(0, 117, 131, 137), new Rectangle(0, 0, 131, 117));
            name.Scale *= scale;
            name.Origin = new Vector2(name.Texture.Width / 2, 169);
            name.Position = new Vector2(x, y + name.SourceRectangle.Value.Height / 2);

            name.Clicked += new EventHandler(name_Clicked);
            


            _keyboard.Add(keyname, name);
            _sprites.Add(name);
        }

        void name_Clicked(object sender, EventArgs e)
        {
            TextButton clickedButton =(TextButton)sender;

            Global.onlineCode += clickedButton.Text;
        }




        int col1 = 350;
        int colDiff = 120;
        int row1 = 470;
        int rowDiff = 120;
        float scale = 0.85f;

        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        FadingFont codeFont;

        TextButton backBtn;
        TextButton spaceBtn;
        TextButton doneBtn;
        
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


            loadkey(Content, "1", col1, row1, scale);
            loadkey(Content, "2", col1 + colDiff, row1, scale);
            loadkey(Content, "3", col1 + colDiff * 2, row1, scale);
            loadkey(Content, "4", col1 + colDiff * 3, row1, scale);
            loadkey(Content, "5", col1 + colDiff * 4, row1, scale);
            loadkey(Content, "6", col1 + colDiff * 5, row1, scale);
            loadkey(Content, "7", col1 + colDiff * 6, row1, scale);
            loadkey(Content, "8", col1 + colDiff * 7, row1, scale);
            loadkey(Content, "9", col1 + colDiff * 8, row1, scale);
            loadkey(Content, "0", col1 + colDiff * 9, row1, scale);
            loadkey(Content, "#", col1 + colDiff * 10, row1, scale);

            loadkey(Content, "Q", col1, row1 + rowDiff, scale);
            loadkey(Content, "W", col1 + colDiff, row1 + rowDiff, scale);
            loadkey(Content, "E", col1 + colDiff * 2, row1 + rowDiff, scale);
            loadkey(Content, "R", col1 + colDiff * 3, row1 + rowDiff, scale);
            loadkey(Content, "T", col1 + colDiff * 4, row1 + rowDiff, scale);
            loadkey(Content, "Y", col1 + colDiff * 5, row1 + rowDiff, scale);
            loadkey(Content, "U", col1 + colDiff * 6, row1 + rowDiff, scale);
            loadkey(Content, "I", col1 + colDiff * 7, row1 + rowDiff, scale);
            loadkey(Content, "O", col1 + colDiff * 8, row1 + rowDiff, scale);
            loadkey(Content, "P", col1 + colDiff * 9, row1 + rowDiff, scale);
            loadkey(Content, "*", col1 + colDiff * 10, row1 + rowDiff, scale);


            loadkey(Content, "A", col1, row1 + rowDiff * 2, scale);
            loadkey(Content, "S", col1 + colDiff, row1 + rowDiff * 2, scale);
            loadkey(Content, "D", col1 + colDiff * 2, row1 + rowDiff * 2, scale);
            loadkey(Content, "F", col1 + colDiff * 3, row1 + rowDiff * 2, scale);
            loadkey(Content, "G", col1 + colDiff * 4, row1 + rowDiff * 2, scale);
            loadkey(Content, "H", col1 + colDiff * 5, row1 + rowDiff * 2, scale);
            loadkey(Content, "J", col1 + colDiff * 6, row1 + rowDiff * 2, scale);
            loadkey(Content, "K", col1 + colDiff * 7, row1 + rowDiff * 2, scale);
            loadkey(Content, "L", col1 + colDiff * 8, row1 + rowDiff * 2, scale);
            loadkey(Content, ".", col1 + colDiff * 9, row1 + rowDiff * 2, scale);
            loadkey(Content, "&", col1 + colDiff * 10, row1 + rowDiff * 2, scale);

            loadkey(Content, "Z", col1, row1 + rowDiff * 3, scale);
            loadkey(Content, "X", col1 + colDiff, row1 + rowDiff * 3, scale);
            loadkey(Content, "C", col1 + colDiff * 2, row1 + rowDiff * 3, scale);
            loadkey(Content, "V", col1 + colDiff * 3, row1 + rowDiff * 3, scale);
            loadkey(Content, "B", col1 + colDiff * 4, row1 + rowDiff * 3, scale);
            loadkey(Content, "N", col1 + colDiff * 5, row1 + rowDiff * 3, scale);
            loadkey(Content, "M", col1 + colDiff * 6, row1 + rowDiff * 3, scale);
            loadkey(Content, "-", col1 + colDiff * 7, row1 + rowDiff * 3, scale);
            loadkey(Content, "_", col1 + colDiff * 8, row1 + rowDiff * 3, scale);
            loadkey(Content, "!", col1 + colDiff * 9, row1 + rowDiff * 3, scale);
            loadkey(Content, "?", col1 + colDiff * 10, row1 + rowDiff * 3, scale);


            backBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Back", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            backBtn.Scale *= scale;
            backBtn.Scale = new Vector2(backBtn.Scale.X + 0.02f, backBtn.Scale.Y);
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 137);
            backBtn.Position = new Vector2(col1 + 120, row1 + rowDiff * 4 + backBtn.SourceRectangle.Value.Height / 2);

            spaceBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, " ", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            spaceBtn.Scale *= scale;
            spaceBtn.Scale = new Vector2(spaceBtn.Scale.X + 0.61f, spaceBtn.Scale.Y);
            spaceBtn.Origin = new Vector2(spaceBtn.Texture.Width / 2, 137);
            spaceBtn.Position = new Vector2(col1 + colDiff * 3 + 240, row1 + rowDiff * 4 + spaceBtn.SourceRectangle.Value.Height / 2);

            doneBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Done", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            doneBtn.Scale *= scale;
            doneBtn.Scale = new Vector2(doneBtn.Scale.X + 0.02f, doneBtn.Scale.Y);
            doneBtn.Origin = new Vector2(doneBtn.Texture.Width / 2, 137);
            doneBtn.Position = new Vector2(col1 + colDiff * 8 + 120, row1 + rowDiff * 4 + doneBtn.SourceRectangle.Value.Height / 2);


            _sprites.Add(codeFont);
            _sprites.Add(backBtn);
            _sprites.Add(spaceBtn);
            _sprites.Add(doneBtn);
         

            if (!Global.UsingKeyboard)
            {
                //doneBtn.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {

            if (backBtn.IsClicked && Global.onlineCode.Length > 0)
            {
                Global.onlineCode = Global.onlineCode.Substring(0, Global.onlineCode.Length - 1);
            }
            else if (spaceBtn.IsClicked)
            {
                Global.onlineCode += " ";
            }
            else if (doneBtn.IsClicked)
            {

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
