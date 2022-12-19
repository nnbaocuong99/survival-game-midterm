using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Survive
{
    public class CollisionManager : GameComponent
    {
        private Player player;
        private List<Platform> platforms;
        private Platform Activeplatform;


        public CollisionManager(Game game, Player player, List<Platform> platforms) : base(game)
        {
            this.player = player;
            this.platforms = platforms;
        }



        public override void Update(GameTime gameTime)
        {

            foreach (Platform item in platforms)
            {

                Rectangle playerRect = player.getBounds();

                Rectangle platformRect = item.getBounds();

                if (playerRect.Intersects(platformRect))
                {
                    player.platform = platforms.IndexOf(item).ToString();
                    Activeplatform = platforms.ElementAtOrDefault(platforms.IndexOf(item));
                }

                if (Activeplatform != null)
                {
                    Rectangle platformRectLeft = Activeplatform.getLeftBounds();

                    Rectangle platformRectRight = Activeplatform.getRightBounds();

                    Rectangle platformRectTop = Activeplatform.getTopBounds();

                    if (playerRect.Intersects(platformRectLeft))
                    {
                        player.isCollideLeft = true;
                    }

                    //right side of platform
                    if (playerRect.Intersects(platformRectRight))
                    {
                        player.isCollideRight = true;
                    }

                    //TODO: Fix jump function
                    if (playerRect.Intersects(platformRectTop) && player.hasjumped && player.isCollideLeft == false && player.isCollideRight == false)
                    {

                        if (player.position.Y != platformRectTop.Top)
                        {
                            player.gravity = 0f;
                            player.isCollideUp = true;
                            player.position.Y = platformRectTop.Top - (player.playerHeight);
                            player.hasjumped = false;
                        }
                    }


                    if (player.isCollideLeft)
                    {
                        player.position.X = platformRectLeft.Right;
                        if (player.position.Y - player.playerHeight > platformRectLeft.Height)
                        {
                            player.isCollideLeft = false;
                        }
                    }

                    if (player.isCollideRight)
                    {
                        player.position.X = platformRectRight.Left - player.playerWidth;
                        if (player.position.Y - player.playerHeight > platformRectLeft.Height)
                        {
                            player.isCollideRight = false;
                        }
                    }

                    if (player.isCollideUp)
                    {
                        if (player.position.X > platformRectTop.Right)
                        {
                            player.hasjumped = true;

                            player.isCollideUp = false;
                        }
                        if (player.position.X + player.playerWidth < platformRectTop.X)
                        {
                            player.hasjumped = true;

                            player.isCollideUp = false;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
