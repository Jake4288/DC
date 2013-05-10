using System;
using Microsoft.Xna.Framework;

namespace XRpgLibrary.SpriteClasses
{
    public enum AnimationKey
    {
        Down,
        Left,
        Right,
        Up
    }

    public class Animation : ICloneable
    {
        #region Field Region

        private readonly Rectangle[] _frames;
        private int _currentFrame;
        private TimeSpan _frameLength;
        private TimeSpan _frameTimer;
        private int _framesPerSecond;

        #endregion

        #region Property Region

        public int FramesPerSecond
        {
            get { return _framesPerSecond; }
            set
            {
                if (value < 1)
                    _framesPerSecond = 1;
                else if (value > 60)
                    _framesPerSecond = 60;
                else
                    _framesPerSecond = value;
                _frameLength = TimeSpan.FromSeconds(1/(double) _framesPerSecond);
            }
        }

        public Rectangle CurrentFrameRect
        {
            get { return _frames[_currentFrame]; }
        }

        public int CurrentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = (int) MathHelper.Clamp(value, 0, _frames.Length - 1); }
        }

        public int FrameWidth { get; private set; }

        public int FrameHeight { get; private set; }

        #endregion

        #region Constructor Region

        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset)
        {
            _frames = new Rectangle[frameCount];
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;

            for (int i = 0; i < frameCount; i++)
            {
                _frames[i] = new Rectangle(xOffset + (frameWidth*i), yOffset, frameWidth, frameHeight);
            }
            FramesPerSecond = 15;
            Reset();
        }

        private Animation(Animation animation)
        {
            _frames = animation._frames;
            FramesPerSecond = 15;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            var animationClone = new Animation(this);

            animationClone.FrameWidth = FrameWidth;
            animationClone.FrameHeight = FrameHeight;
            animationClone.Reset();

            return animationClone;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            _frameTimer += gameTime.ElapsedGameTime;

            if (_frameTimer >= _frameLength)
            {
                _frameTimer = TimeSpan.Zero;
                _currentFrame = (_currentFrame + 1) % _frames.Length;
            }
        }

        public void Reset()
        {
            _currentFrame = 0;
            _frameTimer = TimeSpan.Zero;
        }
    }
}