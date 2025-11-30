using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Tower : MonoBehaviour, IHealth
    {
        [field: SerializeField] public Health Health { get; private set; } 
        
        [field: SerializeField] public float Radius { get; private set; } = 2f;

        private void Awake()
        {
            Health.Death += HealthOnDeath;
        }

        private void HealthOnDeath()
        {
            Health.Death -= HealthOnDeath;
            
            Map.Current.RemoveTower(this);
            Destroy(gameObject);
        }

        public float GetDistance(in Vector3 point) => 
            Vector3.Distance(transform.position, point) - Radius;
    }
}