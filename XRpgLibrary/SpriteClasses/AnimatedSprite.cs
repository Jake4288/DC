using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.SpriteClasses
{
    public class AnimatedSprite
    {
        #region Field Region

        private readonly Dictionary<AnimationKey, Animation> _animations;
        private readonly string _asset;

        private float _speed;
        private Texture2D _texture;
        private Vector2 _velocity;

        #endregion

        #region Property Region

        public AnimationKey CurrentAnimation { get; set; }

        public bool IsAnimating { get; set; }

        public int Width
        {
            get { return _animations[CurrentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return _animations[CurrentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(value, 1.0f, 400.0f); }
        }

        public float Scale { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
                if (_velocity != Vector2.Zero)
                    _velocity.Normalize();
            }
        }

        public Rectangle CollisionRectangle
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, (int) (Width*Scale), (int) (Height*Scale)); }
        }

        #endregion

        #region Constructor Region

        public AnimatedSprite(string asset, Dictionary<AnimationKey, Animation> animation, float speed, float scale)
        {
            _asset = asset;
            _speed = speed;
            Scale = scale;
            _animations = new Dictionary<AnimationKey, Animation>();

            foreach (var key in animation.Keys)
                _animations.Add(key, (Animation) animation[key].Clone());
        }

        #endregion

        public void LoadContent(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_asset);
        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimating)
                _animations[CurrentAnimation].Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _animations[CurrentAnimation].CurrentFrameRect, Color.White, 0.0f,
                             Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}