using UnityEngine;

namespace Project.Scripts.States.OnlyTowerAttack
{
    [CreateAssetMenu(fileName = "_UsualTowerAttackState", menuName = "UnitState/" + nameof(UsualTowerAttackState))]
    public class UsualTowerAttackState : UnitStateAttack
    {
        protected override bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = _unit.transform.position;

            Tower targetTower = Map.Current.GetNearestTower(unitPosition, _unit.IsPlayer);
            if (targetTower.GetDistance(unitPosition) <= _unit.Parameters.StartAttackDistance)
            {
                _targetHealth = targetTower.Health;
                stopAttackDistance = _unit.Parameters.StopAttackDistance + targetTower.Radius;
                return true;
            }

            stopAttackDistance = 0;
            return false;
        }
    }
}