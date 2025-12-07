using UnityEngine;

namespace Project.Scripts.States
{
    [CreateAssetMenu(fileName = "Empty", menuName = "UnitState/Empty")]
    public class EmptyUnitState : UnitState
    {
        public override void Enter()
        {
            _unit.SetState(UnitStateType.Default);
        }

        public override void Run()
        {
        }

        public override void Exit()
        {
            Debug.LogWarning($"{_unit.name} был в пустом состоянии");
        }
    }
}