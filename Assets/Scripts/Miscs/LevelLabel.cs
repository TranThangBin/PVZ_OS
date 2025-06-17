using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LevelLabel : MonoBehaviour
    {
        [SerializeField] private PlayerLevels _playerLevels;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            TextMeshPro tmpro = GetComponent<TextMeshPro>();
            tmpro.text = $"Level {_playerLevels.CurrentLevel}";
            tmpro.DOFade(0, 5).OnComplete((() => Destroy(gameObject)));
        }
    }
}
