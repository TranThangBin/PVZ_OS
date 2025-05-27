using UnityEngine;

namespace Game
{
    public class SunflowerGenerateSunState : StateMachine.State
    {
        [SerializeField] private Sun _sun;

        private Plant _plant;

        public override string GetStateName()
        {
            return typeof(SunflowerGenerateSunState).ToString();
        }

        public override void StateEnter(params object[] parameters)
        {
            if (_plant == null)
            {
                _plant = GetComponent<Plant>();
            }
            Sun sun = Instantiate(_sun, transform.position, Quaternion.identity);
            _plant.InvokeChildInstantiateEvent(sun.gameObject);

            InvokeTransitionListener(this, typeof(SunflowerCooldownState).ToString());
        }
    }
}
