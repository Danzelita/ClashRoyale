using UnityEngine;

namespace Project.Scripts.Menu
{
    [System.Serializable]
    public class Card
    {
        [field: SerializeField] public int Id { get; private set; }
        public string Name;
        public Sprite Sprite;
        public Unit PlayerPrefab;
        public Unit EnemyPrefab;
    }
}