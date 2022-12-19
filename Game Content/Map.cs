using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Survive
{
    public class Map : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;

        public Map(Game game,
           SpriteBatch spriteBatch,
           Texture2D tex
           )
            : base(game)
        {
            this.spriteBatch=spriteBatch;
            this.tex=tex;
            //this.srcRect=srcRect;
          
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, srcRect, Color.White);
            spriteBatch.Draw(tex, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
