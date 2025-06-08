using UnityEngine;

namespace Game
{
    public abstract class ProjectileValues<T> : ScriptableObject
    {
        [SerializeField] private T _pea;

        public T GetValue(ProjectileID projectileID) => projectileID switch
        {
            ProjectileID.Pea => _pea,
            _ => throw new UnityException(),
        };
    }
}
