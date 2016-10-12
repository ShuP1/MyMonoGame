using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame.GUI
{
    public class Element
    {
        protected Rectangle _pos;
        public bool _isHover;
        public bool _isFocus;

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