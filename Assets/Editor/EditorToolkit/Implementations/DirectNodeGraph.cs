using UnityEditor;

namespace GJP.EditorToolkit
{
    public class DirectNodeGraph : AEditorNodeGraph
    {
        public DirectNodeGraph (IEditorPositionable parent, IEditorPositionable child)
            : base (parent, child)
        {            
        }

        public override void Draw ()
        {
            Handles.DrawLine (this.startPoint, this.endPoint);
        }
    }
}