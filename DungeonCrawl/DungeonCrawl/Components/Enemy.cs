using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XRpgLibrary;
using XRpgLibrary.SpriteClasses;
using XRpgLibrary.CharacterClasses;

namespace DungeonCrawl.Components
{
    public class Enemy
    {
        #region Field Region

        Game1 gameRef;
        readonly Character character;
        readonly Player player;


        #endregion

        #region Property Region


        public AnimatedSprite Sprite
        {
            get { return character.Sprite; }
        }

        public Character Character
        {
            get { return character; }
        }

        public Player Player
        {
            get { return player; }
        }

        #endregion

        #region Constructor Region

        public Enemy(Game game, Character character, Player player)
        {
            gameRef = (Game1)game;
            this.character = character;
            this.player = player;
        }

        #endregion

        public void LoadContent(ContentManager contentManager)
        {
            character.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {

            Sprite.Update(gameTime);

            Vector2 motion = new Vector2();
            Vector2 playerpos = player.GetPosition();

            if (playerpos.X < Sprite.Position.X)
                motion.X = -1;
            else if (playerpos.X > Sprite.Position.X)
                motion.X = 1;

            if (playerpos.Y < Sprite.Position.Y)
                motion.Y = -1;
            else if (playerpos.Y > Sprite.Position.Y)
                motion.Y = 1;

            if (motion != Vector2.Zero)
            {
                Sprite.IsAnimating = true;
                motion.Normalize();

                Sprite.Position += motion * Sprite.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Sprite.IsAnimating = false;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            character.Draw(gameTime, spriteBatch);
        }

    }
}
