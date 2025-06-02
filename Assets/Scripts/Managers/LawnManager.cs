using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LawnManager : MonoBehaviour
    {
        public UnityEvent<Transform> OnLawnCellClick = new();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell", "Sun"));

                if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Sun"))
                {
                    OnLawnCellClick.Invoke(hit.collider.transform);
                }
            }
        }
    }
}
