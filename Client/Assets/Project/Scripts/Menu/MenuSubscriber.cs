using System;
using Project.Scripts.Multiplayer;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class MenuSubscriber : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUI2;
        [SerializeField] private SelectedDeckUI _selectedDeckUI3;
        [SerializeField] private AvalibleDeckUI _avalibleDeckUI;
        [SerializeField] private MatchmakingManager _matchmakingManager;

        private void Start()
        {
            _deckManager.SelectedCardsChanged += _selectedDeckUI.UpdateCardsList;
            _deckManager.SelectedCardsChanged += _selectedDeckUI2.UpdateCardsList;
            _deckManager.SelectedCardsChanged += _selectedDeckUI3.UpdateCardsList;
            _deckManager.AvalibleCardsChanged += _avalibleDeckUI.UpdateCardsList;

            _matchmakingManager.Subscribe();
        }

        private void OnDestroy()
        {
            _deckManager.SelectedCardsChanged -= _selectedDeckUI.UpdateCardsList;
            _deckManager.SelectedCardsChanged -= _selectedDeckUI2.UpdateCardsList;
            _deckManager.SelectedCardsChanged -= _selectedDeckUI3.UpdateCardsList;
            _deckManager.AvalibleCardsChanged -= _avalibleDeckUI.UpdateCardsList;
            
            _matchmakingManager.Unsubscribe();
        }
    }
}