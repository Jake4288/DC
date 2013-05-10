using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.SpriteClasses
{
    public class Fireball : AnimatedSprite
    {
        private const int MaxDistance = 500;

        public bool Visible;

        private Vector2 _direction;
        private Vector2 _startPosition;

        public Fireball(string asset, Dictionary<AnimationKey, Animation> animation, float speed, float scale)
            : base(asset, animation, speed, scale)
        {
        }

        public new void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            Scale = 0.7f;
        }

        public new void Update(GameTime gameTime)
        {
            if (Vector2.Distance(_startPosition, Position) > MaxDistance)
            {
                Visible = false;
            }

            if (Visible)
            {
                Position += _direction*Speed*(float) gameTime.ElapsedGameTime.TotalSeconds;
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                base.Draw(gameTime, spriteBatch);
            }
        }

        public void Fire(Vector2 startPosition, float speed, Vector2 direction)
        {
            Position = startPosition;
            _startPosition = startPosition;
            Speed = speed;
            _direction = direction;
            Visible = true;
        }
    }
}