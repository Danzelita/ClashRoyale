using UnityEngine;

namespace Project.Scripts.States
{
    public abstract class UnitState : ScriptableObject
    {
        protected Unit _unit;

        public virtual void Construct(Unit unit)
        {
            _unit = unit;
        }
        
        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
        
#if UNITY_EDITOR
        public virtual void DebugDraw(Unit unit)
        {
        }
#endif
    }
}