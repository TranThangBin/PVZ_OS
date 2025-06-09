using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] float _lifeTime;
        [SerializeField] private SpriteRenderer _sunSprite;

        private void OnDestroy() => DOTween.Kill(this);

        public Sequence StartLifeTime()
        {
            float idleTime = 2 * _lifeTime / 3;
            float timeRemain = _lifeTime - idleTime;
            int loopAmount = 10;

            DOTween.Kill(this);

            return DOTween.Sequence(this).
                AppendInterval(idleTime).
                Append(_sunSprite.
                    DOFade(0, timeRemain / loopAmount).
                    SetLoops(loopAmount, LoopType.Yoyo)).
                OnComplete(() => Destroy(gameObject));
        }

        public Sequence ToTheEnd()
        {
            DOTween.Kill(this);
            return DOTween.Sequence(this).
                AppendCallback(() => _sunSprite.color = Color.white).
                OnComplete(() => Destroy(gameObject));
        }
    }
}
