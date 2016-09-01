using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepSideMenuBar : AEditorMenuBar<DependencyWindow>
    {
        protected GenericToolbarSearch search;

        protected override Color DebugColor
        {
            get
            {
                return Color.cyan;
            }
        }

        public AssetBundleDepSideMenuBar ( DependencyWindow parent, EditorWindowDimension percentageRect )
            : base (parent, percentageRect)
        {			
        }

        protected override void AddButtons ()
        {
            this.search = new GenericToolbarSearch ();
            this.search.TextChanged += this.parentWindow.SidebarSearchTextHasChanged;
            AddDrawable (this.search);

            //TODO filter button
        }

        public AssetDataType Filter
        {
            get
            {
                //TODO implement
                return (AssetDataType)int.MaxValue;
            }
            set
            {
                
            }
        }

    }
}
