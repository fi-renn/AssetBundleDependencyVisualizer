using UnityEngine;

namespace GJP.EditorToolkit
{
    public interface IEditorRectDrawable
    {
        Vector2 GetDimension();

        void Draw( Rect drawRect );
    }
}
