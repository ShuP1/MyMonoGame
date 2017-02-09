using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameAddin
{
	public class ResourcesManager
	{
		public enum KeyMode { path, file }

		public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
		public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
		public Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
		public Dictionary<string, BoxTexture> Boxes = new Dictionary<string, BoxTexture>();

		ContentManager Content;
		GraphicsDevice Graphics;

		public ResourcesManager(ContentManager content, GraphicsDevice graphics)
		{
			Content = content;
			Graphics = graphics;
		}

		public void LoadFont(string assetname, KeyMode keymode = KeyMode.file, bool fullpath = false)
		{
			string keyname;
			if (keymode == KeyMode.file)
			{
				string[] split = assetname.Split('/');
				keyname = split[split.Length - 1];
			}
			else
			{
				keyname = assetname;
			}
			LoadFont(keyname, assetname, fullpath);
		}

		public void LoadFont(string keyname, string path, bool fullpath = false)
		{
			Fonts.Add(keyname, Content.Load<SpriteFont>((fullpath ? "" : "Fonts/") + path));
		}

		public bool TryLoadTextureFile(string keyname, string path)
		{
			try
			{
				if (File.Exists(path))
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Open))
					{
						Textures.Add(keyname, Texture2D.FromStream(Graphics, fileStream));
					}
					return true;
				}
				return false;
			}
			catch { }
			return false;
		}

		public void LoadTexture(string assetname, KeyMode keymode = KeyMode.file, bool fullpath = false)
		{
			string keyname;
			if (keymode == KeyMode.file)
			{
				string[] split = assetname.Split('/');
				keyname = split[split.Length - 1];
			}
			else
			{
				keyname = assetname;
			}
			LoadTexture(keyname, assetname, fullpath);
		}

		public void LoadTexture(string keyname, string path, bool fullpath = false)
		{
			Textures.Add(keyname, Content.Load<Texture2D>((fullpath ? "" : "Textures/") + path));
		}

		public bool TryLoadSoundFile(string keyname, string path)
		{
			try
			{
				if (File.Exists(path))
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Open))
					{
						Sounds.Add(keyname, SoundEffect.FromStream(fileStream));
					}
				}
				return false;
			}
			catch { }
			return false;
		}

		public void LoadSound(string assetname, KeyMode keymode = KeyMode.file, bool fullpath = false)
		{
			string keyname;
			if (keymode == KeyMode.file)
			{
				string[] split = assetname.Split('/');
				keyname = split[split.Length - 1];
			}
			else
			{
				keyname = assetname;
			}
			LoadSound(keyname, assetname, fullpath);
		}

		public void LoadSound(string keyname, string path, bool fullpath = false)
		{
			Sounds.Add(keyname, Content.Load<SoundEffect>((fullpath ? "" : "Sounds/") + path));
		}

		public void LoadBox(string foldername, KeyMode keymode = KeyMode.file, bool textname = false, bool fullpath = false)
		{
			string keyname;
			if (keymode == KeyMode.file)
			{
				string[] split = foldername.Split('/');
				keyname = split[split.Length - 1];
			}
			else
			{
				keyname = foldername;
			}
			LoadBox(keyname, foldername, textname, fullpath);
		}

		public void LoadBox(string keyname, string path, bool textname = false, bool fullpath = false)
		{
			BoxTexture box = new BoxTexture();
			string folderpath = (fullpath ? "" : "Textures/Boxes/") + path + "/";
			if (textname)
			{
				box.top_left = Content.Load<Texture2D>(folderpath + "top_left");
				box.top_middle = Content.Load<Texture2D>(folderpath + "top_middle");
				box.top_right = Content.Load<Texture2D>(folderpath + "top_right");
				box.middle_left = Content.Load<Texture2D>(folderpath + "middle_left");
				box.middle_middle = Content.Load<Texture2D>(folderpath + "middle_middle");
				box.middle_right = Content.Load<Texture2D>(folderpath + "middle_right");
				box.bottom_left = Content.Load<Texture2D>(folderpath + "bottom_left");
				box.bottom_middle = Content.Load<Texture2D>(folderpath + "bottom_middle");
				box.bottom_right = Content.Load<Texture2D>(folderpath + "bottom_right");
			}
			else
			{
				for (int x = 0; x < 3; x++)
				{
					for (int y = 0; y < 3; y++)
					{
						box.Textures[x, y] = Content.Load<Texture2D>(folderpath + x + "_" + y); //0_0.png
					}
				}
			}
			if (!box.IsCorrect())
				throw new Exception("Incorrect Box");
			Boxes.Add(keyname, box);
		}
	}
}
