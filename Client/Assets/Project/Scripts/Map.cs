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
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Current != this) 
                return;
            Current = null;;
        }

        #endregion
        
        [Header("Player")]
        [SerializeField] private List<Tower> _playerTowers = new();
        [SerializeField] private List<Unit> _playerWalkingUnits = new();
        [SerializeField] private List<Unit> _playerFlyUnits = new();
        
        [Header("Enemy")]
        [SerializeField] private List<Tower> _enemyTowers = new();
        [SerializeField] private List<Unit> _enemyWalkingUnits = new();
        [SerializeField] private List<Unit> _enemyFlyUnits = new();

        private void Start()
        {
            SubscribeDestroy(_enemyTowers);
            SubscribeDestroy(_enemyWalkingUnits);
            SubscribeDestroy(_enemyFlyUnits);
            
            SubscribeDestroy(_playerTowers);
            SubscribeDestroy(_playerWalkingUnits);
            SubscribeDestroy(_playerFlyUnits);
        }

        public void AddUnit(Unit unit)
        {
            List<Unit> list;
            
            if (unit.IsPlayer)
                list = unit.Parameters.IsFly ? _playerFlyUnits : _playerWalkingUnits;
            else
                list = unit.Parameters.IsFly ? _enemyFlyUnits : _enemyWalkingUnits;

            AddObjectToList(list, unit);
        }


        public bool TryGetNearestAnyUnit(in Vector3 currentPosition, bool isPlayer, out Unit unit, out float distance)
        {
            TryGetNearestWalkingUnit(currentPosition, isPlayer, out Unit walking, out float walkingDistance);
            TryGetNearestFlyUnit(currentPosition, isPlayer, out Unit fly, out float flyDistance);
            if (flyDistance < walkingDistance)
            {
                unit = fly;
                distance = flyDistance;
            }
            else
            {
                unit = walking;
                distance = walkingDistance;
            }
            return unit;
        }
        public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, bool isPlayer, out Unit unit, out float distance)
        {
            List<Unit> units = isPlayer ? _enemyWalkingUnits : _playerWalkingUnits;
            unit = GetNearest(currentPosition, units, out distance);
            return unit;
        }
        public bool TryGetNearestFlyUnit(in Vector3 currentPosition, bool isPlayer, out Unit unit, out float distance)
        {
            List<Unit> units = isPlayer ? _enemyFlyUnits : _playerFlyUnits;
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

        private void SubscribeDestroy<T>(List<T> list) where T : IDestroyed
        {
            for (int i = 0; i < list.Count; i++)
            {
                T obj = list[i];
                list[i].Destroyed += RemoveAndUnsubscribe;

                void RemoveAndUnsubscribe()
                {
                    RemoveObjectFromList(list, obj);
                    obj.Destroyed -= RemoveAndUnsubscribe;
                }
            }
        }
        
        private void AddObjectToList<T>(List<T> list, T obj)  where T : IDestroyed
        {
            list.Add(obj);
            obj.Destroyed += RemoveAndUnsubscribe;
            void RemoveAndUnsubscribe()
            {
                RemoveObjectFromList(list, obj);
                obj.Destroyed -= RemoveAndUnsubscribe;
            }
        }
        private void RemoveObjectFromList<T>(List<T> list, T obj) where T : IDestroyed
        {
            if (list.Contains(obj))
                list.Remove(obj);
        }
    }
}