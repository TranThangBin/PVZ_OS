using UnityEngine;

namespace Game
{
    public class PeashooterAttackState : StateMachine.State
    {
        [SerializeField] private GameObject _projectile;

        private Plant _plant;

        public override string GetStateName()
        {
            return typeof(PeashooterAttackState).ToString();
        }

        public override void StateEnter()
        {
            if (_plant == null)
            {
                _plant = GetComponent<Plant>();
            }

            GameObject gameObject = Instantiate(_projectile, transform.position + (Vector3.right / 2), Quaternion.identity);
            _plant.InvokePlantAttackListener(gameObject);

            IProjectile projectile = gameObject.GetComponent<IProjectile>();
            projectile.Fire(Vector2.right);

            InvokeTransitionListener(this, typeof(PeashooterCooldownState).ToString());
        }
    }
}