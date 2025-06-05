using UnityEngine;

namespace Game
{
    public class ZombieValues<T> : ScriptableObject
    {
        [SerializeField] private T _basicZombie;

        public T GetValue(ZombieID zombieID)
        {
            return zombieID switch
            {
                ZombieID.BasicZombie => _basicZombie,
                _ => throw new UnityException(),
            };
        }
    }
}
