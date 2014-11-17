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
        //Button startButton;

        DropInFont greatDropInFont;

        DropInFont pDropInFont;
        DropInFont oDropInFont;
        DropInFont nDropInFont;
        DropInFont gDropInFont;

        FadingFont infoFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            ScreenState screenState;

            //TODO Change Button so it says "Start"
            //startButton = new Button(Content.Load<Texture2D>("temp play button"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            //startButton.SetCenterAsOrigin();

            infoFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height - 15), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Press any key to continue "), Color.White, false);
            infoFont.EnableShadow = false;
            infoFont.SetCenterAsOrigin();

            greatDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2 - _viewPort.X, -1000), new Vector2(_viewPort.Width / 2 - _viewPort.X, _viewPort.Height * 0.1f), dropSpeed, "GreatMinds", Color.CornflowerBlue);
            greatDropInFont.IsVisible = true;
            greatDropInFont.SetCenterAsOrigin();
            greatDropInFont.EnableShadow = false;
            greatDropInFont.TintColor = Color.Black;
            greatDropInFont.ShadowPosition = new Vector2(greatDropInFont.Position.X - 4, greatDropInFont.Position.Y + 4);
            greatDropInFont.ShadowColor = Color.Gray;
            greatDropInFont.StateChanged += new EventHandler<StateEventArgs>(epicDropInFont_StateChanged);

            pDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(greatDropInFont.Origin.X + greatDropInFont.Position.X / 2.5f, -1000), new Vector2(greatDropInFont.Origin.X + greatDropInFont.Position.X / 2.5f, 100), dropSpeed, "P", Color.CornflowerBlue);
            pDropInFont.IsVisible = false;
            pDropInFont.SetCenterAsOrigin();
            pDropInFont.EnableShadow = false;
            pDropInFont.TintColor = Color.Black;
            pDropInFont.ShadowPosition = new Vector2(pDropInFont.Position.X - 4, pDropInFont.Position.Y + 4);
            pDropInFont.ShadowColor = Color.Gray;
            pDropInFont.StateChanged += new EventHandler<StateEventArgs>(pDropInFont_StateChanged);

            oDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(pDropInFont.Origin.X * 2.5f + pDropInFont.Position.X, -1000), new Vector2(pDropInFont.Origin.X * 2.5f + pDropInFont.Position.X, 100), dropSpeed, "O", Color.CornflowerBlue);
            oDropInFont.IsVisible = false;
            oDropInFont.SetCenterAsOrigin();
            oDropInFont.EnableShadow = false;
            oDropInFont.TintColor = Color.Black;
            oDropInFont.ShadowPosition = new Vector2(oDropInFont.Position.X - 4, oDropInFont.Position.Y + 4);
            oDropInFont.ShadowColor = Color.Gray;
            oDropInFont.StateChanged += new EventHandler<StateEventArgs>(oDropInFont_StateChanged);

            nDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(pDropInFont.Origin.X * 2.5f + oDropInFont.Position.X, -1000), new Vector2(pDropInFont.Origin.X * 2.5f + oDropInFont.Position.X, 100), dropSpeed, "N", Color.CornflowerBlue);
            nDropInFont.IsVisible = false;
            nDropInFont.SetCenterAsOrigin();
            nDropInFont.EnableShadow = false;
            nDropInFont.TintColor = Color.Black;
            nDropInFont.ShadowPosition = new Vector2(nDropInFont.Position.X - 4, nDropInFont.Position.Y + 4);
            nDropInFont.ShadowColor = Color.Gray;
            nDropInFont.StateChanged += new EventHandler<StateEventArgs>(nDropInFont_StateChanged);

            gDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(pDropInFont.Origin.X * 2.5f + nDropInFont.Position.X, -1000), new Vector2(pDropInFont.Origin.X * 2.5f + nDropInFont.Position.X, 100), dropSpeed, "G", Color.CornflowerBlue);
            gDropInFont.IsVisible = false;
            gDropInFont.SetCenterAsOrigin();
            gDropInFont.EnableShadow = false;
            gDropInFont.TintColor = Color.Black;
            gDropInFont.ShadowPosition = new Vector2(gDropInFont.Position.X - 4, gDropInFont.Position.Y + 4);
            gDropInFont.ShadowColor = Color.Gray;
            gDropInFont.StateChanged += new EventHandler<StateEventArgs>(gDropInFont_StateChanged);

            //_sprites.Add(startButton);
            _sprites.Add(infoFont);
            _sprites.Add(greatDropInFont);
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
            Keys[] pressedKeys = keyboard.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {
                ScreenManager.Change(ScreenState.MainMenu);
            }

            //if(startButton.IsClicked)
            //{
            //    ScreenManager.Change(ScreenState.MainMenu);
            //}

            base.Update(gameTime);
        }

        public override void Reset()
        {
            greatDropInFont.Reset();
            pDropInFont.Reset();
            oDropInFont.Reset();
            nDropInFont.Reset();
            gDropInFont.Reset();

            base.Reset();
        }
    }
}
