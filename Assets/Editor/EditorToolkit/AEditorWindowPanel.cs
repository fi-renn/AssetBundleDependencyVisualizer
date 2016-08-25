using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorWindowPanel
    {
        public EditorWindow ParentWindow;
        public bool DebugMode;

        protected EditorWindowDimension percentageRect;
        protected Rect drawRect;

        protected abstract Color DebugColor { get; }

        protected AEditorWindowPanel( EditorWindow parent, EditorWindowDimension dimension )
        {
            this.ParentWindow = parent;
            this.percentageRect = dimension;
        }

        public void Draw( )
        {
            if (this.DebugMode)
            {
                EditorGUI.DrawRect (this.drawRect, DebugColor);
            }

            DrawContent ();
        }

        protected abstract void DrawContent( );

        public void Recalculate( )
        {
            drawRect = this.percentageRect.CalculateDimension (this.ParentWindow.position);
        }

        public void SetDimension( EditorWindowDimension newdimension )
        {
            this.percentageRect = newdimension;
            Recalculate ();
        }
    }
}
