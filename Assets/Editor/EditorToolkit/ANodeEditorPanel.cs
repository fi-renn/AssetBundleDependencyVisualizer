using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public abstract class ANodeEditorPanel<P,N, S> : AEditorWindowPanel<P> 
        where P : APanelEditorWindow<P>
        where N : AEditorNode
        where S : IEditorNodeGrouper, new()
    {
        protected Texture2D background;
        protected List<N> nodes;
        protected S grouper;
        protected Vector2 scrollPosition;

        private Rect scrollRect;
        private Rect texRect;

        protected ANodeEditorPanel (P parent, EditorWindowDimension dimension)
            : base (parent, dimension)
        {            
            this.background = AssetDatabase.LoadAssetAtPath<Texture2D> ("Assets/Editor/background.png");
            this.background.wrapMode = TextureWrapMode.Repeat;
            this.nodes = new List<N> ();
            this.grouper = new S ();
        }

        protected override void DrawContent ()
        {
            if (!this.DebugMode)
            {
                // background
                GUI.DrawTextureWithTexCoords (this.drawRect, this.background, texRect);
            }

            this.scrollPosition = GUI.BeginScrollView (this.drawRect, this.scrollPosition, this.scrollRect);

            this.parentWindow.BeginWindows ();
            for (int i = 0; i < this.nodes.Count; ++i)
            {
                this.nodes[i].Draw ();
            }
            this.parentWindow.EndWindows ();

            GUI.EndScrollView ();
        }

        protected void ApplyNewNodes (List<N> newNodes)
        {
            this.nodes.Clear ();
            this.scrollRect = this.grouper.GroupNodes (newNodes);
            this.nodes.AddRange (newNodes);
        }

        protected void GroupNodes ()
        {
            this.scrollRect = this.grouper.GroupNodes (this.nodes);
        }

        protected override void OnRectRecalculated ()
        {
            texRect = new Rect (Vector2.zero, this.drawRect.size / background.width);
        }

    }
}