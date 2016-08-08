using UnityEngine;

public static class NodeUtils
{
    public static void GetBorderPoints (IEditorPositionable start, IEditorPositionable end, out Vector3 startPos, out Vector3 endPos)
    {
        // find direction
        Vector3 diff = end.GetPosition (EditorPositionBorder.Center) - start.GetPosition (EditorPositionBorder.Center);
        //diff.Normalize ();


        EditorPositionBorder startBorder, endBorder;

        if (Mathf.Abs (diff.x) > Mathf.Abs (diff.y))
        {
            // connect vertical
            if (diff.x > 0f)
            {
                // left to rigth
                startBorder = EditorPositionBorder.Right;
                endBorder = EditorPositionBorder.Left;
            }
            else
            {
                // right to left
                startBorder = EditorPositionBorder.Left;
                endBorder = EditorPositionBorder.Right;
            }

        }
        else
        {
            // connect horizontal
            if (diff.y > 0f)
            {
                // bottom to top
                startBorder = EditorPositionBorder.Top;
                endBorder = EditorPositionBorder.Bottom;
            }
            else
            {
                // top to bottom
                startBorder = EditorPositionBorder.Bottom;
                endBorder = EditorPositionBorder.Top;
            }
        }


        // get points
        endPos = end.GetPosition (endBorder);
        startPos = start.GetPosition (startBorder);
    }
}