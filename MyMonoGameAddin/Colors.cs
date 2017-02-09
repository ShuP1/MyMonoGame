using Microsoft.Xna.Framework;

namespace MyMonoGameAddin
{
	public class Colors
	{
		public Color Normal;
		public Color Hover;
		public Color Focus;

		public Colors(Color color)
		{
			Normal = color;
			Hover = color;
			Focus = color;
		}

		public Colors(Color normal, Color hover)
		{
			Normal = normal;
			Hover = hover;
			Focus = hover;
		}

		public Colors(Color normal, Color hover, Color focus)
		{
			Normal = normal;
			Hover = hover;
			Focus = focus;
		}

		internal Color Get(UIManager.State status)
		{
			switch (status)
			{
				case UIManager.State.Hover:
					return Hover;

				case UIManager.State.Focus:
				case UIManager.State.Active:
					return Focus;

				default:
					return Normal;
			}
		}
	}
}