using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class PotatoMine : MonoBehaviour
    {
        [SerializeField] private PotatoMineProps _potatoMineProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() =>
            DOTween.
                Sequence(this).
                AppendInterval(_potatoMineProps.PreparationTime).
                AppendCallback(() =>
                {
                    Weapon weapon = Instantiate(_potatoMineProps.Armed, transform.parent);
                    weapon.gameObject.layer = gameObject.layer;
                    weapon.tag = tag;
                    Destroy(gameObject);
                });
    }
}
