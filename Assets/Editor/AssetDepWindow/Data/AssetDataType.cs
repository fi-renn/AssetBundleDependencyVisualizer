using System;
using System.Text;

namespace GJP.AssetBundleDependencyVisualizer
{
    [Flags]
    public enum AssetDataType : uint
    {
        Prefab = 1u << 1,
        Mesh = 1u << 2,
        Material = 1u << 3,
        Texture = 1u << 4,
        Audio = 1u << 5,
        Bundle = 1u << 6,
        Script = 1u << 7,
        Other = 1u << 8,

        Included = 1u << 30,
        Hidden = 1u << 31,

        Visiblity = 3u << 30,
    }

    public static class AssetDataTypeExtensions
    {
        public static bool Filter (this AssetDataType filter, AssetData data)
        {
            AssetDataType typeFilter = filter & ~AssetDataType.Visiblity;
            AssetDataType visFilter = filter & AssetDataType.Visiblity;
            return typeFilter.Contains (data) && visFilter.Contains (data);
        }

        public static bool Contains (this AssetDataType filter, AssetData data)
        {
            return (data.AssetType & filter) != 0;
        }

        public static bool Contains (this AssetDataType filter, AssetDataType other)
        {
            return (other & filter) != 0;
        }

        public static string LogValue (this AssetDataType filter)
        {
            StringBuilder builder = new StringBuilder ();

            builder.AppendLine ("Filter value:");

            AssetDataType[] allValues = (AssetDataType[])Enum.GetValues (typeof(AssetDataType));
            foreach (var item in allValues)
            {    
                builder.AppendFormat ("{0}: {1}\n", item, (filter & item) > 0);
            }

            return builder.ToString ();
        }

    }
}