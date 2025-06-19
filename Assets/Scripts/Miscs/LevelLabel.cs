using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LevelLabel : MonoBehaviour
    {
        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            TextMeshPro tmpro = GetComponent<TextMeshPro>();
            tmpro.text = $"Level {PlayerLevels.CurrentLevel}";
            tmpro.DOFade(0, 5).OnComplete((() => Destroy(gameObject)));
        }
    }
}
