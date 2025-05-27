using UnityEngine;

namespace Game
{
    public class SplitPeaCooldownState : StateMachine.State
    {
        [SerializeField] private float _cooldown;

        private float _timer;

        public override string GetStateName()
        {
            return typeof(SplitPeaCooldownState).ToString();
        }

        public override void StateEnter(params object[] parameters)
        {
            _timer = _cooldown;
        }

        public override void StateUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                InvokeTransitionListener(this, typeof(SplitPeaReadyState).ToString());
            }
        }
    }
}