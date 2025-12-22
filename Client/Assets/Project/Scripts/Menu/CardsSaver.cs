using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class CardsSaver : MonoBehaviour
    {
        [SerializeField] private DeckLoader _deckLoader;
        [SerializeField] private CardSelector _cardSelector;

        public void SaveCards()
        {
            string selectedCards = string.Join("|", _cardSelector.SelectedCards.Select(t => t.Id));
            
            Dictionary<string, string> data = new()
            {
                ["userID"] = UserInfo.Instance.ID.ToString(),
                ["selectedIDs"] = selectedCards,
            };
            
            Debug.Log(selectedCards);
            
            Network.Instance.Post(URLLibrary.SetSelectedCards, data, Succes, Error);
            LoadingScreen.Instance.Show();
        }

        private void Succes(string data)
        {
            if (data != "succes")
                return;
            
            _deckLoader.StartLoad();
        }

        private void Error(string data) => 
            Debug.LogError(data);
    }
}