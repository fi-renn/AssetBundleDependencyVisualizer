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

        protected AEditorNode (int controlId, string title, GUIStyle style)
        {
            this.ControlId = controlId;
            this.title = title;
            this.style = style;
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

        public Vector2 Position
        {
            get { return this.nodeRect.position; }
            set { this.nodeRect.position = value; }
        }

        public Vector2 GetSize ()
        {
            return this.nodeRect.size;
        }

        public Vector2 GetPosition (EditorWindowAnchor border)
        {
            //TODO implement
            return this.nodeRect.position;
            /*
            Vector2 rectCenter = this.currentRect.center;
            Vector3 result = new Vector3(rectCenter.x, rectCenter.y);
            switch (border)
            {
                case EditorPositionBorder.Top:
                    result.y += this.currentRect.height;
                    break;

                case EditorPositionBorder.Left:
                    result.x -= this.currentRect.width;
                    break;

                case EditorPositionBorder.Right:
                    result.x += this.currentRect.width;
                    break;

                case EditorPositionBorder.Bottom:
                    result.y -= this.currentRect.height;
                    break;           
            }

            return result;
            */
        }
    }
}
