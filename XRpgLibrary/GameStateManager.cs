using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace XRpgLibrary
{
    public enum ChangeType { Change, Pop, Push }

    public class GameStateManager : GameComponent
    {
        public event EventHandler OnStateChange;

        #region Fields and Properties Region

        Stack<GameState> _gameStates = new Stack<GameState>();

        const int StartDrawOrder = 5000;
        const int DrawOrderInc = 100;
        int _drawOrder;

        public GameState CurrentState
        {
            get { return _gameStates.Peek(); }
        }
                
        #endregion

        #region Constructor Region

        public GameStateManager(Game game) : base(game)
        {
            _drawOrder = StartDrawOrder;
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void PopState()
        {
            if (_gameStates.Count > 0)
            {
                RemoveState();
                _drawOrder -= DrawOrderInc;

                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
        }

        private void RemoveState()
        {
            GameState state = _gameStates.Peek();

            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            _drawOrder += DrawOrderInc;
            newState.DrawOrder = _drawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);

        }

        private void AddState(GameState newState)
        {
            _gameStates.Push(newState);

            Game.Components.Add(newState);

            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (_gameStates.Count > 0)
                RemoveState();

            newState.DrawOrder = StartDrawOrder;
            _drawOrder = StartDrawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);

        }
    }
}
