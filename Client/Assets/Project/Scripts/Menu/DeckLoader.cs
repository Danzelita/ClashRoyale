using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private List<int> _avalibleCards = new();
        [SerializeField] private int[] _selectedCards = new int[6];

        private void Start()
        {
            StartLoad();
            LoadingScreen.Instance.Show();
        }

        public void StartLoad()
        {
            Dictionary<string, string> data = new()
            {
                ["userID"] = $"{UserInfo.Instance.ID}",
            };
            
            Network.Instance.Post(URLLibrary.GetDeckInfo, data, Succes, Error);
        }

        public DeckData DeckData;
        private void Succes(string data)
        {
            DeckData deckData = JsonUtility.FromJson<DeckData>(data);
            
            _selectedCards = new int[deckData.selectedIDs.Length];
            
            for (int i = 0; i < _selectedCards.Length; i++) 
                _selectedCards[i] = int.Parse(deckData.selectedIDs[i]);

            for (int i = 0; i < deckData.avalibleCards.Length; i++) 
                _avalibleCards.Add(int.Parse(deckData.avalibleCards[i].id));
            
            _deckManager.Init(_avalibleCards, _selectedCards);
            
            LoadingScreen.Instance.Hide();
        }

        private void Error(string error) => 
            StartLoad();
    }
}

[System.Serializable]
public class DeckData
{
    public AvalibleCards[] avalibleCards;
    public string[] selectedIDs;
}
[System.Serializable]
public class AvalibleCards
{
    public string id;
    public string name;
}

