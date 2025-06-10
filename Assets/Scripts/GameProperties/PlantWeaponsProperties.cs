using System;

namespace Game
{
    [Serializable]
    public struct PlantWeaponsProperties
    {
        public PeaProperties Pea;
        public FireProperties Fire;
        public MelonProperties Melon;
        public FumeProperties Fume;
    }

    [Serializable]
    public struct PeaProperties
    {
        public int Damage;
        public float FlySpeed;
    }

    [Serializable]
    public struct FireProperties
    {
        public int Damage;
    }

    [Serializable]
    public struct MelonProperties
    {
        public int Damage;
        public float FlySpeed;
        public float ThrowForce;
    }

    [Serializable]
    public struct FumeProperties
    {
        public int Damage;
    }
}
