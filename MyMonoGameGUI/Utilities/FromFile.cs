using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace MyMonoGame.Utilities
{
    /// <summary>
    /// Load Assets from disk
    /// </summary>
    public class FromFile
    {
        /// <summary>
        /// Load Texture2D from files
        /// </summary>
        /// <param name="path">File .png path</param>
        /// <param name="sprite">Result sprite</param>
        static public Texture2D SpriteFromPng(string path, GraphicsDevice graphics)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return Texture2D.FromStream(graphics, fileStream);
                }
            }
            return null;
        }

        /// <summary>
        /// Load BoxSprite from files
        /// </summary>
        /// <param name="path">folder path</param>
        /// <param name="sprite">Result box</param>
        static public GUI.boxSprites BoxFormFolder(string path, GraphicsDevice graphics)
        {
            if (Directory.Exists(path))
            {
                return new GUI.boxSprites(
                    SpriteFromPng(path + "/topLeft.png", graphics),
                    SpriteFromPng(path + "/topCenter.png", graphics),
                    SpriteFromPng(path + "/topRight.png", graphics),
                    SpriteFromPng(path + "/centerLeft.png", graphics),
                    SpriteFromPng(path + "/centerCenter.png", graphics),
                    SpriteFromPng(path + "/centerRight.png", graphics),
                    SpriteFromPng(path + "/bottomLeft.png", graphics),
                    SpriteFromPng(path + "/bottomCenter.png", graphics),
                    SpriteFromPng(path + "/bottomRight.png", graphics)
                );
            }
            return new GUI.boxSprites();
        }

        /// <summary>
        /// Load SoundEffect from files
        /// </summary>
        /// <param name="path">File .mp3 path</param>
        /// <param name="sound">Result sound</param>
        static public SoundEffect SoundFromMp3(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return SoundEffect.FromStream(fileStream);
                }
            }
            return null;
        }
    }
}