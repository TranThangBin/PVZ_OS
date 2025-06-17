using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class Jalapeno : MonoBehaviour
    {
        [SerializeField] private JalapenoProps _jalapenoProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() => DOTween.
            Sequence(this).
            Append(transform.DOShakePosition(_jalapenoProps.DelayTime,
                vibrato: 20, fadeOut: false)).
            Join(transform.DOScale(1.3f, _jalapenoProps.DelayTime)).
            AppendCallback(() =>
            {
                Weapon weapon = Instantiate(_jalapenoProps.Fire, transform.parent);
                weapon.gameObject.layer = gameObject.layer;
                weapon.tag = tag;
                Destroy(gameObject);
            });
    }
}
