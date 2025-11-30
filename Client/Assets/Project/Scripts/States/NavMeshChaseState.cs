using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.States
{
    [CreateAssetMenu(fileName = "_NavMeshChaseState", menuName = "UnitState/NavMeshChase")]
    public class NavMeshChaseState : UnitState
    {
        private NavMeshAgent _agent;
        private Unit _targetUnit;
        private float _startAttackDistance = 0f;

        public override void Construct(Unit unit)
        {
            base.Construct(unit);

            _agent = _unit.GetComponent<NavMeshAgent>();
        }

        public override void Enter()
        {
            if (Map.Current.TryGetNearestUnit(_unit.transform.position, _unit.IsPlayer, out _targetUnit, out float distance))
            {
                _startAttackDistance = _unit.Parameters.StartAttackDistance + _targetUnit.Parameters.ModelRadius;
            }
        }

        public override void Run()
        {
            if (_targetUnit == null)
            {
                _unit.SetState(UnitStateType.Default);
                return;
            }

            float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);
            if (distanceToTarget > _unit.Parameters.StopChaseDistance)
                _unit.SetState(UnitStateType.Default);
            else if (distanceToTarget <= _startAttackDistance)
                _unit.SetState(UnitStateType.Attack);
            else
                _agent.SetDestination(_targetUnit.transform.position);
        }

        public override void Exit() =>
            _agent.SetDestination(_unit.transform.position);

#if UNITY_EDITOR
        public override void DebugDraw(Unit unit)
        {
            base.DebugDraw(unit);

            Handles.color = Color.green;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StartChaseDistance);
            Handles.color = Color.red;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StopChaseDistance);
        }
#endif
    }
}