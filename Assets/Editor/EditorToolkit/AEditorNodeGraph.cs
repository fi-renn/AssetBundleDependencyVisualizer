using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorNodeGraph
    {
        protected IEditorPositionable parent, child;

        protected EditorWindowAnchor parentAnchor, childAnchor;
        protected Vector2 startPoint, endPoint;
        protected Color lineColor;

        protected AEditorNodeGraph (IEditorPositionable parent, IEditorPositionable child)
        {
            this.parent = parent;
            this.child = child;
            this.lineColor = Color.white;

            NodeUtils.GetClosestCrossAnchorPoints (parent, child, out parentAnchor, out childAnchor);
        }

        public virtual void SetAnchorPoints (EditorWindowAnchor parentAnchor, EditorWindowAnchor childAnchor)
        {
            this.parentAnchor = parentAnchor;
            this.childAnchor = childAnchor;
        }

        public void UpdateCachedPositions ()
        {
            startPoint = this.parent.GetPosition (parentAnchor);
            endPoint = this.child.GetPosition (childAnchor);
        }

        public virtual void SetLineColor (Color lineCol)
        {
            this.lineColor = lineCol;
        }

        public abstract void Draw ();
    }
}