using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonCrawl.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XRpgLibrary;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Field and Property Region

        Player _player;
        Enemy _enemy;
        CollisionHandler _collisionHandler;

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Enemy Enemy
        {
            get { return _enemy; }
            set { _enemy = value; }
        }

        #endregion

        #region Constructor Region

        public GamePlayScreen(Game game, GameStateManager manager) : base(game, manager)
        {
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var animations = new Dictionary<AnimationKey, Animation>();

            var animation = new Animation(3, 32, 32, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            animations.Add(AnimationKey.Up, animation);

            var sprite = new AnimatedSprite(@"PlayerSprites\malepriest", animations, 200.0f, 1.5f);
            var enemsprite = new AnimatedSprite(@"PlayerSprites\femalepriest", animations, 100.0f, 2.0f);
            sprite.Position = new Vector2(300, 300);

            var character = new Character(sprite);
            var enemcharacter = new Character(enemsprite);

            _player = new Player(GameRef, character);
            _enemy = new Enemy(GameRef, enemcharacter, _player);

            _player.LoadContent(GameRef.Content);
            _enemy.LoadContent(GameRef.Content);

            _collisionHandler = new CollisionHandler(GameRef, _player, _enemy);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _enemy.Update(gameTime);
            _collisionHandler.Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef._spriteBatch.Begin();
            
            base.Draw(gameTime);

            _player.Draw(gameTime, GameRef._spriteBatch);
            _enemy.Draw(gameTime, GameRef._spriteBatch);

            GameRef._spriteBatch.End();
        }
    }
}
