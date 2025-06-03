using UnityEngine;

namespace Game
{
    public class Peashooter : Plant
    {
        private enum PeashooterState { COOLDOWN, READY, ATTACK }

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Timer _rechargeTimer;

        private PeashooterState _state = PeashooterState.COOLDOWN;

        private void Start()
        {
            _rechargeTimer.TimerStart();
        }

        private void Update()
        {
            switch (_state)
            {
                case PeashooterState.ATTACK:
                    {
                        GameObject gameObject = Instantiate(_projectile, transform.position + (Vector3.right / 2), Quaternion.identity, transform.parent);
                        IProjectile projectile = gameObject.GetComponent<IProjectile>();
                        projectile.Fire(Vector2.right);

                        _state = PeashooterState.COOLDOWN;
                        _rechargeTimer.TimerRestart();
                    }
                    break;
                case PeashooterState.READY:
                case PeashooterState.COOLDOWN:
                    break;
                default:
                    throw new UnityException();
            }
        }

        private void FixedUpdate()
        {
            if (_state == PeashooterState.READY)
            {
                RaycastHit2D rc = Physics2D.Raycast(transform.position, Vector2.right, 100, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position, Vector3.right * 100, Color.red);
                if (rc.collider != null)
                {
                    _state = PeashooterState.ATTACK;
                }
            }
        }

        public void OnTimerTimeOut()
        {
            _state = PeashooterState.READY;
        }
    }
}
