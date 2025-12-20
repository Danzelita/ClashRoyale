using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.States
{
    public abstract class UnitStateNavMeshMove : UnitState
    {
        private NavMeshAgent _agent;
        protected Tower _nearestTower;

        public override void Construct(Unit unit)
        {
            base.Construct(unit);
            
            _agent = _unit.GetComponent<NavMeshAgent>();
            _agent.speed = _unit.Parameters.Speed;
            _agent.radius = _unit.Parameters.ModelRadius;
            _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        }

        public override void Enter()
        {
            Vector3 unitPosition = _agent.transform.position;
            _nearestTower = Map.Current.GetNearestTower(unitPosition, _unit.IsPlayer);
            _agent.SetDestination(_nearestTower.transform.position);
        }

        public override void Run()
        {
            if (TryFindTarget(out UnitStateType changeType)) 
                _unit.SetState(changeType);
        }
        
        protected abstract bool TryFindTarget(out UnitStateType changeType);

        

        public override void Exit() => 
            _agent.SetDestination(_unit.transform.position);
    }
}