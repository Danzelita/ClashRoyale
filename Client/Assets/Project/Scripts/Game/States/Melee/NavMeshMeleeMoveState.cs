using UnityEngine;

namespace Project.Scripts.States.Melee
{
    [CreateAssetMenu(fileName = "_NavMeshMeleeMoveState", menuName = "UnitState/" + nameof(NavMeshMeleeMoveState))]
    public class NavMeshMeleeMoveState : UnitStateNavMeshMove
    {
        protected override bool TryFindTarget(out UnitStateType changeType)
        {
            if (TryAttackTower())
            {
                changeType = UnitStateType.Attack;
                return true;
            }

            if (TryChaseUnit())
            {
                changeType = UnitStateType.Chase;
                return true;
            }

            changeType = UnitStateType.None;
            return true;
        }

        private bool TryAttackTower()
        {
            if (_nearestTower == null)
                return false;
            
            float distance = _nearestTower.GetDistance(_unit.transform.position);
            if (distance < _unit.Parameters.StartAttackDistance)
            {
                return true;
            }

            return false;
        }

        private bool TryChaseUnit()
        {
            bool hasEnemy = Map.Current.TryGetNearestWalkingUnit(_unit.transform.position, _unit.IsPlayer, out Unit enemy,
                out float distance);
            if (hasEnemy == false)
                return false;

            if (_unit.Parameters.StartChaseDistance >= distance)
            {
                return true;
            }

            return false;
        }
    }
}