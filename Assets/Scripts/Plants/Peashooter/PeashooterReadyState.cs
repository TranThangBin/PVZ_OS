using UnityEngine;

namespace Game
{
    public class PeaShooterReadyState : StateMachine.State
    {
        public override string GetStateName()
        {
            return typeof(PeaShooterReadyState).ToString();
        }

        public override void StateFixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 100, LayerMask.GetMask("Enemy"));
            Debug.DrawRay(transform.position, Vector3.right * 100, Color.red);

            if (hit.collider != null)
            {
                InvokeTransitionListener(this, typeof(PeashooterAttackState).ToString());
            }
        }
    }
}
