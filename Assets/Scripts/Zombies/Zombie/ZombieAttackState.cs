using NUnit.Framework;
using UnityEngine;

namespace Game
{
    public class ZombieAttackState : StateMachine.State
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;

        private HealthManager _plantHealth;

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
            if (_timer <= 0)
            {
                _plantHealth.ReduceHealth(_damage);
                _timer = _attackCooldown;
                if (_plantHealth.Hp <= 0)
                {
                    InvokeTransitionListener(this, typeof(ZombieMoveState).ToString());
                }
            }
        }

        public override void StateCollisionStay2D(Collision2D collision)
        {
            _plantHealth = collision.gameObject.GetComponent<HealthManager>();
        }
    }
}