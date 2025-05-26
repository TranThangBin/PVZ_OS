using NUnit.Framework;
using UnityEngine;

public class ZombieAttackState : StateMachine.State
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;

    private float _timer;

    public override string GetStateName()
    {
        return typeof(ZombieAttackState).ToString();
    }

    public override void StateEnter()
    {
        _timer = _attackCooldown;
    }

    public override void StateUpdate()
    {
        _timer -= Time.deltaTime;
    }

    public override void StateCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager healthManager) && _timer <= 0)
        {
            healthManager.ReduceHealth(_damage);
            _timer = _attackCooldown;
            if (healthManager.Hp <= 0)
            {
                InvokeTransitionListener(this, typeof(ZombieMoveState).ToString());
            }
        }
    }
}
