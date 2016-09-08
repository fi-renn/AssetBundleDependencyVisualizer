using UnityEngine;

namespace GJP.EditorToolkit
{
    public class EditorWindowDimension
    {
        public float Height;
        public float Width;
        public EditorWindowAnchor AnchorPoint;
        public float OffsetX;
        public float OffsetY;

        public bool HeightIsFixed;
        public bool WidthIsFixed;
        public bool OffsetXIsFixed;
        public bool OffsetYIsFixed;

        public Rect CalculateDimension (Rect parentRect)
        {
            // values for the rect
            Vector2 position, size;

            size = new Vector2 (
                this.WidthIsFixed ? this.Width : parentRect.width * this.Width,
                this.HeightIsFixed ? this.Height : parentRect.height * this.Height
            );

            // TODO fix check for bounds
            GetPosition (ref parentRect, ref size, out position);

            Rect result = new Rect (position, size);
            ClampToParent (parentRect, ref result);
            return result;
        }

        private void GetPosition (ref Rect parent, ref Vector2 size, out Vector2 offset)
        {
            // get the general offset to the ankor
            offset = new Vector2 (
                this.OffsetXIsFixed ? this.OffsetX : parent.width * this.OffsetX,
                this.OffsetYIsFixed ? this.OffsetY : parent.height * this.OffsetY
            );

            Vector2 spaceToWindow = new Vector2 (parent.width - size.x, parent.height - size.y);

            // now have the offset if the anchor is top left, for other 
            // positons we need to calculate
            switch (this.AnchorPoint)
            {
                case EditorWindowAnchor.TopLeft:
                    // unchanced
                    break;

                case EditorWindowAnchor.Top:
                    offset.x += spaceToWindow.x / 2f;
                    break;

                case EditorWindowAnchor.TopRight:
                    offset.x += spaceToWindow.x;
                    break;

                case EditorWindowAnchor.Left:
                    offset.y += spaceToWindow.y / 2f;
                    break;

                case EditorWindowAnchor.Center:
                    offset.y += spaceToWindow.y / 2f;
                    offset.x += spaceToWindow.x / 2f;
                    break;

                case EditorWindowAnchor.Right:
                    offset.x += spaceToWindow.x;
                    offset.y += spaceToWindow.y / 2f;
                    break;

                case EditorWindowAnchor.BottomLeft:
                    offset.y += spaceToWindow.y;
                    break;

                case EditorWindowAnchor.Bottom:
                    offset.x += spaceToWindow.x / 2f;
                    offset.y += spaceToWindow.y;
                    break;

                case EditorWindowAnchor.BottomRight:
                    offset.x += spaceToWindow.x;
                    offset.y += spaceToWindow.y;
                    break;
            }
        }

        private void ClampToParent (Rect parent, ref Rect selfRect)
        {           
            // clamp from left and top
            Vector2 newPos = selfRect.position;
            Vector2 newSize = selfRect.size;

            if (selfRect.xMin < 0f)
            {
                newPos.x = 0 - selfRect.xMin;
            }
            if (selfRect.yMin < 0f)
            {
                newPos.y = 0 - selfRect.xMin;
            }

            newSize -= selfRect.position - newPos;
            selfRect.position = newPos;
            selfRect.size = newSize;          

            // clamp size from right and bottom
            Vector2 parentSize = parent.size;
            newSize = selfRect.size;
            if (parentSize.x < selfRect.xMax)
            {
                newSize.x += parentSize.x - selfRect.xMax;
            }
            if (parentSize.y < selfRect.yMax)
            {
                newSize.y += parentSize.y - selfRect.yMax;
            } 
            selfRect.size = newSize;
        }
    }
}
