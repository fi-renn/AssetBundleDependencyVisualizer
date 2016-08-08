using UnityEngine;
using UnityEditor;

public class EditorLine : IEditorDrawable
{
    protected IEditorPositionable start;

    protected IEditorPositionable end;

    protected Color color;


    public EditorLine (IEditorPositionable start, IEditorPositionable end, Color color)
    {
        this.start = start;
        this.end = end;
        this.color = color;
    }

    public void SetEndPoint (IEditorPositionable newEnd)
    {
        this.end = newEnd;
    }

    protected bool MemberAreValid
    {
        get
        {
            return start != null && end != null;
        }
    }

    public virtual void Draw ()
    {
        if (!MemberAreValid)
        {
            return;
        }

        Vector3 startPos, endPos;
        NodeUtils.GetBorderPoints (start, end, out startPos, out endPos);

        Handles.color = this.color;
        Handles.DrawLine (startPos, endPos);
    }
}
