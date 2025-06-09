using UnityEngine;

namespace Game
{
    public abstract class PlantWeaponValues<T> : ScriptableObject
    {
        [SerializeField] private T _pea;
        [SerializeField] private T _fire;
        [SerializeField] private T _melon;
        [SerializeField] private T _fume;

        public T GetValue(PlantWeaponID plantWeaponID) => plantWeaponID switch
        {
            PlantWeaponID.Pea => _pea,
            PlantWeaponID.Fire => _fire,
            PlantWeaponID.Melon => _melon,
            PlantWeaponID.Fume => _fume,
            _ => throw new UnityException(),
        };
    }

    public enum PlantWeaponID
    {
        Pea,
        Fire,
        Melon,
        Fume,
    }
}
