using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary
{
    public class InputHandler : GameComponent
    {

        #region Game Pad Field Region

        #endregion

        #region Keyboard Property Region

        public static KeyboardState KeyboardState { get; private set; }

        public static KeyboardState LastKeyboardState { get; private set; }

        #endregion

        #region Game Pad Property Region

        public static GamePadState[] GamePadStates { get; private set; }

        public static GamePadState[] LastGamePadStates { get; private set; }

        #endregion

        #region Constructor Region

        public InputHandler(Game game)
            : base(game)
        {
            KeyboardState = Keyboard.GetState();

            GamePadStates = new GamePadState[Enum.GetValues(typeof (PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof (PlayerIndex)))
                GamePadStates[(int) index] = GamePad.GetState(index);
        }

        #endregion

        #region XNA methods

        public override void Update(GameTime gameTime)
        {
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            LastGamePadStates = (GamePadState[]) GamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof (PlayerIndex)))
                GamePadStates[(int) index] = GamePad.GetState(index);

            base.Update(gameTime);
        }

        #endregion

        #region General Method Region

        public static void Flush()
        {
            LastKeyboardState = KeyboardState;
        }

        #endregion

        #region Keyboard Region

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) &&
                   LastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) &&
                   LastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        #endregion

        #region Game Pad Region

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int) index].IsButtonUp(button) &&
                   LastGamePadStates[(int) index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int) index].IsButtonDown(button) &&
                   LastGamePadStates[(int) index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int) index].IsButtonDown(button);
        }

        #endregion
    }
}