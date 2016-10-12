using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyMonoGame.GUI
{
    public class TextField : Label
    {
        protected string _placeHolder;
        protected string _value;
        public string output { get { return _value; } set { _value = value; _text = (_placeHolder != null && _value == null) ? _placeHolder : _value; OnTextChange(); } }
        protected event EventHandler _validate;

        public TextField(Rectangle pos, string value, SpriteFont font, Colors colors, textAlign align = textAlign.centerCenter, string placeHolder = null, EventHandler validate = null, bool enable = true, ElementLink parentLink = null)
        {
            _value = value;
            _font = font;
            _colors = colors;
            _align = align;
            _placeHolder = placeHolder;
            _validate = validate;
            _text = (placeHolder != null && value == null) ? placeHolder : value;
            isEnable = enable;
            parent = parentLink;
            OnTextChange();
        }

        public TextField(Vector vector, string value, SpriteFont font, Colors colors, textAlign align = textAlign.bottomRight, string placeHolder = null, EventHandler validate = null, bool enable = true, ElementLink parentLink = null)
        {
            _pos = new Rectangle(vector.X, vector.Y, 0, 0);
            _value = value;
            _font = font;
            _colors = colors;
            _align = align;
            _placeHolder = placeHolder;
            _validate = validate;
            _text = (placeHolder != null && value == null) ? placeHolder : value;
            isEnable = enable;
            parent = parentLink;
            OnTextChange();
        }

        public override void Update(int x, int y, Mouse mouse, Keys key, bool isMaj, EventArgs e)
        {
            base.Update(x, y, mouse, key, isMaj, e);

            if (_isFocus)
            {
                //Only QWERTY support wait monogame 4.6 (https://github.com/MonoGame/MonoGame/issues/3836)
                switch (key)
                {
                    case Keys.Back:
                        if (_value.Length > 0) { _value = _value.Remove(_value.Length - 1); _text = (_placeHolder != null && _value == null) ? _placeHolder : _value; OnTextChange(); }
                        break;

                    case Keys.Enter:
                        Validate(this, e);
                        break;

                    default:
                        char ch;
                        if (KeyString.KeyToString(key, isMaj, out ch)) { _value += ch; _text = (_placeHolder != null && _value == null) ? _placeHolder : _value; OnTextChange(); }
                        break;
                }
            }
        }

        public void Validate(object sender, EventArgs e)
        {
            if (_validate != null)
            {
                _validate.Invoke(sender, e);
            }
        }
    }
}