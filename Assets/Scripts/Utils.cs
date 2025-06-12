using UnityEngine;

namespace Game
{
    public class Utils : MonoBehaviour
    {
        public static float CalculateTime(Vector2 origin, Vector2 target, float velocity)
        {
            float edge1 = Mathf.Abs(origin.y - target.y);
            float edge2 = Mathf.Abs(origin.x - target.x);
            float distance = Mathf.Sqrt(edge1 * edge1 + edge2 * edge2);
            return distance / velocity;
        }
    }
}
