using UnityEngine;

namespace Game
{
    public class EditorTimeUtils : MonoBehaviour
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
    }
}
