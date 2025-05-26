using UnityEngine;

public class ZombieMoveState : StateMachine.State
{
    [SerializeField] private float _velocity;

    private Rigidbody2D _rb;

    public override string GetStateName()
    {
        return typeof(ZombieMoveState).ToString();
    }

    public override void StateEnter()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        _rb.linearVelocity = _velocity * Vector2.left;
    }

    public override void StateCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            _rb.linearVelocity = Vector2.zero;
            InvokeTransitionListener(this, typeof(ZombieAttackState).ToString());
        }
    }
}
