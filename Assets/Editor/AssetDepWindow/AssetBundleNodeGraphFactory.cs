using System.Collections.Generic;
using GJP.EditorToolkit;

namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNodeGraphFactory<T>
        where T : AEditorNodeGraph
    {
        public delegate T GraphCreationDel (IEditorPositionable parent, IEditorPositionable child);

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
                        result.Add (del (item, childNode));
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