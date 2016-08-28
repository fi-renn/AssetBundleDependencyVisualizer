using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorWindowPanel<T> where T : APanelEditorWindow<T>
    {
        public bool DebugMode;

        protected T parentWindow;
        protected EditorWindowDimension percentageRect;
        protected Rect drawRect;

        protected abstract Color DebugColor { get; }

        protected AEditorWindowPanel(T parent, EditorWindowDimension dimension)
        {
            this.parentWindow = parent;
            this.percentageRect = dimension;
        }

        public void Draw()
        {
            if (this.DebugMode)
            {
                EditorGUI.DrawRect (this.drawRect, DebugColor);
            }

            DrawContent ();
        }

        protected abstract void DrawContent();

        public void Recalculate()
        {
            drawRect = this.percentageRect.CalculateDimension (this.parentWindow.position);
        }

        public void SetDimension( EditorWindowDimension newdimension )
        {
            this.percentageRect = newdimension;
            Recalculate ();
        }
    }
}
