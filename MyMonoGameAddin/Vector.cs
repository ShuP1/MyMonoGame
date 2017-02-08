namespace MyMonoGameAddin
{
	public class Vector
	{
		public int X;
		public int Y;

		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Add(Vector v)
		{
			X += v.X;
			Y += v.Y;
		}

		public static Vector Zero { get { return new Vector(0, 0); } }
	}
}
