using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGame.GUI
{
    public class Manager
    {
        private Dictionary<ElementLink ,Element> elements = new Dictionary<ElementLink, Element>();
        private int mouseX;
        private int mouseY;
        private Keys[] newKeys;
        private MouseState newState;
        private Keys[] oldKeys;
        private MouseState oldState;
        private long index = 0;

        public void Initialise()
        {
            Utilities.KeyString.InitializeKeyString();
        }

        public void Update()
        {
            oldState = newState;
            newState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            mouseX = newState.X;
            mouseY = newState.Y;
            Utilities.Mouse nowState;
            nowState.leftPress = (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed);
            nowState.leftRelease = (oldState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released);
            nowState.rightPress = (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed);
            nowState.rightRelease = (oldState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released);

            oldKeys = newKeys;
            newKeys = Keyboard.GetState().GetPressedKeys();

            Keys key = Keys.None;

            foreach (Keys newKey in newKeys)
            {
                if (!oldKeys.Contains(newKey)) { key = newKey; }
            }

            EventArgs e = new EventArgs();
            foreach (Element element in elements.Values.ToArray())
            {
                bool isEnable = element.isEnable;
                if (isEnable)
                {
                    ElementLink parent = element.parent;
                    if (parent != null) { isEnable = Get(parent).isEnable; }
                }

                if (isEnable) {
                    element.Update(mouseX, mouseY, nowState, key, Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift), e);
                }
            }
        }

        public void Clear()
        {
            elements.Clear();
        }

        public ElementLink Add(Element element)
        {
            ElementLink link = new ElementLink(index);
            index++;
            elements.Add(link, element);
            return link;
        }

        public void Remove(ElementLink link)
        {
            if (elements.ContainsKey(link))
            {
                elements.Remove(link);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void SetEnable(ElementLink link, bool enable)
        {
            if (elements.ContainsKey(link))
            {
                elements[link].isEnable = enable;
            }
            else
            {
                Console.WriteLine("SetEnable Null");
                throw new NullReferenceException();
            }
        }

        public Element Get(ElementLink link)
        {
            if (elements.ContainsKey(link))
            {
                return elements[link];
            }
            else
            {
                Console.WriteLine("GetElement Null");
                throw new NullReferenceException();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Element element in elements.Values.ToArray())
            {
                bool isEnable = element.isEnable;
                if (isEnable)
                {
                    ElementLink parent = element.parent;
                    if (parent != null) { isEnable = Get(parent).isEnable; }
                }

                if (isEnable)
                {
                    element.Draw(spriteBatch);
                }
            }
        }
    }
}
