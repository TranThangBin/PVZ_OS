using UnityEngine;

namespace Game
{
    public class Squash : MonoBehaviour
    {
        private void FixedUpdate()
        {
            float rayDistance = 2;
            RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.left * rayDistance / 2, Vector3.right, rayDistance, LayerMask.GetMask("Enemy"));
            Debug.DrawRay(transform.position - Vector3.left * rayDistance / 2, Vector3.right * rayDistance, Color.red);
            if (hit.collider != null)
            {

            }
        }
    }
}
