using UnityEditor;
using UnityEngine;

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
            Color oldCol = Handles.color;
            Handles.color = this.lineColor;
            Handles.DrawLine (this.startPoint, this.endPoint);
            Handles.color = oldCol;
        }
    }
}