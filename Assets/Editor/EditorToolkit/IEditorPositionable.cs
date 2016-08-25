using UnityEngine;

namespace GJP.EditorToolkit
{
    public interface IEditorPositionable
    {
        Vector2 GetPosition( EditorWindowAnchor border );
    }
}
