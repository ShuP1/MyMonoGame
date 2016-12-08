using Microsoft.Xna.Framework.Input;
using System.Linq;
using Keyboar = Microsoft.Xna.Framework.Input.Keyboard;

namespace MyMonoGame.Utilities
{
    /// <summary>
    /// Manager Keyboard
    /// </summary>
    public class Keyboard
    {
        private KeyboardState oldKeyboard;
        private KeyboardState nowKeyboard;

        private Keys _key;

        /// <summary>
        /// Last key pressed
        /// </summary>
        public Keys key { get { return _key; } }

        public Keyboard()
        {
        }

        public void Update()
        {
            oldKeyboard = nowKeyboard;
            nowKeyboard = Keyboar.GetState();

            _key = Keys.None;
            foreach (Keys k in nowKeyboard.GetPressedKeys())
            {
                if (!oldKeyboard.GetPressedKeys().Contains(k)) { _key = k; }
            }
        }

        public bool IsDown(Keys key)
        {
            return nowKeyboard.IsKeyDown(key);
        }

        public bool IsPressed(Keys key)
        {
            return nowKeyboard.IsKeyDown(key) && oldKeyboard.IsKeyUp(key);
        }

        public bool IsReleased(Keys key)
        {
            return nowKeyboard.IsKeyUp(key) && oldKeyboard.IsKeyDown(key);
        }
    }
}