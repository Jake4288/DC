using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace DungeonCrawl.GameScreens
{
    public abstract class BaseGameState : GameState
    {
        #region Fields region

        protected ControlManager ControlManager;
        protected Game1 GameRef;

        protected BaseGameState TransitionTo;

        protected bool Transitioning;

        protected ChangeType ChangeType;
        protected PlayerIndex PlayerIndexInControl;

        protected TimeSpan TransitionInterval = TimeSpan.FromSeconds(0.5);
        protected TimeSpan TransitionTimer;

        #endregion

        #region Constructor Region

        protected BaseGameState(Game game, GameStateManager manager) : base(game, manager)
        {
            GameRef = (Game1) game;

            PlayerIndexInControl = PlayerIndex.One;
        }

        #endregion

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            var menuFont = content.Load<SpriteFont>(@"Fonts\ControlFont");
            ControlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Transitioning)
            {
                TransitionTimer += gameTime.ElapsedGameTime;

                if (TransitionTimer >= TransitionInterval)
                {
                    Transitioning = false;
                    switch (ChangeType)
                    {
                        case ChangeType.Change:
                            StateManager.ChangeState(TransitionTo);
                            break;
                        case ChangeType.Pop:
                            StateManager.PopState();
                            break;
                        case ChangeType.Push:
                            StateManager.PushState(TransitionTo);
                            break;
                    }
                }
            }

            base.Update(gameTime);
        }

        public virtual void Transition(ChangeType change, BaseGameState gameState)
        {
            Transitioning = true;
            ChangeType = change;
            TransitionTo = gameState;
            TransitionTimer = TimeSpan.Zero;
        }
    }
}