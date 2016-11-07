using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepNodePanel : ANodeEditorPanel<DependencyWindow,
                                            AssetBundleNode, DirectNodeGraph>
    {
        private readonly AssetBundleNodeFactory nodeFactory;
        private readonly AssetBundleNodeGraphFactory<DirectNodeGraph> graphFactory;
        private AssetDataType filter;
        private AssetBundleData curBundle;

        public AssetBundleDepNodePanel (DependencyWindow parent, EditorWindowDimension dimension)
            : base (parent, dimension)
        {      
            this.nodeFactory = new AssetBundleNodeFactory ();
            this.graphFactory = new AssetBundleNodeGraphFactory<DirectNodeGraph> (
                (p, c) => new DirectNodeGraph (p, c));
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

        public void ForceRefresh ()
        {
            RefreshNodes ();
        }

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
            ApplyNewNodes (nodeFactory.GetNodes (this.curBundle, this.filter, AssetClicked, this.drawRect.center));              
            ApplyNewGraphs (graphFactory.GetVisibleGraphs (this.nodes));
        }
    }
}  