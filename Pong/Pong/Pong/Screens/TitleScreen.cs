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
using Pong.Sprites;


namespace Pong.Screens
{
    public class TitleScreen : BaseScreen
    {
        Button startButton;

        DropInFont greatDropInFont;
        DropInFont mindsDropInFont;

        DropInFont pDropInFont;
        DropInFont oDropInFont;
        DropInFont nDropInFont;
        DropInFont gDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            ScreenState screenState;

            //TODO Change Button so it says "Start"
            startButton = new Button(Content.Load<Texture2D>("temp play button"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            startButton.SetCenterAsOrigin();

            greatDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(320, -1000), new Vector2(320, 50), dropSpeed, "Great", Color.CornflowerBlue);
            greatDropInFont.IsVisible = true;
            greatDropInFont.SetCenterAsOrigin();
            greatDropInFont.EnableShadow = false;
            greatDropInFont.TintColor = Color.Black;
            greatDropInFont.ShadowPosition = new Vector2(greatDropInFont.Position.X - 4, greatDropInFont.Position.Y + 4);
            greatDropInFont.ShadowColor = Color.Gray;
            greatDropInFont.StateChanged += new EventHandler<StateEventArgs>(epicDropInFont_StateChanged);

            mindsDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(490, -1000), new Vector2(490, 50), dropSpeed, "Minds", Color.CornflowerBlue);
            mindsDropInFont.IsVisible = false;
            mindsDropInFont.SetCenterAsOrigin();
            mindsDropInFont.EnableShadow = false;
            mindsDropInFont.TintColor = Color.Black;
            mindsDropInFont.ShadowPosition = new Vector2(mindsDropInFont.Position.X - 4, mindsDropInFont.Position.Y + 4);
            mindsDropInFont.ShadowColor = Color.Gray;
            mindsDropInFont.StateChanged += new EventHandler<StateEventArgs>(mindsDropInFont_StateChanged);

            pDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(325, -1000), new Vector2(325, 100), dropSpeed, "P", Color.CornflowerBlue);
            pDropInFont.IsVisible = false;
            pDropInFont.SetCenterAsOrigin();
            pDropInFont.EnableShadow = false;
            pDropInFont.TintColor = Color.Black;
            pDropInFont.ShadowPosition = new Vector2(pDropInFont.Position.X - 4, pDropInFont.Position.Y + 4);
            pDropInFont.ShadowColor = Color.Gray;
            pDropInFont.StateChanged += new EventHandler<StateEventArgs>(pDropInFont_StateChanged);

            oDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(355, -1000), new Vector2(355, 100), dropSpeed, "O", Color.CornflowerBlue);
            oDropInFont.IsVisible = false;
            oDropInFont.SetCenterAsOrigin();
            oDropInFont.EnableShadow = false;
            oDropInFont.TintColor = Color.Black;
            oDropInFont.ShadowPosition = new Vector2(oDropInFont.Position.X - 4, oDropInFont.Position.Y + 4);
            oDropInFont.ShadowColor = Color.Gray;
            oDropInFont.StateChanged += new EventHandler<StateEventArgs>(oDropInFont_StateChanged);

            nDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(390, -1000), new Vector2(390, 100), dropSpeed, "N", Color.CornflowerBlue);
            nDropInFont.IsVisible = false;
            nDropInFont.SetCenterAsOrigin();
            nDropInFont.EnableShadow = false;
            nDropInFont.TintColor = Color.Black;
            nDropInFont.ShadowPosition = new Vector2(nDropInFont.Position.X - 4, nDropInFont.Position.Y + 4);
            nDropInFont.ShadowColor = Color.Gray;
            nDropInFont.StateChanged += new EventHandler<StateEventArgs>(nDropInFont_StateChanged);

            gDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(425, -1000), new Vector2(425, 100), dropSpeed, "G", Color.CornflowerBlue);
            gDropInFont.IsVisible = false;
            gDropInFont.SetCenterAsOrigin();
            gDropInFont.EnableShadow = false;
            gDropInFont.TintColor = Color.Black;
            gDropInFont.ShadowPosition = new Vector2(gDropInFont.Position.X - 4, gDropInFont.Position.Y + 4);
            gDropInFont.ShadowColor = Color.Gray;
            gDropInFont.StateChanged += new EventHandler<StateEventArgs>(gDropInFont_StateChanged);

            _sprites.Add(startButton);
            _sprites.Add(greatDropInFont);
            _sprites.Add(mindsDropInFont);
            _sprites.Add(pDropInFont);
            _sprites.Add(oDropInFont);
            _sprites.Add(nDropInFont);
            _sprites.Add(gDropInFont);

        }

        void epicDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
                //TODO Add sound effects
                mindsDropInFont.IsVisible = true;
            }
        }

        void mindsDropInFont_StateChanged(object sender, StateEventArgs e)
        {
            if ((DropInFont.FontState)e.Data == DropInFont.FontState.Compress)
            {
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

            if(startButton.IsClicked)
            {
                ScreenManager.Change(ScreenState.MainMenu);
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            greatDropInFont.Reset();
            mindsDropInFont.Reset();
            pDropInFont.Reset();
            oDropInFont.Reset();
            nDropInFont.Reset();
            gDropInFont.Reset();

            base.Reset();
        }
    }
}
