using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XRpgLibrary;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.Components
{
    public class Player
    {
        #region Field Region

        private readonly Character _character;
        private readonly List<Fireball> _fireballs = new List<Fireball>();
        private ContentManager _contentManager;
        private Game1 _gameRef;

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

        public List<Fireball> Fireballs
        {
            get { return _fireballs; }
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

        private void UpdateFireball(GameTime gameTime)
        {
            foreach (Fireball fireball in _fireballs)
            {
                fireball.Update(gameTime);
            }

            if (InputHandler.KeyPressed(Keys.Right))
            {
                ShootFireball(new Vector2(1, 0));
            }
            else if (InputHandler.KeyPressed(Keys.Left))
            {
                ShootFireball(new Vector2(-1, 0));
            }
            else if (InputHandler.KeyPressed(Keys.Up))
            {
                ShootFireball(new Vector2(0, -1));
            }
            else if (InputHandler.KeyPressed(Keys.Down))
            {
                ShootFireball(new Vector2(0, 1));
            }
        }

        private void ShootFireball(Vector2 direction)
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


            bool createNew = true;
            foreach (Fireball fireball in _fireballs)
            {
                if (fireball.Visible == false)
                {
                    createNew = false;
                    fireball.Fire(GetPosition(), 200.0f, direction);
                    break;
                }
            }

            if (createNew)
            {
                var fireball = new Fireball(@"PlayerSprites\femalefighter", animations, 800.0f, 1.0f);
                fireball.LoadContent(_contentManager);
                fireball.Fire(GetPosition(), 200.0f, new Vector2(1, 0));
                _fireballs.Add(fireball);
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            _contentManager = contentManager;

            foreach (Fireball fireball in _fireballs)
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

            UpdateFireball(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Fireball fireball in _fireballs)
            {
                fireball.Draw(gameTime, spriteBatch);
            }

            _character.Draw(gameTime, spriteBatch);
        }
    }
}