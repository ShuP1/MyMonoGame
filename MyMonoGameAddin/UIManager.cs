using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameAddin
{
	public class UIManager
	{
		public enum State { Normal, Hover, Focus, Active }
		public enum textAlign { topLeft, topCenter, topRight, centerLeft, centerCenter, centerRight, bottomLeft, bottomCenter, bottomRight };

		SpriteBatch spriteBatch;
		InputsManager inputs;
		public KeyChar.Layout layout = KeyChar.Layout.QWERTY;
		bool moved = false;

		public Vector focus;

		public Skin skin;

		public UIManager(SpriteBatch batch, InputsManager input, Skin defaultSkin)
		{
			spriteBatch = batch;
			inputs = input;
			skin = defaultSkin;
		}

		public void SetBatch(SpriteBatch batch)
		{
			spriteBatch = batch;
		}

		public void Update()
		{
			moved = false;

			if (inputs.Mouse.LeftState == ButtonState.Releasing)
			{
				focus = inputs.Mouse.Position;
			}
		}

		public void ResetFocus()
		{
			focus = null;
		}

		public State GetState(Rectangle rect)
		{
			bool contain = rect.Contains(inputs.Mouse.X, inputs.Mouse.Y);

			if (inputs.Mouse.LeftPressed)
				return contain ? State.Active : State.Normal;

			if (focus != null)
				if (rect.Contains(focus.X, focus.Y))
					return State.Focus;

			return contain ? State.Hover : State.Normal;
		}

		Vector GetLabelPos(Vector _pos, textAlign _align, SpriteFont _font, string _text)
		{
			switch (_align)
			{
				case textAlign.topLeft:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y);

				case textAlign.topCenter:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y);

				case textAlign.topRight:
					return new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y);

				case textAlign.centerLeft:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.centerCenter:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.centerRight:
					return new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.bottomLeft:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y);

				case textAlign.bottomCenter:
					return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y);

				case textAlign.bottomRight:
					return new Vector(_pos.X, _pos.Y);

				default:
					return new Vector(_pos.X, _pos.Y);
			}
		}

		Vector GetLabelPos(Rectangle _pos, textAlign _align, SpriteFont _font, string _text)
		{
			switch (_align)
			{
				case textAlign.topLeft:
					return new Vector(_pos.X, _pos.Y);

				case textAlign.topCenter:
					return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y);

				case textAlign.topRight:
					return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y);

				case textAlign.centerLeft:
					return new Vector(_pos.X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.centerCenter:
					return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.centerRight:
					return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);

				case textAlign.bottomLeft:
					return new Vector(_pos.X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);

				case textAlign.bottomCenter:
					return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);

				case textAlign.bottomRight:
					return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);

				default:
					return new Vector(_pos.X, _pos.Y);
			}
		}

		void Next(Vector next, Rectangle pos)
		{
			if (next != null)
			{
				if (focus != null)
				{
					if (!moved)
					{
						if (inputs.Keyboard.GetKeyState(Microsoft.Xna.Framework.Input.Keys.Tab) == ButtonState.Pressing)
						{
							if (GetState(pos) == State.Focus)
							{
								moved = true;
								focus = next;
							}
						}
					}
				}
			}
		}

		public void Texture(Rectangle pos, Texture2D texture, Color color, Vector next = null)
		{
			Next(next, pos);
			spriteBatch.Draw(texture, pos, color);
		}

		public void Texture(Rectangle pos, Texture2D texture, Colors color = null, Vector next = null)
		{
			if (color == null)
				color = skin.backColors;

			Texture(pos, texture, color.Get(GetState(pos)), next);
		}

		public void Box(Rectangle pos, Color backColor, BoxTexture backSprites = null, Vector next = null)
		{
			if (backSprites == null)
				backSprites = skin.texture;

			Next(next, pos);
			int leftWidth = backSprites.top_left.Width;
			int rightWidth = backSprites.top_right.Width;
			int centerWidth = pos.Width - leftWidth - rightWidth;

			int topHeight = backSprites.top_left.Height;
			int bottomHeight = backSprites.bottom_left.Height;
			int centerHeight = pos.Height - topHeight - bottomHeight;

			spriteBatch.Draw(backSprites.top_left, new Rectangle(pos.X, pos.Y, leftWidth, topHeight), backColor);
			spriteBatch.Draw(backSprites.top_middle, new Rectangle(pos.X + leftWidth, pos.Y, centerWidth, topHeight), backColor);
			spriteBatch.Draw(backSprites.top_right, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y, rightWidth, topHeight), backColor);
			spriteBatch.Draw(backSprites.middle_left, new Rectangle(pos.X, pos.Y + topHeight, leftWidth, centerHeight), backColor);
			spriteBatch.Draw(backSprites.middle_middle, new Rectangle(pos.X + leftWidth, pos.Y + topHeight, centerWidth, centerHeight), backColor);
			spriteBatch.Draw(backSprites.middle_right, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + topHeight, rightWidth, centerHeight), backColor);
			spriteBatch.Draw(backSprites.bottom_left, new Rectangle(pos.X, pos.Y + pos.Height - bottomHeight, leftWidth, bottomHeight), backColor);
			spriteBatch.Draw(backSprites.bottom_middle, new Rectangle(pos.X + leftWidth, pos.Y + pos.Height - bottomHeight, centerWidth, bottomHeight), backColor);
			spriteBatch.Draw(backSprites.bottom_right, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + pos.Height - bottomHeight, rightWidth, bottomHeight), backColor);
		}

		public void Box(Rectangle pos, Colors backColor = null, BoxTexture backSprites = null, Vector next = null)
		{
			if (backColor == null)
				backColor = skin.backColors;

			Box(pos, backColor.Get(GetState(pos)), backSprites, next);
		}

		public void Label(Rectangle pos, string text, Color color, SpriteFont font = null, textAlign align = textAlign.centerCenter, Vector next = null)
		{
			if (font == null)
				font = skin.font;

			Next(next, pos);
			Vector v = GetLabelPos(pos, align, font, text);
			spriteBatch.DrawString(font, text, new Vector2(v.X, v.Y), color);
		}

		public void Label(Rectangle pos, string text, Colors colors = null, SpriteFont font = null, textAlign align = textAlign.centerCenter, Vector next = null)
		{
			if (colors == null)
				colors = skin.foreColors;

			Label(pos, text, colors.Get(GetState(pos)), font, align, next);
		}

		public void Label(Vector vector, string text, Color color, SpriteFont font = null, textAlign align = textAlign.bottomRight, Vector next = null)
		{
			if (font == null)
				font = skin.font;

			Vector v = GetLabelPos(vector, align, font, text);

			if (next != null)
				Next(next, new Rectangle(v.X, v.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y));

			spriteBatch.DrawString(font, text, new Vector2(v.X, v.Y), color);
		}

		public void Label(Vector vector, string text, Colors colors = null, SpriteFont font = null, textAlign align = textAlign.bottomRight, Vector next = null)
		{
			if (colors == null)
				colors = skin.foreColors;

			if (font == null)
				font = skin.font;

			Vector v = GetLabelPos(vector, align, font, text);

			if (next != null)
				Next(next, new Rectangle(v.X, v.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y));

			spriteBatch.DrawString(font, text, new Vector2(v.X, v.Y), colors.Get(GetState(new Rectangle(v.X, v.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y))));
		}

		bool Button(State state)
		{
			switch (state)
			{
				case State.Active:
					return true;

				case State.Focus:
					return inputs.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.Enter);

				default:
					return false;
			}
		}

		public bool Button(Rectangle pos, Texture2D texture, Color color, Vector next = null)
		{
			Texture(pos, texture, color, next);
			return Button(GetState(pos));
		}

		public bool Button(Rectangle pos, Texture2D texture, Colors color = null, Vector next = null)
		{
			if (color == null)
				color = skin.backColors;

			return Button(pos, texture, color.Get(GetState(pos)), next);
		}

		public bool Button(Rectangle pos, Color color, BoxTexture texture = null, Vector next = null)
		{
			if (texture == null)
				texture = skin.texture;

			Box(pos, color, texture, next);
			return Button(GetState(pos));
		}

		public bool Button(Rectangle pos, Colors color = null, BoxTexture texture = null, Vector next = null)
		{
			if (color == null)
				color = skin.backColors;

			return Button(pos, color.Get(GetState(pos)), texture, next);
		}

		public bool Button(Rectangle pos, string text, Color backcolor, Color forecolor, BoxTexture texture = null, SpriteFont font = null, textAlign align = textAlign.centerCenter, Vector next = null)
		{
			Box(pos, backcolor, texture, next);
			Label(pos, text, forecolor, font, align);
			return Button(GetState(pos));
		}

		public bool Button(Rectangle pos, String text, Colors backcolor = null, Colors forecolor = null, BoxTexture texture = null, SpriteFont font = null, textAlign align = textAlign.centerCenter, Vector next = null)
		{
			if (backcolor == null)
				backcolor = skin.backColors;

			if (forecolor == null)
				forecolor = skin.foreColors;

			State state = GetState(pos);
			return Button(pos, text, backcolor.Get(state), forecolor.Get(state), texture, font, align, next);
		}

		string fieldText(string value, string placeHolder)
		{
			return (placeHolder != null && (value == null || value == "")) ? placeHolder : value;
		}

		public bool TextField(Rectangle pos, ref string value, Color color, SpriteFont font = null, textAlign align = textAlign.centerCenter, string placeHolder = null, Vector next = null)
		{
			string _text = fieldText(value, placeHolder);
			if (inputs.Keyboard.PressingKeys.Count > 0)
			{
				if (GetState(pos) == State.Focus)
				{
					if (inputs.Keyboard.PressingKeys[0] == Microsoft.Xna.Framework.Input.Keys.Back)
					{
						if (value != null)
						{
							if (value.Length > 0)
							{
								value = value.Remove(value.Length - 1);
								_text = fieldText(value, placeHolder);
							}
						}
					}
					else
					{
						Nullable<char> ch;
						if (KeyChar.KeyToChar(inputs.Keyboard.PressingKeys[0], inputs.Keyboard.IsPressed(KeyboardManager.SpecialKeys.Shift), out ch, layout, inputs.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.RightAlt)))
						{
							if (ch.HasValue)
							{
								value += ch.Value;
								_text = fieldText(value, placeHolder);
							}
						}
					}
				}
			}
			Label(pos, _text, color, font, align, next);
			return inputs.Keyboard.GetKeyState(Microsoft.Xna.Framework.Input.Keys.Enter) == ButtonState.Pressing;
		}

		public bool TextField(Rectangle pos, ref string value, Colors color = null, SpriteFont font = null, textAlign align = textAlign.centerCenter, string placeHolder = null, Vector next = null)
		{
			if (color == null)
				color = skin.foreColors;

			return TextField(pos, ref value, color.Get(GetState(pos)), font, align, placeHolder, next);
		}

		public bool TextField(Vector vector, ref string value, Color color, SpriteFont font = null, textAlign align = textAlign.centerCenter, string placeHolder = null, Vector next = null)
		{
			if (font == null)
				font = skin.font;

			string _text = fieldText(value, placeHolder);
			if (inputs.Keyboard.PressingKeys.Count > 0)
			{
				if (GetState(new Rectangle(vector.X, vector.Y, (int)font.MeasureString(_text).X, (int)font.MeasureString(_text).Y)) == State.Focus)
				{
					if (inputs.Keyboard.PressingKeys[0] == Microsoft.Xna.Framework.Input.Keys.Back)
					{
						if (value != null)
						{
							if (value.Length > 0)
							{
								value = value.Remove(value.Length - 1);
								_text = fieldText(value, placeHolder);
							}
						}
					}
					else
					{
						Nullable<char> ch;
						if (KeyChar.KeyToChar(inputs.Keyboard.PressingKeys[0], inputs.Keyboard.IsPressed(KeyboardManager.SpecialKeys.Shift), out ch, layout, inputs.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.RightAlt)))
						{
							if (ch.HasValue)
							{
								value += ch.Value;
								_text = fieldText(value, placeHolder);
							}
						}
					}
				}
			}
			Label(vector, _text, color, font, align, next);
			return inputs.Keyboard.GetKeyState(Microsoft.Xna.Framework.Input.Keys.Enter) == ButtonState.Pressing;
		}

		public bool TextField(Vector vector, ref string value, Colors color = null, SpriteFont font = null, textAlign align = textAlign.centerCenter, string placeHolder = null, Vector next = null)
		{
			if (color == null)
				color = skin.foreColors;

			if (font == null)
				font = skin.font;

			string _text = fieldText(value, placeHolder);
			return TextField(vector, ref value, color.Get(GetState(new Rectangle(vector.X, vector.Y, (int)font.MeasureString(_text).X, (int)font.MeasureString(_text).Y))), font, align, placeHolder, next);
		}
	}
}
