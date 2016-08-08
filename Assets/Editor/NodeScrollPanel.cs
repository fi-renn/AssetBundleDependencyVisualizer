using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeScrollPanel
{
    public event System.Action<int> WindowSelected;

    #region member

    protected NodeTestWindow parentWindow;
    protected Rect WindowRect = new Rect (0, 0, 10000, 10000);
    protected Vector2 scrollPosition;

    #endregion

    public NodeScrollPanel (NodeTestWindow parentWindow)
    {
        this.parentWindow = parentWindow;
    }

    public void Draw (Rect panelRect)
    {
        GUI.BeginGroup (this.WindowRect);
        this.scrollPosition = GUI.BeginScrollView (panelRect, scrollPosition, this.WindowRect, true, true);

        foreach (IEditorDrawable drawable in this.parentWindow.Lines)
        {
            drawable.Draw ();
        }

        parentWindow.BeginWindows ();
        foreach (IEditorDrawable drawable in this.parentWindow.Windows)
        {
            drawable.Draw ();
        }
        parentWindow.EndWindows ();

        GUI.EndScrollView ();
        GUI.EndGroup ();
    }

    public void SelectIndex (int index)
    {
        if (index < 0)
        {
            // deselect
            return;
        }

        EditorNode node = this.parentWindow.Windows [index];
        this.scrollPosition = node.GetPosition (EditorPositionBorder.Center);
    }
}
