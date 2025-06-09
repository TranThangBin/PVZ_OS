using UnityEngine;

namespace Game
{
    public abstract class PlantWeaponValues<T> : ScriptableObject
    {
        [SerializeField] private T _pea;
        [SerializeField] private T _fire;
        [SerializeField] private T _melon;

        public T GetValue(PlantWeaponID plantWeaponID) => plantWeaponID switch
        {
            PlantWeaponID.Pea => _pea,
            PlantWeaponID.Fire => _fire,
            PlantWeaponID.Melon => _melon,
            _ => throw new UnityException(),
        };
    }
}
