using UnityEngine;

namespace Game
{
    public interface ISelectable
    {
        bool CanSelect(SunManager sunManager);
        bool ActionOnLocation(Transform location, SunManager sunManager);
    }
}