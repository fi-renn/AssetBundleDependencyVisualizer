using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorMenuBar<T> : AEditorWindowPanel<T> where T : APanelEditorWindow<T>
    {
        protected float border = 5f;

        protected List<IEditorRectDrawable> contentList;
        protected GUIStyle style;

        protected AEditorMenuBar(T parent, EditorWindowDimension percentageRect)
            : base (parent, percentageRect )
        {
            this.contentList = new List<IEditorRectDrawable>();
            this.style = EditorStyles.toolbar;
            AddButtons ();
        }

        protected override void DrawContent()
        {
            if (!this.DebugMode)
            {
                DrawBackground ();
            }

            Vector2 positon = new Vector2(this.drawRect.x + border, this.drawRect.y);
            Vector2 size = new Vector2(0, this.drawRect.height);

            for (int i = 0; i < contentList.Count; ++i)
            {
                IEditorRectDrawable drawable = contentList[i];
                Vector2 dimension = drawable.GetDimension ();
                // ignore height
                size.x = dimension.x;

                drawable.Draw (new Rect(positon, size));

                positon.x += dimension.x;
            }
        }

        protected abstract void AddButtons();

        protected void AddDrawable( IEditorRectDrawable element )
        {
            this.contentList.Add (element);
        }

        protected virtual void DrawBackground()
        {
            EditorGUI.DrawPreviewTexture (this.drawRect, this.style.normal.background);
        }
    }
}
