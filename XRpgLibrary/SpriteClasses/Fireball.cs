using System.Collections.Generic;

namespace XRpgLibrary.SpriteClasses
{
    public class Fireball : Projectile
    {
        public Fireball(Dictionary<AnimationKey, Animation> animation, float scale)
            : base(@"PlayerSprites\femalefighter", animation, 800.0f, scale, 2)
        {
        }

    }
}