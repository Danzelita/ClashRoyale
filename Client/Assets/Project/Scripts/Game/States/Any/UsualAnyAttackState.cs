using UnityEngine;

namespace Project.Scripts.States.Bee
{
    [CreateAssetMenu(fileName = "_UsualAnyAttackState", menuName = "UnitState/" + nameof(UsualAnyAttackState))]
    public class UsualAnyAttackState : UnitStateAttack
    {
        protected override bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = _unit.transform.position;
            
            bool hasEnemy = Map.Current.TryGetNearestAnyUnit(unitPosition, _unit.IsPlayer, out Unit enemy, out float distance);
            if (hasEnemy && distance - enemy.Parameters.ModelRadius <= _unit.Parameters.StartAttackDistance)
            {
                _targetHealth = enemy.Health;
                stopAttackDistance = _unit.Parameters.StopAttackDistance + enemy.Parameters.ModelRadius;
                return true;
            }
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