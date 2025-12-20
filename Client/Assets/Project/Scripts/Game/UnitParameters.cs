using UnityEngine;

namespace Project.Scripts
{
    public class UnitParameters : MonoBehaviour
    {
        [field: SerializeField] public bool IsFly { get; private set; } = false;
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float StartChaseDistance { get; private set; } = 5f;
        [field: SerializeField] public float StopChaseDistance { get; private set; } = 7f;
        [field: SerializeField] public float DamageDelay { get; private set; } = 2f;

        public float ModelRadius => _modelRadius;
        public float StartAttackDistance => _modelRadius + _startAttackDistance;
        public float StopAttackDistance => _modelRadius + _stopAttackDistance;
        
        [SerializeField] private float _modelRadius;
        [SerializeField] private float _startAttackDistance;
        [SerializeField] private float _stopAttackDistance;
    }
}