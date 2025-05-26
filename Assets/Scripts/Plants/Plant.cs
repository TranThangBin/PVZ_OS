using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private int _plantCost;
        public int PlantCost { get => _plantCost; }

        private UnityEvent<GameObject> _onPlantAttack;

        public void AddPlantAttackListener(UnityAction<GameObject> listener)
        {
            if (_onPlantAttack == null)
            {
                _onPlantAttack = new UnityEvent<GameObject>();
            }
            _onPlantAttack.AddListener(listener);
        }

        public void InvokePlantAttackListener(GameObject projectile)
        {
            if (_onPlantAttack != null)
            {
                _onPlantAttack.Invoke(projectile);
            }
        }
    }
}
