using UnityEngine;

namespace ChinchillaCoding.AssetBundleDependencyVisualizer
{
    public interface IEditorPositionable
    {
        Vector3 GetPosition(EditorPositionBorder border);
    }
}