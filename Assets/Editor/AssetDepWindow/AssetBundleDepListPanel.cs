using GJP.EditorToolkit;
using UnityEditor;
using UnityEngine;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepListPanel : AEditorWindowPanel
    {
        public AssetBundleDepListPanel(EditorWindow parent, EditorWindowDimension dimension)
            : base(parent, dimension)
        {
            
        }

        protected override Color DebugColor
        {
            get
            {
                return Color.blue;
            }
        }


        protected override void DrawContent()
        {
            
        }
    }
}

