using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitsSettings", menuName = "UnitsSettings")]
    public class UnitsSettings : ScriptableObject
    {
        public Unit UnitPrefab;
    }
}