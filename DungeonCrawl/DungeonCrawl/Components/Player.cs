using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XRpgLibrary;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;
using XRpgLibrary.Weapons;

namespace DungeonCrawl.Components
{
    public class Player
    {
        #region Field Region

        private readonly Character _character;
        private readonly List<Projectile> _projectiles = new List<Projectile>();
        private readonly Sword _sword;
        private ContentManager _contentManager;
        private readonly Game1 _gameRef;
        private int _projectileType = 1;

        #endregion

        #region Property Region

        public AnimatedSprite Sprite
        {
            get { return _character.Sprite; }
        }

        public Character Character
        {
            get { return _character; }
        }

        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
        }

        #endregion

        #region Constructor Region

        public Player(Game game, Character character)
        {
            _gameRef = (Game1) game;
            _character = character;
        }

        #endregion

        public Vector2 GetPosition()
        {
            return Sprite.Position;
        }

        private void UpdateProjectile(GameTime gameTime)
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Update(gameTime);
            }

            if (InputHandler.KeyPressed(Keys.D1))
                _projectileType = 1;
            else if (InputHandler.KeyPressed(Keys.D2))
                _projectileType = 2;
            if (InputHandler.KeyPressed(Keys.Right))
            {
                ShootProjectile(new Vector2(1, 0));
            }
            else if (InputHandler.KeyPressed(Keys.Left))
            {
                ShootProjectile(new Vector2(-1, 0));
            }
            else if (InputHandler.KeyPressed(Keys.Up))
            {
                ShootProjectile(new Vector2(0, -1));
            }
            else if (InputHandler.KeyPressed(Keys.Down))
            {
                ShootProjectile(new Vector2(0, 1));
            }
        }

        private void ShootProjectile(Vector2 direction)
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

            switch (_projectileType)
            {
                case 1:
                    ShootFireball(direction, animations);
                    break;
                case 2:
                    ShootMeteor(direction, animations);
                    break;
            }
        }

        private void ShootFireball(Vector2 direction, Dictionary<AnimationKey, Animation> animations)
        {
            var createNew = true;
            foreach (var projectile in _projectiles)
            {
                if (projectile.Visible == false && projectile is Fireball)
                {
                    createNew = false;
                    projectile.Fire(GetPosition(), 1000.0f, direction);
                    break;
                }
            }

            if (createNew)
            {
                var fireball = new Fireball(animations, 1.0f);
                fireball.LoadContent(_contentManager);
                fireball.Fire(GetPosition(), 1000.0f, direction);
                _projectiles.Add(fireball);
            }
        }

        private void ShootMeteor(Vector2 direction, Dictionary<AnimationKey, Animation> animations)
        {
            var createNew = true;
            foreach (var projectile in _projectiles)
            {
                if (projectile.Visible == false && projectile is Meteor)
                {
                    createNew = false;
                    projectile.Fire(GetPosition(), 1000.0f, direction);
                    break;
                }
            }

            if (createNew)
            {
                var meteor = new Meteor(animations, 1.0f);
                meteor.LoadContent(_contentManager);
                meteor.Fire(GetPosition(), 1000.0f, direction);
                _projectiles.Add(meteor);
            }
        }

        private void LockSprite()
        {
            if (Sprite.Position.X < 0)
            {
                Vector2 position = Sprite.Position;
                position.X = 0;
                Sprite.Position = position;
            }
            if (Sprite.Position.X + Sprite.Width > _gameRef.ScreenWidth)
            {
                Vector2 position = Sprite.Position;
                position.X = _gameRef.ScreenWidth - Sprite.Width;
                Sprite.Position = position;
            }
            if (Sprite.Position.Y < 0)
            {
                Vector2 position = Sprite.Position;
                position.Y = 0;
                Sprite.Position = position;
            }
            if (Sprite.Position.Y + Sprite.Height > _gameRef.ScreenHeight)
            {
                Vector2 position = Sprite.Position;
                position.Y = _gameRef.ScreenHeight - Sprite.Height;
                Sprite.Position = position;
            }

        }

        public void LoadContent(ContentManager contentManager)
        {
            _contentManager = contentManager;

            foreach (Fireball fireball in _projectiles)
            {
                fireball.LoadContent(contentManager);
            }

            _character.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            var motion = new Vector2();

            if (InputHandler.KeyDown(Keys.W))
            {
                Sprite.CurrentAnimation = AnimationKey.Up;
                motion.Y = -1;
            }
            else if (InputHandler.KeyDown(Keys.S) ||
                     InputHandler.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.One))
            {
                Sprite.CurrentAnimation = AnimationKey.Down;
                motion.Y = 1;
            }
            if (InputHandler.KeyDown(Keys.A))
            {
                Sprite.CurrentAnimation = AnimationKey.Left;
                motion.X = -1;
            }
            else if (InputHandler.KeyDown(Keys.D))
            {
                Sprite.CurrentAnimation = AnimationKey.Right;
                motion.X = 1;
            }

            if (motion != Vector2.Zero)
            {
                Sprite.IsAnimating = true;
                motion.Normalize();

                Sprite.Position += motion*Sprite.Speed*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Sprite.IsAnimating = false;
            }
            LockSprite();

            UpdateProjectile(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Projectile projectile in _projectiles)
            {
                projectile.Draw(gameTime, spriteBatch);
            }

            _character.Draw(gameTime, spriteBatch);
        }
    }
}