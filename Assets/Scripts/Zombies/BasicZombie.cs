using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicZombie : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private ZombiesProperties _zombiesProps;

        private BasicZombieProperties BasicZombieProps => _zombiesProps.BasicZombie;
        private Rigidbody2D _rb;

        private void OnDestroy() => DOTween.Kill(this);

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start()
        {
            _rb.linearVelocity = BasicZombieProps.MovementSpeed * Vector2.left;
            gameObject.AddComponent<HealthManager>().InitHealth(BasicZombieProps.Health);
        }

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayLength = BasicZombieProps.AttackRange;

                RaycastHit2D hit = Utils.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Ally"), Color.black);

                if (hit.collider == null) { return; }

                HealthManager plantHealth = hit.collider.GetComponentInParent<HealthManager>();

                if (plantHealth == null) { return; }

                _rb.linearVelocity = Vector2.zero;
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        plantHealth.ReduceHealth(BasicZombieProps.AttackDamage);
                        if (plantHealth.IsOutOfHealth())
                        {
                            _rb.linearVelocity = BasicZombieProps.MovementSpeed * Vector2.left;
                            DOTween.Kill(this);
                        }
                    }).
                    AppendInterval(BasicZombieProps.AttackCooldown).
                    SetLoops(-1);
            }
        }

        public void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            sender.BlinkSpriteColor(sr);
        }
    }
}
