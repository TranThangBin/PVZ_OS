using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State _activeState;

        private Dictionary<string, State> _states;

        public void Awake()
        {
            _states = new Dictionary<string, State>();

            foreach (State state in GetComponents<State>())
            {
                if (!_states.ContainsKey(state.GetStateName()))
                {
                    _states.Add(state.GetStateName(), state);
                    state.AddOnTransitionListener(_onStateTransition);
                }
            }
        }

        public void Start()
        {
            _activeState.StateEnter();
        }

        public void Update()
        {
            _activeState.StateUpdate();
        }

        public void FixedUpdate()
        {
            _activeState.StateFixedUpdate();
        }

        private void _onStateTransition(State state, string newStateName, params object[] parameters)
        {
            Assert.IsTrue(state == _activeState, $"Something is wrong {_activeState.GetStateName()} " +
                $"does not match with transition state {state.GetStateName()}");
            Assert.IsTrue(_states.ContainsKey(newStateName), $"Something is wrong {newStateName} " +
                $"does not exists in {name}'s states");

            State newState = _states[newStateName];

            if (_activeState != null)
            {
                _activeState.StateExit();
            }

            _activeState = newState;
            newState.StateEnter(parameters);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            _activeState.StateCollisionEnter2D(collision);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            _activeState.StateCollisionStay2D(collision);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            _activeState.StateCollisionExit2D(collision);
        }

        public abstract class State : MonoBehaviour
        {
            private UnityEvent<State, string, object[]> _onTransition;

            public abstract string GetStateName();

            public virtual void StateEnter(params object[] parameters) { }
            public virtual void StateUpdate() { }
            public virtual void StateFixedUpdate() { }
            public virtual void StateLateUpdate() { }
            public virtual void StateExit() { }
            public virtual void StateCollisionEnter2D(Collision2D collision) { }
            public virtual void StateCollisionStay2D(Collision2D collision) { }
            public virtual void StateCollisionExit2D(Collision2D collision) { }

            public void AddOnTransitionListener(UnityAction<State, string, object[]> listener)
            {
                if (_onTransition == null)
                {
                    _onTransition = new UnityEvent<State, string, object[]>();
                }
                _onTransition.AddListener(listener);
            }

            public void InvokeTransitionListener(State state, string newStateName, params object[] parameters)
            {
                if (_onTransition != null)
                {
                    _onTransition.Invoke(state, newStateName, parameters);
                }
            }
        }
    }

}