using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    public class Label : Element
    {
        public enum textAlign { topLeft, topCenter, topRight, centerLeft, centerCenter, centerRight, bottomLeft, bottomCenter, bottomRight };
        protected textAlign _align;
        protected string _text;
        protected SpriteFont _font;
        protected Colors _colors;
        protected Vector _vector;

        public Label() { }

        public Label(Rectangle pos, string text, SpriteFont font, Colors colors, textAlign align = textAlign.centerCenter, bool enable = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _text = text;
            _font = font;
            _colors = colors;
            _align = align;
            isEnable = enable;
            parent = parentLink;
            OnTextChange();
        }

        public Label(Vector vector, string text, SpriteFont font, Colors colors, textAlign align = textAlign.bottomRight, bool enable = true, ElementLink parentLink = null)
        {
            _pos = new Rectangle(vector.X, vector.Y, 0, 0);
            _text = text;
            _font = font;
            _colors = colors;
            _align = align;
            isEnable = enable;
            parent = parentLink;
            OnTextChange();
        }

        public override bool Contain(int x, int y)
        {
            bool isVector = (_pos.Height == 0 && _pos.Width == 0);
            if (isVector)
            {
                return new Rectangle(_vector.X, _vector.Y, (int)_font.MeasureString(_text).X, (int)_font.MeasureString(_text).Y).Contains(x, y);
            }
            else
            {
                return base.Contain(x, y);
            }
        }

        protected void OnTextChange()
        {
            bool isVector = (_pos.Height == 0 && _pos.Width == 0);
            switch (_align)
            {
                case textAlign.topLeft:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        _vector = new Vector(_pos.X, _pos.Y);
                    }
                    break;

                case textAlign.topCenter:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y);
                    }
                    break;

                case textAlign.topRight:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y);
                    }
                    break;

                case textAlign.centerLeft:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        _vector = new Vector(_pos.X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }
                    break;

                case textAlign.centerCenter:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }
                    break;

                case textAlign.centerRight:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X, _pos.Y - (int)_font.MeasureString(_text).Y / 2);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height / 2 - (int)_font.MeasureString(_text).Y / 2);
                    }
                    break;

                case textAlign.bottomLeft:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X, _pos.Y);
                    }
                    else {
                        _vector = new Vector(_pos.X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }
                    break;

                case textAlign.bottomCenter:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X - (int)_font.MeasureString(_text).X / 2, _pos.Y);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width / 2 - (int)_font.MeasureString(_text).X / 2, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }
                    break;

                case textAlign.bottomRight:
                    if (isVector)
                    {
                        _vector = new Vector(_pos.X, _pos.Y);
                    }
                    else {
                        _vector = new Vector(_pos.X + _pos.Width - (int)_font.MeasureString(_text).X, _pos.Y + _pos.Height - (int)_font.MeasureString(_text).Y);
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = _isFocus ? _colors._focus : (_isHover ? _colors._hover : _colors._normal);
            spriteBatch.DrawString(_font, _text, new Vector2(_vector.X, _vector.Y), color);
        }
    }
}