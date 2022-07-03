using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitsSettings", menuName = "UnitsSettings")]
    public class UnitsSettings : ScriptableObject
    {
        public Unit UnitPrefab;
        public float AnimalDetectRadius;
        public float TargetUpdateCooldown;
        public float MovementErrorDistance;
        public float HunterDetectRadius;
        public float HunterWanderDistance;
    }
}