using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class APanelEditorWindow : EditorWindow
    {
        protected List<AEditorWindowPanel> panels = new List<AEditorWindowPanel> ();

        protected Rect oldPosition;
        [System.NonSerialized]
        private bool isSetup;

        protected void OnGUI( )
        {
            if (!this.isSetup)
            {
                this.panels.Clear ();
                InitPanels ();
                RecalculateLayout ();
                isSetup = true;
            }

            if (Event.current.type == EventType.Layout && this.oldPosition != this.position)
            {
                RecalculateLayout ();
            }

            for (int i = 0; i < panels.Count; i++)
            {
                panels [i].Draw ();
            }

            AdditonOnGUI ();
        }

        protected virtual void AdditonOnGUI( )
        {
        }

        protected abstract void InitPanels( );

        protected void RecalculateLayout( )
        {
            this.oldPosition = this.position;
            for (int i = 0; i < panels.Count; i++)
            {
                panels [i].Recalculate ();
            }
        }
    }
}
