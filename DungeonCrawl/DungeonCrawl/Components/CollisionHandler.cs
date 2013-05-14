using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.Components
{
    public class CollisionHandler
    {
        private readonly List<Enemy> _enemies;
        private readonly List<Projectile> _projectiles = new List<Projectile>();
        private readonly Game _game;
        private readonly Player _player;

        private bool _immune;
        private readonly TimeSpan _immuneWindow = TimeSpan.FromSeconds(2);
        private TimeSpan _immuneTimer;

        public CollisionHandler(Game game, Player player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
            _game = game;

            foreach (var projectile in player.Projectiles)
            {
                _projectiles.Add(projectile);
            }
        }


        public void Update(GameTime gameTime)
        {
            if (_immune)
            {
                _immuneTimer += gameTime.ElapsedGameTime;
                if (_immuneTimer >= _immuneWindow)
                {
                    _immune = false;
                    _immuneTimer = TimeSpan.Zero;
                }
            }
            HandleProjectileCollision();
            HandlePlayerCollision();
            if (_enemies.Count == 0)
                _game.Exit();
            if (_player.Character.Hp <= 0)
                _game.Exit();
        }

        private void HandlePlayerCollision()
        {
            if (_immune)
                return;
            foreach (var enemy in _enemies)
            {
                if (enemy.Sprite.CollisionRectangle.Intersects(_player.Sprite.CollisionRectangle))
                {
                    _player.Character.ReduceHp(enemy.Character.Damage);
                    _immune = true;
                    
                }
            }
        }

        private void HandleProjectileCollision()
        {
            var listOfFireballsToRemove = new List<Projectile>();
            var listOfEnemiesToRemove = new List<Enemy>();

            foreach (var projectile in _player.Projectiles)
            {
                foreach (var enemy in _enemies)
                {
                    if (projectile.CollisionRectangle.Intersects(enemy.Sprite.CollisionRectangle))
                    {
                        enemy.Character.ReduceHp(projectile.Damage);
                        listOfFireballsToRemove.Add(projectile);
                        if (enemy.Character.Hp <= 0)
                        {
                            listOfEnemiesToRemove.Add(enemy);
                        }
                    }
                }
            }
            foreach (var fireball in listOfFireballsToRemove)
                _player.Projectiles.Remove(fireball);
            foreach (var enemy in listOfEnemiesToRemove)
                _enemies.Remove(enemy);
        }
    }
}