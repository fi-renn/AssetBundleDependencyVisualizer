using UnityEngine;
using GJP.EditorToolkit;
using UnityEditor;
using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNode : AEditorNode
    {
        public event System.Action<AssetData> AssetClicked;

        private AssetBundleData data;
        private AssetDataType filter;
        private List<GUIContent> toDraw;
        private List<AssetData> filteredAssets;
        private GUIStyle assetStyle;
        private Vector2 buttonSize;

        private const float ButtonHeight = 20f;

        //TODO support coloring
        public AssetBundleNode (int controlId, AssetBundleData data, AssetDataType filter)
            : base (controlId, data.Name, new GUIStyle ("flow node 1"))
        { 
            this.data = data;
            this.filter = filter;
            this.toDraw = new List<GUIContent> ();
            this.filteredAssets = new List<AssetData> ();
            this.assetStyle = new GUIStyle ("button");

            UpdateDrawList ();
        }

        protected override void DrawNode (int windowId)
        {
            Rect buttonRect = this.drawRect;
            buttonRect.height = buttonSize.y;

            for (int i = 0; i < this.toDraw.Count; ++i)
            {
                if (GUI.Button (buttonRect, this.toDraw[i]))
                {
                    if (this.AssetClicked != null)
                    {
                        this.AssetClicked (this.filteredAssets[i]);
                    }
                }
                buttonRect.y += buttonSize.y;
            }
        }

        protected override Vector2 CalcSize ()
        {
            this.buttonSize = Vector2.zero;

            foreach (var item in this.toDraw)
            {
                this.buttonSize = Vector2.Max (this.assetStyle.CalcSize (item), this.buttonSize);
            }
            this.buttonSize.y = ButtonHeight;
            Vector2 result = this.buttonSize;

            result.y *= this.toDraw.Count;
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
            this.toDraw.Clear ();
            this.filteredAssets.Clear ();

            foreach (var asset in this.data.BundledAssets)
            {
                // TODO maybe sort by type
                if (this.filter.Filter (asset))
                {
                    this.toDraw.Add (new GUIContent (asset.Name, asset.Icon));
                    this.filteredAssets.Add (asset);
                }
            }
        }
    }
}