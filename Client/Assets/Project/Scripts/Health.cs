using System;
using System.Collections;
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

        public void ApplyDamage(float damage) => 
            ApplyDelayDamage(damage, 0.075f);

        public void ApplyDelayDamage(in float damage, in float delay) => 
            StartCoroutine(DelayDamage(damage, delay));

        private IEnumerator DelayDamage(float damage, float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyDamageAndCheckDie(damage);
        }

        private void ApplyDamageAndCheckDie(float damage)
        {
            Current -= damage;
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