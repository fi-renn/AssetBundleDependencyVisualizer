using UnityEngine;
using UnityEditor;

namespace GJP.EditorToolkit
{
    public class GenericToolbarSearch : IEditorRectDrawable
    {
        public event System.Action<string> TextChanged;

        public GUIStyle TextFieldStyle;
        public GUIStyle ClearButtonStyle;
        public GUIStyle EmptyButtonStyle;
        public string Text;

        protected const float SpaceToButton = 5f;
        protected const float ClearButtonSize = 5f;
        protected const float DefaultWidth = 50f;

        protected Vector2 dimension;

        public GenericToolbarSearch()
        {          
            this.TextFieldStyle = GUI.skin.GetStyle ("ToolbarSeachTextField");
            this.ClearButtonStyle = GUI.skin.GetStyle ("ToolbarSeachCancelButton");
            this.EmptyButtonStyle = GUI.skin.GetStyle ("ToolbarSeachCancelButtonEmpty");
            UpdateDimension ();
        }

        public Vector2 GetDimension()
        {
            return this.dimension;
        }

        public void Draw( Rect drawRect )
        {
            Rect textFieldRect, buttonRect;
            SplitRect (ref drawRect, out textFieldRect, out buttonRect);

            GUIStyle buttonStyle = !string.IsNullOrEmpty (this.Text) ? this.ClearButtonStyle : this.EmptyButtonStyle;

            string newText = EditorGUI.TextField (textFieldRect, this.Text, this.TextFieldStyle);

            if (GUI.Button (buttonRect, "", buttonStyle))
            {
                newText = string.Empty;
                GUI.FocusControl (null);
            }

            if (newText != this.Text)
            {
                this.Text = newText;
                UpdateDimension ();
                if (this.TextChanged != null)
                {
                    this.TextChanged (this.Text);
                }
            }
        }

        private void UpdateDimension()
        {
            this.dimension = this.TextFieldStyle.CalcSize (new GUIContent(this.Text));
            if (this.dimension.x < DefaultWidth)
            {
                this.dimension.x = DefaultWidth;
            }

            this.dimension.x += ClearButtonSize + SpaceToButton + 10f;
        }

        private void SplitRect( ref Rect drawRect,
                                out Rect textRect,
                                out Rect buttonRect )
        {
            // padding in toolbar
            drawRect.y += 2f;

            textRect = buttonRect = drawRect;
            buttonRect.width = ClearButtonSize;
            textRect.width -= buttonRect.width;
            buttonRect.x += textRect.width;
        }

    }
}

