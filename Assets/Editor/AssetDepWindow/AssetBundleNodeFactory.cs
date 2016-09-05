using UnityEngine;
using System;
using System.Collections.Generic;


namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNodeFactory
    {
        public List<AssetBundleNode> GetNodes (AssetBundleData primaryData, AssetDataType filter, Action<AssetData> assetClickCallback)
        {
            int controlCounter = 0;

            var result = new List<AssetBundleNode> ();

            // create center
            result.Add (new AssetBundleNode (controlCounter++, primaryData, filter));

            // TODO create childs
            // TODO create parents
            // TODO set coloring
            // TODO recursive deps 
            // TODO add dep support

            for (int i = 0; i < result.Count; ++i)
            {
                result[i].AssetClicked += assetClickCallback;
                result[i].RecalcSize ();
            }

            return result;
        }
    }
}