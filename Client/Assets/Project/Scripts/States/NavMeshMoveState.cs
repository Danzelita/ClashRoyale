using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.States
{
    [CreateAssetMenu(fileName = "_NavMeshMoveState", menuName = "UnitState/NavMeshMove")]
    public class NavMeshMoveState : UnitState
    {
        private NavMeshAgent _agent;
        private Tower _nearestTower;

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
            if (TryAttackTower())
                return;

            if (TryAttackUnit())
                return;
        }

        private bool TryAttackUnit()
        {
            bool hasEnemy = Map.Current.TryGetNearestUnit(_unit.transform.position, _unit.IsPlayer, out Unit enemy, out float distance);
            if (hasEnemy == false)
                return false;
            
            //+ enemy.Parameters.ModelRadius
            if (_unit.Parameters.StartChaseDistance >= distance)
            {
                _unit.SetState(UnitStateType.Chase);
                return true;
            }
            return false;
        }

        private bool TryAttackTower()
        {
            float distance = _nearestTower.GetDistance(_unit.transform.position);
            if (distance < _unit.Parameters.StartAttackDistance)
            {
                Debug.Log("Добежал");
                _unit.SetState(UnitStateType.Attack);
                return true;
            }
            return false;
        }

        public override void Exit() => 
            _agent.SetDestination(_unit.transform.position);
    }
}