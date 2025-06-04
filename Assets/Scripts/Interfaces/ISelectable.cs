using UnityEditor;
using UnityEngine;

namespace Game
{
    public interface ISelectable : ILawnAction
    {
        void SetSelected(bool isSelected);
    }
}