using System;
using UnityEngine;

namespace Project.Scripts
{
    public class AttachToTransform : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _removeParrent;

        private void Awake() =>
            transform.parent = _removeParrent ? null : transform.parent;

        private void LateUpdate()
        {
            if (_target != null)
                transform.position = _target.position;
            else
                Destroy(gameObject);
        }
    }
}