using UnityEngine;

namespace GJP.EditorToolkit
{
    public abstract class AEditorNode : IEditorPositionable, IEditorDrawable
    {
        #region member

        public readonly int ControlId;
        public readonly Vector2 Position;

        #endregion

        protected AEditorNode(int controlId, Vector2 position)
        {
            this.ControlId = controlId;
            this.Position = position;
        }

        public abstract void Draw();

        public Vector2 GetPosition(EditorWindowAnchor border)
        {
            return Position;
            /*
            Vector2 rectCenter = this.currentRect.center;
            Vector3 result = new Vector3(rectCenter.x, rectCenter.y);
            switch (border)
            {
                case EditorPositionBorder.Top:
                    result.y += this.currentRect.height;
                    break;

                case EditorPositionBorder.Left:
                    result.x -= this.currentRect.width;
                    break;

                case EditorPositionBorder.Right:
                    result.x += this.currentRect.width;
                    break;

                case EditorPositionBorder.Bottom:
                    result.y -= this.currentRect.height;
                    break;           
            }

            return result;
            */
        }
    }
}
