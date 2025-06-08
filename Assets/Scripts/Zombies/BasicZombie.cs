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
        [SerializeField] private ZombieRanges _zombieRanges;
        [SerializeField] private Rigidbody2D _rb;

        private void Start() => _rb.linearVelocity = _zombieVelocities.GetValue(_zombieID) * Vector2.left;
        private void OnDestroy() => DOTween.Kill(this);

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayLength = _zombieRanges.GetValue(_zombieID);

                RaycastHit2D hit = RunTimeUtils.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Ally"), Color.black);

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
                                DOTween.Kill(this);
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
