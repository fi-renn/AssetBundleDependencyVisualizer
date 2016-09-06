using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public static class NodeUtils
    {
        public static void GetBorderPoints (IEditorPositionable start, IEditorPositionable end, out Vector3 startPos, out Vector3 endPos)
        {
            // find direction
            Vector3 diff = end.GetPosition (EditorWindowAnchor.Center) - start.GetPosition (EditorWindowAnchor.Center);
            //diff.Normalize ();
            
            
            EditorWindowAnchor startBorder, endBorder;
            
            if (Mathf.Abs (diff.x) > Mathf.Abs (diff.y))
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
            endPos = end.GetPosition (endBorder);
            startPos = start.GetPosition (startBorder);
        }

        public static Rect PutNodesOnRect<N> (List<N> nodes) where N : AEditorNode
        {
            if (nodes == null || nodes.Count == 0)
            {
                return new Rect ();
            }

            Vector2 min = new Vector2 (float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2 (float.MinValue, float.MinValue);

            // find min and max points
            foreach (var curNode in nodes)
            {   
                Vector2 nodeMin = curNode.GetPosition (EditorWindowAnchor.TopLeft);
                Vector2 nodeMax = curNode.GetPosition (EditorWindowAnchor.BottomRight);

                Debug.Log (string.Format ("{0} ({1}/{2})", curNode, nodeMin, nodeMax));

                min.x = Mathf.Min (nodeMin.x, min.x);
                min.y = Mathf.Min (nodeMin.y, min.y);

                max.x = Mathf.Max (nodeMax.x, max.x);
                max.y = Mathf.Max (nodeMax.y, max.y);
            }

            // correct coordinates below 0,0
            Vector2 offset = Vector2.zero;
            if (min.x < 0f)
            {
                offset.x = -min.x;
            }
            if (min.y < 0f)
            {
                offset.y = -min.y;
            }

            max += offset;
            foreach (var curNode in nodes)
            {
                curNode.TopLeftPosition += offset;
            }                

            return new Rect (Vector2.zero, max);         
        }
    }
}
