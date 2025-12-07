using UnityEngine;

namespace Project.Scripts.States.Melee
{
    [CreateAssetMenu(fileName = "_NavMeshMeleeChaseState", menuName = "UnitState/" + nameof(NavMeshMeleeChaseState))]
    public class NavMeshMeleeChaseState : UnitStateNavMeshChase
    {
        protected override void FindTargetUnit(out Unit targetUnit)
        {
            Map.Current.TryGetNearestWalkingUnit(_unit.transform.position, _unit.IsPlayer, out targetUnit, out float distance);
        }
    }
}