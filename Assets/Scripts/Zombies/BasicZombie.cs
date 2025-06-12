using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(HealthManager))]
    public class BasicZombie : MonoBehaviour,
        HealthManager.IDestroyOnOutOfHealth, HealthManager.IHealthy
    {
        [SerializeField] private BasicZombieProperties _basicZombieProps;

        public int Health => _basicZombieProps.Hp;

        private Rigidbody2D _rb;

        private void OnDestroy() => DOTween.Kill(this);

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _rb.linearVelocity = _basicZombieProps.MovementSpeed * Vector2.left;

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayLength = _basicZombieProps.AttackRange;

                RaycastHit2D hit = Utils.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Ally"), Color.black);

                if (hit.collider == null) { return; }

                HealthManager plantHealth = hit.collider.GetComponentInParent<HealthManager>();

                if (plantHealth == null) { return; }

                _rb.linearVelocity = Vector2.zero;
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        plantHealth.ReduceHealth(_basicZombieProps.Damage);
                        if (plantHealth == null)
                        {
                            _rb.linearVelocity = _basicZombieProps.MovementSpeed * Vector2.left;
                            DOTween.Kill(this);
                        }
                    }).
                    AppendInterval(_basicZombieProps.AttackCooldown).
                    SetLoops(-1);
            }
        }
    }
}
