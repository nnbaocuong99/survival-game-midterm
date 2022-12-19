using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Survive
{
    //this is the main menu
    public class StartScene : GameScene
    {
        //menu will be declared
        public MenuComponent menu { get; set; }

        private SpriteBatch spriteBatch;
        Game1 g;
        string title = "Ohio Simulator";
        string[] menuItems = { "Start", "Help", "About", "Credit", "Quit" };


        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;
            //instantiate menu here
            SpriteFont regular = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilight = game.Content.Load<SpriteFont>("fonts/hilightFont");
            menu = new MenuComponent(game, spriteBatch, regular, hilight, menuItems, title);
            components.Add(menu);


            //Added music
            Song backgroundMusic = g.Content.Load<Song>("songs/InGameMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }
    }
}
