using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepNodePanel : ANodeEditorPanel<DependencyWindow,
                                            AssetBundleNode, TreeNodeGrouper>
    {
        private readonly AssetBundleNodeFactory factory;
        private AssetDataType filter;
        private AssetBundleData curBundle;


        public AssetBundleDepNodePanel (DependencyWindow parent, EditorWindowDimension dimension)
            : base (parent, dimension)
        {      
            this.factory = new AssetBundleNodeFactory ();
        }

        protected override Color DebugColor
        {
            get
            {
                return Color.red;
            }
        }

        #region interface to window

        public void ApplyFilter (AssetDataType filter)
        {
            this.filter = filter;
            for (int i = 0; i < this.nodes.Count; ++i)
            {
                this.nodes[i].ApplyFilter (filter);
            }
        }

        public void SetBundleToFocus (AssetBundleData bundle)
        {
            if (this.curBundle == bundle)
            {
                return;
            }
            this.curBundle = bundle;

            ApplyNewNodes (factory.GetNodes (bundle, this.filter, AssetClicked));
        }

        private void AssetClicked (AssetData data)
        {
            data.SelectAsset ();
        }

        #endregion
    }
}  