using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Tower : MonoBehaviour, IHealth, IDestroyed
    {
        public event Action Destroyed;
        [field: SerializeField] public Health Health { get; private set; }

        [field: SerializeField] public float Radius { get; private set; } = 2f;


        private void Awake()
        {
            Health.Death += OnDeath;
        }

        private void OnDeath()
        {
            Health.Death -= OnDeath;

            Destroy(gameObject);
            
            Destroyed?.Invoke();
        }

        public float GetDistance(in Vector3 point) => 
            Vector3.Distance(transform.position, point) - Radius;
    }
}