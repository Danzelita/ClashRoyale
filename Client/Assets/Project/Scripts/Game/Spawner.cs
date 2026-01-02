using UnityEngine;

namespace Project.Scripts.Game
{
    public class Spawner : MonoBehaviour
    {
        public void Spawn(string id, in Vector3 spawnPosition, bool isPlayer)
        {
            Unit unit;
            Quaternion rotation = Quaternion.identity;
            if (isPlayer)
            {
                unit = CardsInGame.Instance.PlayerDeck[id].PlayerPrefab;
                rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                unit = CardsInGame.Instance.EnemyDeck[id].EnemyPrefab;
            }
            
            Unit newUnit = Instantiate(unit, spawnPosition, Quaternion.identity);
            newUnit.Init(isPlayer);
            Map.Current.AddUnit(newUnit);
        }
    }
}