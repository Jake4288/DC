using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.AttackClasses
{
    public class Sword : Attack
    {
        private float _swordDistance = 50f;
        private float _swordRotation = MathHelper.ToRadians(0);

        private const float SwordRotationSpeed = 720f;

        public Sword(Components.Player player)
        {
            Player = player;
            BaseDamage = 10;
            CollisionPoints.Add(new Vector2(0,10f));
            CollisionPoints.Add(new Vector2(0,50f));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _swordRotation += MathHelper.ToRadians(SwordRotationSpeed) * elapsed;

            var swordMatrix = Matrix.CreateRotationZ(_swordRotation) * GetPlayerWorldMatrix();

            var rotatedPoints = new List<Vector2>();
            foreach (var point in CollisionPoints)
            {
                rotatedPoints.Add(Vector2.Transform(point, swordMatrix));
            }
            CollisionPoints = rotatedPoints;
        }

        private Matrix GetPlayerWorldMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(Player.GetPosition(), 0f));
        }

    }
}
