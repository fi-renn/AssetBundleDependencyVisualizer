using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeTestWindow : EditorWindow
{
    [MenuItem ("Custom/Node editor")]   
    public static void OpenForEditor ()
    {
        NodeTestWindow window = EditorWindow.GetWindow<NodeTestWindow> ();
        window.EditorMode = true;
    }

    [MenuItem ("Custom/Assetbundel dep")]  
    public static void OpenForAssetsBundle ()
    {
        NodeTestWindow window = EditorWindow.GetWindow<NodeTestWindow> ();
        window.EditorMode = false;
    }

    #region member

    public bool EditorMode;

    [System.NonSerialized]
    private bool isSetup;

    public List<IEditorDrawable> Lines = new List<IEditorDrawable> ();
    public List<EditorNode> Windows = new List<EditorNode> ();
    public int selectedIndex = -1;


    protected NodeScrollPanel scrollPanel;
    protected NodeSidePanel sidePanel;

    protected bool canCreateNew;

    #endregion

    protected void InitForEditor ()
    {
        canCreateNew = true;
        this.name = "NodeTesting";
        this.titleContent = new GUIContent (this.name);

        // add dummy data
        this.Windows.Add (new EditorNode (new Vector3 (10, 10), new Vector2 (100, 100), 0, "laAsset"));
        this.Windows.Add (new EditorNode (new Vector3 (20, 250), new Vector2 (100, 100), 1, "laAsset2"));
        // TODO test lines
    }

    protected void InitForAssetBundles ()
    {
        canCreateNew = false;
        this.name = "AssetBundle dependency";
        this.titleContent = new GUIContent (this.name);
    }

    protected void InitComponents ()
    {
        this.scrollPanel = new NodeScrollPanel (this);
        this.sidePanel = new NodeSidePanel (this);
        this.scrollPanel.WindowSelected += this.OnScrollPanelIndexSelected;
        this.sidePanel.IndexSelected += this.OnSidePanelIndexSelected;
    }

    protected void OnGUI ()
    {
        if (!this.isSetup)
        {
            if (this.EditorMode)
            {
                InitForEditor ();
            }
            else
            {
                InitForAssetBundles ();
            }
            InitComponents ();
            this.isSetup = true;
        }

        Rect contentRect, sidebarRect;
        GetMainLayoutRects (out contentRect, out sidebarRect);
        // debug
        DrawDebugLayout (contentRect, Color.red);
        DrawDebugLayout (sidebarRect, Color.green);

        this.scrollPanel.Draw (contentRect);
        this.sidePanel.Draw (sidebarRect);
    }

    private const float SidebarWidthPercentage = 0.20f;

    private void GetMainLayoutRects (out Rect content, out Rect sidebar)
    {
        float sidebarWidth = this.position.width * SidebarWidthPercentage;
        float generalHeight = this.position.height;
        float contentWidth = this.position.width - sidebarWidth;

        content = new Rect (0, 0, contentWidth, generalHeight);
        sidebar = new Rect (contentWidth, 0, sidebarWidth, generalHeight);
    }

    private void SelectIndex (int index)
    {
        if (index >= this.Windows.Count)
        {
            Debug.LogError ("Can't select index " + index + " it is out of range!");
            return;
        }

        this.selectedIndex = index;
        this.scrollPanel.SelectIndex (index);
        this.sidePanel.SelectIndex (index);
    }
    
    // debug methods
    private void DrawDebugLayout (Rect rect, Color color)
    {
        EditorGUI.DrawRect (rect, color);
    }

    #region component listener

    private void OnSidePanelIndexSelected (int index)
    {
        Debug.Log ("OnSidePanelIndexSelected " + index);
        SelectIndex (index);
    }

    private void OnScrollPanelIndexSelected (int index)
    {
        Debug.Log ("OnScrollPanelIndexSelected " + index);
        SelectIndex (index);
    }

    #endregion
}
