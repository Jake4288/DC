using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.SpriteClasses
{
    public class AnimatedSprite
    {
        #region Field Region

        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;

        string asset;
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float speed;
        float scale;

        #endregion

        #region Property Region

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public int Width
        {
            get { return animations[currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return animations[currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 400.0f); }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
            }
        }

        public Rectangle collisionRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)(Width * scale), (int)(Height * scale));  }
        }
        #endregion

        #region Constructor Region

        public AnimatedSprite(string asset, Dictionary<AnimationKey, Animation> animation, float speed, float scale)
        {
            this.asset = asset;
            this.speed = speed;
            this.scale = scale;
            animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
        }

        #endregion

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(asset);
        }

        public void Update(GameTime gameTime)
        {
            if (isAnimating)
                animations[currentAnimation].Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, animations[currentAnimation].CurrentFrameRect, Color.White,0.0f,Vector2.Zero,scale,SpriteEffects.None,0);
        }
    }
}
