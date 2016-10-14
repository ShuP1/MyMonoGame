using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGame.GUI
{
    public class Manager
    {
        private int mouseX;
        private int mouseY;
        private Keys[] newKeys;
        private MouseState newState;
        private Keys[] oldKeys;
        private Keys nowKey;
        private MouseState oldState;
        private Utilities.Mouse nowState;
        private int lastFocusX = short.MinValue;
        private int lastFocusY = short.MinValue;
        internal enum Status { Normal, Hover, Active, Focus }
        public enum textAlign { topLeft, topCenter, topRight, centerLeft, centerCenter, centerRight, bottomLeft, bottomCenter, bottomRight };
        private SpriteBatch spriteBatch;

        public void Initialise()
        {
            Utilities.KeyString.InitializeKeyString();
        }

        public void Update()
        {
            oldState = newState;
            newState = Mouse.GetState();
            mouseX = newState.X;
            mouseY = newState.Y;
            nowState.leftPress = (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed);
            nowState.leftRelease = (oldState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released);
            nowState.rightPress = (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed);
            nowState.rightRelease = (oldState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released);

            oldKeys = newKeys;
            newKeys = Keyboard.GetState().GetPressedKeys();

            nowKey = Keys.None;

            foreach (Keys newKey in newKeys)
            {
                if (!oldKeys.Contains(newKey)) { nowKey = newKey; }
            }

            if (nowState.leftPress)
            {
                lastFocusX = mouseX;
                lastFocusY = mouseY;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            spriteBatch = _spriteBatch;
        }

        private Status GetStatus(Rectangle pos)
        {
            if (nowState.leftPress)
            {
                if (pos.Contains(mouseX, mouseY))
                {
                    return Status.Active;
                }
                else
                {
                    return Status.Normal;
                }
            }
            else
            {
                if (pos.Contains(lastFocusX, lastFocusY))
                {
                    return Status.Focus;
                }
                else
                {
                    if (pos.Contains(mouseX, mouseY))
                    {
                        return Status.Hover;
                    }
                    else
                    {
                        return Status.Normal;
                    }
                }
            }
        }

        private Vector GetLabelPos(Rectangle _pos, textAlign _align, SpriteFont _font, string _text)
        {
            bool isVector = (_pos.Height == 0 && _pos.Width == 0);
            switch (_align)
            {
                case textAlign.topLeft:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        return new Vector(_pos.X, _pos.Y);
                    }

                case textAlign.topCenter:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y);
                    }

                case textAlign.topRight:
                    if (isVector)
                    {
                        return new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y);
                    }

                case textAlign.centerLeft:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        return new Vector(_pos.X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }

                case textAlign.centerCenter:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }

                case textAlign.centerRight:
                    if (isVector)
                    {
                        return new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }

                case textAlign.bottomLeft:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y);
                    }
                    else {
                        return new Vector(_pos.X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }

                case textAlign.bottomCenter:
                    if (isVector)
                    {
                        return new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }

                case textAlign.bottomRight:
                    if (isVector)
                    {
                        return new Vector(_pos.X, _pos.Y);
                    }
                    else {
                        return new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }

                default:
                    return new Vector(_pos.X, _pos.Y);
            }
        }

        private string fieldText(string value, string placeHolder)
        {
            return (placeHolder != null && (value == null || value == "")) ? placeHolder : value;
        }

        public bool TextField(Rectangle pos, ref string value, SpriteFont font, Colors colors, textAlign align = textAlign.centerCenter, string placeHolder = null)
        {
            string _text = fieldText(value, placeHolder);
            Vector v = GetLabelPos(pos, align, font, _text);
            Status status = GetStatus(pos);
            if(status == Status.Focus)
            {
                //Only QWERTY support wait monogame 4.6 (https://github.com/MonoGame/MonoGame/issues/3836)
                switch (nowKey)
                {
                    case Keys.Back:
                        if (value != null)
                        {
                            if (value.Length > 0)
                            {
                                value = value.Remove(value.Length - 1);
                                _text = fieldText(value, placeHolder);
                            }
                        }
                        break;

                    default:
                        char ch;
                        if (Utilities.KeyString.KeyToString(nowKey, newKeys.Contains(Keys.LeftShift) || newKeys.Contains(Keys.RightShift), out ch))
                        {
                            value += ch;
                            _text = fieldText(value, placeHolder);
                        }
                        break;
                }
            }
            Color backColor = colors.Get(status);
            spriteBatch.DrawString(font, _text, new Vector2(v.X, v.Y), backColor);
            return nowKey == Keys.Enter;
        }

        public bool TextField(Vector vector, ref string value, SpriteFont font, Colors colors, textAlign align = textAlign.bottomRight, string placeHolder = null)
        {
            string _text = fieldText(value, placeHolder);
            Vector v = GetLabelPos(new Rectangle(vector.X, vector.Y, 0, 0), align, font, _text);
            Status status = GetStatus(new Rectangle(v.X, v.Y, (int)font.MeasureString(_text).X, (int)font.MeasureString(_text).Y));
            if (status == Status.Focus)
            {
                //Only QWERTY support wait monogame 4.6 (https://github.com/MonoGame/MonoGame/issues/3836)
                switch (nowKey)
                {
                    case Keys.Back:
                        if (value != null)
                        {
                            if (value.Length > 0)
                            {
                                value = value.Remove(value.Length - 1);
                                _text = fieldText(value, placeHolder);
                            }
                        }
                        break;                       

                    default:
                        char ch;
                        if (Utilities.KeyString.KeyToString(nowKey, newKeys.Contains(Keys.LeftShift) || newKeys.Contains(Keys.RightShift), out ch))
                        {
                            value += ch;
                            _text = fieldText(value, placeHolder);
                        }
                        break;
                }
            }
            Color backColor = colors.Get(status);
            spriteBatch.DrawString(font, _text, new Vector2(v.X, v.Y), backColor);
            return nowKey == Keys.Enter;
        }

        public void Label(Rectangle pos, string text, SpriteFont font, Colors colors, textAlign align = textAlign.centerCenter)
        {
            Vector v = GetLabelPos(pos, align, font, text);
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            spriteBatch.DrawString(font, text, new Vector2(v.X, v.Y), backColor);
        }

        public void ResetFocus()
        {
            lastFocusX = short.MinValue;
            lastFocusY = short.MinValue;
        }

        public void Label(Vector vector, string text, SpriteFont font, Colors colors, textAlign align = textAlign.bottomRight)
        {
            Vector v = GetLabelPos(new Rectangle(vector.X, vector.Y, 0,0), align, font, text);
            Status status = GetStatus(new Rectangle(v.X, v.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y));
            Color backColor = colors.Get(status);
            spriteBatch.DrawString(font, text, new Vector2(v.X, v.Y), backColor);
        }

        public void Texture(Rectangle pos, Texture2D texture, Colors colors)
        {
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            spriteBatch.Draw(texture, pos, backColor);
        }

        public bool ButtonTexture(Rectangle pos, Texture2D texture, Colors colors)
        {
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            spriteBatch.Draw(texture, pos, backColor);
            return status == Status.Active;
        }

        public bool Button(Rectangle pos)
        {
            Status status = GetStatus(pos);
            return status == Status.Active;
        }

        public bool ButtonBoxLabel(Rectangle pos, boxSprites backSprites, Colors colors, string text, SpriteFont font, Colors textColors, textAlign align = textAlign.centerCenter)
        {
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            RenderBox(pos, backSprites, backColor);
            Label(pos, text, font, textColors, align);
            return status == Status.Active;
        }

        public bool ButtonBox(Rectangle pos, boxSprites backSprites, Colors colors)
        {
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            RenderBox(pos, backSprites, backColor);
            return status == Status.Active;
        }

        public void Box(Rectangle pos, boxSprites backSprites, Colors colors)
        {
            Status status = GetStatus(pos);
            Color backColor = colors.Get(status);
            RenderBox(pos, backSprites, backColor);
        }

        public void RenderBox(Rectangle pos, boxSprites backSprites, Color backColor)
        {
            int leftWidth = backSprites.topLeft.Width;
            int rightWidth = backSprites.topRight.Width;
            int centerWidth = pos.Width - leftWidth - rightWidth;

            int topHeight = backSprites.topLeft.Height;
            int bottomHeight = backSprites.bottomLeft.Height;
            int centerHeight = pos.Height - topHeight - bottomHeight;

            spriteBatch.Draw(backSprites.topLeft, new Rectangle(pos.X, pos.Y, leftWidth, topHeight), backColor);
            spriteBatch.Draw(backSprites.topCenter, new Rectangle(pos.X + leftWidth, pos.Y, centerWidth, topHeight), backColor);
            spriteBatch.Draw(backSprites.topRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y, rightWidth, topHeight), backColor);
            spriteBatch.Draw(backSprites.centerLeft, new Rectangle(pos.X, pos.Y + topHeight, leftWidth, centerHeight), backColor);
            spriteBatch.Draw(backSprites.centerCenter, new Rectangle(pos.X + leftWidth, pos.Y + topHeight, centerWidth, centerHeight), backColor);
            spriteBatch.Draw(backSprites.centerRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + topHeight, rightWidth, centerHeight), backColor);
            spriteBatch.Draw(backSprites.bottomLeft, new Rectangle(pos.X, pos.Y + pos.Height - bottomHeight, leftWidth, bottomHeight), backColor);
            spriteBatch.Draw(backSprites.bottomCenter, new Rectangle(pos.X + leftWidth, pos.Y + pos.Height - bottomHeight, centerWidth, bottomHeight), backColor);
            spriteBatch.Draw(backSprites.bottomRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + pos.Height - bottomHeight, rightWidth, bottomHeight), backColor);
        }
    }
}
