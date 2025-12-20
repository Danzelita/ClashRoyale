using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private bool _showOnStart = true;
        [SerializeField] private Transform _visual;
        [SerializeField] private Health _health;
        [SerializeField] private Image _background;
        [SerializeField] private Image _scaler;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private ColorsData _colorsData;

        private void Start()
        {
            _health.HealthChange += OnHealthChange;
            SetHealth(_health.Current, _health.Max);

            if (_colorsData != null) 
                SetColors(_colorsData);
            
            _visual.gameObject.SetActive(_showOnStart);
        }

        private void OnDestroy() => 
            _health.HealthChange -= OnHealthChange;

        private void OnHealthChange(float current, float max) => 
            SetHealth(current, max);

        private void SetHealth(float current, float max)
        {
            _scaler.transform.localScale = new Vector3(current / max, 1f, 1f);
            _healthText.text = $"{Mathf.CeilToInt(current)}";
            _visual.gameObject.SetActive(true);
        }

        private void SetColors(ColorsData colorsData)
        {
            _scaler.color = colorsData.AccentColor;
            _background.color = colorsData.DarkColor;
            _healthText.color = colorsData.HightlightColor;
        }
    }
}