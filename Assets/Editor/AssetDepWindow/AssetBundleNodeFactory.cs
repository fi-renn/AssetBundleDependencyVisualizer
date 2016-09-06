using UnityEngine;
using System;
using System.Collections.Generic;


namespace GJP.AssetBundleDependencyVisualizer
{
    public class AssetBundleNodeFactory
    {
        private AssetBundleData primaryData;
        private AssetDataType filter;
        private Action<AssetData> assetClickCallback;
        private int controlCounter;
        private Vector2 mapCenter;

        private List<AssetBundleNode> parents, childs;
        private AssetBundleNode selected;

        private const float MinDistance = 80f;

        public List<AssetBundleNode> GetNodes (AssetBundleData primaryData, AssetDataType filter, Action<AssetData> assetClickCallback, Vector2 mapCenter)
        {
            if (primaryData == null)
            {
                return new List<AssetBundleNode> ();
            }

            controlCounter = 0;
            this.primaryData = primaryData;
            this.filter = filter;
            this.assetClickCallback = assetClickCallback;
            this.mapCenter = mapCenter;

            var result = new List<AssetBundleNode> ();

            // create nodes
            SplitNodes ();
            selected.CenterPosition = mapCenter;

            result.Add (selected);
            result.AddRange (parents);
            result.AddRange (childs);

            PositionNodes (parents, 180f);
            PositionNodes (childs, 0f);

            // TODO recursive deps 
            // TODO add dep support           

            return result;
        }

        private void SplitNodes ()
        {
            selected = CreateNode (primaryData, AssetBundleNoteRelationship.Selected); 
            parents = new List<AssetBundleNode> ();
            childs = new List<AssetBundleNode> ();

            foreach (var parentBundle in primaryData.ParentDependencies)
            {
                parents.Add (CreateNode (parentBundle, AssetBundleNoteRelationship.Parent));
            }

            foreach (var childBundle in primaryData.ChildDependencies)
            {
                childs.Add (CreateNode (childBundle, AssetBundleNoteRelationship.Child));
            }
        }

        private AssetBundleNode CreateNode (AssetBundleData data, AssetBundleNoteRelationship relation)
        {
            var result = new AssetBundleNode (controlCounter++, data, filter, relation);
            result.AssetClicked += assetClickCallback;
            result.RecalcSize ();
            return result;
        }

        private void PositionNodes (List<AssetBundleNode> nodes, float startAngel)
        {
            if (nodes.Count == 0)
            {
                return;
            }

            // find max size
            float maxSize = 0f;
            foreach (var item in nodes)
            {
                maxSize = Mathf.Max (maxSize, item.GetSize ().magnitude);
            }
            // extra padding for low numbers
            if (nodes.Count < 3)
            {
                maxSize += 15f;                
            }

            // length we need to position
            float lenght = maxSize * nodes.Count;

            // 2 PI r = U || r = U / (PI*2)  
            // but we only want a half circle 
            float radius = lenght / Mathf.PI;

            // keep a minimal distance
            radius = Mathf.Max (radius, MinDistance);

            // We don't start at angle 0 and need to
            // count one extra
            int count = nodes.Count;

            // unity calcs in radiant (180° / count)
            float stepSize = Mathf.PI / count;

            startAngel = Mathf.Deg2Rad * startAngel;

            for (int i = 0; i < nodes.Count; ++i)
            {
                float step = (stepSize * (i)) + startAngel;
                step += (stepSize / 2f);
                Vector2 position = new Vector2 (Mathf.Cos (step), Mathf.Sin (step));
                position *= radius;
                nodes[i].CenterPosition = position + mapCenter;
            }
        }
    }
}