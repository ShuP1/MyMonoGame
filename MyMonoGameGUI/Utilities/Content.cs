using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.GUI;
using System.Collections.Generic;

namespace MyMonoGame.Utilities
{
    /// <summary>
    /// Load and Store Contents
    /// </summary>
    /// <remarks>Content format must be:
    /// Content
    /// \ Fonts
    /// \ Sounds
    /// \ Textures
    ///
    /// Or use fullpath
    /// </remarks>
    public class Content
    {
        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SoundEffect> sounds;
        private Dictionary<string, boxSprites> boxs;
        private Dictionary<string, SpriteFont> fonts;

        private const string errorName = "error";

        private ContentManager content;

        public static Texture2D nullSprite;
        public static boxSprites nullBox;

        public Content()
        {
            textures = new Dictionary<string, Texture2D>();
            sounds = new Dictionary<string, SoundEffect>();
            boxs = new Dictionary<string, boxSprites>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        public void Initialise(ContentManager Content, GraphicsDevice graphicsDevice, string errorPath = null)
        {
            nullSprite = new Texture2D(graphicsDevice, 1, 1);
            nullSprite.SetData(new Color[1 * 1] { Color.White });

            nullBox = new boxSprites(nullSprite, nullSprite, nullSprite, nullSprite, nullSprite, nullSprite, nullSprite, nullSprite, nullSprite);

            content = Content;

            if (errorPath != null)
            {
                AddTexture(errorName, errorPath);
                Texture2D errorT = GetTexture(errorName);
                AddBox(errorName, new boxSprites(errorT, errorT, errorT, errorT, errorT, errorT, errorT, errorT, errorT));
            }
        }

        //Textures
        public void AddTexture(string key, string path = null, bool fullPath = false)
        {
            if (path == null)
                path = key;

            AddTexture(key, content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : ".png")));
        }

        public void AddTexture(string key, Texture2D texture)
        {
            if (textures.ContainsKey(key))
                throw new System.Exception("Allready in dictonary");

            textures.Add(key, texture);
        }

        public Texture2D GetTexture(string key)
        {
            if (!textures.ContainsKey(key))
            {
                if (key == errorName)
                    return nullSprite;

                return GetTexture(errorName);
            }
            else
            {
                return textures[key];
            }
        }

        //Boxs
        public void AddBox(string key, string path = null, bool fullPath = false)
        {
            if (path == null)
                path = key;

            AddBox(key, new boxSprites(
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/topLeft.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/topCenter.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/topRight.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/centerLeft.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/centerCenter.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/centerRight.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/bottomLeft.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/bottomCenter.png")),
                content.Load<Texture2D>((fullPath ? "" : "Textures/") + path + (fullPath ? "" : "/bottomRight.png"))
                ));
        }

        public void AddBox(string key, boxSprites box)
        {
            if (boxs.ContainsKey(key))
                throw new System.Exception("Allready in dictonary");

            boxs.Add(key, box);
        }

        public boxSprites GetBox(string key)
        {
            if (!boxs.ContainsKey(key))
            {
                if (key == errorName)
                    return nullBox;

                return GetBox(errorName);
            }
            else
            {
                return boxs[key];
            }
        }

        //Sounds
        public void AddSound(string key, string path = null, bool fullPath = false)
        {
            if (path == null)
                path = key;

            AddSound(key, content.Load<SoundEffect>((fullPath ? "" : "Sounds/") + path + (fullPath ? "" : ".mp3")));
        }

        public void AddSound(string key, SoundEffect sound)
        {
            if (textures.ContainsKey(key))
                throw new System.Exception("Allready in dictonary");

            sounds.Add(key, sound);
        }

        public SoundEffect GetSound(string key)
        {
            if (!sounds.ContainsKey(key))
            {
                if (key == errorName)
                    return null;

                return GetSound(errorName);
            }
            else
            {
                return sounds[key];
            }
        }

        //Fonts
        public void AddFont(string key, string path = null, bool fullPath = false)
        {
            if (path == null)
                path = key;

            AddFont(key, content.Load<SpriteFont>((fullPath ? "" : "Fonts/") + path));
        }

        public void AddFont(string key, SpriteFont font)
        {
            if (textures.ContainsKey(key))
                throw new System.Exception("Allready in dictonary");

            fonts.Add(key, font);
        }

        public SpriteFont GetFont(string key)
        {
            if (!fonts.ContainsKey(key))
            {
                if (key == errorName)
                    return null;

                return GetFont(errorName);
            }
            else
            {
                return fonts[key];
            }
        }
    }
}