using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class APanelEditorWindow<T> : EditorWindow where T : APanelEditorWindow<T>
    {
        protected List<AEditorWindowPanel<T>> panels = new List<AEditorWindowPanel<T>> ();

        protected Rect oldPosition;
        [System.NonSerialized]
        private bool isSetup;

        protected void OnGUI ()
        {
            if (!this.isSetup)
            {
                this.panels.Clear ();
                if (Event.current.type == EventType.Repaint)
                {
                    InitPanels ();
                    RecalculateLayout ();
                    isSetup = true;
                }
                return;
            }

            if (Event.current.type == EventType.Layout && this.oldPosition != this.position)
            {
                RecalculateLayout ();
            }

            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].Draw ();
            }

            AdditonOnGUI ();
        }

        protected virtual void AdditonOnGUI ()
        {
        }

        protected abstract void InitPanels ();

        protected void RecalculateLayout ()
        {
            this.oldPosition = this.position;
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].Recalculate ();
            }
        }
    }
}
