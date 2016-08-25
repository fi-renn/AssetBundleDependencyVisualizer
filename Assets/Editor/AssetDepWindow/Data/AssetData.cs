using UnityEngine;
using UnityEditor;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetData
    {
        public readonly string Path;
        public readonly Texture Icon;
        public readonly string BundleName;
        public readonly AssetBundleData Parent;
        public readonly bool IsHidden;
        public readonly AssetDataType AssetType;


        public AssetData (string path, AssetBundleData parent, bool isHidden)
        {
            this.Parent = parent;
            this.Path = path;
            this.IsHidden = isHidden;

            this.Icon = AssetDatabase.GetCachedIcon (path);
            this.BundleName = parent.Name;
            this.AssetType = FindAssetType ();
        }

        private AssetDataType FindAssetType ()
        {
            Object assetObj = AssetDatabase.LoadAssetAtPath<Object> (this.Path);
            AssetDataType result = AssetDataType.Other;

            if (assetObj is GameObject)
            {
                result = AssetDataType.Prefab;
            }
            else if (assetObj is Mesh)
            {
                result = AssetDataType.Mesh;
            }
            else if (assetObj is Material)
            {
                result = AssetDataType.Material;
            }
            else if (assetObj is Texture)
            {
                result = AssetDataType.Texture;
            }
            else if (assetObj is AudioClip)
            {
                result = AssetDataType.Audio;
            }

            return result;
        }

        public void SelectAsset ()
        {
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object> (Path);
        }
    }
}

