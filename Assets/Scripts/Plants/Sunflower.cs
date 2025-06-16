using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class Sunflower : MonoBehaviour
    {
        [SerializeField] private SunflowerProps _sunflowerProps;

        private void Start() => InvokeRepeating(nameof(SpawnSun),
            _sunflowerProps.SunGenerateInterval, _sunflowerProps.SunGenerateInterval);

        private void SpawnSun()
        {
            Sun sun = Instantiate(_sunflowerProps.Sun, transform.parent);
            sun.
                StartLifeTime().
                Prepend(sun.transform.DOJump(transform.position, 2, 1, 1));
        }
    }
}
