using UnityEngine;
using UnityEditor;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class DependencyWindow : APanelEditorWindow<DependencyWindow>
    {
        #region editor menu entry

        [MenuItem ("Assets/AssetBundle/Show dependencies")]   
        public static void OpenForEditor ()
        {
            DependencyWindow window = EditorWindow.GetWindow<DependencyWindow> ();
        }

        #endregion

        #region member

        public AssetBundleDepData Data;

        #endregion

        #region panels

        private AssetBundleDepMenuBar menuBarNode;
        private AssetBundleDepSideMenuBar menuBarList;
        private AssetBundleDepNodePanel nodePanel;
        private AssetBundleDepListPanel listPanel;

        #endregion

        protected override void InitPanels ()
        {           
            // menu bar
            EditorWindowDimension menuDimension = new EditorWindowDimension ()
            {
                HeightIsFixed = true,
                Height = 18f, 
                Width = 1f,
                AnchorPoint = EditorWindowAnchor.TopLeft,
            };
            this.menuBarNode = new AssetBundleDepMenuBar (this, menuDimension);
            this.panels.Add (this.menuBarNode);

            // node panel
            EditorWindowDimension nodePanelDimension = new EditorWindowDimension ()
            {
                AnchorPoint = EditorWindowAnchor.BottomLeft,
                OffsetYIsFixed = true,
                OffsetY = menuDimension.Height,
                Height = 1f,
                Width = 0.7f,
            };
            this.nodePanel = new AssetBundleDepNodePanel (this, nodePanelDimension);
            this.nodePanel.DebugMode = true;
            this.panels.Add (this.nodePanel);

            // menu bar
            EditorWindowDimension sidePanelMenuDimension = new EditorWindowDimension ()
            {
                HeightIsFixed = true,
                Height = 18f,
                Width = 0.3f,
                AnchorPoint = EditorWindowAnchor.TopRight,
            };
            this.menuBarList = new AssetBundleDepSideMenuBar (this, sidePanelMenuDimension);
            this.panels.Add (this.menuBarList);

            // side panel
            EditorWindowDimension sidepanelDimension = new EditorWindowDimension ()
            {
                OffsetYIsFixed = true,
                OffsetY = sidePanelMenuDimension.Height,
                AnchorPoint = EditorWindowAnchor.Right,
                Height = 1f,
                Width = 0.3f,
            };

            this.listPanel = new AssetBundleDepListPanel (this, sidepanelDimension);
            this.panels.Add (this.listPanel);

            RefreshBundleData ();
        }

        #region event listener

        public void RefreshBundleData ()
        {
            this.Data = AssetBundleDepData.ReadDataFromUnity ();
            this.listPanel.ApplyTypeFilter (this.menuBarList.Filter);
            this.nodePanel.ApplyFilter (this.menuBarNode.Filter);
        }

        public void SidebarSearchTextHasChanged (string newText)
        {
            Debug.Log ("Sidebar search: " + newText);
            this.listPanel.ApplyNameFilter (newText);
        }

        public void SidebarFilterHasChanged (AssetDataType newFilter)
        {
            Debug.Log ("Sidebar filter: " + newFilter.LogValue ());
            this.listPanel.ApplyTypeFilter (newFilter);
        }

        public void NodePanelFilterHasChanged (AssetDataType newFilter)
        {
            Debug.Log ("Nodes filter: " + newFilter.LogValue ());
            //TODO implement
        }

        public void NodePanelZoomHasChanged (float value)
        {
            Debug.Log ("Changed zoom to " + value);
            //TODO implement
        }

        public void SidebarBundleClicked (AssetBundleData data)
        {
            Debug.Log ("Show data of " + data.Name);
            //TODO implement
        }

        #endregion
    }
}
