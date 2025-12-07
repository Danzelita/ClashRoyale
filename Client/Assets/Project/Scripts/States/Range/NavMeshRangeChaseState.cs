using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.States.Range
{
    [CreateAssetMenu(fileName = "_NavMeshRangeChaseState", menuName = "UnitState/" + nameof(NavMeshRangeChaseState))]
    public class NavMeshRangeChaseState : UnitStateNavMeshChase
    {
        protected override void FindTargetUnit(out Unit targetUnit)
        {
            Map.Current.TryGetNearestAnyUnit(_unit.transform.position, _unit.IsPlayer, out targetUnit, out float distance);
        }
    }
}