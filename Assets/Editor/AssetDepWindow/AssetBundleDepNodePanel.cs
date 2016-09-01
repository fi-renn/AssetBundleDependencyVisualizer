using UnityEditor;
using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepNodePanel : AEditorWindowPanel<DependencyWindow>
    {
        public AssetBundleDepNodePanel ( DependencyWindow parent, EditorWindowDimension dimension )
            : base (parent, dimension )
        {            
        }

        protected override Color DebugColor
        {
            get
            {
                return Color.red;
            }
        }

        protected override void DrawContent ()
        {

            //TODO implement
        }

        public void ApplyFilter ( AssetDataType filter )
        {
            //TODO implement
        }
    }
}

