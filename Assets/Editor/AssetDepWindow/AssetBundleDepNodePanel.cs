using UnityEditor;
using UnityEngine;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepNodePanel : AEditorWindowPanel
    {
        public AssetBundleDepNodePanel(EditorWindow parent, EditorWindowDimension dimension)
            : base(parent, dimension)
        {            
        }

        protected override UnityEngine.Color DebugColor
        {
            get
            {
                return Color.red;
            }
        }

        protected override void DrawContent()
        {

            //TODO implement
        }
    }
}

