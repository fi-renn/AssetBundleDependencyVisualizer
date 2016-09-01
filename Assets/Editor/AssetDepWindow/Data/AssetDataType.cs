using System;
using System.Text;

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
        Bundle = 1 << 6,
        Other = 1 << 7,
    }

    public static class AssetDataTypeExtensions
    {
        public static bool Matches (this AssetDataType filter, AssetData data)
        {        
            return (filter & data.AssetType) > 0;        
        }

        public static bool Matches (this AssetDataType filter, AssetDataType other)
        {
            return (filter & other) > 0;        
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