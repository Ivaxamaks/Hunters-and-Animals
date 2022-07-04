using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitsSettings", menuName = "UnitsSettings")]
    public class UnitsSettings : ScriptableObject
    {
        public Unit UnitPrefab;
        public float TargetUpdateCooldown;
        public float MovementErrorDistance;
        public float HunterDetectRadius;
        public Vector2 HunterWanderDistance;
        public Vector2 AnimalMinMaxRunDistance;
        public float AnimalDetectRadius;
    }
}