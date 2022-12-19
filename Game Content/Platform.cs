using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Survive
{
    public class Platform : DrawableGameComponent
    {
        public static Platform Instance { get; private set; }
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }

        public Vector2 position;

        public Platform(Game game, SpriteBatch spriteBatch,
        Texture2D tex, Vector2 Position) : base(game)
        {
            Instance = this;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = Position;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        //hitbox
        public Rectangle getLeftBounds()
        {
            return new Rectangle((int)position.X + tex.Width - 5, (int)position.Y + 3, tex.Width - (tex.Width - 5), tex.Height);
        }
        public Rectangle getRightBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y + 3, tex.Width - (tex.Width - 5), tex.Height);
        }
        public Rectangle getTopBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height - (tex.Height - 5));
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
