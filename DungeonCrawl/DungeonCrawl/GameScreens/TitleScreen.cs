using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using XRpgLibrary;
using XRpgLibrary.Controls;

namespace DungeonCrawl.GameScreens
{
    public class TitleScreen : BaseGameState
    {

        #region Field Region

        Texture2D backgroundImage;
        LinkLabel startLabel;

        #endregion

        #region Constructor Region

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }

        #endregion

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\titlescreen");
            
            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(280, 260);
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);

            ControlManager.Add(startLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef._spriteBatch.Begin();
            
            base.Draw(gameTime);

            GameRef._spriteBatch.Draw(backgroundImage, GameRef.ScreenRectangle, Color.White);

            ControlManager.Draw(GameRef._spriteBatch);

            GameRef._spriteBatch.End();
        }

        private void startLabel_Selected(object sender, EventArgs e)
        {
            Transition(ChangeType.Push, GameRef.GamePlayScreen);
        }
    }
}
