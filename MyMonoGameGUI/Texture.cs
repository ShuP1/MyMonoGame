using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    public class Texture : Element
    {
        protected Colors _colors;
        protected Texture2D _sprite;

        public Texture() { }

        public Texture(Rectangle pos, Texture2D sprite, Colors colors, bool enable = true, bool render = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _sprite = sprite;
            _colors = colors;
            isEnable = enable;
            isRender = render;
            parent = parentLink;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color backColor = _isFocus ? _colors._focus : (_isHover ? _colors._hover : _colors._normal);
            spriteBatch.Draw(_sprite, _pos, backColor);
        }
    }
}