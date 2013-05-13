using System;
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

        private Texture2D _backgroundImage;
        private LinkLabel _startLabel;

        #endregion

        #region Constructor Region

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion

        protected override void LoadContent()
        {
            ContentManager content = GameRef.Content;

            _backgroundImage = content.Load<Texture2D>(@"Backgrounds\titlescreen");

            base.LoadContent();

            _startLabel = new LinkLabel
                              {
                                  Position = new Vector2(280, 260),
                                  Text = "Press ENTER to begin",
                                  Color = Color.White,
                                  TabStop = true,
                                  HasFocus = true
                              };
            _startLabel.Selected += StartLabelSelected;

            ControlManager.Add(_startLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(_backgroundImage, GameRef.ScreenRectangle, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void StartLabelSelected(object sender, EventArgs e)
        {
            Transition(ChangeType.Push, GameRef.GamePlayScreen);
        }
    }
}