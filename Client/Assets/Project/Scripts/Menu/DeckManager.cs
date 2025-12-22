using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private Card[] _allCards;

        public IReadOnlyList<Card> AvaliableCards => _avaliableCards;
        public IReadOnlyList<Card> SelectedCards => _selectedCards;

        [SerializeField] private List<Card> _avaliableCards = new();
        [SerializeField] private List<Card> _selectedCards = new();


        public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> AvalibleCardsChanged;
        public event Action<IReadOnlyList<Card>> SelectedCardsChanged;

        public void Init(List<int> avaliableCardsIndexes, int[] selectedCardsIndexes)
        {
            _avaliableCards.Clear();
            _selectedCards.Clear();
            
            for (int i = 0; i < avaliableCardsIndexes.Count; i++)
                _avaliableCards.Add(_allCards[avaliableCardsIndexes[i]]);

            for (int i = 0; i < selectedCardsIndexes.Length; i++)
                _selectedCards.Add(_allCards[selectedCardsIndexes[i]]);

            AvalibleCardsChanged?.Invoke(_avaliableCards, _selectedCards);
            SelectedCardsChanged?.Invoke(_selectedCards);
        }

#if UNITY_EDITOR

        #region Editor

        [SerializeField] private AvalibleDeckUI _avalibleDeckUI;

        private void OnValidate()
        {
            _avalibleDeckUI.SetAllCardsCount(_allCards);
        }

        #endregion

#endif
    }

    [System.Serializable]
    public class Card
    {
        [field: SerializeField] public int Id { get; private set; }
        public string Name;
        public Sprite Sprite;
    }
}