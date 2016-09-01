using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepData
    {
        #region member

        public readonly List<AssetBundleData> AssetBundles;
        public readonly List<AssetData> BundledAssets;

        public readonly Dictionary<string, AssetBundleData> NameToBundle;
        public readonly List<string> BundleNames;

        #endregion

        private AssetBundleDepData ()
        {
            this.AssetBundles = new List<AssetBundleData> ();
            this.NameToBundle = new Dictionary<string, AssetBundleData> ();
            this.BundleNames = new List<string> ();
            this.BundledAssets = new List<AssetData> ();
        }

        #region loading data

        public static AssetBundleDepData ReadDataFromUnity ()
        {
            AssetBundleDepData result = new AssetBundleDepData ();
            string[] allAssetBundles = AssetDatabase.GetAllAssetBundleNames ();

            // create nodes
            for (int i = 0; i < allAssetBundles.Length; ++i)
            {
                EditorUtility.DisplayProgressBar ("Reading asset bundle data",
                    string.Format ("({0}/{1}) {2}", i, allAssetBundles.Length, allAssetBundles[i]),
                    (float)i / allAssetBundles.Length);

                AssetBundleData curBundle = result.GetOrCreateBundle (allAssetBundles[i]);

                try
                {
                    result.FilterDependencies (curBundle);  
                    curBundle.BundledAssets.Sort (CompareBundledAssets);
                }
                catch (System.Exception e)
                {
                    Debug.LogErrorFormat ("Can't load data for bundle {0}. {1}", allAssetBundles[i], e);
                }

            }           
            EditorUtility.ClearProgressBar ();
            return result;
        }

        private void FilterDependencies (AssetBundleData bundle)
        {
            string[] pathsInBundle = AssetDatabase.GetAssetPathsFromAssetBundle (bundle.Name);
            string[] assetDeps = AssetDatabase.GetDependencies (pathsInBundle, false);

            Queue<string> assetToCheck = new Queue<string> (pathsInBundle);
            foreach (var item in assetDeps)
            {
                assetToCheck.Enqueue (item);
            }

            while (assetToCheck.Count > 0)
            {
                string assetPath = assetToCheck.Dequeue ();
                if (AssignAsset (assetPath, bundle))
                {
                    string[] newDeps = AssetDatabase.GetDependencies (assetPath, false);
                    for (int i = 1; i < newDeps.Length; ++i)
                    {
                        assetToCheck.Enqueue (newDeps[i]);                        
                    }
                }
            }              
        }

        private AssetBundleData GetOrCreateBundle (string bundleName)
        {            
            AssetBundleData result;
            if (!this.NameToBundle.TryGetValue (bundleName, out result))
            {
                result = new AssetBundleData (bundleName);
                this.AssetBundles.Add (result);

                this.NameToBundle.Add (bundleName, result);
                this.BundleNames.Add (bundleName);
            }

            return result;
        }

        private bool AssignAsset (string path, AssetBundleData bundle)
        {
            string assignedBundleName = AssetImporter.GetAtPath (path).assetBundleName;
            if (string.IsNullOrEmpty (assignedBundleName) || (assignedBundleName == bundle.Name))
            {
                // add to bundled
                if (!bundle.ContainsBundledAsset (path))
                {
                    AssetData asset = new AssetData (path, bundle, string.IsNullOrEmpty (assignedBundleName));
                    bundle.BundledAssets.Add (asset);
                    this.BundledAssets.Add (asset);

                    // continue search
                    return true;
                }
            }
            else
            {
                if (!bundle.ContainsChildDep (assignedBundleName))
                {
                    // add to deps
                    bundle.ChildDependencies.Add (GetOrCreateBundle (assignedBundleName));
                }
            }
            // end found stop search
            return false;
        }

        private static int CompareBundledAssets (AssetData data1, AssetData data2)
        {
            return data1.AssetType.CompareTo (data2.AssetType);
        }

        #endregion

        #region get logic

        public List<AssetData> GetBundledAssets (AssetDataType typeFilter, string nameFilter)
        {
            var result = new List<AssetData> ();
            bool nameFilterActive = !string.IsNullOrEmpty (nameFilter);
            if (nameFilterActive)
            {
                nameFilter = nameFilter.ToLower ();
            }

            for (int i = 0; i < this.BundledAssets.Count; ++i)
            {                
                AssetData element = this.BundledAssets[i];
                if (!typeFilter.Filter (element) ||
                    (nameFilterActive && !element.Name.ToLower ().Contains (nameFilter)))
                {
                    continue;
                }


                result.Add (element);                  
            }
            return result;
        }

        public List<AssetBundleData> GetBundels (AssetDataType typeFilter, string nameFilter)
        {
            if (!typeFilter.Contains (AssetDataType.Bundle))
            {
                return new List<AssetBundleData> ();
            }
                
            if (string.IsNullOrEmpty (nameFilter))
            {
                return new List<AssetBundleData> (this.AssetBundles);
            }

            nameFilter = nameFilter.ToLower ();

            var result = new List<AssetBundleData> ();
            for (int i = 0; i < this.AssetBundles.Count; ++i)
            {
                if (this.AssetBundles[i].Name.ToLower ().Contains (nameFilter))
                {
                    result.Add (this.AssetBundles[i]);
                }
            }

            return result;
        }

        #endregion
    }
}