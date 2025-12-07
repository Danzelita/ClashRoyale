using Project.Scripts.States;
using UnityEngine;

namespace Project.Scripts
{
    public class UnitAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

        public void Init(Unit unit) => 
            _animator.SetFloat(AttackSpeed, 1f / unit.Parameters.DamageDelay);

        public void SetState(UnitStateType stateType) => 
            _animator.SetInteger(State, (int)stateType);
    }
}