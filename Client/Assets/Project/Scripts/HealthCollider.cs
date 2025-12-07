using UnityEngine;

namespace Project.Scripts
{
    public class HealthCollider : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }
    }
}