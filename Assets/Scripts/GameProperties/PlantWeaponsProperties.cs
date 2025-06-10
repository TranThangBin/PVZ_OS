using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapons Properties")]
    public class PlantWeaponsProperties : ScriptableObject
    {
        public PeaProperties Pea;
        public FireProperties Fire;
        public MelonProperties Melon;
        public FumeProperties Fume;
    }

    [Serializable]
    public struct PlantWeaponProperties
    {
        public bool DestroyOnCollision;
        public int Damage;
    }

    [Serializable]
    public struct PeaProperties
    {
        public PlantWeaponProperties PlantWeaponProps;
        public float FlySpeed;
    }

    [Serializable]
    public struct FireProperties
    {
        public PlantWeaponProperties PlantWeaponProps;
    }

    [Serializable]
    public struct MelonProperties
    {
        public PlantWeaponProperties PlantWeaponProps;
        public float FlySpeed;
        public float ThrowForce;
    }

    [Serializable]
    public struct FumeProperties
    {
        public PlantWeaponProperties PlantWeaponProps;
    }
}
