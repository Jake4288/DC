using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XRpgLibrary.SpriteClasses;


namespace DungeonCrawl.Components
{

    public class CollisionHandler
    {

        Player player;
        Enemy enemy;
        Game game;
        List<Fireball> fireballs = new List<Fireball>();

        public CollisionHandler(Game game, Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
            this.game = game;

            foreach (Fireball fireball in player.Fireballs)
            {
                fireballs.Add(fireball);
            }
        }


        public  void Update(GameTime gameTime)
        {
                foreach (Fireball fireball in player.Fireballs)
                {
                    if (fireball.collisionRectangle.Intersects(enemy.Sprite.collisionRectangle))
                        game.Exit();
                }

        }
    }
}
