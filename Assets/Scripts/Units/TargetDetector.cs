using UnityEngine;

namespace Units
{
    public class TargetDetector
    {
        public Unit Target { get; private set; }
        
        private float _updateReadyTimer;
        
        public void DetectTarget(UnitsSettingsProvider settings, UnitType currentType, Vector3 position)
        {
            _updateReadyTimer += Time.deltaTime;
            var cooldown = settings.TargetUpdateCooldown;
            if (_updateReadyTimer <= cooldown)
            {
                return;
            }
            
            _updateReadyTimer = 0;
            var detectRadius = settings.GetDetectRadius(currentType);
            Target = GetTarget(currentType, position, detectRadius);
        }

        private Unit GetTarget(UnitType currentType, Vector3 position, float radius)
        {
            var layerMask = GetLayerMask(currentType);
            var maxColliders = 15;
            var hits = new Collider[maxColliders];
            var hitsCount = Physics.OverlapSphereNonAlloc(position, radius, hits, layerMask);
            return hitsCount == 0 ? null : GetNearestTarget(hits, hitsCount, position);
        }

        private int GetLayerMask(UnitType currentType)
        {
            var targetType = currentType == UnitType.Hunter ? UnitType.Animal : UnitType.Hunter;
            var layerName = targetType.ToString();
            var layerIndex = LayerMask.NameToLayer(layerName);
            return 1 << layerIndex;
        }

        private Unit GetNearestTarget(Collider[] hits, int hitsCount, Vector3 position)
        {
            var hit = hits[0];
            var distance = Vector3.Distance(hits[0].gameObject.transform.position, position);
            for (var i = 0; i < hitsCount; i++)
            {
                var comperingDistance = Vector3.Distance(hits[i].gameObject.transform.position, position);
                if (!(distance > comperingDistance)) continue;
                hit = hits[i];
                distance = comperingDistance;
            }
            
            return hit.GetComponent<Unit>();
        }
    }
}