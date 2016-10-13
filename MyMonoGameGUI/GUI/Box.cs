using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    public class Box : Element
    {
        protected boxSprites _backSprites;
        protected Colors _colors;

        public Box() { }

        public Box(Rectangle pos, boxSprites backSprites, Colors colors, bool enable = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _backSprites = backSprites;
            _colors = colors;
            isEnable = enable;
            parent = parentLink;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector relative)
        {
            Color backColor = _isFocus ? _colors._focus : (_isHover ? _colors._hover : _colors._normal);

            Rectangle pos = new Rectangle(_pos.X + relative.X, _pos.Y + relative.Y, _pos.Width, _pos.Height);

            int leftWidth = _backSprites.topLeft.Width;
            int rightWidth = _backSprites.topRight.Width;
            int centerWidth = pos.Width - leftWidth - rightWidth;

            int topHeight = _backSprites.topLeft.Height;
            int bottomHeight = _backSprites.bottomLeft.Height;
            int centerHeight = pos.Height - topHeight - bottomHeight;

            spriteBatch.Draw(_backSprites.topLeft, new Rectangle(pos.X, pos.Y, leftWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.topCenter, new Rectangle(pos.X + leftWidth, pos.Y, centerWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.topRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y, rightWidth, topHeight), backColor);
            spriteBatch.Draw(_backSprites.centerLeft, new Rectangle(pos.X, pos.Y + topHeight, leftWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.centerCenter, new Rectangle(pos.X + leftWidth, pos.Y + topHeight, centerWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.centerRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + topHeight, rightWidth, centerHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomLeft, new Rectangle(pos.X, pos.Y + pos.Height - bottomHeight, leftWidth, bottomHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomCenter, new Rectangle(pos.X + leftWidth, pos.Y + pos.Height - bottomHeight, centerWidth, bottomHeight), backColor);
            spriteBatch.Draw(_backSprites.bottomRight, new Rectangle(pos.X + pos.Width - rightWidth, pos.Y + pos.Height - bottomHeight, rightWidth, bottomHeight), backColor);
        }
    }
}