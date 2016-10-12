using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    public class Box : Element
    {
        protected boxSprites _backSprites;
        protected Colors _colors;

        public Box() { }

        public Box(Rectangle pos, boxSprites backSprites, Colors colors, bool enable = true, bool render = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _backSprites = backSprites;
            _colors = colors;
            isEnable = enable;
            isRender = render;
            parent = parentLink;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color backColor = _isFocus ? _colors._focus : (_isHover ? _colors._hover : _colors._normal);

            int leftWidth = _backSprites.topLeft.Width;
            int rightWidth = _backSprites.topRight.Width;
            int centerWidth = _pos.Width - leftWidth - rightWidth;

            int topHeight = _backSprites.topLeft.Height;
            int bottomHeight = _backSprites.bottomLeft.Height;
            int centerHeight = _pos.Height - topHeight - bottomHeight;

            spriteBatch.Draw(_backSprites.topLeft, new Rectangle(_pos.X, _pos.Y, leftWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.topCenter, new Rectangle(_pos.X + leftWidth, _pos.Y, centerWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.topRight, new Rectangle(_pos.X + _pos.Width - rightWidth, _pos.Y, rightWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.centerLeft, new Rectangle(_pos.X, _pos.Y + topHeight, leftWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.centerCenter, new Rectangle(_pos.X + leftWidth, _pos.Y + topHeight, centerWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.centerRight, new Rectangle(_pos.X + _pos.Width - rightWidth, _pos.Y + topHeight, rightWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomLeft, new Rectangle(_pos.X, _pos.Y + _pos.Height - bottomHeight, leftWidth, bottomHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomCenter, new Rectangle(_pos.X + leftWidth, _pos.Y + _pos.Height - bottomHeight, centerWidth, bottomHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomRight, new Rectangle(_pos.X + _pos.Width - rightWidth, _pos.Y + _pos.Height - bottomHeight, rightWidth, bottomHeight), backColor);
        }
    }
}