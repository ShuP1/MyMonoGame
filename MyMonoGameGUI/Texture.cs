using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    public class Texture : Element
    {
        protected Colors _colors;
        protected Texture2D _sprite;

        public Texture() { }

        public Texture(Rectangle pos, Texture2D sprite, Colors colors)
        {
            _pos = pos;
            _sprite = sprite;
            _colors = colors;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color backColor = _isFocus ? _colors._focus : (_isHover ? _colors._hover : _colors._normal);
            spriteBatch.Draw(_sprite, _pos, backColor);
        }
    }
}