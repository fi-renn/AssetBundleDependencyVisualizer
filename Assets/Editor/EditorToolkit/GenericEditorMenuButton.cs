using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public class GenericEditorMenuButton : IEditorRectDrawable
    {
        protected System.Action action;
        protected Vector2 dimension;
        protected GUIStyle style;

        protected GUIContent guiContent;
        protected float iconWidth = 10f;

        public GenericEditorMenuButton ( System.Action action, string text, Texture2D icon = null )
        {
            this.guiContent = new GUIContent (text, icon);
            this.action = action;
            this.style = EditorStyles.toolbarButton;
            CalculateDimension ();
        }

        private void CalculateDimension ()
        {
            this.dimension = style.CalcSize (this.guiContent);
        }

        public Vector2 GetDimension ()
        {
            return this.dimension;
        }

        public void Draw (Rect drawRect)
        {
            if (GUI.Button (drawRect, this.guiContent, this.style))
            {
                if (this.action != null)
                {
                    this.action ();
                }
            }
        }
    }
}

