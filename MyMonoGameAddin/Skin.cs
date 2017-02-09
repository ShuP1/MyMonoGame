using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameAddin
{
	public struct Skin
	{
		public Colors backColors;
		public Colors foreColors;
		public BoxTexture texture;
		public SpriteFont font;

		public Skin(BoxTexture text, SpriteFont fon, Colors back, Colors fore)
		{
			texture = text;
			font = fon;
			backColors = back;
			foreColors = fore;
		}
	}
}