using GJP.EditorToolkit;
using UnityEngine;
using System.Collections.Generic;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleDepListPanel : AEditorWindowPanel<DependencyWindow>
    {
        #region member

        protected AssetDataType currentTypeFilter;
        protected string currentSearchFilter;
        protected bool needsUpdate = true;

        protected Vector2 scrollPosition;
        protected Rect scrollRect;
        protected List<AssetReferenceButton> filteredList;

        protected const float ElementHeight = 25f;

        #endregion

        public AssetBundleDepListPanel (DependencyWindow parent, EditorWindowDimension dimension)
            : base (parent, dimension)
        {   
            this.filteredList = new List<AssetReferenceButton> ();
            this.currentSearchFilter = string.Empty;
        }

        protected override Color DebugColor
        {
            get
            {
                return Color.blue;
            }
        }

        #region drawing

        protected override void DrawContent ()
        {
            if (this.needsUpdate)
            {
                UpdateFilterList ();
            }

            Rect elementRect = new Rect ();
            elementRect.width = this.drawRect.width;
            elementRect.height = ElementHeight;

            this.scrollPosition = GUI.BeginScrollView (this.drawRect, this.scrollPosition, this.scrollRect);

            for (int i = 0; i < this.filteredList.Count; ++i)
            {
                this.filteredList[i].Draw (elementRect);
                elementRect.y += ElementHeight;
            }

            GUI.EndScrollView ();
        }

        private void UpdateFilterList ()
        {
            this.filteredList.Clear ();

            var bundels = this.parentWindow.Data.GetBundels (this.currentTypeFilter, this.currentSearchFilter);
            var assets = this.parentWindow.Data.GetBundledAssets (this.currentTypeFilter, this.currentSearchFilter);

            System.Action<AssetBundleData> bundleCallback = this.parentWindow.SidebarBundleClicked;
            System.Action<AssetData> assetCallback = this.AssetClicked;

            // add bundles
            for (int i = 0; i < bundels.Count; ++i)
            {
                this.filteredList.Add (new AssetReferenceButton (bundels[i], bundleCallback));
            }

            // add filtered assets
            for (int i = 0; i < assets.Count; ++i)
            {
                this.filteredList.Add (new AssetReferenceButton (assets[i], assetCallback));
            }
            this.filteredList.Sort ();

            // calc rect
            UpdateScrollRect ();

            this.needsUpdate = false;
        }

        private void UpdateScrollRect ()
        {
            Vector2 size = this.drawRect.size;
            size.y = this.filteredList.Count * ElementHeight;
            this.scrollRect = new Rect (Vector2.zero, size);
        }

        protected override void OnRectRecalculated ()
        {            
            UpdateScrollRect ();
        }

        #endregion

        public void ApplyTypeFilter (AssetDataType filter)
        {
            if (this.currentTypeFilter == filter)
            {
                return;
            }

            this.currentTypeFilter = filter;
            this.needsUpdate = true;
        }

        public void ApplyNameFilter (string filter)
        {
            if (this.currentSearchFilter == filter)
            {
                return;
            }

            this.currentSearchFilter = filter;
            this.needsUpdate = true;
        }

        private void AssetClicked (AssetData data)
        {
            data.SelectAsset ();
            this.parentWindow.SidebarBundleClicked (data.Parent);
        }
    }
}