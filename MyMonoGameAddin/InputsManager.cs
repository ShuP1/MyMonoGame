using System.Collections.Generic;
using Input = Microsoft.Xna.Framework.Input;

namespace MyMonoGameAddin
{
	public enum ButtonState { Released, Pressing, Pressed, Releasing }

	public class InputsManager
	{
		public MouseManager Mouse;
		public KeyboardManager Keyboard;

		public InputsManager(bool mouse = true, bool keyboard = true)
		{
			Mouse = new MouseManager(mouse);
			Keyboard = new KeyboardManager(keyboard);

		}

		public void Update()
		{
			Mouse.Update();
			Keyboard.Update();
		}
	}

	public class KeyboardManager
	{
		public enum SpecialKeys { Control, Alt, Shift, Windows }

		public bool Enable;

		public KeyboardManager(bool enable = true) { Enable = enable; }

		public Input.KeyboardState State { get { return nowKeyboardState; } }
		Input.KeyboardState oldKeyboardState;
		Input.KeyboardState nowKeyboardState;

		public Input.Keys FirstKey { get { return nowKeyboardState.GetPressedKeys().Length > 0 ? nowKeyboardState.GetPressedKeys()[0] : Input.Keys.None; } }

		public List<Input.Keys> Keys { get { return nowKeys; } }
		List<Input.Keys> nowKeys = new List<Input.Keys>();
		List<Input.Keys> oldKeys = new List<Input.Keys>();

		public List<Input.Keys> PressingKeys { get { return _PressingKeys; } }
		List<Input.Keys> _PressingKeys = new List<Input.Keys>();

		public List<Input.Keys> ReleassingKeys { get { return _ReleassingKeys; } }
		List<Input.Keys> _ReleassingKeys = new List<Input.Keys>();

		public void Update()
		{
			if (Enable)
			{
				oldKeyboardState = nowKeyboardState;
				nowKeyboardState = Input.Keyboard.GetState();

				oldKeys = nowKeys;
				nowKeys = new List<Input.Keys>(nowKeyboardState.GetPressedKeys());

				_PressingKeys.Clear();
				foreach (Input.Keys newKey in nowKeys)
				{
					if (!oldKeys.Contains(newKey))
						_PressingKeys.Add(newKey);
				}

				_ReleassingKeys.Clear();
				foreach (Input.Keys oldKey in oldKeys)
				{
					if (!nowKeys.Contains(oldKey))
						_ReleassingKeys.Add(oldKey);
				}
			}
		}

		public bool IsPressed(Input.Keys key)
		{
			return nowKeyboardState.IsKeyDown(key);
		}

		public bool IsPressed(SpecialKeys key)
		{
			switch (key)
			{
				case SpecialKeys.Control:
					return IsPressed(Input.Keys.LeftControl) || IsPressed(Input.Keys.RightControl);

				case SpecialKeys.Alt:
					return IsPressed(Input.Keys.LeftAlt) || IsPressed(Input.Keys.RightAlt);

				case SpecialKeys.Shift:
					return IsPressed(Input.Keys.LeftShift) || IsPressed(Input.Keys.RightShift);

				case SpecialKeys.Windows:
					return IsPressed(Input.Keys.LeftWindows) || IsPressed(Input.Keys.RightWindows);

				default:
					return false;
			}
		}

		public ButtonState GetKeyState(Input.Keys key)
		{
			if (nowKeyboardState.IsKeyDown(key))
			{
				if (oldKeyboardState.IsKeyUp(key))
					return ButtonState.Pressing;

				return ButtonState.Pressed;
			}
			else
			{
				if (oldKeyboardState.IsKeyDown(key))
					return ButtonState.Releasing;

				return ButtonState.Released;
			}
		}
	}

	public class MouseManager
	{
		public bool Enable;

		public MouseManager(bool enable = true) { Enable = enable; }

		public Input.MouseState State { get { return nowMouseState; } }
		Input.MouseState oldMouseState;
		Input.MouseState nowMouseState;

		public Vector Position { get { return new Vector(nowMouseState.X, nowMouseState.Y); } }
		public int X { get { return nowMouseState.X; } }
		public int Y { get { return nowMouseState.Y; } }

		public bool LeftPressed { get { return nowMouseState.LeftButton == Input.ButtonState.Pressed; } }
		public ButtonState LeftState { get { return GetState(nowMouseState.LeftButton, oldMouseState.LeftButton); } }

		public bool RightPressed { get { return nowMouseState.RightButton == Input.ButtonState.Pressed; } }
		public ButtonState RightState { get { return GetState(nowMouseState.RightButton, oldMouseState.RightButton); } }

		public bool MiddlePressed { get { return nowMouseState.MiddleButton == Input.ButtonState.Pressed; } }
		public ButtonState MiddleState { get { return GetState(nowMouseState.MiddleButton, oldMouseState.MiddleButton); } }

		public int RelativeScroolWheel { get { return nowMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue; } }
		public int AbsoluteScroolWheel { get { return nowMouseState.ScrollWheelValue; } }

		public void Update()
		{
			if (Enable)
			{
				oldMouseState = nowMouseState;
				nowMouseState = Input.Mouse.GetState();
			}
		}

		public void SetPosition(Vector v)
		{
			SetPositon(v.X, v.Y);
		}

		public void SetPositon(int x, int y)
		{
			Input.Mouse.SetPosition(x, y);
		}

		private ButtonState GetState(Input.ButtonState now, Input.ButtonState old)
		{
			if (now == Input.ButtonState.Pressed)
			{
				if (old == Input.ButtonState.Released)
					return ButtonState.Pressing;

				return ButtonState.Pressed;
			}
			else
			{
				if (old == Input.ButtonState.Pressed)
					return ButtonState.Releasing;

				return ButtonState.Released;
			}
		}
	}
}
