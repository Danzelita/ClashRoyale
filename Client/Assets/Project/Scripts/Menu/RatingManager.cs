using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class RatingManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ratingText;
        private void Start()
        {
            InitRating();
        }

        private void InitRating()
        {
            Dictionary<string, string> data = new()
            {
                ["userID"] = UserInfo.Instance.ID.ToString(),
            };
            Network.Instance.Post(URLLibrary.GetRating, data, Succes, Error);
        }

        private void Succes(string obj)
        {
            string[] result = obj.Split('|');
            if (result.Length != 3)
            {
                Error(obj);
                return;
            }

            if (result[0] != "ok")
            {
                Error(obj);
                return;
            }

            _ratingText.text = $"<color=green>{result[1]}</color> : <color=red>{result[2]}</color>";
        }

        private void Error(string obj) => 
            Debug.LogError(obj);
        
        
    }
}