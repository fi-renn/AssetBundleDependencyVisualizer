using System;
using System.Text;
using UnityEngine;

namespace GJP.AssetBundleDependencyVisualizer
{
    [Flags]
    public enum AssetDataType : int
    {
        Prefab = 1 << 0,
        Mesh = 1 << 1,
        Material = 1 << 2,
        Texture = 1 << 3,
        Audio = 1 << 4,
        Bundle = 1 << 5,
        Script = 1 << 6,
        Other = 1 << 7,

        Included = 1 << 8,
        Hidden = 1 << 9,
    }

    public static class AssetDataTypeUtility
    {
        public const AssetDataType Visiblity = AssetDataType.Included | AssetDataType.Hidden;

        public const AssetDataType DefaultFilter = AssetDataType.Prefab | AssetDataType.Mesh |
                                                   AssetDataType.Texture | AssetDataType.Other |
                                                   AssetDataType.Included | AssetDataType.Hidden;

        public static AssetDataType EnsureVisiblity (AssetDataType filter, AssetDataType previousValue)
        {
            var visFilter = filter & Visiblity;
            if (visFilter == 0)
            {
                if (previousValue.Contains (AssetDataType.Hidden))
                {
                    filter |= AssetDataType.Included;
                }
                else
                {
                    filter |= AssetDataType.Hidden;
                }
            } 
            return filter;
        }
    }

    public static class AssetDataTypeExtensions
    {
        public static bool Filter (this AssetDataType filter, AssetData data)
        {
            AssetDataType typeFilter = filter & ~AssetDataTypeUtility.Visiblity;
            AssetDataType visFilter = filter & AssetDataTypeUtility.Visiblity;
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