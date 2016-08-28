using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepMenuBar : AEditorMenuBar<DependencyWindow>
    {
        protected override Color DebugColor
        {
            get
            {
                return Color.cyan;
            }
        }

        public AssetBundleDepMenuBar(DependencyWindow parent, EditorWindowDimension percentageRect)
            : base (parent, percentageRect )
        {
        }

        protected override void AddButtons()
        {
            AddDrawable (new GenericEditorMenuButton(this.parentWindow.RefreshBundleData, "Refresh"));
            //TODO filter button
            //TODO zoom button
        }
    }
}
