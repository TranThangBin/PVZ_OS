using UnityEngine;

public class ZombieMoveState : StateMachine.State
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _velocity;

    public override string GetStateName()
    {
        return typeof(ZombieMoveState).ToString();
    }

    public override void StateEnter()
    {
        _rb.linearVelocity = _velocity * Vector2.left;
    }
}
