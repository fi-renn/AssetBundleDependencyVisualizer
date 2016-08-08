using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorNode :  IEditorDrawable, IEditorPositionable
{
    protected int controlId;
    protected Rect currentRect;
    public string Name;

    public EditorNode (Vector2 centerPosition, Vector2 dimension, int controlId, string name = "unnamed")
    {
        this.controlId = controlId;
        this.currentRect = new Rect (
            centerPosition.x,
            centerPosition.y,
            dimension.x * 2f,
            dimension.y * 2f);
        this.Name = name;
    }

    public void Draw ()
    {
        currentRect = GUI.Window (this.controlId, currentRect, DrawContent, Name);
    }

    private void DrawContent (int controlId)
    {
        
    }

    public Vector3 GetPosition (EditorPositionBorder border)
    {
        Vector2 rectCenter = this.currentRect.center;
        Vector3 result = new Vector3 (rectCenter.x, rectCenter.y);
        switch (border)
        {
            case EditorPositionBorder.Top:
                result.y += this.currentRect.height;
                break;

            case EditorPositionBorder.Left:
                result.x -= this.currentRect.width;
                break;

            case EditorPositionBorder.Right:
                result.x += this.currentRect.width;
                break;

            case EditorPositionBorder.Bottom:
                result.y -= this.currentRect.height;
                break;           
        }

        return result;
    }
}