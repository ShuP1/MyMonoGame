using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyMonoGame.GUI
{
    public class Button : Element
    {
        protected event EventHandler _click;
        protected int _unFocusTime = 0;

        public Button() { }

        public Button(Rectangle pos, EventHandler click = null, bool enable = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _click = click;
            isEnable = enable;
            parent = parentLink;
        }

        public override void Click(object sender, EventArgs e)
        {
            if (_click != null)
            {
                _click.Invoke(sender, e);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector relative)
        {
            if (_isFocus)
            {
                if (_unFocusTime < 10)
                {
                    _unFocusTime++;
                }
                else {
                    _isFocus = false;
                    _unFocusTime = 0;
                }
            }
        }
    }
}