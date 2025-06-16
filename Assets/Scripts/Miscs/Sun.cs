using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sunSprite;

        private void OnDestroy() => DOTween.Kill(this);

        public Sequence StartLifeTime()
        {
            float lifeTime = 7;

            float idleTime = 2 * lifeTime / 3;
            float timeRemain = lifeTime - idleTime;
            int loopAmount = 10;

            return DOTween.
                Sequence(this).
                AppendInterval(idleTime).
                Append(_sunSprite.
                    DOFade(0, timeRemain / loopAmount).
                    SetLoops(loopAmount, LoopType.Yoyo)).
                OnComplete(() => Destroy(gameObject));
        }

        public Tween MoveTo(Vector3 position, float velocity = 15) =>
            transform.
                DOMove(position, Vector3.Distance(transform.position, position) / velocity).
                SetId(this);

        public Sequence ToTheEnd(Vector3 position)
        {
            float velocity = 50;
            return DOTween.
                Sequence(this).
                Prepend(MoveTo(position, velocity)).
                PrependCallback(() => _sunSprite.color = Color.white).
                SetEase(Ease.Linear).
                OnComplete(() => Destroy(gameObject));
        }
    }
}
