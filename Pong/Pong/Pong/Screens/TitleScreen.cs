using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontEffectsLib.SpriteTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;

using Microsoft.Xna.Framework.Input;
using Pong.CoreTypes;


namespace Pong.Screens
{
    public class TitleScreen : BaseScreen
    {
        GameSprite titleLogoSprite;

        DropInFont epicDropInFont;

        DropInFont pDropInFont;
        DropInFont oDropInFont;
        DropInFont nDropInFont;
        DropInFont gDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            
 
            ScreenState screenState;
            titleLogoSprite = new GameSprite(Content.Load<Texture2D>("titleLogo"), new Vector2(_viewPort.Width / 2 - 25, _viewPort.Height / 2), Color.White);


            _sprites.Add(titleLogoSprite);

            epicDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(300, -1000), new Vector2(300, 50), dropSpeed, "EPIC", Color.CornflowerBlue);
            epicDropInFont.IsVisible = true;
            epicDropInFont.SetCenterAsOrigin();
            epicDropInFont.EnableShadow = false;
            epicDropInFont.TintColor = Color.Black;
            epicDropInFont.ShadowPosition = new Vector2(epicDropInFont.Position.X - 4, epicDropInFont.Position.Y + 4);
            epicDropInFont.ShadowColor = Color.Gray;
            epicDropInFont.StateChanged += new EventHandler<StateEventArgs>(epicDropInFont_StateChanged);

            _sprites.Add(epicDropInFont);

            pDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(305, -1000), new Vector2(305, 100), dropSpeed, "P", Color.CornflowerBlue);
            pDropInFont.IsVisible = false;
            pDropInFont.SetCenterAsOrigin();
            pDropInFont.EnableShadow = false;
            pDropInFont.TintColor = Color.Black;
            pDropInFont.ShadowPosition = new Vector2(pDropInFont.Position.X - 4, pDropInFont.Position.Y + 4);
            pDropInFont.ShadowColor = Color.Gray;
            pDropInFont.StateChanged += new EventHandler<StateEventArgs>(pDropInFont_StateChanged);

            _sprites.Add(pDropInFont);

            oDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(335, -1000), new Vector2(335, 100), dropSpeed, "O", Color.CornflowerBlue);
            oDropInFont.IsVisible = false;
            oDropInFont.SetCenterAsOrigin();
            oDropInFont.EnableShadow = false;
            oDropInFont.TintColor = Color.Black;
            oDropInFont.ShadowPosition = new Vector2(oDropInFont.Position.X - 4, oDropInFont.Position.Y + 4);
            oDropInFont.ShadowColor = Color.Gray;
            oDropInFont.StateChanged += new EventHandler<StateEventArgs>(oDropInFont_StateChanged);

            _sprites.Add(oDropInFont);

            nDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(370, -1000), new Vector2(370, 100), dropSpeed, "N", Color.CornflowerBlue);
            nDropInFont.IsVisible = false;
            nDropInFont.SetCenterAsOrigin();
            nDropInFont.EnableShadow = false;
            nDropInFont.TintColor = Color.Black;
            nDropInFont.ShadowPosition = new Vector2(nDropInFont.Position.X - 4, nDropInFont.Position.Y + 4);
            nDropInFont.ShadowColor = Color.Gray;
            nDropInFont.StateChanged += new EventHandler<StateEventArgs>(nDropInFont_StateChanged);

            _sprites.Add(nDropInFont);

            gDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(405, -1000), new Vector2(405, 100), dropSpeed, "G", Color.CornflowerBlue);
            gDropInFont.IsVisible = false;
            gDropInFont.SetCenterAsOrigin();
            gDropInFont.EnableShadow = false;
            gDropInFont.TintColor = Color.Black;
            gDropInFont.ShadowPosition = new Vector2(gDropInFont.Position.X - 4, gDropInFont.Position.Y + 4);
            gDropInFont.ShadowColor = Color.Gray;
            gDropInFont.StateChanged += new EventHandler<StateEventArgs>(gDropInFont_StateChanged);

            _sprites.Add(gDropInFont);

        }

        void epicDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                //TODO Add sound effects
                pDropInFont.IsVisible = true;
            }
        }

        void pDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                //TODO Add sound effects
                oDropInFont.IsVisible = true;
            }
        }

        void oDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                //TODO Add sound effects
                nDropInFont.IsVisible = true;
            }
        }

        void nDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                 //TODO Add sound effects
                gDropInFont.IsVisible = true;
            }
        }

        void gDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                //TODO Add sound effects
            }
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Enter))
            {
                ScreenManager.Change(ScreenState.MainMenu);
            }

            base.Update(gameTime);
        }
    }
}
