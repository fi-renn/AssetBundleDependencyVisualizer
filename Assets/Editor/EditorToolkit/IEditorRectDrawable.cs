using UnityEngine;

namespace GJP.EditorToolkit
{
    public interface IEditorRectDrawable
    {
        Rect GetDimension( );

        void Draw( Rect drawRect );
    }
}
