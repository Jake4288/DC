using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary.SpriteClasses;

namespace XRpgLibrary.CharacterClasses
{
    public class Character
    {
        #region Field Region

        protected AnimatedSprite sprite;

        #endregion

        #region Property Region

        public AnimatedSprite Sprite
        {
            get { return sprite; }
        }

        #endregion

        #region Constructor Region

        public Character(AnimatedSprite sprite)
        {
            this.sprite = sprite;
        }

        #endregion

        public virtual void LoadContent(ContentManager contentManager)
        {
            sprite.LoadContent(contentManager);
        }

        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }
    }
}