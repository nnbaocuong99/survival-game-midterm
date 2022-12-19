using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Survive
{
    public class Player : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position;
        public int speed { get; set; }
        public int jump { get; set; }
        public bool hasjumped { get; set; }
        public float gravity { get; set; }

        public bool isCollideLeft;

        public bool isCollideRight;

        public bool isCollideUp;

        public bool isHit;

        public double time = 0f;

        public double timeTotal = 0f;

        public int spriteSizeX = 3;

        public int spriteSizeY = 3;

        public int playerWidth;

        public int playerHeight;

        public int frameX = 0;

        public int frameY = 0;

        public int framePause = 6;

        public int frameTime = 0;

        public int health = 100;

        public string platform;

        public int score = 0;

        public bool isDead;

        public bool shoot;

        public bool debugMode;

        private SoundEffect jumpSound;

        private SoundEffect gunSound;

        public MouseState lastMouseState;

        public MouseState currentMouseState;
        public Player(Game game, SpriteBatch spriteBatch,
            Texture2D tex, int Speed, int Jump, Vector2 newPosition, SoundEffect jumpSound, SoundEffect gunSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = newPosition;
            speed = Speed;
            jump = Jump;
            this.jumpSound = jumpSound;
            playerWidth = tex.Width / 3;
            playerHeight = 34;
            this.gunSound=gunSound;
        }




        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            timeTotal += gameTime.ElapsedGameTime.TotalSeconds;

            if (health <= 0)
            {
                Enabled = false;
                position = new Vector2(4589, 2364);
                isDead = true;
            }

            if (!isDead)
            {
                if (time  >= 1)
                {
                    score++;
                    time = 0;
                }

            }

            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse.GetState();

            // Recognize a single click of the left mouse button
            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                // React to the click
                // ...
                shoot = true;
                gunSound.Play();
            }
            else
            {
                shoot = false;
            }

            position.Y += gravity;
            platform = "";


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (position.X > Shared.stage.X - playerWidth)
                {
                    position = new Vector2(Shared.stage.X - playerWidth, position.Y);
                }
                else if (!isCollideLeft)
                {
                    position.X += speed;
                    CreateFrames();
                }
                isCollideLeft = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (position.X < 0)
                {
                    position = new Vector2(0, position.Y);
                }
                else if (!isCollideRight)
                {
                    position.X -= speed;
                    CreateFrames();
                }
                isCollideRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasjumped == false)
            {
                position.Y -= jump;
                gravity = -10f;
                hasjumped = true;
                jumpSound.Play();
                CreateFrames();
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

                if (position.Y + playerHeight >= 800)
                {
                    hasjumped = false;
                }
            }

            //NOTE: there are some weird edge cases with this but it works
            else if (hasjumped == false)
            {
                if (position.Y < Shared.stage.Y && !isCollideUp)
                {
                    position.Y = Shared.stage.Y - playerHeight;
                }
                else
                {
                    gravity = 0f;
                }
            }



            base.Update(gameTime);
        }

        public void CreateFrames()
        {
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
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, new Rectangle(tex.Width / spriteSizeX * frameX + 9, tex.Height / spriteSizeY * frameY + 9, playerWidth, playerHeight), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //hitbox
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight);

        }
    }
}
