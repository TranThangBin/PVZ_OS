using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private int _plantCost;
        public int PlantCost { get => _plantCost; }

        private UnityEvent<GameObject> _onChildInstantiate;

        public void AddChildInstiateListener(UnityAction<GameObject> listener)
        {
            if (_onChildInstantiate == null)
            {
                _onChildInstantiate = new UnityEvent<GameObject>();
            }
            _onChildInstantiate.AddListener(listener);
        }

        public void InvokeChildInstantiateEvent(GameObject child)
        {
            if (_onChildInstantiate != null)
            {
                _onChildInstantiate.Invoke(child);
            }
        }
    }
}
