using Microsoft.Xna.Framework.Input;
using Mous = Microsoft.Xna.Framework.Input.Mouse;

namespace MyMonoGame.Utilities
{
    /// <summary>
    /// Manager Mouse
    /// </summary>
    public class Mouse
    {
        private MouseState oldMouse;
        private MouseState nowMouse;

        public enum Clicks { Left, Right, Middle };

        public int X { get { return nowMouse.X; } }
        public int Y { get { return nowMouse.Y; } }

        public int scroll { get { return nowMouse.ScrollWheelValue; } }
        public int scrollChange { get { return (nowMouse.ScrollWheelValue - oldMouse.ScrollWheelValue); } }

        public Mouse()
        {
        }

        public void Update()
        {
            oldMouse = nowMouse;
            nowMouse = Mous.GetState();
        }

        public bool IsDown(Clicks button, bool old = false)
        {
            MouseState mouse = old ? oldMouse : nowMouse;
            switch (button)
            {
                case Clicks.Left:
                    return mouse.LeftButton == ButtonState.Pressed;

                case Clicks.Right:
                    return mouse.RightButton == ButtonState.Pressed;

                case Clicks.Middle:
                    return mouse.MiddleButton == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public bool IsPressed(Clicks button)
        {
            return IsDown(button) && !IsDown(button, true);
        }

        public bool IsReleased(Clicks button)
        {
            return !IsDown(button) && IsDown(button, true);
        }
    }
}