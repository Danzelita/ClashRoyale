using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Menu
{
    public class SelectedDeckUI : MonoBehaviour
    {
        [SerializeField] private CardView[] _cardViewPool;
        
        public void UpdateCardsList(IReadOnlyList<Card> cards)
        {
            Debug.Log(cards.Count);
            for (int i = 0; i < _cardViewPool.Length; i++) 
                _cardViewPool[i].gameObject.SetActive(i < cards.Count);
            
            for (int i = 0; i < cards.Count; i++)
                _cardViewPool[i].Init(cards[i]);
        }
    }
}