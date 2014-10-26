using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.CoreTypes;

namespace Pong.Screens
{
    class OptionsScreen : BaseScreen
    {

        Button editContBtn;

        Button backBtn;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            editContBtn = new Button(Content.Load<Texture2D>("temp edit controls button"), new Vector2(0, 0), Color.White);
            editContBtn.SetCenterAsOrigin();
            editContBtn.Position = new Vector2(_viewPort.Width / 2, 150);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(editContBtn);


            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if (editContBtn.IsClicked)
            {
                ScreenManager.Change(ScreenState.EditControls);
            }
            else if (backBtn.IsClicked)
            {
                ScreenManager.Back();
            }

            base.Update(gameTime);
        }
    }
}
