using UnityEngine;
using System;
using GJP.EditorToolkit;
using UnityEditor;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class ToolbarFilterButton : IEditorRectDrawable
    {
        public event Action<AssetDataType> FilterChanged;

        public AssetDataType Value;

        private Vector2 size;
        private GUIStyle guiStyle;


        public ToolbarFilterButton ()
        {
            this.guiStyle = EditorStyles.toolbarDropDown;
            this.Value = AssetDataTypeUtility.DefaultFilter;
            UpdateSize ();
        }

        #region IEditorRectDrawable implementation

        public Vector2 GetDimension ()
        {
            return this.size;
        }

        public void Draw (Rect drawRect)
        {
            AssetDataType newValue = (AssetDataType)EditorGUI.EnumMaskField (drawRect, GUIContent.none, this.Value, this.guiStyle);

            newValue = AssetDataTypeUtility.EnsureVisiblity (newValue, this.Value);

            if (newValue != this.Value)
            {
                this.Value = newValue;
                if (this.FilterChanged != null)
                {
                    this.FilterChanged (this.Value);
                }
            }
        }

        #endregion

        private void UpdateSize ()
        {
            this.size = this.guiStyle.CalcSize (GUIContent.none);
            this.size.x += 45f;
        }
    }
}