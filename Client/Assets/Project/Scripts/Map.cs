using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class Map : MonoBehaviour
    {
        #region SingletonOneScene

        public static Map Current { get; private set; }
        private void Awake()
        {
            if (Current != null)
            {
                Destroy(gameObject);
                return;
            }
            Current = this;
        }

        private void OnDestroy()
        {
            if (Current != this) 
                return;
            Current = null;;
        }

        #endregion
        
        [SerializeField] private List<Tower> _playerTowers = new();
        [SerializeField] private List<Tower> _enemyTowers = new();
        
        [SerializeField] private List<Unit> _playerUnits = new();
        [SerializeField] private List<Unit> _enemyUnits = new();

        public bool TryGetNearestUnit(in Vector3 currentPosition, bool isPlayer, out Unit unit, out float distance)
        {
            List<Unit> units = isPlayer ? _enemyUnits : _playerUnits;
            unit = GetNearest(currentPosition, units, out distance);
            return unit;
        }

        public Tower GetNearestTower(in Vector3 currentPosition, bool isPlayer)
        {
            List<Tower> towers = isPlayer ? _enemyTowers : _playerTowers;
            return GetNearest(currentPosition, towers, out float distance);
        }

        private T GetNearest<T>(Vector3 currentPosition, List<T> objects, out float distance) where T : MonoBehaviour
        {
            distance = float.MaxValue;
            if (objects.Count <= 0)
                return null;
            
            distance = Vector3.Distance(currentPosition, objects[0].transform.position);
            T nearest = objects[0];

            for (int i = 1; i < objects.Count; i++)
            {
                float tempDitance = Vector3.Distance(currentPosition, objects[i].transform.position);
                if (tempDitance > distance)
                    continue;
                
                distance = tempDitance;
                nearest = objects[i];
            }
            return nearest;
        }

        public void RemoveUnit(Unit unit)
        {
            if (_playerUnits.Remove(unit))
                return;

            _enemyUnits.Remove(unit);
        }

        public void RemoveTower(Tower tower)
        {
            if (_playerTowers.Remove(tower))
                return;
            
            _enemyTowers.Remove(tower);
        }
    }
}