using UnityEngine;

public class SimpleNodePoint : IEditorPositionable
{
    protected Vector3 curPosition;


    public SimpleNodePoint (Vector3 position)
    {
        this.curPosition = position;
    }

    public void SetPosition (Vector3 position)
    {
        this.curPosition = position;
    }

    public Vector3 GetPosition (EditorPositionBorder border)
    {
        return this.curPosition;
    }
}