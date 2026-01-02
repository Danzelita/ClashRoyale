using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Menu;
using UnityEngine;

namespace Project.Scripts
{
    public class CardsInGame : MonoBehaviour
    {
        #region Singleton
        public static CardsInGame Instance {get; private set;}
        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
        
        public IReadOnlyDictionary<string, Card> PlayerDeck { get; private set; }
        public IReadOnlyDictionary<string, Card> EnemyDeck { get; private set; }

        public void SetDecks(string[] playerDecks, string[] enemyCards)
        {
            DeckManager deckManager = FindObjectOfType<DeckManager>();
            bool player = deckManager.TryGetDeck(playerDecks, out Dictionary<string, Card> playerDeck);
            bool enemy = deckManager.TryGetDeck(enemyCards, out Dictionary<string, Card> enemyDeck);

            if (player == false || enemy == false) 
                Debug.LogError("Не удалось загрузить колоды");
            
            PlayerDeck = playerDeck;
            EnemyDeck = enemyDeck;
        }

        public List<string> GetAllID() => PlayerDeck.Keys.ToList();
    }
}