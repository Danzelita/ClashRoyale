using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Menu;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Game
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private CardController[] _cardControllers;
        [SerializeField] private NextCard _nextCardPreview;
        [SerializeField] private int _layerIndex = 6;
        private string[] _ids;
        private CardsInGame _cardsInGame;
        private List<string> _freeCardsId;
        private string _nextCardId;
        private Camera _camera;

        private void Start()
        {
            _ids = new string[_cardControllers.Length];
            _camera = Camera.main;
            _cardsInGame = CardsInGame.Instance;

            _freeCardsId = _cardsInGame.GetAllID();
            MixList(_freeCardsId);

            for (int i = 0; i < _cardControllers.Length; i++)
            {
                string cardID = _freeCardsId[0];
                _freeCardsId.RemoveAt(0);
                _ids[i] = cardID;
                Card card = _cardsInGame.PlayerDeck[cardID];
                _cardControllers[i].Init(this, i, card);
            }

            SetNextRandom();
        }

        private void SetNextRandom()
        {
            int randomIndex = Random.Range(0, _freeCardsId.Count);
            _nextCardId = _freeCardsId[randomIndex];
            _freeCardsId.RemoveAt(randomIndex);
            _nextCardPreview.SetCard(_cardsInGame.PlayerDeck[_nextCardId]);
        }

        public void Release(int controllerIndex, Vector2 screenPointPosition)
        {
            if (TryGetSpawnPoint(screenPointPosition, out Vector3 spawnPoint) == false)
                return;
            
            string id = _ids[controllerIndex];
            _freeCardsId.Add(id);
            _ids[controllerIndex] = _nextCardId;
            _cardControllers[controllerIndex].SetCard(_cardsInGame.PlayerDeck[_nextCardId]);
            
            SetNextRandom();
            
            FindObjectOfType<Spawner>().Spawn(id, spawnPoint, isPlayer: true);
        }

        private bool TryGetSpawnPoint(Vector2 screenPointPosition, out Vector3 spawnPoint)
        {
            Ray ray = _camera.ScreenPointToRay(screenPointPosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.layer == _layerIndex)
            {
                spawnPoint = hit.point;
                return true;
            }
            
            spawnPoint = Vector3.zero;
            return false;
        }

        private void MixList(List<string> ids)
        {
            int length = ids.Count;

            int[] arr = new int[length];
            for (int i = 0; i < length; i++)
                arr[i] = i;

            System.Random random = new System.Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();

            string[] tempArr = new string[length];
            for (int i = 0; i < length; i++)
                tempArr[i] = ids[i];

            for (int i = 0; i < length; i++)
                ids[i] = tempArr[arr[i]];
        }
    }
}