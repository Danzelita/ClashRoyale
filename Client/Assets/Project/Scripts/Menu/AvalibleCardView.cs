using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Menu
{
    public class AvalibleCardView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _lockedColor;
        [SerializeField] private CardSelector _selector;
        [SerializeField] private int _id;
        
        private CardStateType _currentState;

        public void Create(Card card, CardSelector selector, int id)
        {
            _name.text = card.Name;

            if (card.Sprite != null) 
                _image.sprite = card.Sprite;
            
            _selector = selector;
            _id = id;

            UnityEditor.EditorUtility.SetDirty(this);
        }

        public void SetState(CardStateType cardState)
        {
            _currentState = cardState;
            switch (cardState)
            {
                case CardStateType.Available:
                    _name.color = _availableColor;
                    break;
                case CardStateType.Selected:
                    _name.color = _selectedColor;
                    break;
                case CardStateType.Locked:
                    _name.color = _lockedColor;
                    break;
            }
        }

        public void Click()
        {
            switch (_currentState)
            {
                case CardStateType.Available:
                    _selector.SelectCard(_id);
                    SetState(CardStateType.Selected);
                    break;
                case CardStateType.Selected:
                    break;
                case CardStateType.Locked:
                    break;
            }
        }
    }

    public enum CardStateType
    {
        None,
        Available,
        Selected,
        Locked,
    }
}