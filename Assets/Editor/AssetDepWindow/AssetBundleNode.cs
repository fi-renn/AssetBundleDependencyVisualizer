using UnityEngine;
using GJP.EditorToolkit;
using UnityEditor;
using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNode : AEditorNode
    {
        private AssetBundleData data;
        private AssetDataType filter;
        private List<AssetData> toDraw;

        //TODO support coloring
        public AssetBundleNode (int controlId, AssetBundleData data, AssetDataType filter)
            : base (controlId, data.Name, new GUIStyle ("flow node 1"))
        { 
            this.data = data;
            this.filter = filter;
            this.toDraw = new List<AssetData> ();
        }

        protected override void DrawNode (int windowId)
        {
        }

        protected override Vector2 CalcSize ()
        {
            Vector2 result = Vector2.zero;

            // TODO calc size, also width


            return result;
        }

        public void ApplyFilter (AssetDataType filter)
        {
            this.filter = filter;
            UpdateDrawList ();
            RecalcSize ();
        }

        private void UpdateDrawList ()
        {
            // TODO generate button list
        }
    }
}