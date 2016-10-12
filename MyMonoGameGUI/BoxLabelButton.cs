using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame.GUI
{
    public class BoxLabelButton : BoxLabel
    {
        protected Button _button;
        private int _unFocusTime;

        public BoxLabelButton(Rectangle pos, boxSprites backSprites, Colors colors, string text, SpriteFont font, Colors textColors, Label.textAlign align = Label.textAlign.centerCenter, EventHandler click = null, bool enable = true, ElementLink parentLink = null)
        {
            _pos = pos;
            _backSprites = backSprites;
            _colors = colors;
            _button = new Button(pos, click);
            _label = new Label(pos, text, font, textColors, align);
            isEnable = enable;
            parent = parentLink;
        }

        public override void Update(int x, int y, Mouse mouse, Keys key, bool isMaj, EventArgs e)
        {
            base.Update(x, y, mouse, key, isMaj, e);
            _button.Update(x, y, mouse, key, isMaj, e);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (_isFocus)
            {
                if (_unFocusTime < 10)
                {
                    _unFocusTime++;
                }
                else {
                    _isFocus = false;
                    _label._isFocus = false;
                    _unFocusTime = 0;
                }
            }

            _button.Draw(spriteBatch);
        }
    }
}