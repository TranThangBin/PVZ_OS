using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class BasicZombie : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private ZombieID _zombieID;
        [SerializeField] private ZombieVelocities _zombieVelocities;
        [SerializeField] private ZombieDamages _zombieDamages;
        [SerializeField] private ZombieChargeTimes _zombieChargeTimes;
        [SerializeField] private Rigidbody2D _rb;

        private void Start()
        {
            _rb.linearVelocity = _zombieVelocities.GetValue(_zombieID) * Vector2.left;
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayDistance = 3.5f;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, LayerMask.GetMask("Ally"));
                Debug.DrawRay(transform.position, Vector2.left * rayDistance, Color.black);
                if (hit.collider != null)
                {
                    HealthManager plantHealth = hit.collider.GetComponentInParent<HealthManager>();
                    _rb.linearVelocity = Vector2.zero;
                    DOTween.
                        Sequence(this).
                        AppendCallback(() =>
                        {
                            plantHealth.ReduceHealth(_zombieDamages.GetValue(_zombieID));
                            if (plantHealth.IsOutOfHealth())
                            {
                                _rb.linearVelocity = _zombieVelocities.GetValue(_zombieID) * Vector2.left;
                                DOTween.Pause(this);
                            }
                        }).
                        AppendInterval(_zombieChargeTimes.GetValue(_zombieID)).
                        SetLoops(-1);
                }
            }
        }

        public void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            sender.BlinkSpriteColor(sr);
        }
    }
}
