using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl.AttackClasses
{
    public abstract class Attack
    {
        protected Components.Player Player;

        public int BaseDamage { get; set; }
        public bool Enabled { get; set; }
        public List<Vector2> CollisionPoints { get; set; }
        public AnimatedSprite Sprite { get; set; }

        protected Attack()
        {
            Enabled = true;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
