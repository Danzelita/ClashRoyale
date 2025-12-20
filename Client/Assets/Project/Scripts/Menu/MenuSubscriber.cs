using System;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class MenuSubscriber : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUI2;
        [SerializeField] private AvalibleDeckUI _avalibleDeckUI;

        private void Start()
        {
            _deckManager.SelectedCardsChanged += _selectedDeckUI.UpdateCardsList;
            _deckManager.SelectedCardsChanged += _selectedDeckUI2.UpdateCardsList;
            _deckManager.AvalibleCardsChanged += _avalibleDeckUI.UpdateCardsList;
        }

        private void OnDestroy()
        {
            _deckManager.SelectedCardsChanged -= _selectedDeckUI.UpdateCardsList;
            _deckManager.SelectedCardsChanged -= _selectedDeckUI2.UpdateCardsList;
            _deckManager.AvalibleCardsChanged -= _avalibleDeckUI.UpdateCardsList;
        }
    }
}