using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FontEffectsLib.CoreTypes;
using Pong.Sprites;
using Pong.CoreTypes;

namespace Pong.Screens
{
    public abstract class BaseScreen : ISprite
    {
        protected List<ISprite> _sprites;


        protected Viewport _viewPort;

        public Viewport ViewPort
        {
            set
            {
                _viewPort = value;
            }
        }

        public BaseScreen()
        {
            _viewPort = new Viewport(0, 0, 1920, 1080);
            _sprites = new List<ISprite>();
        }

        public BaseScreen(Viewport viewPort)
        {
            _sprites = new List<ISprite>();
            _viewPort = viewPort;
        }

        public abstract void Load(ContentManager Content);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public virtual void Update(GameTime gameTime)
        {            
            foreach (ISprite sprite in _sprites)
            {
                sprite.Update(gameTime);
            }

                //Check for selected button if no controller is connected
                if(!Global.UsingKeyboard)
                {
                    bool isAnyButtonSelected = false;

                    foreach (Button button in _sprites.OfType<Button>())
                    {
                        if (button.IsPressed)
                        {
                            isAnyButtonSelected = true;
                            break;
                        }
                    }

                    if (!isAnyButtonSelected)
                    { 
                        //Select any button
                        Button selectMe = _sprites.OfType<Button>().FirstOrDefault();
                        if (selectMe != null)
                        {
                            selectMe.IsPressed = true;
                        }
                    }

                }

        }

        public virtual void Reset()
        {

        }
    }
}
