using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public class GenericEditorMenuButton : IEditorRectDrawable
    {
        protected System.Action action;
        protected string text;
        protected Texture2D icon;
        protected Rect dimension;
        protected GUIStyle style;

        protected float iconWidth = 10f;

        public GenericEditorMenuButton( System.Action action, string text, Texture2D icon = null )
        {
            this.action = action;
            this.text = text;
            this.icon = icon;
            this.style = EditorStyles.toolbarButton;
            CalculateDimension ();
        }

        private void CalculateDimension( )
        {
            Vector2 textSize = style.CalcSize (new GUIContent (text));
            if (this.icon != null)
            {
                textSize.x += iconWidth;
            }

            this.dimension = new Rect (Vector2.zero, textSize);
        }

        public Rect GetDimension( )
        {
            return this.dimension;
        }

        public void Draw( Rect drawRect )
        {
            if (this.icon != null)
            {
                Rect iconRect = drawRect;
                iconRect.width = iconWidth;
                drawRect.x += iconWidth;
                drawRect.width -= iconWidth;

                EditorGUI.DrawPreviewTexture (iconRect, this.icon);
            }

            if (GUI.Button (drawRect, this.text, this.style))
            {
                if (this.action != null)
                {
                    this.action ();
                }
            }
        }
    }
}

