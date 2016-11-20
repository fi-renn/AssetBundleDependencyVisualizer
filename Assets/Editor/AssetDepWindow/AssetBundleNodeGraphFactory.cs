using System.Collections.Generic;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNodeGraphFactory<T>
        where T : AEditorNodeGraph
    {
        public delegate T GraphCreationDel ( IEditorPositionable parent, IEditorPositionable child );

        private GraphCreationDel del;

        public AssetBundleNodeGraphFactory (GraphCreationDel del)
        {
            this.del = del;
        }

        public List<T> GetVisibleGraphs (List<AssetBundleNode> visibleNodes)
        {
            List<T> result = new List<T> ();

            foreach (var item in visibleNodes)
            {
                foreach (var childDep in item.Data.ChildDependencies)
                {
                    AssetBundleNode childNode;
                    if (TryFindChildNode (visibleNodes, childDep.Name, out childNode))
                    {
                        T graph = del (item, childNode);
                        // even the parent has it child we draw from center to leaf
                        result.Add (graph);
                        if (item.RelationShip == AssetBundleNoteRelationship.Selected ||
                            childNode.RelationShip == AssetBundleNoteRelationship.Selected)
                        {
                            // found a relationship to selected, use visual anchor point
                            EditorWindowAnchor parentPoint, childPoint;
                            if (item.RelationShip == AssetBundleNoteRelationship.Selected)
                            {
                                NodeUtils.GetBestVisualAnchorPoints (item, childNode, out parentPoint, out childPoint);
                            }
                            else
                            {
                                NodeUtils.GetBestVisualAnchorPoints (childNode, item, out parentPoint, out childPoint);
                            }
                            graph.SetAnchorPoints (parentPoint, childPoint);
                        }
                    }
                }
            }

            return result;
        }

        private bool TryFindChildNode (List<AssetBundleNode> visibleNodes, string bundleName,
                                       out AssetBundleNode value)
        {
            value = null;
            foreach (var node in visibleNodes)
            {
                if (node.Data.Name == bundleName)
                {
                    value = node;
                    break;
                }
            }
            return value != null;
        }
    }
}