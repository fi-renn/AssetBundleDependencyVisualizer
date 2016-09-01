using UnityEngine;
using UnityEditor;
using GJP.EditorToolkit;
using System;

namespace GJP.AssetBundleDependencyVisualizer
{
    public sealed class AssetReferenceButton : IEditorRectDrawable, IComparable<AssetReferenceButton>
    {
        private AssetBundleData bundle;
        private AssetData assetData;
        private Action<AssetBundleData> bundleCallback;
        private Action<AssetData> assetCallback;

        private GUIStyle style = new GUIStyle (GUI.skin.button);
        private GUIContent guiContent;

        public AssetReferenceButton ( AssetBundleData bundle, Action<AssetBundleData> callback )
        {
            this.bundle = bundle;
            this.bundleCallback = callback;
            this.guiContent = new GUIContent (bundle.Name, EditorGUIUtility.FindTexture ("Folder Icon"));
            this.style.alignment = TextAnchor.MiddleLeft;
        }

        public AssetReferenceButton ( AssetData assetData, Action<AssetData> callback )
        {
            this.assetData = assetData;
            this.assetCallback = callback;
            this.guiContent = new GUIContent (assetData.PreviewString, assetData.Icon);

            this.style.alignment = TextAnchor.MiddleLeft;
           
            if (assetData.AssetType.Contains (AssetDataType.Hidden))
            {
                this.style.fontStyle = FontStyle.BoldAndItalic;
                this.style.normal.textColor = new Color (0.3f, 0.46f, 0.6f);
            }
        }

        public Vector2 GetDimension ()
        {
            return this.style.CalcSize (this.guiContent);
        }

        public void Draw (Rect drawRect)
        {
            if (GUI.Button (drawRect, this.guiContent, this.style))
            {
                if (this.bundleCallback != null)
                {
                    this.bundleCallback (this.bundle);
                }

                if (this.assetCallback != null)
                {
                    this.assetCallback (this.assetData);
                }
            }
        }

        public int CompareTo (AssetReferenceButton other)
        {
            return this.guiContent.text.CompareTo (other.guiContent.text);
        }
    }
}

