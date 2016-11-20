using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
    public class BezierNodeGraph : AEditorNodeGraph
    {
        private const float DISTANCE_TANGENT = 50f;

        private Vector2 startTagentOffset, endTangentOffset;

        public BezierNodeGraph (IEditorPositionable parent, IEditorPositionable child)
            : base (parent, child)
        {            
            startTagentOffset = this.parentAnchor.ToDirection () * DISTANCE_TANGENT;
            endTangentOffset = this.childAnchor.ToDirection () * DISTANCE_TANGENT;
        }

        public override void SetAnchorPoints (EditorWindowAnchor parentAnchor, EditorWindowAnchor childAnchor)
        {
            base.SetAnchorPoints (parentAnchor, childAnchor);
            startTagentOffset = this.parentAnchor.ToDirection () * DISTANCE_TANGENT;
            endTangentOffset = this.childAnchor.ToDirection () * DISTANCE_TANGENT;
        }

        public override void Draw ()
        {
            Vector3 startTangent = this.startPoint + startTagentOffset;
            Vector3 endTangent = this.endPoint + endTangentOffset;

            Handles.DrawBezier (
                this.startPoint,
                this.endPoint,
                startTangent,
                endTangent,
                Color.white,
                null,
                5f);
//            Handles.DrawLine (this.startPoint, this.endPoint);
        }
    }
}

