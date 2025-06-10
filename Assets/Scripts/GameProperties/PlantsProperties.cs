using System;
using UnityEngine;

namespace Game
{
    using FumeshroomProperties = WeaponizedPlantProperties;
    using MelonPultProperties = WeaponizedPlantProperties;
    using PeashooterProperties = WeaponizedPlantProperties;
    using SplitPeaProperties = WeaponizedPlantProperties;

    [CreateAssetMenu(menuName = "Scriptable Objects/Plants Properties")]
    public class PlantsProperties : ScriptableObject
    {
        public SunflowerProperties Sunflower;
        public PeashooterProperties Peashooter;
        public SplitPeaProperties SplitPea;
        public FumeshroomProperties Fumeshroom;
        public MelonPultProperties MelonPult;
        public SquashProperties Squash;
        public JalapenoProperties Jalapeno;
        public PotatoMineProperties PotatoMine;
        public ChomperProperties Chomper;
    }

    [Serializable]
    public struct PlantProperties
    {
        public int Cost;
        public int Health;
        public float Cooldown;
        public Sprite Sprite;
    }

    [Serializable]
    public struct SunflowerProperties
    {
        public PlantProperties PlantProps;
        public float SunGenerateTime;
        public Sun Sun;
    }

    [Serializable]
    public struct WeaponizedPlantProperties
    {
        public PlantProperties PlantProps;
        public float VisionLength;
        public float AttackRechargeTime;
        public PlantWeapon Weapon;
    }

    [Serializable]
    public struct SquashProperties
    {
        public PlantProperties PlantProps;
        public float Damage;
        public float DelayTime;
        public float VisionLength;
        public float JumpForce;
    }

    [Serializable]
    public struct JalapenoProperties
    {
        public PlantProperties PlantProps;
        public float DelayTime;
        public PlantWeapon Fire;
    }

    [Serializable]
    public struct PotatoMineProperties
    {
        public PlantProperties PlantProps;
        public float Damage;
        public float GrowTime;
    }

    [Serializable]
    public struct ChomperProperties
    {
        public PlantProperties PlantProps;
        public float Damage;
        public float ChewTime;
        public float VisionLength;
    }
}
