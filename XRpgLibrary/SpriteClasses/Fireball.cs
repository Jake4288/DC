using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace XRpgLibrary.SpriteClasses
{
    public class Fireball : AnimatedSprite
    {
        const int MAX_DISTANCE = 500;

        public bool Visible = false;

        Vector2 startPosition;
        Vector2 direction;

        public Fireball(string asset, Dictionary<AnimationKey, Animation> animation, float speed, float scale)
            : base(asset, animation, speed, scale)
        { }

        public void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            Scale = 0.7f;
        }

        public void Update(GameTime gameTime)
        {
            if (Vector2.Distance(startPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }

            if (Visible == true)
            {
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible == true)
            {
                base.Draw(gameTime, spriteBatch);
            }
        }

        public void Fire(Vector2 startPosition, float speed, Vector2 direction)
        {
            Position = startPosition;
            this.startPosition = startPosition;
            Speed = speed;
            this.direction = direction;
            Visible = true;
        }

    }
}
