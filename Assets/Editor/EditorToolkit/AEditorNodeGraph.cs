using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorNodeGraph
    {
        protected IEditorPositionable parent, child;

        protected Vector2 startPoint, endPoint;

        protected AEditorNodeGraph (IEditorPositionable parent, IEditorPositionable child)
        {
            this.parent = parent;
            this.child = child;

            NodeUtils.GetClosestCrossAnchorPoints (parent, child, out startPoint, out endPoint);
        }

        public abstract void Draw ();
    }
}