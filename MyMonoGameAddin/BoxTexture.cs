using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameAddin
{
	public class BoxTexture
	{
		public Texture2D[,] Textures = new Texture2D[3, 3];

		public Texture2D top_left
		{
			get
			{
				return Textures[0, 0];
			}
			set
			{
				Textures[0, 0] = value;
			}
		}
		public Texture2D top_middle
		{
			get
			{
				return Textures[0, 1];
			}
			set
			{
				Textures[0, 1] = value;
			}
		}
		public Texture2D top_right
		{
			get
			{
				return Textures[0, 2];
			}
			set
			{
				Textures[0, 2] = value;
			}
		}
		public Texture2D middle_left
		{
			get
			{
				return Textures[1, 0];
			}
			set
			{
				Textures[1, 0] = value;
			}
		}
		public Texture2D middle_middle
		{
			get
			{
				return Textures[1, 1];
			}
			set
			{
				Textures[1, 1] = value;
			}
		}
		public Texture2D middle_right
		{
			get
			{
				return Textures[1, 2];
			}
			set
			{
				Textures[1, 2] = value;
			}
		}
		public Texture2D bottom_left
		{
			get
			{
				return Textures[2, 0];
			}
			set
			{
				Textures[2, 0] = value;
			}
		}
		public Texture2D bottom_middle
		{
			get
			{
				return Textures[2, 1];
			}
			set
			{
				Textures[2, 1] = value;
			}
		}
		public Texture2D bottom_right
		{
			get
			{
				return Textures[2, 2];
			}
			set
			{
				Textures[2, 2] = value;
			}
		}

		public bool IsCorrect()
		{
			if (Textures.GetLength(0) != 3 || Textures.GetLength(1) != 3)
				return false;

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					if (Textures[x, y] == null)
						return false;
				}
			}
			return true;
		}
	}
}
