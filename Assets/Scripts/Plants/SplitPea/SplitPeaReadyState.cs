using UnityEngine;

namespace Game
{
    public class SplitPeaReadyState : StateMachine.State
    {
        public override string GetStateName()
        {
            return typeof(SplitPeaReadyState).ToString();
        }

        public override void StateFixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left * 100, Vector3.right, 200, LayerMask.GetMask("Enemy"));
            Debug.DrawRay(transform.position + Vector3.left * 100, Vector3.right * 200, Color.red);

            if (hit.collider != null)
            {
                InvokeTransitionListener(this, typeof(SplitPeaAttackState).ToString());
            }
        }
    }
}
