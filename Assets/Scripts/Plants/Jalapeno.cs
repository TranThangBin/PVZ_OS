using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class Jalapeno : MonoBehaviour
    {
        [SerializeField] private JalapenoProps _jalapenoProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() => Invoke(nameof(Attack), _jalapenoProps.DelayTime);

        private void Attack()
        {
            Weapon weapon = Instantiate(_jalapenoProps.Fire, transform.parent);
            weapon.gameObject.layer = gameObject.layer;
            weapon.tag = tag;
            Destroy(gameObject);
        }
    }
}
