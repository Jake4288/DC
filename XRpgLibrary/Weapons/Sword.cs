using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XRpgLibrary.SpriteClasses;

namespace XRpgLibrary.Weapons
{
    public class Sword
    {
        private float _swordDistance = 50f;
        private float _swordRotation = MathHelper.ToRadians(0);

        private const float SwordRotationSpeed = 720f;

        private readonly Rectangle _swordSize = new Rectangle(0, 0, 10, 10);
        private readonly Vector2 _swordOrigin = new Vector2(5, 5);

        public Vector2 PlayerPosition { get; set; }
        public AnimatedSprite Sprite { get; set; }
        public bool Swinging { get; set; }

        public Sword()
        {
            PlayerPosition = Vector2.Zero;
        }

        private Matrix GetPlayerWorldMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(PlayerPosition, 0f));
        }

        public void Update(GameTime gameTime)
        {
            var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _swordRotation += MathHelper.ToRadians(SwordRotationSpeed) * elapsed;

            Vector2 localSword = new Vector2(0, _swordDistance);
            Matrix swordMatrix = Matrix.CreateRotationZ(_swordRotation) * GetPlayerWorldMatrix();
            Vector2 swordPosition = Vector2.Transform(localSword, swordMatrix);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(gameTime, spriteBatch);
        }
    }
}
