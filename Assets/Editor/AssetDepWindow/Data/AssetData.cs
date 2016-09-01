using UnityEngine;
using UnityEditor;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetData
    {
        public readonly string Path;
        public readonly Texture2D Icon;
        public readonly string BundleName;
        public readonly AssetBundleData Parent;
        private readonly bool IsHidden;
        public readonly AssetDataType AssetType;
        public readonly string Name;


        public AssetData ( string path, AssetBundleData parent, bool isHidden )
        {
            this.Parent = parent;
            this.Path = path;
            this.IsHidden = isHidden;

            this.Icon = (Texture2D)AssetDatabase.GetCachedIcon (path);
            this.BundleName = parent.Name;
            this.AssetType = FindAssetType ();
            this.Name = System.IO.Path.GetFileNameWithoutExtension (path);
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
            else if (assetObj is MonoScript)
            {
                result = AssetDataType.Script;
            }

            if (this.IsHidden && result != AssetDataType.Script)
            {
                result |= AssetDataType.Hidden;
            }
            else
            {
                result |= AssetDataType.Included;
            }

            return result;
        }

        public void SelectAsset ()
        {
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object> (Path);
        }

        public string PreviewString
        {
            get
            {
                return string.Format ("{0} ( {1} )", this.Name, this.BundleName);
            }
        }

    }
}

