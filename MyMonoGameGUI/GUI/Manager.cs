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
            newState = Mouse.GetState();
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
            foreach (ElementLink elementLink in elements.Keys.ToArray())
            {
                if (IsEnable(elementLink)) {
                    elements[elementLink].Update(mouseX, mouseY, nowState, key, Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift), e);
                }
            }
        }

        public bool IsEnable(ElementLink elementLink)
        {
            Vector temp;
            return IsEnable(elementLink ,out temp);
        }

        private bool IsEnable(ElementLink elementLink, out Vector relative)
        {
            bool isEnable = elements[elementLink].isEnable;
            relative = new Vector();
            if (isEnable)
            {
                ElementLink actualLink = elementLink;
                while (isEnable)
                {
                    ElementLink parentLink = elements[actualLink].parent;
                    if (parentLink != null)
                    {
                        Element parent = Get(parentLink);
                        if (parent != null)
                        {
                            isEnable = parent.isEnable;
                            if (isEnable)
                            {
                                relative.Add(parent.Pos());
                                actualLink = parentLink;
                            }
                        }
                        else
                        {
                            isEnable = false;
                            elements.Remove(actualLink);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return isEnable;
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
                return null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ElementLink elementLink in elements.Keys.ToArray())
            {
                Vector relative;
                if (IsEnable(elementLink, out relative))
                {
                    elements[elementLink].Draw(spriteBatch, relative);
                }
            }
        }
    }
}
