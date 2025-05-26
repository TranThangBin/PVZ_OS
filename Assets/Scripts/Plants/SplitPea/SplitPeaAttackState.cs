using System.Collections;
using UnityEngine;

namespace Game
{
    public class SplitPeaAttackState : StateMachine.State
    {
        [SerializeField] private GameObject _projectile;

        private Vector3[] _directions = new Vector3[] { Vector3.right, Vector3.left, Vector3.left };
        private float[] _shootWaitTime = new float[] { 0.5f, 0.25f, 0.25f };

        public override string GetStateName()
        {
            return typeof(SplitPeaAttackState).ToString();
        }

        public override void StateEnter()
        {
            StartCoroutine(_attack());
            InvokeTransitionListener(this, typeof(SplitPeaCooldownState).ToString());
        }

        private IEnumerator _attack()
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                GameObject gameObject = Instantiate(_projectile, transform.position + _directions[i], Quaternion.identity);
                IProjectile projectile = gameObject.GetComponent<IProjectile>();
                projectile.Fire(_directions[i]);
                yield return new WaitForSeconds(_shootWaitTime[i]);
            }
        }
    }
}