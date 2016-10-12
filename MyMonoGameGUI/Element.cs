using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame.GUI
{
    public class Element
    {
        protected Rectangle _pos;
        public bool _isHover = false;
        public bool _isFocus = false;
        public bool isEnable = true;
        public bool isRender = true;
        public ElementLink parent;

        public Element(bool enable = true, bool render = true, ElementLink parentLink = null) {
            isEnable = enable;
            isRender = render;
            parent = parentLink;
            _pos = Rectangle.Empty;
        }

        public Element(Rectangle pos, bool enable = true, bool render = true, ElementLink parent = null)
        {
            isEnable = enable;
            isRender = render;
            _pos = pos;
        }

        public virtual bool Contain(int x, int y)
        {
            return _pos.Contains(x, y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Update(int x, int y, Mouse mouse, Keys key, bool isMaj, EventArgs e)
        {
            if (mouse.leftPress)
            {
                if (Contain(x, y))
                {
                    _isFocus = true;
                    Click(this, e);
                }
                else { _isFocus = false; }
            }
            else { _isHover = Contain(x, y); }
        }

        public virtual void Click(object sender, EventArgs e)
        {

        }
    }
}