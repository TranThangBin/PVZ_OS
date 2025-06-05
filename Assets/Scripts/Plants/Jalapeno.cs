using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Jalapeno : Plant
    {
        [SerializeField] private float _damage;

        private void Start()
        {
            float killRange = 500;
            DOTween.
                Sequence(this).
                AppendInterval(1).
                AppendCallback(() =>
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.left * killRange / 2, Vector3.right, killRange, LayerMask.GetMask("Enemy"));
                    Debug.DrawRay(transform.position + Vector3.left * killRange / 2, Vector3.right * killRange, Color.red);
                    foreach (var hit in hits)
                    {
                        hit.collider.GetComponent<HealthManager>().ReduceHealth(_damage);
                    }
                    Destroy(gameObject);
                });
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}
