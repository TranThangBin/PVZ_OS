using System.Collections;
using UnityEngine;

namespace Game
{
    public class SplitPea : Plant
    {
        private enum SplitPeashooterState { COOLDOWN, READY, ATTACK }

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Timer _rechargeTimer;

        private SplitPeashooterState _state = SplitPeashooterState.COOLDOWN;

        private readonly Vector3[] _directions = new Vector3[] { Vector3.right, Vector3.left, Vector3.left };
        private readonly float[] _shootWaitTime = new float[] { 0.5f, 0.25f, 0.25f };

        private void Start()
        {
            _rechargeTimer.TimerStart();
        }

        private void Update()
        {
            switch (_state)
            {
                case SplitPeashooterState.ATTACK:
                    {
                        StartCoroutine(Attack());

                        _state = SplitPeashooterState.COOLDOWN;
                        _rechargeTimer.TimerRestart();
                    }
                    break;
                case SplitPeashooterState.READY:
                case SplitPeashooterState.COOLDOWN:
                    break;
                default:
                    throw new UnityException();
            }
        }

        private void FixedUpdate()
        {
            if (_state == SplitPeashooterState.READY)
            {
                float rayDistance = 200;
                RaycastHit2D rc = Physics2D.Raycast(transform.position + Vector3.left * rayDistance / 2, Vector2.right, rayDistance, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position + Vector3.left * rayDistance / 2, Vector3.right * rayDistance, Color.red);
                if (rc.collider != null)
                {
                    _state = SplitPeashooterState.ATTACK;
                }
            }
        }

        public void OnTimerTimeOut()
        {
            _state = SplitPeashooterState.READY;
        }

        private IEnumerator Attack()
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                GameObject gameObject = Instantiate(_projectile, transform.position + (_directions[i] / 2), Quaternion.identity, transform.parent);

                IProjectile projectile = gameObject.GetComponent<IProjectile>();
                projectile.Fire(_directions[i]);

                yield return new WaitForSeconds(_shootWaitTime[i]);
            }
        }
    }
}
