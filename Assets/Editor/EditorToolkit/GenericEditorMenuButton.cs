using UnityEditor;
using UnityEngine;

namespace GJP.EditorToolkit
{
	public class GenericEditorMenuButton : IEditorRectDrawable
	{
		protected System.Action action;
		protected string text;
		protected Texture2D icon;
		protected Vector2 dimension;
		protected GUIStyle style;

		protected float iconWidth = 10f;

		public GenericEditorMenuButton (System.Action action, string text, Texture2D icon = null)
		{			
			this.action = action;
			this.text = text;
			this.icon = icon;
			this.style = EditorStyles.toolbarButton;
			CalculateDimension ();
		}

		private void CalculateDimension ()
		{
			this.dimension = style.CalcSize (new GUIContent (text));
			if (this.icon != null) {
				this.dimension.x += iconWidth;
			}
		}

		public Vector2 GetDimension ()
		{
			return this.dimension;
		}

		public void Draw (Rect drawRect)
		{
			if (this.icon != null) {
				Rect iconRect = drawRect;
				iconRect.width = iconWidth;
				drawRect.x += iconWidth;
				drawRect.width -= iconWidth;

				EditorGUI.DrawPreviewTexture (iconRect, this.icon);
			}

			if (GUI.Button (drawRect, this.text, this.style)) {
				if (this.action != null) {
					this.action ();
				}
			}
		}
	}
}

