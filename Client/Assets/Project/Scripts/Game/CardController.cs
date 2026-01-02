using Project.Scripts.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Game
{
    public class CardController :  MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TextMeshProUGUI _cardText;
        [SerializeField] private Image _cardPreview;
        [SerializeField] private float _dragSize = 0.75f;
        
        private bool _isDragging;
        private Vector3 _startPosition;
        private int _index;
        private CardManager _cardManager;

        public void Init(CardManager cardManager, int index, Card card)
        {
            _cardManager = cardManager;
            _index = index;
            _startPosition = transform.localPosition;
            
            SetCard(card);
        }

        public void SetCard(Card card)
        {
            if (card.Sprite != null)
            {
                _cardPreview.sprite = card.Sprite;
            }
            _cardText.text = card.Name;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (false)
            {
                return;
            }
            _isDragging = true;
            
            transform.localScale = Vector3.one * _dragSize;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false) 
                return;
            
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
                return;
            
            _isDragging = false;
            
            _cardManager.Release(_index, eventData.position);
            
            transform.localPosition = _startPosition;
            transform.localScale = Vector3.one;
        }
    }
}