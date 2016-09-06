using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepNodePanel : ANodeEditorPanel<DependencyWindow,
                                            AssetBundleNode>
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

        protected override void OnRectRecalculated ()
        {
            base.OnRectRecalculated ();
            RefreshNodes ();
        }

        #region interface to window

        public void ApplyFilter (AssetDataType filter)
        {
            if (this.filter != filter)
            {
                this.filter = filter;  
                RefreshNodes ();
            }
        }

        public void SetBundleToFocus (AssetBundleData bundle)
        {
            if (this.curBundle == bundle)
            {
                return;
            }
            this.curBundle = bundle;

            RefreshNodes ();
        }

        private void AssetClicked (AssetData data)
        {
            data.SelectAsset ();
        }

        #endregion

        private void RefreshNodes ()
        {
            ApplyNewNodes (factory.GetNodes (this.curBundle, this.filter, AssetClicked, this.drawRect.center));              
        }
    }
}  