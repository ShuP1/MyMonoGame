using Microsoft.Xna.Framework;

namespace MyMonoGame
{
    public class Colors
    {
        public Color _normal;
        public Color _hover;
        public Color _focus;

        public Colors(Color color)
        {
            _normal = color;
            _hover = color;
            _focus = color;
        }

        public Colors(Color normal, Color hover)
        {
            _normal = normal;
            _hover = hover;
            _focus = hover;
        }

        public Colors(Color normal, Color hover, Color focus)
        {
            _normal = normal;
            _hover = hover;
            _focus = focus;
        }
    }
}