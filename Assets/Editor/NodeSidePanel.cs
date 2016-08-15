using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace ChinchillaCoding.AssetBundleDependencyVisualizer
{
    public class NodeSidePanel
    {
        protected AssetBundleDepVisWindow parentWindow;
        protected Vector2 scrollPosition;

        #region cache values

        protected Rect contentRect;
        protected Rect labelRect;
        protected Rect backgroundRect;
        protected Rect itemRect;

        #endregion

        public event System.Action<int> IndexSelected;

        private const float BorderSize = 5f;

        public NodeSidePanel(AssetBundleDepVisWindow parentWindow)
        {
            this.parentWindow = parentWindow;
        }

        public void Draw(Rect contentRect)
        {        
            //TODO add filter
            
            this.contentRect = contentRect;
            this.labelRect = GetLabelRect();
            this.backgroundRect = GetBackgroundRect();
            
            Rect viewRect = GetCompleteViewRect();
            
            //this.scrollPosition = GUI.BeginScrollView (contentRect, this.scrollPosition, viewRect);
            
            // header
            EditorGUI.LabelField(this.labelRect, "Nodes:");
            
            // boxBackground
            EditorGUI.DrawRect(this.backgroundRect, Color.white);
            
            // list
            this.itemRect = GetItemRect();
            for (int i = 0; i < this.parentWindow.Windows.Count; ++i)
            {
                EditorGUI.DrawRect(this.itemRect, Color.yellow);
                if (GUI.Button(this.itemRect, new GUIContent(this.parentWindow.Windows[i].Name)))
                {
                    Debug.Log("Pressed index " + i);
                }
                
                this.itemRect.y += EditorGUIUtility.singleLineHeight;
            }
            
            //GUI.EndScrollView ();
        }

        public void SelectIndex(int index)
        {
            //TODO search for hitlist when filter is implemented
            //TODO implement scrollPositon
        }

        private float GetHeight()
        {
            // header
            
            // content
            
            return 0f;
        }

        private Rect GetCompleteViewRect()
        {
            return new Rect();
            
        }

        private Rect GetLabelRect()
        {
            return new Rect(
                this.contentRect.x + BorderSize, 
                this.contentRect.y + BorderSize,
                this.contentRect.width,
                EditorGUIUtility.singleLineHeight);
        }

        private Rect GetBackgroundRect()
        {
            return new Rect(
                this.labelRect.x,
                this.labelRect.y + EditorGUIUtility.singleLineHeight,
                this.contentRect.width - (BorderSize * 2),
                (this.parentWindow.Windows.Count * EditorGUIUtility.singleLineHeight) + (2f * BorderSize));
        }

        private Rect GetItemRect()
        {
            Rect result = backgroundRect;
            result.y += BorderSize;
            result.height = EditorGUIUtility.singleLineHeight;
            result.x += BorderSize;
            result.width -= BorderSize * 2f;
            
            return result;
        }
    }
}