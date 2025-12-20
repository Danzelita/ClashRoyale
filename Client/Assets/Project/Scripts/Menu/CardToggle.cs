using UnityEngine;

namespace Project.Scripts.Menu
{
    public class CardToggle : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private CardSelector _selector;
        public void Click(bool value)
        {
            if (value == false)
                return;

            _selector.SetSelectToggleIndex(_index);
        }
    }
}