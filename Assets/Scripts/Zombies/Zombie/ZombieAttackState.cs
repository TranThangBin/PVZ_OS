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

        public override void StateEnter(params object[] parameters)
        {
            _timer = _attackCooldown;

            Assert.IsTrue(parameters.Length == 1);
            Assert.IsTrue(parameters[0] is HealthManager);

            _plantHealth = (HealthManager)parameters[0];
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

        public override void StateCollisionExit2D(Collision2D collision)
        {
            InvokeTransitionListener(this, typeof(ZombieMoveState).ToString());
        }
    }
}