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

		public static Vector Add(Vector v1, Vector v2)
		{
			return new Vector(v1.X + v2.X, v1.Y + v2.Y);
		}

		public void Add(Vector v)
		{
			X += v.X;
			Y += v.Y;
		}

		public static Vector Zero = new Vector(0, 0);
		public static Vector One = new Vector(1, 1);
	}
}
