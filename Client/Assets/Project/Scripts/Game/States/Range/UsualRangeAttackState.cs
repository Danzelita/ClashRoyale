using UnityEngine;

namespace Project.Scripts.States.Range
{
    [CreateAssetMenu(fileName = "_UsualRangeAttackState", menuName = "UnitState/" + nameof(UsualRangeAttackState))]
    public class UsualRangeAttackState : UnitStateAttack
    {
        [SerializeField] private Bullet _bulletPrefab;
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

        protected override void Attack(float damage)
        {
            Vector3 unitPosition = _unit.transform.position;
            Vector3 targetPosition = _targetHealth.transform.position;
            
            Bullet bullet = Instantiate(_bulletPrefab, unitPosition, Quaternion.identity);
            bullet.Init(targetPosition);
            
            float delay = Vector3.Distance(unitPosition, targetPosition) / bullet.Speed;
            _targetHealth.ApplyDelayDamage(damage, delay);
        }
    }
}