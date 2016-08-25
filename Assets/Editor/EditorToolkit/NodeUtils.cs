using UnityEngine;

namespace GJP.EditorToolkit
{
    public static class NodeUtils
    {
        public static void GetBorderPoints(IEditorPositionable start, IEditorPositionable end, out Vector3 startPos, out Vector3 endPos)
        {
            // find direction
            Vector3 diff = end.GetPosition(EditorWindowAnchor.Center) - start.GetPosition(EditorWindowAnchor.Center);
            //diff.Normalize ();
            
            
            EditorWindowAnchor startBorder, endBorder;
            
            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                // connect vertical
                if (diff.x > 0f)
                {
                    // left to rigth
                    startBorder = EditorWindowAnchor.Right;
                    endBorder = EditorWindowAnchor.Left;
                }
                else
                {
                    // right to left
                    startBorder = EditorWindowAnchor.Left;
                    endBorder = EditorWindowAnchor.Right;
                }
                
            }
            else
            {
                // connect horizontal
                if (diff.y > 0f)
                {
                    // bottom to top
                    startBorder = EditorWindowAnchor.Top;
                    endBorder = EditorWindowAnchor.Bottom;
                }
                else
                {
                    // top to bottom
                    startBorder = EditorWindowAnchor.Bottom;
                    endBorder = EditorWindowAnchor.Top;
                }
            }
            
            
            // get points
            endPos = end.GetPosition(endBorder);
            startPos = start.GetPosition(startBorder);
        }
    }
}
