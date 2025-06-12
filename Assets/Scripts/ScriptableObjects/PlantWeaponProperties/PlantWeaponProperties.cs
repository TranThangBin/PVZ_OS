using UnityEngine;

namespace Game
{
    [System.Serializable]
    public struct PlantWeaponProperties
    {
        [SerializeField] private int _damage;
        [SerializeField] private bool _destroyOnImpact;

        public readonly int Damage => _damage;
        public readonly bool DestroyOnImpact => _destroyOnImpact;
    }
}
