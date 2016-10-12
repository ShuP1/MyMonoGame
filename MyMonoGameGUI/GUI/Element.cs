using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame.GUI
{
    public class Element
    {
        protected Rectangle _pos;
        internal bool _isHover = false;
        internal bool _isFocus = false;
        internal bool isEnable = true;
        public ElementLink parent;

        public Element(bool enable = true, ElementLink parentLink = null) {
            isEnable = enable;
            parent = parentLink;
            _pos = Rectangle.Empty;
        }

        public Element(Rectangle pos, bool enable = true, ElementLink parent = null)
        {
            isEnable = enable;
            _pos = pos;
        }

        public virtual bool Contain(int x, int y)
        {
            return _pos.Contains(x, y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Update(int x, int y, Utilities.Mouse mouse, Keys key, bool isMaj, EventArgs e)
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