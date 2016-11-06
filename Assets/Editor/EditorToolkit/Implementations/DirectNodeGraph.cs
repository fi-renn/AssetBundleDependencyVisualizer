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
            throw new System.NotImplementedException ();
        }
    }
}