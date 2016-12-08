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
        static public void SpriteFromPng(string path, ref Texture2D sprite, GraphicsDevice graphics)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    sprite = Texture2D.FromStream(graphics, fileStream);
                }
            }
        }

        /// <summary>
        /// Load SoundEffect from files
        /// </summary>
        /// <param name="path">File .mp3 path</param>
        /// <param name="sound">Result sound</param>
        static public void SoundFromMp3(string path, ref SoundEffect sound)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    sound = SoundEffect.FromStream(fileStream);
                }
            }
        }
    }
}