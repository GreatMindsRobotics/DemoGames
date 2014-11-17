using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.CoreTypes;
using Pong.Screens;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;
namespace Pong.Screens
{
    class OnePlayerSelectScreen : BaseScreen
    {
        DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button easyBtn;
        Button mediumBtn;
        Button hardBtn;
        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "SinglePlayer", Color.CornflowerBlue);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            easyBtn = new Button(Content.Load<Texture2D>("temp easy button"), new Vector2(0, 0), Color.White);
            easyBtn.SetCenterAsOrigin();
            easyBtn.Position = new Vector2(_viewPort.Width / 2, titleDropInFont.Position.Y + easyBtn.Origin.Y * 2.5f);

            mediumBtn = new Button(Content.Load<Texture2D>("temp medium button"), new Vector2(0, 0), Color.White);
            mediumBtn.SetCenterAsOrigin();
            mediumBtn.Position = new Vector2(_viewPort.Width / 2, easyBtn.Position.Y + mediumBtn.Origin.Y * 4);

            hardBtn = new Button(Content.Load<Texture2D>("temp hard button"), new Vector2(0, 0), Color.White);
            hardBtn.SetCenterAsOrigin();
            hardBtn.Position = new Vector2(_viewPort.Width / 2, mediumBtn.Position.Y + hardBtn.Origin.Y * 4);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(titleDropInFont);
            _sprites.Add(easyBtn);
            _sprites.Add(mediumBtn);
            _sprites.Add(hardBtn);
            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
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

            base.Update(gameTime);
        }
    }
}
