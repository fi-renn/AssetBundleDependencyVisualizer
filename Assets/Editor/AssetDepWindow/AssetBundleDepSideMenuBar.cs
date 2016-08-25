using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepSideMenuBar : AEditorMenuBar
    {
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
            DependencyWindow window = (DependencyWindow) this.ParentWindow;
            //TODO search box
            // AddDrawableToList(new GenericEditorMenuButton(window.RefreshBundleData, "Refresh"));
            //TODO filter button
        }
    }
}
