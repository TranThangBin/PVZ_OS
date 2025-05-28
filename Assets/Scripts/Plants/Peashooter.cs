using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class Peashooter : MonoBehaviour
    {
        private enum PeashooterState { COOLDOWN, READY, ATTACK }

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Timer _timer;

        private PeashooterState _state = PeashooterState.COOLDOWN;

        public void Start()
        {
            _timer.TimerStart();
        }

        public void Update()
        {
            switch (_state)
            {
                case PeashooterState.READY:
                    {
                        RaycastHit2D rc = Physics2D.Raycast(transform.position, Vector2.right, 100, LayerMask.GetMask("Enemy"));
                        Debug.DrawRay(transform.position, Vector3.right * 100, Color.red);
                        if (rc.collider != null)
                        {
                            _state = PeashooterState.ATTACK;
                        }
                    }
                    break;
                case PeashooterState.ATTACK:
                    {
                        GameObject gameObject = Instantiate(_projectile, transform.position + (Vector3.right / 2), Quaternion.identity, transform.parent);
                        IProjectile projectile = gameObject.GetComponent<IProjectile>();
                        projectile.Fire(Vector2.right);
                        _state = PeashooterState.COOLDOWN;

                        _timer.TimerRestart();
                    }
                    break;
                case PeashooterState.COOLDOWN:
                    break;
                default:
                    throw new UnityException();
            }
        }

        public void OnTimerTimeOut()
        {
            _state = PeashooterState.READY;
        }
    }
}
