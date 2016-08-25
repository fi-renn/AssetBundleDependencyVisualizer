using System;

namespace GJP.AssetBundleDependencyVisualizer
{
    [Flags]
    public enum AssetDataType
    {
        Prefab = 0,
        Mesh = 1 << 1,
        Material = 1 << 2,
        Texture = 1 << 3,
        Audio = 1 << 4,
        Other = 1 << 5,
    }
}

