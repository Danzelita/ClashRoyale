using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.Multiplayer
{
    public class MatchmakingManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuCanvas;
        [SerializeField] private GameObject _matchmakingCanvas;

        [SerializeField] private Button _canselButton;
        [SerializeField] private string _gameSceneName;

        public void Subscribe()
        {
            MultiplayerManager.Instance.GetReady += OnGetReady;
            MultiplayerManager.Instance.StartGame += OnStartGame;
            MultiplayerManager.Instance.CancelGame += OnCancelGame;
        }
        public void Unsubscribe()
        {
            MultiplayerManager.Instance.GetReady -= OnGetReady;
            MultiplayerManager.Instance.StartGame -= OnStartGame;
            MultiplayerManager.Instance.CancelGame -= OnCancelGame;
        }

        private void OnGetReady()
        {
            _canselButton.gameObject.SetActive(false);
        }

        private void OnCancelGame()
        {
            _canselButton.gameObject.SetActive(false);
        }

        private void OnStartGame(string jsonDecks)
        {
            Decks decks = JsonUtility.FromJson<Decks>(jsonDecks);

            string[] playerDeck;
            string[] enemyDeck;
            Debug.Log($"{MultiplayerManager.Instance.ClientId} | {jsonDecks}");
            if (decks.player1ID == MultiplayerManager.Instance.ClientId)
            {
                playerDeck = decks.player1;
                enemyDeck = decks.player2;
            }
            else
            {
                playerDeck = decks.player2;
                enemyDeck = decks.player1;
            }
            
            CardsInGame.Instance.SetDecks(playerDeck, enemyDeck);
            SceneManager.LoadScene(_gameSceneName);
        }

        public async void FindOpponent()
        {
            _canselButton.gameObject.SetActive(false);
            _mainMenuCanvas.SetActive(false);
            _matchmakingCanvas.SetActive(true);

            await MultiplayerManager.Instance.Connect();
            
            _canselButton.gameObject.SetActive(true);
        }

        public void CanselFind()
        {
            _mainMenuCanvas.SetActive(true);
            _matchmakingCanvas.SetActive(false);
            
            MultiplayerManager.Instance.Leave();
        }
        
        public class Decks
        {
            public string player1ID;
            public string[] player1;
            public string[] player2;
        }
    }
}