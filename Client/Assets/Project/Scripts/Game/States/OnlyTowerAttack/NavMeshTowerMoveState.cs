using UnityEngine;

namespace Project.Scripts.States.OnlyTowerAttack
{
    [CreateAssetMenu(fileName = "_NavMeshTowerMoveState", menuName = "UnitState/" + nameof(NavMeshTowerMoveState))]
    public class NavMeshTowerMoveState : UnitStateNavMeshMove
    {
        protected override bool TryFindTarget(out UnitStateType changeType)
        {
            if (_nearestTower == null)
            {
                changeType = UnitStateType.None;
                return true;
            }
            float distance = _nearestTower.GetDistance(_unit.transform.position);
            if (distance < _unit.Parameters.StartAttackDistance)
            {
                changeType = UnitStateType.Attack;
                return true;
            }
            else
            {
                changeType = UnitStateType.None;
                return false;
            }
        }
    }
}