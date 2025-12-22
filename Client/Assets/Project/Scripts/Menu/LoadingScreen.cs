using System;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private RectTransform _loadingImage;
        [SerializeField] private float _rotationSpeed;

        #region Singleton
        public static LoadingScreen Instance {get; private set;}
        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        private void Start() => 
            Hide();

        public void Show() => 
            _loadingScreen.SetActive(true);

        public void Hide() => 
            _loadingScreen.SetActive(false);

        private void Update()
        {
            _loadingImage.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}