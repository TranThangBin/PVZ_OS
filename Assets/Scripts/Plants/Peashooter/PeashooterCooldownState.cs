using UnityEngine;

namespace Game
{
    public class PeashooterCooldownState : StateMachine.State
    {
        [SerializeField] private float _cooldown;

        private float _timer;

        public override string GetStateName()
        {
            return typeof(PeashooterCooldownState).ToString();
        }

        public override void StateEnter()
        {
            _timer = _cooldown;
        }

        public override void StateUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                InvokeTransitionListener(this, typeof(PeashooterAttackState).ToString());
            }
        }
    }

}