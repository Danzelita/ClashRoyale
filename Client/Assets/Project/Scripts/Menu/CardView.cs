using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Menu
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _preview;

        public void Init(Card card)
        {
            _name.text = card.Name;

            if (card.Sprite != null)
            {
                _preview.sprite = card.Sprite;
            }
        }
    }
}