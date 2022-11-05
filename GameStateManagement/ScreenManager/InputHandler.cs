using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatacombsNClash.ScreenManager
{
    internal class InputHandler
    {
        #region Variables
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        #endregion

        #region Constructor
        public InputHandler()
        {
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Methods

        public bool IsNewKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) &&
                     !previousKeyboardState.IsKeyDown(key);
        }

        #endregion

        #region GetterSetter

        public KeyboardState getCurrentKeyboardState()
        {
            return this.currentKeyboardState;
        }

        public void setCurrentKeyboardState(KeyboardState currentKeyboardState)
        {
            this.currentKeyboardState = currentKeyboardState;
        }

        public KeyboardState getPreviousKeyboardState()
        {
            return this.previousKeyboardState;
        }

        public void setPreviousKeyboardState(KeyboardState previousKeyboardState)
        {
            this.previousKeyboardState = previousKeyboardState;
        }

        #endregion
    }
}
