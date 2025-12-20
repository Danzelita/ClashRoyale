using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class CardSelector : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private AvalibleDeckUI _avalibleDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        
        public IReadOnlyList<Card> AvaliableCards => _avaliableCards;
        public IReadOnlyList<Card> SelectedCards => _selectedCards;
        
        private List<Card> _avaliableCards = new();
        private List<Card> _selectedCards  = new();
        
        private int _selectToggleIndex;

        private void OnEnable()
        {
            _avaliableCards.Clear();
            for (int i = 0; i < _deckManager.AvaliableCards.Count; i++) 
                _avaliableCards.Add(_deckManager.AvaliableCards[i]);
            
            _selectedCards.Clear();
            for (int i = 0; i < _deckManager.SelectedCards.Count; i++) 
                _selectedCards.Add(_deckManager.SelectedCards[i]);
        }

        public void SetSelectToggleIndex(int index)
        {
            _selectToggleIndex = index;
        }

        public void SelectCard(int cardId)
        {
            _selectedCards[_selectToggleIndex] = _avaliableCards[cardId - 1];
            _selectedDeckUI.UpdateCardsList(SelectedCards);
            _avalibleDeckUI.UpdateCardsList(AvaliableCards, SelectedCards);
        }
    }
}