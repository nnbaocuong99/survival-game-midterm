using System;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Survive
{
    public class Enemy : DrawableGameComponent
    {
        public Game game { get; set; }

        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position;
        public int speed { get; set; }
        public int jump { get; set; }
        public bool hasjumped { get; set; }
        public string test { get; set; }
        public float gravity { get; set; }

        public bool isCollideLeft;

        public bool isCollideRight;

        public bool isCollideUp;

        public bool isDead;

        public double time = 0f;

        public Player player;

        public int enemyWidth;

        public int enemyHeight;

        public int health = 45;

        public int frameX = 0;

        public int frameY = 0;

        public int framePause = 7;

        public int frameTime = 0;

        public int spriteSizeX = 3;

        public int spriteSizeY = 3;

        public int killCount = 0;

        public bool debugMode;


        public Enemy(Game game, SpriteBatch spriteBatch,
            Texture2D tex, int Speed, int Jump, Vector2 newPosition, Player Player) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = newPosition;
            speed = Speed;
            jump = Jump;
            player = Player;
            enemyWidth = tex.Width / 3;
            enemyHeight = 50;
        }

        public Enemy CreateRandomEnemy(Vector2 position)
        {
            var enemy = new Enemy(game, spriteBatch, tex, speed, jump, position, player);
            return enemy;
        }


        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;

            if (player.shoot)
            {
                health -= 5;

            }

            if (health <= 0)
            {
                isDead = true;
            }

            if (isDead)
            {
                health = 45;
                isDead = false;
                killCount++;
                player.score += 5;
            }

            position.Y += gravity;
            if (getBounds().Contains(player.position.X + player.playerWidth, player.position.Y) && time >= 1 ||
                getBounds().Contains(player.position.X - player.playerWidth, player.position.Y) && time >= 1)
            {
                player.health -= 5;
                player.isHit = true;
                time = 0;
            }
            else
            {
                player.isHit = false;
            }

            if (!player.isDead)
            {
                if (player.position.X <= position.X + enemyWidth)
                {
                    if (!isCollideLeft)
                    {
                        position.X -= 3;
                    }
                    isCollideLeft = false;
                }

                if (player.position.X + player.playerWidth / 2 >= position.X)
                {
                    if (!isCollideLeft)
                    {
                        position.X += 3;
                    }
                    isCollideRight = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                debugMode = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.CapsLock))
            {
                debugMode = false;
            }

            if (hasjumped == true)
            {
                float i = jump;
                gravity += 0.15f * i;

                if (position.Y + enemyHeight >= 800)
                {
                    hasjumped = false;
                }
            }

            //NOTE: there are some weird edge cases with this but it works
            else if (hasjumped == false)
            {
                if (position.Y < Shared.stage.Y && !isCollideUp)
                {
                    position.Y = Shared.stage.Y - enemyHeight;
                }
                else
                {
                    gravity = 0f;
                }
            }

            frameTime++;
            if (frameTime >= framePause)
            {
                frameX++;
                if (frameX == 1 && frameY == 2)
                {
                    frameX = 0;
                    frameY = 0;
                }
                else if (frameX >= spriteSizeX)
                {
                    frameX = 0;
                    frameY++;
                    if (frameY >= spriteSizeY)
                    {
                        frameY = 0;
                    }
                }
                frameTime = 0;
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            //spriteBatch.Draw(tex, position, null, Color.White);

            if (!isDead)
            {
                spriteBatch.Draw(tex, position, new Rectangle(tex.Width / spriteSizeX * frameX + 9, tex.Height / spriteSizeY * frameY + 9, enemyWidth, enemyHeight), Color.White);
            }

            spriteBatch.DrawString(regular, health.ToString(), new Vector2(position.X, position.Y - 20), Color.White);
            if (debugMode)
            {
                spriteBatch.DrawString(regular, "enemy.isCollideUp: " + isCollideUp.ToString(), new Vector2(800, 0), Color.White);
                spriteBatch.DrawString(regular, "enemy.isCollideLeft: " + isCollideLeft.ToString(), new Vector2(800, 20), Color.White);
                spriteBatch.DrawString(regular, "enemy.isCollideRight: " + isCollideRight.ToString(), new Vector2(800, 40), Color.White);
                spriteBatch.DrawString(regular, "Enemy X Position: " + position.X.ToString(), new Vector2(800, 60), Color.White);
                spriteBatch.DrawString(regular, "Enemy Y Position: " + position.Y.ToString(), new Vector2(800, 80), Color.White);
                spriteBatch.DrawString(regular, "Enemy.HasJump: " + hasjumped.ToString(), new Vector2(800, 100), Color.White);
                spriteBatch.DrawString(regular, "Enemy Health: " + health.ToString(), new Vector2(800, 120), Color.White);
                spriteBatch.DrawString(regular, "Enemy Width: " + enemyWidth.ToString(), new Vector2(800, 140), Color.White);
                spriteBatch.DrawString(regular, "Enemy Height: " + enemyHeight.ToString(), new Vector2(800, 160), Color.White);
                spriteBatch.DrawString(regular, "Is Enemy Dead? " + isDead.ToString(), new Vector2(800, 180), Color.White);
                spriteBatch.DrawString(regular, "Player.isCollideUp: " + player.isCollideUp.ToString(), new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(regular, "Player.isCollideLeft: " + player.isCollideLeft.ToString(), new Vector2(0, 20), Color.White);
                spriteBatch.DrawString(regular, "Player.isCollideRight: " + player.isCollideRight.ToString(), new Vector2(0, 40), Color.White);
                spriteBatch.DrawString(regular, "Player.HasJump: " + player.hasjumped.ToString(), new Vector2(0, 60), Color.White);
                spriteBatch.DrawString(regular, "Player X Position: " + player.position.X.ToString(), new Vector2(0, 80), Color.White);
                spriteBatch.DrawString(regular, "Player Y Position: " + player.position.Y.ToString(), new Vector2(0, 100), Color.White);
                spriteBatch.DrawString(regular, "Player.isHit: " + player.isHit.ToString(), new Vector2(0, 120), Color.White);
                spriteBatch.DrawString(regular, "Player Health: " + player.health.ToString(), new Vector2(0, 140), Color.White);
                spriteBatch.DrawString(regular, "Time alive: " + player.timeTotal.ToString(), new Vector2(0, 180), Color.White);
                spriteBatch.DrawString(regular, "Score: " + player.score.ToString(), new Vector2(0, 200), Color.White);
                spriteBatch.DrawString(regular, "Is the Player Shooting? " + player.shoot.ToString(), new Vector2(0, 220), Color.White);
                spriteBatch.DrawString(regular, "PRESS TO CAPS LOCK TO ESCAPE", new Vector2(0, 240), Color.White);
            }
            else
            {
                spriteBatch.DrawString(regular, "Kills: " + killCount.ToString(), new Vector2(0, 40), Color.White);
                spriteBatch.DrawString(regular, "Health: " + player.health.ToString(), new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(regular, "Score: " + player.score.ToString(), new Vector2(0, 20), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //hitbox
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, enemyWidth, enemyHeight);
            //return new Rectangle((tex.Width / spriteSizeX * frameX) + 9, 9, enemyWidth, enemyHeight);

        }
    }
}
