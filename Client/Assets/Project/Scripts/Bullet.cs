using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        
        private Vector3 _targetPosition = Vector3.zero;
        
        public void Init(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            transform.LookAt(targetPosition);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

            if (transform.position == _targetPosition) 
                Destroy(gameObject);
        }
    }
}