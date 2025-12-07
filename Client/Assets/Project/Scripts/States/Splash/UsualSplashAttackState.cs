using Project.Scripts.States.Bee;
using UnityEngine;

namespace Project.Scripts.States.Splash
{
    [CreateAssetMenu(fileName = "_UsualSplashAttackState", menuName = "UnitState/" + nameof(UsualSplashAttackState))]
    public class UsualSplashAttackState : UnitStateAttack
    {
        [SerializeField] private GameObject _splashEffect;
        [SerializeField] private bool _dieAfterAttack;
        [SerializeField] private float _radius;
        private readonly Collider[] _colliders = new Collider[10];

        protected override bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = _unit.transform.position;
            
            bool hasEnemy = Map.Current.TryGetNearestWalkingUnit(unitPosition, _unit.IsPlayer, out Unit enemy, out float distance);
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
            Vector3 targetPosition = _unit.transform.position;
            
            int count = Physics.OverlapSphereNonAlloc(targetPosition, _radius, _colliders);
            for (int i = 0; i < count; i++)
                if (_colliders[i].GetComponent<HealthCollider>() is HealthCollider healthCollider) 
                    healthCollider.Health.ApplyDamage(damage);
            if (_splashEffect != null)
            {
                Instantiate(_splashEffect, targetPosition, Quaternion.identity);
            }
            
            if (_dieAfterAttack)
                _unit.Health.ApplyDamage(10000f);
        }
#if UNITY_EDITOR
        
        public override void DebugDraw(Unit unit)
        {
            base.DebugDraw(unit);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(unit.transform.position, _radius);
        }
#endif
    }
}