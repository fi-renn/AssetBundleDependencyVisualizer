using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public abstract class ANodeEditorPanel<P,N,G> : AEditorWindowPanel<P> 
        where P : APanelEditorWindow<P>
        where N : AEditorNode
        where G : AEditorNodeGraph
    {
        protected Texture2D background;
        protected List<N> nodes;
        protected List<G> graphs;
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
            this.graphs = new List<G> ();
        }

        protected override void DrawContent ()
        {
            if (!this.DebugMode)
            {
                // background
                GUI.DrawTextureWithTexCoords (this.drawRect, this.background, texRect);
            }

            this.scrollPosition = GUI.BeginScrollView (this.drawRect, this.scrollPosition, this.scrollRect);

            // draw dependencies
            for (int i = 0; i < this.graphs.Count; ++i)
            {
                this.graphs[i].Draw ();
            }


            // draw nodes
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

        protected void ApplyNewGraphs (List<G> newGraphs)
        {
            this.graphs.Clear ();
            this.graphs.AddRange (newGraphs);
            foreach (var item in this.graphs)
            {
                item.UpdateCachedPositions ();
            }
        }

        protected void UpdateScrollRect ()
        {
            this.scrollRect = NodeUtils.PutNodesOnRect (this.nodes);
            foreach (var item in this.graphs)
            {
                item.UpdateCachedPositions ();
            }
        }

        protected override void OnRectRecalculated ()
        {
            texRect = new Rect (Vector2.zero, this.drawRect.size / background.width);
        }

    }
}