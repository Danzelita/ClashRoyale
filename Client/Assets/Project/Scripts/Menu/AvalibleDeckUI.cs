using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class AvalibleDeckUI : MonoBehaviour
    {
        [SerializeField] private CardSelector _cardSelector;
        [SerializeField] private List<AvalibleCardView> _avalibleCards = new();
#if UNITY_EDITOR

        #region Editor

        [SerializeField] private Transform _avalibleCardParent;
        [SerializeField] private AvalibleCardView _avalibleCardPrefab;

        public void SetAllCardsCount(Card[] cards)
        {
            for (int i = 0; i < _avalibleCards.Count; i++)
            {
                GameObject go = _avalibleCards[i].gameObject;
                UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(go);
            }
            
            _avalibleCards.Clear();

            for (int i = 1; i < cards.Length; i++)
            {
                AvalibleCardView newCard = Instantiate(_avalibleCardPrefab, _avalibleCardParent);
                newCard.Create(cards[i], _cardSelector, i);
                _avalibleCards.Add(newCard);
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
        }

        #endregion
#endif

        public void UpdateCardsList(IReadOnlyList<Card> avalibleCards, IReadOnlyList<Card> selectedCards)
        {
            for (int i = 0; i < _avalibleCards.Count; i++) 
                _avalibleCards[i].SetState(CardStateType.Locked);

            for (int i = 0; i < avalibleCards.Count; i++) 
                _avalibleCards[avalibleCards[i].Id - 1].SetState(CardStateType.Available);

            for (int i = 0; i < selectedCards.Count; i++) 
                _avalibleCards[selectedCards[i].Id - 1].SetState(CardStateType.Selected);
        }
    }
}