using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public float Max { get; private set; } = 10f;
        public float Current { get; private set; }

        public event Action<float, float> HealthChange;
        public event Action Death;

        private void Awake() => 
            Current = Max;

        public void ApplyDamage(float value)
        {
            Current -= value;
            Current = Mathf.Max(0f, Current);
            
            HealthChange?.Invoke(Current, Max);
            
            Debug.Log($"{name}'s Health: " + Current);

            CheckDie();
        }

        private void CheckDie()
        {
            if (Current <= 0f)
                Die();
        }

        private void Die() => 
            Death?.Invoke();
    }
}