using UnityEngine;

namespace Game
{
    public class Utils : MonoBehaviour
    {
        public static void CleanupChildren(Transform target)
        {
            GameObject[] children = new GameObject[target.childCount];
            for (int i = 0; i < target.childCount; i++)
            {
                children[i] = target.GetChild(i).gameObject;
            }
            foreach (var child in children)
            {
                DestroyImmediate(child);
            }
        }

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask, Color color)
        {
            Debug.DrawRay(origin, direction * distance, color);
            return Physics2D.Raycast(origin, direction, distance, layerMask);
        }

        public static float CalculateTime(Vector2 origin, Vector2 target, float velocity)
        {
            float edge1 = Mathf.Abs(origin.y - target.y);
            float edge2 = Mathf.Abs(origin.x - target.x);
            float distance = Mathf.Sqrt(edge1 * edge1 + edge2 * edge2);
            return distance / velocity;
        }
    }
}
