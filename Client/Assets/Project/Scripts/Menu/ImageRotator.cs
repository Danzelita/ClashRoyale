using UnityEngine;

namespace Project.Scripts.Menu
{
    public class ImageRotator : MonoBehaviour
    {
        [SerializeField] private RectTransform _loadingImage;
        [SerializeField] private float _rotationSpeed = 720f;
        private void Update() => 
            _loadingImage.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}