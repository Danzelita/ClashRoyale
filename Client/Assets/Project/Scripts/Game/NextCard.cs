using Project.Scripts.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game
{
    public class NextCard : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public void SetCard(Card card)
        {
            if (card.Sprite != null)
            {
                _image.sprite = card.Sprite;
            }
            _text.text = card.Name;
        }
    }
}