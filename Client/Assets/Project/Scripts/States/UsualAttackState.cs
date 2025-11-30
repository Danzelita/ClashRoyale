using UnityEngine;

namespace Project.Scripts.States
{
    [CreateAssetMenu(fileName = "_UsualAttackState", menuName = "UnitState/UsualAttack")]
    public class UsualAttackState : UnitState
    {
        [SerializeField] private float _damage = 1.5f;
        [SerializeField] private float _delay = 1f;
        [SerializeField] private float _enterDelay = 0.5f;
        
        private float _stopAttackDistance = 0f;
        private float _time = 0;
        private Health _targetHealth;

        public override void Enter()
        {
            if (!TryFindTarget(out _stopAttackDistance))
            {
                _unit.SetState(UnitStateType.Default);
                return;
            }
            
            _time = _delay - _enterDelay;
            _unit.transform.LookAt(_targetHealth.transform.position);
        }

        public override void Run()
        {
            _time += Time.deltaTime;

            if (_targetHealth == null)
            {
                _unit.SetState(UnitStateType.Default);
                return;
            }


            float distanceToTarget = Vector3.Distance(_targetHealth.transform.position, _unit.transform.position);
            if (distanceToTarget > _stopAttackDistance)
            {
                _unit.SetState(UnitStateType.Chase);
            }

            if (_time < _delay)
                return;
            _time -= _delay;

            _targetHealth.ApplyDamage(_damage);
        }

        public override void Exit()
        {
        }

        private bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = _unit.transform.position;
            
            bool hasEnemy = Map.Current.TryGetNearestUnit(unitPosition, _unit.IsPlayer, out Unit enemy, out float distance);
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