using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public class TreeNodeGrouper : IEditorNodeGrouper
    {
        private float MarginSize = 20f;

        public Rect GroupNodes<N> (List<N> nodes)
            where N : AEditorNode
        {
            if (nodes == null || nodes.Count == 0)
            {
                return new Rect ();
            }

            //TODO do grouping


            return ClacNodeDimensions (nodes);
        }

        private Rect ClacNodeDimensions<N> (List<N> nodes) 
            where N : AEditorNode
        {
            Vector2 min, max;
            min = max = Vector2.zero;

            // find min and max points
            for (int i = 0; i < nodes.Count; ++i)
            {
                AEditorNode curNode = nodes[i];
                Vector2 nodeMin = curNode.GetPosition (EditorWindowAnchor.TopLeft);
                Vector2 nodeMax = curNode.GetPosition (EditorWindowAnchor.BottomRight);

                min.x = Mathf.Min (nodeMin.x, min.x);
                min.y = Mathf.Min (nodeMin.y, min.y);

                max.x = Mathf.Max (nodeMax.x, max.x);
                max.y = Mathf.Max (nodeMax.y, max.y);
            }

            Vector2 distance = max - min;
            distance.x = Mathf.Abs (distance.x);
            distance.y = Mathf.Abs (distance.y);

            float margin = MarginSize * 2;
            ;

            // add margin for beauty
            distance.x += margin;
            distance.y += margin;

            return new Rect (Vector2.zero, distance);
        }
    }
}