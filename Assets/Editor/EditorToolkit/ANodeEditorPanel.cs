using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public abstract class ANodeEditorPanel<P,N> : AEditorWindowPanel<P> 
        where P : APanelEditorWindow<P>
        where N : AEditorNode
    {
        protected Texture2D background;
        protected List<N> nodes;
        protected Vector2 scrollPosition;

        private Rect scrollRect;
        private Rect texRect;

        protected ANodeEditorPanel (P parent, EditorWindowDimension dimension)
            : base (parent, dimension)
        {            
            this.background = EditorGUIUtility.Load ("NodeEditor/background.png") as Texture2D;
            if (this.background == null)
            {
                this.background = Texture2D.whiteTexture;
            }
            this.background.wrapMode = TextureWrapMode.Repeat;
            this.nodes = new List<N> ();
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
            this.nodes.AddRange (newNodes);
            this.scrollRect = NodeUtils.PutNodesOnRect (this.nodes);
        }

        protected void UpdateScrollRect ()
        {
            this.scrollRect = NodeUtils.PutNodesOnRect (this.nodes);
        }

        protected override void OnRectRecalculated ()
        {
            texRect = new Rect (Vector2.zero, this.drawRect.size / background.width);
        }

    }
}