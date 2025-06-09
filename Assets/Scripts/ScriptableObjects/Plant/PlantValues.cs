using UnityEngine;

namespace Game
{
    public abstract class PlantValues<T> : ScriptableObject
    {
        [SerializeField] private T _sunFlower;
        [SerializeField] private T _peaShooter;
        [SerializeField] private T _splitPea;
        [SerializeField] private T _squash;
        [SerializeField] private T _japaleno;
        [SerializeField] private T _potatoMine;
        [SerializeField] private T _melonPult;
        [SerializeField] private T _chomper;

        public T GetValue(PlantID plantID) => plantID switch
        {
            PlantID.SunFlower => _sunFlower,
            PlantID.PeaShooter => _peaShooter,
            PlantID.SplitPea => _splitPea,
            PlantID.Squash => _squash,
            PlantID.Japaleno => _japaleno,
            PlantID.PotatoMine => _potatoMine,
            PlantID.MelonPult => _melonPult,
            PlantID.Chomper => _chomper,
            _ => throw new UnityException(),
        };
    }
}