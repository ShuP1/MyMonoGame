namespace MyMonoGame
{
    public class Vector
    {
        public int X;
        public int Y;

        public Vector()
        {
            X = 0;
            Y = 0;
        }

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

        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }
    }
}
