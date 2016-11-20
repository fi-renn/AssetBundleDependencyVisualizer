using UnityEngine;

namespace GJP.EditorToolkit
{
    public enum EditorWindowAnchor
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
    }

    public static class EditorWindowAnchorExtensions
    {
        public static Vector2 ToDirection (this EditorWindowAnchor anchor)
        {
            switch (anchor)
            {
                case  EditorWindowAnchor.TopLeft:
                    return new Vector2 (-0.5f, -0.5f);
                case  EditorWindowAnchor.Top:
                    return new Vector2 (0f, -1f);
                case  EditorWindowAnchor.TopRight:
                    return new Vector2 (0.5f, -0.5f);
                case  EditorWindowAnchor.Left:
                    return new Vector2 (-1f, 0f);
                case  EditorWindowAnchor.Center:
                    return new Vector2 (-0f, 0f);
                case  EditorWindowAnchor.Right:
                    return new Vector2 (1f, 0f);
                case  EditorWindowAnchor.BottomLeft:
                    return new Vector2 (-0.5f, 0.5f);
                case  EditorWindowAnchor.Bottom:
                    return new Vector2 (0f, 1f);
                case  EditorWindowAnchor.BottomRight:
                    return new Vector2 (0.5f, 0.5f);
            }
            return Vector2.zero;
        }
    }
}
