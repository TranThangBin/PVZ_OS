using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        public float ZombieSpawnInterval;
        public BasicZombie BasicZombie;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() =>
            DOTween.
                Sequence(this).
                AppendInterval(ZombieSpawnInterval).
                AppendCallback(() =>
                {
                    int idx = Random.Range(0, transform.childCount);
                    Transform zombieParent = transform.GetChild(idx);
                    BasicZombie zombie = Instantiate(BasicZombie, zombieParent);
                    zombie.tag = zombieParent.tag;
                }).
                SetLoops(-1);
    }
}
