using System.Collections.Generic;

namespace XRpgLibrary.SpriteClasses
{
    public class Meteor : Projectile
    {
        public Meteor(Dictionary<AnimationKey, Animation> animation, float scale)
            : base(@"PlayerSprites\femalewizard", animation, 100.0f, scale, 10)
        {
        }

    }
}