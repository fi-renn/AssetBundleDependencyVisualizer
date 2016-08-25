using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepMenuBar : AEditorMenuBar
    {
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
            DependencyWindow window = (DependencyWindow) this.ParentWindow;
            AddDrawableToList(new GenericEditorMenuButton(window.RefreshBundleData, "Refresh"));
            //TODO filter button
            //TODO zoom button
        }
    }
}
