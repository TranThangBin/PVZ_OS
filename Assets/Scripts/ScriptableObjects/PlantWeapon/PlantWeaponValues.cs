using UnityEngine;

namespace Game
{
    public abstract class PlantWeaponValues<T> : ScriptableObject
    {
        [SerializeField] private T _pea;
        [SerializeField] private T _fire;

        public T GetValue(PlantWeaponID plantWeaponID) => plantWeaponID switch
        {
            PlantWeaponID.Pea => _pea,
            PlantWeaponID.Fire => _fire,
            _ => throw new UnityException(),
        };
    }
}
