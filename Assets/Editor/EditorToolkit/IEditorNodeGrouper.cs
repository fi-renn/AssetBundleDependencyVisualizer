using UnityEngine;
using System.Collections.Generic;

namespace GJP.EditorToolkit
{
    public interface IEditorNodeGrouper
    {
        Rect GroupNodes<N> (List<N> nodes) where N : AEditorNode;
    }
}

