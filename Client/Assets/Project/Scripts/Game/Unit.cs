using System;
using Project.Scripts.States;
using UnityEngine;

namespace Project.Scripts
{
    [RequireComponent(typeof(UnitParameters), typeof(Health))]
    public class Unit : MonoBehaviour, IHealth, IDestroyed
    {
        public event Action Destroyed;
        [field: SerializeField] public Health Health { get; private set; } 
        [field: SerializeField] public bool IsPlayer;
        [field: SerializeField] public UnitParameters Parameters;
        [SerializeField] private UnitAnimation _animation;
        [SerializeField] private UnitState _defaultStateSO;
        [SerializeField] private UnitState _chaseStateSO;
        [SerializeField] private UnitState _attackStateSO;

        private UnitState _defaultState;
        private UnitState _chaseState;
        private UnitState _attackState;

        private UnitState _currentState;


        private void Start()
        {
            _animation.Init(this);
            
            CreateStates();

            _currentState = _defaultState;
            _currentState.Enter();
            
            Health.Death += OnDeath;
        }

        private void CreateStates()
        {
            _defaultState = Instantiate(_defaultStateSO);
            _defaultState.Construct(this);
            
            _chaseState = Instantiate(_chaseStateSO);
            _chaseState.Construct(this);
            
            _attackState = Instantiate(_attackStateSO);
            _attackState.Construct(this);
        }

        private void Update() => 
            _currentState?.Run();

        private void OnDeath()
        {
            Health.Death -= OnDeath;

            Destroy(gameObject);
            
            Destroyed?.Invoke();
        }

        public void SetState(UnitStateType stateType)
        {
            _currentState.Exit();
            
            switch (stateType)
            {
                case UnitStateType.Default:
                    _currentState = _defaultState;
                    break;
                case UnitStateType.Chase:
                    _currentState = _chaseState;
                    break;
                case UnitStateType.Attack:
                    _currentState = _attackState;
                    break;
                default:
                    //Debug.LogError("Invalid state type");
                    break;
            }
            
            _currentState.Enter();
            _animation.SetState(stateType);
        }

#if UNITY_EDITOR

        [Space(24)]
        [SerializeField] private bool _debug;

        private void OnDrawGizmos()
        {
            if (_debug == false)
                return;
            
            if (Application.isPlaying == false)
            {
                _defaultStateSO?.DebugDraw(this);
                _chaseStateSO?.DebugDraw(this);
                _attackStateSO?.DebugDraw(this);
            }
            else
            {
                _currentState?.DebugDraw(this);
            }
        }
#endif
    }
}
