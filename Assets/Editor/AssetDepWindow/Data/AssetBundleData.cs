using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleData
    {
        //TODO docu
        public string Name;

        public readonly List<AssetData> BundledAssets;

        public readonly List<AssetBundleData> ChildDependencies;

        public readonly List<AssetBundleData> ParentDependencies;

        public AssetBundleData (string name)
        {
            this.Name = name;
            this.BundledAssets = new List<AssetData> ();
            this.ChildDependencies = new List<AssetBundleData> ();
            this.ParentDependencies = new List<AssetBundleData> ();
        }

        public bool ContainsBundledAsset (string assetPath)
        {
            for (int i = 0; i < this.BundledAssets.Count; ++i)
            {
                if (this.BundledAssets[i].Path == assetPath)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsChildDep (string bundleName)
        {
            for (int i = 0; i < this.ChildDependencies.Count; ++i)
            {
                if (this.ChildDependencies[i].Name == bundleName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ConatinsParentDep (string bundlName)
        {
            for (int i = 0; i < this.ParentDependencies.Count; ++i)
            {
                if (this.ParentDependencies[i].Name == bundlName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}