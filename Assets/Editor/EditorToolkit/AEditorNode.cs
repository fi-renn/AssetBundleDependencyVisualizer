using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public abstract class AEditorNode : IEditorPositionable, IEditorDrawable
    {
        protected const float HeaderSizeY = 30f;
        protected const float MinWidth = 150f;

        #region member

        public readonly int ControlId;
        protected string title;
        protected GUIStyle style;

        protected Rect drawRect;

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
            GUI.Window (this.ControlId, this.drawRect, DrawNode, this.title, this.style);

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
            contentSize.y += HeaderSizeY;
            if (contentSize.x < MinWidth)
            {
                contentSize.x = MinWidth;
            }
            this.drawRect.size = contentSize;
        }

        public Vector2 Position
        {
            get { return this.drawRect.position; }
            set { this.drawRect.position = value; }
        }


        public Vector2 GetPosition (EditorWindowAnchor border)
        {
            //TODO implement
            return this.drawRect.position;
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
