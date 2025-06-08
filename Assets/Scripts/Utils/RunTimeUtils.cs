using UnityEngine;

namespace Game
{
    public class RunTimeUtils : MonoBehaviour
    {
        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask, Color color)
        {
            Debug.DrawRay(origin, direction, color);
            return Physics2D.Raycast(origin, direction, distance, layerMask);
        }
    }
}
