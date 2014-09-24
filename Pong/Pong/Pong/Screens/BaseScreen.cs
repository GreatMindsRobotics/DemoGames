using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FontEffectsLib.CoreTypes;

namespace Pong.Screens
{
    public abstract class BaseScreen : ISprite
    {
        protected List<ISprite> _sprites;

        public BaseScreen()
        {
            _sprites = new List<ISprite>();
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
        }
    }
}
