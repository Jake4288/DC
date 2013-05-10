using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.Components
{
    public class CollisionHandler
    {
        private readonly Enemy _enemy;
        private readonly List<Fireball> _fireballs = new List<Fireball>();
        private readonly Game _game;
        private readonly Player _player;

        public CollisionHandler(Game game, Player player, Enemy enemy)
        {
            _player = player;
            _enemy = enemy;
            _game = game;

            foreach (var fireball in player.Fireballs)
            {
                _fireballs.Add(fireball);
            }
        }


        public void Update(GameTime gameTime)
        {
            foreach (var fireball in _player.Fireballs)
            {
                if (fireball.CollisionRectangle.Intersects(_enemy.Sprite.CollisionRectangle))
                    _game.Exit();
            }
        }
    }
}