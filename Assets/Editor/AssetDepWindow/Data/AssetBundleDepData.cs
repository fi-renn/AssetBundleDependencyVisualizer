using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepData
    {
        #region testing code

        [MenuItem ("Assets/Get Asset Bundle names")]
        public static void GetNames()
        {
            var names = AssetDatabase.GetAllAssetBundleNames ();
            foreach (string name in names)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.AppendLine ("Assetbundle: " + name);
                var paths = AssetDatabase.GetAssetPathsFromAssetBundle (name);
                foreach (string path in paths)
                {
                    builder.AppendLine ("\t" + path);
                    var deps = AssetDatabase.GetDependencies (path, false);
                    foreach (string depPath  in deps)
                    {
                        if (depPath == path)
                            continue;

                        AssetImporter importer = AssetImporter.GetAtPath (depPath);
                        builder.AppendLine ("\t\t[" + importer.assetBundleName + "] " + depPath);                       
                    }
                }
                Debug.Log (builder);
            }
        }

        [MenuItem ("Assets/Testing")]
        public static void TestTypes()
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object> ("Assets/Example/Prefab1.prefab");
            Object conky = AssetDatabase.LoadAssetAtPath<Object> ("Assets/Example/conky.png");

            Debug.Log ("obj :" + obj.GetType ().Name);
            Debug.Log ("conky: " + conky.GetType ().Name);

        }

        [MenuItem ("Assets/Build bundles")]
        public static void BuildBundles()
        {
            BuildPipeline.BuildAssetBundles ("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
           
        }

        #endregion

        #region member

        public readonly List<AssetBundleData> AssetBundles;
        public readonly List<AssetData> BundledAssets;

        public readonly Dictionary<string, AssetBundleData> NameToBundle;
        public readonly List<string> BundleNames;

        #endregion

        private AssetBundleDepData()
        {
            this.AssetBundles = new List<AssetBundleData>();
            this.NameToBundle = new Dictionary<string, AssetBundleData>();
            this.BundleNames = new List<string>();
            this.BundledAssets = new List<AssetData>();
        }

        #region loading data

        public static AssetBundleDepData ReadDataFromUnity()
        {
            AssetBundleDepData result = new AssetBundleDepData();
            string[] allAssetBundles = AssetDatabase.GetAllAssetBundleNames ();

            // create nodes
            foreach (string assetBundleName in allAssetBundles)
            {
                AssetBundleData curBundle = result.GetOrCreateBundle (assetBundleName);

                result.FilterDependencies (curBundle);   

                curBundle.BundledAssets.Sort (CompareBundledAssets);
            }                
            return result;
        }

        private void FilterDependencies( AssetBundleData bundle )
        {
            string[] pathsInBundle = AssetDatabase.GetAssetPathsFromAssetBundle (bundle.Name);
            string[] assetDeps = AssetDatabase.GetDependencies (pathsInBundle, false);

            Queue<string> assetToCheck = new Queue<string>(assetDeps);
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

        private AssetBundleData GetOrCreateBundle( string bundleName )
        {            
            AssetBundleData result;
            if (!this.NameToBundle.TryGetValue (bundleName, out result))
            {
                result = new AssetBundleData(bundleName);
                this.AssetBundles.Add (result);

                this.NameToBundle.Add (bundleName, result);
                this.BundleNames.Add (bundleName);
            }

            return result;
        }

        private bool AssignAsset( string path, AssetBundleData bundle )
        {
            bool result = false;
            string assignedBundleName = AssetImporter.GetAtPath (path).assetBundleName;
            if (string.IsNullOrEmpty (assignedBundleName) || (assignedBundleName == bundle.Name))
            {
                // add to bundled
                if (!bundle.ContainsBundledAsset (path))
                {
                    AssetData asset = new AssetData(path, bundle, string.IsNullOrEmpty (assignedBundleName));
                    bundle.BundledAssets.Add (asset);
                    this.BundledAssets.Add (asset);

                    // continue search
                    result = true;
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

            return result;
        }

        private static int CompareBundledAssets( AssetData data1, AssetData data2 )
        {
            return data1.AssetType.CompareTo (data2.AssetType);
        }

        #endregion

        #region get logic

        public List<AssetData> GetBundledAssets( AssetDataType filter )
        {
            var result = new List<AssetData>();

            for (int i = 0; i < this.BundledAssets.Count; ++i)
            {
                if (filter.Matches (this.BundledAssets[i]))
                {
                    result.Add (this.BundledAssets[i]);
                }
            }
            return result;
        }

        #endregion
    }
}