using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame.GUI
{
    public class TexturedButton : Texture
    {
        Button button;
        private int _unFocusTime;

        public TexturedButton(Rectangle pos, Texture2D sprite, Colors colors, EventHandler click = null)
        {
            _pos = pos;
            _sprite = sprite;
            _colors = colors;
            button = new Button(pos, click);
        }

        public override void Update(int x, int y, Mouse mouse, Keys key, bool isMaj, EventArgs e)
        {
            base.Update(x, y, mouse, key, isMaj, e);
            button.Update(x, y, mouse, key, isMaj, e);
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
                    _unFocusTime = 0;
                }
            }

            button.Draw(spriteBatch);
        }
    }
}