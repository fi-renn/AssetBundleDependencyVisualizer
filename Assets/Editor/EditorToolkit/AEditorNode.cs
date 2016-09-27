using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public abstract class AEditorNode : IEditorPositionable, IEditorDrawable
    {
        protected const float HeaderSizeY = 25f;
        protected const float MinWidth = 150f;
        protected const float Margin = 5f;

        #region member

        public readonly int ControlId;
        protected string title;
        protected GUIStyle style;

        protected Rect drawRect;
        private Rect nodeRect;

        //public readonly List<object> Dependencies;

        #endregion

        protected AEditorNode (int controlId, string title)
        {
            this.ControlId = controlId;
            this.title = title;
            this.style = new GUIStyle ("flow node 0");
            this.nodeRect = new Rect ();
        }

        public void Draw ()
        {
            GUI.Window (this.ControlId, this.nodeRect, DrawNode, this.title, this.style);

            //for (int i = 0; i < this.Dependencies.Count; ++i)
            {
                // TODO deps generic, meths with add, clear,//TODO implement addrange
            }
        }

        protected abstract void DrawNode (int windowId);

        protected abstract Vector2 CalcSize ();

        public void RecalcSize ()
        {
            Vector2 contentSize = CalcSize ();
            if (contentSize.x < MinWidth)
            {
                contentSize.x = MinWidth;
            }

            // update rect for implementation
            this.drawRect = new Rect (
                new Vector2 (Margin, HeaderSizeY),
                contentSize);

            // add header and margins to own size
            contentSize.y += HeaderSizeY + Margin;
            contentSize.x += 2f * Margin;

            this.nodeRect.size = contentSize;
        }

        public Vector2 TopLeftPosition
        {
            get { return this.nodeRect.position; }
            set { this.nodeRect.position = value; }
        }

        public Vector2 GetSize ()
        {
            return this.nodeRect.size;
        }

        public Vector2 GetPosition (EditorWindowAnchor anchor)
        {
            Vector2 size = this.nodeRect.size;
            Vector2 halfSize = size * 0.5f;

            Vector2 result = this.nodeRect.position;
            switch (anchor)
            {
                case EditorWindowAnchor.TopLeft:
                    break;

                case EditorWindowAnchor.Top:
                    result.x += halfSize.x;
                    break;

                case EditorWindowAnchor.TopRight:
                    result.x += size.x;
                    break;

                case EditorWindowAnchor.Left:
                    result.y += halfSize.y;
                    break;

                case EditorWindowAnchor.Center:
                    result += halfSize;
                    break;

                case EditorWindowAnchor.Right:
                    result.y += halfSize.y;
                    result.x += size.x;
                    break;

                case EditorWindowAnchor.BottomLeft:
                    result.y += size.y;
                    break;

                case EditorWindowAnchor.Bottom:
                    result.y += size.y;
                    result.x += halfSize.x;
                    break;

                case EditorWindowAnchor.BottomRight:
                    result += size;
                    break;
            }
            return result;
        }

        public void SetPosition (EditorWindowAnchor anchor, Vector2 newPos)
        {
            Vector2 size = this.nodeRect.size;
            Vector2 halfSize = size * 0.5f;

            switch (anchor)
            {
                case EditorWindowAnchor.TopLeft:
                    break;

                case EditorWindowAnchor.Top:
                    newPos.x -= halfSize.x;
                    break;

                case EditorWindowAnchor.TopRight:
                    newPos.x -= size.x;
                    break;

                case EditorWindowAnchor.Left:
                    newPos.y -= halfSize.y;
                    break;

                case EditorWindowAnchor.Center:
                    newPos -= halfSize;
                    break;

                case EditorWindowAnchor.Right:
                    newPos.y -= halfSize.y;
                    newPos.x -= size.x;
                    break;

                case EditorWindowAnchor.BottomLeft:
                    newPos.y -= size.y;
                    break;

                case EditorWindowAnchor.Bottom:
                    newPos.y -= size.y;
                    newPos.x -= halfSize.x;
                    break;

                case EditorWindowAnchor.BottomRight:
                    newPos -= size;
                    break;
            }

            this.nodeRect.position = newPos;
        }
    }
}
