using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.Components
{
    public class Enemy
    {
        #region Field Region

        private readonly Character _character;
        private readonly Player _player;
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

        public Player Player
        {
            get { return _player; }
        }

        #endregion

        #region Constructor Region

        public Enemy(Game game, Character character, Player player)
        {
            _gameRef = (Game1) game;
            _character = character;
            _player = player;
        }

        #endregion

        public void LoadContent(ContentManager contentManager)
        {
            _character.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            var motion = new Vector2();
            Vector2 playerpos = _player.GetPosition();

            if (playerpos.X < Sprite.Position.X)
                motion.X = -1;
            else if (playerpos.X > Sprite.Position.X)
                motion.X = 1;

            if (playerpos.Y < Sprite.Position.Y)
                motion.Y = -1;
            else if (playerpos.Y > Sprite.Position.Y)
                motion.Y = 1;

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
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _character.Draw(gameTime, spriteBatch);
        }
    }
}