using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGame.GUI
{
    /// <summary>
    /// 9 Textures2D for a box
    /// </summary>
    public struct boxSprites
    {
        public Texture2D topLeft;
        public Texture2D topCenter;
        public Texture2D topRight;
        public Texture2D centerLeft;
        public Texture2D centerCenter;
        public Texture2D centerRight;
        public Texture2D bottomLeft;
        public Texture2D bottomCenter;
        public Texture2D bottomRight;

        public bool correct { get { return (topLeft != null && topCenter != null && topRight != null && centerLeft != null && centerCenter != null && centerRight != null && bottomLeft != null && bottomCenter != null && bottomRight != null); } }

        public boxSprites(Texture2D TopLeft,
         Texture2D TopCenter,
         Texture2D TopRight,
         Texture2D CenterLeft,
         Texture2D CenterCenter,
         Texture2D CenterRight,
         Texture2D BottomLeft,
         Texture2D BottomCenter,
         Texture2D BottomRight)
        {
            topLeft = TopLeft;
            topCenter = TopCenter;
            topRight = TopRight;
            centerLeft = CenterLeft;
            centerCenter = CenterCenter;
            centerRight = CenterRight;
            bottomLeft = BottomLeft;
            bottomCenter = BottomCenter;
            bottomRight = BottomRight;
        }
    }
}