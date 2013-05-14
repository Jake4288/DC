using System.Collections.Generic;
using DungeonCrawl.Components;
using Microsoft.Xna.Framework;
using XRpgLibrary;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Field and Property Region

        private CollisionHandler _collisionHandler;
        private List<Enemy> _enemies;
        private Player _player;

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public List<Enemy> Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        #endregion

        #region Constructor Region

        public GamePlayScreen(Game game, GameStateManager manager) : base(game, manager)
        {
        }

        #endregion

        protected override void LoadContent()
        {
            var animations = new Dictionary<AnimationKey, Animation>();
            _enemies = new List<Enemy>();

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
            var enemsprite2 = new AnimatedSprite(@"PlayerSprites\femalefighter", animations, 50.0f, 2.0f);

            sprite.Position = new Vector2(300, 300);
            enemsprite2.Position = new Vector2(0, 500);

            var character = new Character(sprite, 40, 1);
            var enemcharacter1 = new Character(enemsprite, 20, 10);
            var enemcharacter2 = new Character(enemsprite2, 30, 50);

            _player = new Player(GameRef, character);
            _enemies.Add(new Enemy(GameRef, enemcharacter1, _player));
            _enemies.Add(new Enemy(GameRef, enemcharacter2, _player));

            _player.LoadContent(GameRef.Content);
            foreach (var enemy in _enemies)
                enemy.LoadContent(GameRef.Content);

            _collisionHandler = new CollisionHandler(GameRef, _player, _enemies);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            foreach (var enemy in _enemies)
                enemy.Update(gameTime);
            _collisionHandler.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            _player.Draw(gameTime, GameRef.SpriteBatch);
            foreach (var enemy in Enemies)
                enemy.Draw(gameTime, GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }
    }
}