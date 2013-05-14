using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.SpriteClasses
{
    public class Projectile : AnimatedSprite
    {
        private const int MaxDistance = 500;

        public bool Visible;

        protected Vector2 Direction;
        protected Vector2 StartPosition;

        public int Damage { get; set; }

        public Projectile(string asset, Dictionary<AnimationKey, Animation> animation, float speed, float scale, int damage)
            : base(asset, animation, speed, scale)
        {
            Damage = damage;
            Speed = speed;
        }

        public new void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            Scale = 0.7f;
        }

        public new void Update(GameTime gameTime)
        {
            if (Vector2.Distance(StartPosition, Position) > MaxDistance)
            {
                Visible = false;
            }

            if (Visible)
            {
                Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            StartPosition = startPosition;
            Direction = direction;
            Visible = true;
        }
    }
}