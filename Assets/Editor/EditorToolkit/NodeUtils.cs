using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public static class NodeUtils
    {
        public static void GetBorderPoints (IEditorPositionable start, IEditorPositionable end, 
                                            out Vector3 startPos, out Vector3 endPos)
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

        public static void GetBestVisualAnchorPoints (IEditorPositionable center, IEditorPositionable leaf,
                                                      out EditorWindowAnchor start, out EditorWindowAnchor end)
        {
            Vector2 diff = center.GetPosition (EditorWindowAnchor.Center) - leaf.GetPosition (EditorWindowAnchor.Center);

            const float border = 50f;

            float angle = Vector2.Angle (Vector2.right, diff.normalized);
            float diffToSquareAngle = Mathf.Abs (90f - angle);

            if (diff.y > 0f)
            {
                // top 
                end = EditorWindowAnchor.Top;
                if (diffToSquareAngle < border)
                {
                    start = EditorWindowAnchor.Bottom;
                }
                else
                {
                    if (angle < 90f)
                    {
                        start = EditorWindowAnchor.Right;
                    }
                    else
                    {
                        start = EditorWindowAnchor.Left;
                    }
                }
            }
            else
            {
                start = EditorWindowAnchor.Bottom;
                if (diffToSquareAngle < border)
                {
                    end = EditorWindowAnchor.Top;
                }
                else
                {
                    if (angle < 90f)
                    {
                        end = EditorWindowAnchor.Right;
                    }
                    else
                    {
                        end = EditorWindowAnchor.Left;
                    }
                }
            }
        }

        public static void GetClosestCrossAnchorPoints (IEditorPositionable parent, IEditorPositionable child, 
                                                        out EditorWindowAnchor start, out EditorWindowAnchor end)
        {
            // diff vector
            Vector2 diff = child.GetPosition (EditorWindowAnchor.Center) - parent.GetPosition (EditorWindowAnchor.Center);
            EditorWindowAnchor[] checkList = new EditorWindowAnchor[4];

            // axis check
            if (diff.x > 0)
            {
                checkList[0] = EditorWindowAnchor.Right;
                checkList[2] = EditorWindowAnchor.Left;
            }
            else
            {
                checkList[0] = EditorWindowAnchor.Left;
                checkList[2] = EditorWindowAnchor.Right;
            }

            if (diff.y > 0)
            {
                checkList[1] = EditorWindowAnchor.Bottom;
                checkList[3] = EditorWindowAnchor.Top;
            }
            else
            {
                checkList[1] = EditorWindowAnchor.Top;
                checkList[3] = EditorWindowAnchor.Bottom;
            }

            int closeIndex1 = 0;
            int closeIndex2 = 2;
            float min = float.MaxValue;
            // check anchor points
            for (int i = 0; i < 2; ++i)
            {
                for (int t = 2; t < 4; ++t)
                {
                    Vector2 testDiff = child.GetPosition (checkList[t]) - parent.GetPosition (checkList[i]);
                    float testValue = testDiff.sqrMagnitude;
                    if (testValue < min)
                    {
                        min = testValue;
                        closeIndex1 = i;
                        closeIndex2 = t;
                    }
                }
            }

            start = checkList[closeIndex1];
            end = checkList[closeIndex2];
        }
    
        
    }
}
