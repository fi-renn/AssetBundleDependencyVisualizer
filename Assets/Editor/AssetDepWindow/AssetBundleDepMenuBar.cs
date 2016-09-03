using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepMenuBar : AEditorMenuBar<DependencyWindow>
    {
        protected ToolbarFilterButton filter;

        protected override Color DebugColor
        {
            get
            {
                return Color.cyan;
            }
        }

        public AssetBundleDepMenuBar (DependencyWindow parent, EditorWindowDimension percentageRect)
            : base (parent, percentageRect)
        {
        }

        protected override void AddButtons ()
        {
            AddDrawable (new GenericEditorMenuButton (this.parentWindow.RefreshBundleData, "Refresh"));
            this.filter = new ToolbarFilterButton ();
            this.filter.FilterChanged += this.parentWindow.NodePanelFilterChanged ();
            AddDrawable (this.filter);
            //TODO zoom button
        }

        public AssetDataType Filter
        {
            get
            {
                return this.filter.Value;
            }
            set
            {
                this.filter.Value = value;
            }
        }
    }
}
