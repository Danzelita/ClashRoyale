using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "ColorsData", menuName = "Colors/ColorsData")]
    public class ColorsData : ScriptableObject
    {
        public Color AccentColor;
        public Color DarkColor;
        public Color HightlightColor;
    }
}