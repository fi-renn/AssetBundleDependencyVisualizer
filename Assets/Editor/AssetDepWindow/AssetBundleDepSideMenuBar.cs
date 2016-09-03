using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepSideMenuBar : AEditorMenuBar<DependencyWindow>
    {
        protected GenericToolbarSearch search;
        protected ToolbarFilterButton filter;

        protected override Color DebugColor
        {
            get
            {
                return Color.cyan;
            }
        }

        public AssetBundleDepSideMenuBar (DependencyWindow parent, EditorWindowDimension percentageRect)
            : base (parent, percentageRect)
        {			
        }

        protected override void AddButtons ()
        {
            this.search = new GenericToolbarSearch ();
            this.search.TextChanged += this.parentWindow.SidebarSearchTextChanged;
            AddDrawable (this.search);

            this.filter = new ToolbarFilterButton ();
            this.filter.FilterChanged += this.parentWindow.SidebarFilterChanged;
            AddDrawable (this.filter);
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
