using System;

namespace GJP.AssetBundleDependencyVisualizer
{
    [Flags]
    public enum AssetDataType
    {
        Prefab = 1 << 1,
        Mesh = 1 << 2,
        Material = 1 << 3,
        Texture = 1 << 4,
        Audio = 1 << 5,
        Other = 1 << 6,
    }

    public static class AssetDataTypeExtensions
    {
        public static bool Matches( this AssetDataType filter, AssetData data )
        {        
            return (filter & data.AssetType) > 0;        
        }
    }
}