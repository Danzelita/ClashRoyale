using UnityEditor;
using UnityEngine;

namespace Project.Scripts.States
{
    public abstract class UnitStateAttack : UnitState
    {
        [SerializeField] private float _damage = 1.5f;
        private float _delay;
        
        private float _stopAttackDistance = 0f;
        private float _time = 0;
        protected Health _targetHealth;

        public override void Construct(Unit unit)
        {
            base.Construct(unit);
            _delay = unit.Parameters.DamageDelay;
        }

        public override void Enter()
        {
            if (!TryFindTarget(out _stopAttackDistance))
            {
                _unit.SetState(UnitStateType.Default);
                return;
            }
            
            _time = _delay;
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
                _unit.SetState(UnitStateType.Chase);

            if (_time < _delay)
                return;
            _time -= _delay;

            Attack(_damage);
        }

        protected virtual void Attack(float damage) => 
            _targetHealth.ApplyDamage(damage);

        public override void Exit()
        {
        }

        protected abstract bool TryFindTarget(out float stopAttackDistance);

#if UNITY_EDITOR
        public override void DebugDraw(Unit unit)
        {
            base.DebugDraw(unit);
            
            Handles.color = Color.red;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StartAttackDistance);
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StopAttackDistance);
        }
#endif
    }
}