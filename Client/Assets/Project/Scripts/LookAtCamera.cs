using UnityEngine;

namespace Project.Scripts
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start() => 
            _cameraTransform = Camera.main.transform;

        private void LateUpdate() => 
            transform.localEulerAngles = new Vector3(_cameraTransform.localEulerAngles.x, 0f, 0f);
    }
}