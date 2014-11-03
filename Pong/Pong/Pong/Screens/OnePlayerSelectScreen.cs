using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.CoreTypes;

namespace Pong.Screens
{
    class OnePlayerSelectScreen : BaseScreen
    {        
        Button easyBtn;
        Button mediumBtn;
        Button hardBtn;

        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            easyBtn = new Button(Content.Load<Texture2D>("temp easy button"), new Vector2(0, 0), Color.White);
            easyBtn.SetCenterAsOrigin();
            easyBtn.Position = new Vector2(_viewPort.Width / 2, 100);

            mediumBtn = new Button(Content.Load<Texture2D>("temp medium button"), new Vector2(0, 0), Color.White);
            mediumBtn.SetCenterAsOrigin();
            mediumBtn.Position = new Vector2(_viewPort.Width / 2, 250);

            hardBtn = new Button(Content.Load<Texture2D>("temp hard button"), new Vector2(0, 0), Color.White);
            hardBtn.SetCenterAsOrigin();
            hardBtn.Position = new Vector2(_viewPort.Width / 2, 400);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;


            _sprites.Add(easyBtn);
            _sprites.Add(mediumBtn);
            _sprites.Add(hardBtn);

            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if (easyBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Game);
            }
            else if (mediumBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.Game);
            }
            else if (hardBtn.IsClicked)
            {
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
