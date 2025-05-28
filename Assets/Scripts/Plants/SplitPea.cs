using System.Collections;
using UnityEngine;

namespace Game
{
    public class SplitPea : MonoBehaviour
    {
        private enum SplitPeashooterState { COOLDOWN, READY, ATTACK }

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Timer _timer;

        private SplitPeashooterState _state = SplitPeashooterState.COOLDOWN;

        private readonly Vector3[] _directions = new Vector3[] { Vector3.right, Vector3.left, Vector3.left };
        private readonly float[] _shootWaitTime = new float[] { 0.5f, 0.25f, 0.25f };

        private void Start()
        {
            _timer.TimerStart();
        }

        private void Update()
        {
            switch (_state)
            {
                case SplitPeashooterState.READY:
                    {
                        RaycastHit2D rc = Physics2D.Raycast(transform.position + Vector3.left * 100, Vector2.right, 200, LayerMask.GetMask("Enemy"));
                        Debug.DrawRay(transform.position + Vector3.left * 100, Vector3.right * 200, Color.red);
                        if (rc.collider != null)
                        {
                            _state = SplitPeashooterState.ATTACK;
                        }
                    }
                    break;
                case SplitPeashooterState.ATTACK:
                    {
                        StartCoroutine(Attack());

                        _state = SplitPeashooterState.COOLDOWN;
                        _timer.TimerRestart();
                    }
                    break;
                case SplitPeashooterState.COOLDOWN:
                    break;
                default:
                    throw new UnityException();
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
