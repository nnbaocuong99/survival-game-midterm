using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Survive
{
    public class EnemyCollisionManager : GameComponent
    {

        private Enemy enemy;
        private List<Platform> platforms;
        private Platform Activeplatform;


        public EnemyCollisionManager(Game game, Enemy Enemy, List<Platform> PlatForms) : base(game)
        {
            this.enemy = Enemy;
            this.platforms = PlatForms;
        }



        public override void Update(GameTime gameTime)
        {
            foreach (Platform item in platforms)
            {
                Rectangle playerRect = enemy.player.getBounds();

                Rectangle enemyRect = enemy.getBounds();

                Rectangle platformRect = item.getBounds();

                if (playerRect.Intersects(enemyRect))
                {
                    enemy.player.isHit = true;
                }

                if (enemyRect.Intersects(platformRect))
                {
                    Activeplatform = platforms.ElementAtOrDefault(platforms.IndexOf(item));
                }

                if (Activeplatform != null)
                {
                    Rectangle platformRectLeft = Activeplatform.getLeftBounds();

                    Rectangle platformRectRight = Activeplatform.getRightBounds();

                    Rectangle platformRectTop = Activeplatform.getTopBounds();

                    //if the enemy hits the left side of the platform
                    //set iscollideleft to true
                    if (enemyRect.Intersects(platformRectLeft))
                    {
                        enemy.isCollideLeft = true;
                    }

                    //right side of platform
                    if (enemyRect.Intersects(platformRectRight))
                    {
                        enemy.isCollideRight = true;
                    }

                    //TODO: Fix jump function
                    if (enemyRect.Intersects(platformRectTop) && enemy.hasjumped && enemy.isCollideLeft == false && enemy.isCollideRight == false)
                    {

                        if (enemy.position.Y != platformRectTop.Top)
                        {
                            enemy.gravity = 0f;
                            enemy.isCollideUp = true;
                            enemy.position.Y = platformRectTop.Top - (enemy.enemyHeight);
                            enemy.hasjumped = false;
                        }

                    }

                    //if the enemy hits a wall and hasjumped is not true
                    if (enemy.isCollideLeft && enemy.hasjumped != true)
                    {
                        //set the position of the enemy
                        enemy.position.X = platformRectLeft.Right;

                        //if the emeny's height is not higher than the platform (they are on the ground)
                        //make the enemy jump
                        if (enemy.position.Y - enemy.enemyHeight > platformRectLeft.Height)
                        {
                            enemy.isCollideLeft = false;
                            enemy.position.Y -= enemy.jump;
                            enemy.gravity = -10f;
                            enemy.hasjumped = true;
                        }
                    }


                    //if the enemy hits a wall and hasjumped is not true
                    if (enemy.isCollideRight && enemy.hasjumped != true)
                    {
                        //set the position of the enemy
                        enemy.position.X = platformRectRight.Left - enemy.enemyWidth;

                        //if the emeny's height is not higher than the platform (they are on the ground)
                        //make the enemy jump
                        if (enemy.position.Y - enemy.enemyHeight > platformRectLeft.Height)
                        {
                            enemy.isCollideRight = false;
                            enemy.position.Y -= enemy.jump;
                            enemy.gravity = -10f;
                            enemy.hasjumped = true;
                        }
                    }

                    //if the enemy is on top of a platform
                    if (enemy.isCollideUp)
                    {
                        //if the enemy walks off the platform
                        //make it fall back to ground level
                        if (enemy.position.X > platformRectTop.Right && enemy.hasjumped != true)
                        {
                            enemy.hasjumped = true;

                            enemy.isCollideUp = false;
                        }

                        //if the enemy walks off the platform
                        //make it fall back to ground level
                        if (enemy.position.X + enemy.enemyWidth < platformRectTop.X && enemy.hasjumped != true)
                        {
                            enemy.hasjumped = true;

                            enemy.isCollideUp = false;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
