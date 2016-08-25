using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorMenuBar : AEditorWindowPanel
    {
        protected float border = 5f;

        protected List<IEditorRectDrawable> contentList;
        protected GUIStyle style;

        protected AEditorMenuBar( EditorWindow parent, EditorWindowDimension percentageRect ) : base (parent, percentageRect)
        {
            this.contentList = new List<IEditorRectDrawable> ();
            this.style = EditorStyles.toolbar;
            AddButtons ();
        }

        protected override void DrawContent( )
        {
            if (!this.DebugMode)
            {
                DrawBackground ();
            }

            float x = this.drawRect.x + border;
            float y = this.drawRect.y;
            float height = this.drawRect.height;

            for (int i = 0; i < contentList.Count; ++i)
            {
                IEditorRectDrawable drawable = contentList [i];
                Rect dimension = drawable.GetDimension ();
                // ignore height
                dimension.height = height;

                dimension.x = x;
                dimension.y = y;
                drawable.Draw (dimension);

                x += dimension.width;
            }
        }

        protected abstract void AddButtons( );

        protected void AddDrawableToList( IEditorRectDrawable element )
        {
            this.contentList.Add (element);
        }

        protected virtual void DrawBackground( )
        {
            EditorGUI.DrawPreviewTexture (this.drawRect, this.style.normal.background);
        }
    }
}
