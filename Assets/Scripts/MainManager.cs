using MapGeneration;
using UnityEngine;
using Units;

public class MainManager : MonoBehaviour
{
    public Map Map { get; private set; }
    
    [SerializeField]
    private MapGenerator _mapGenerator;
    [SerializeField]
    private MapGenerationSettings _mapGenerationSettings;
    [SerializeField]
    private UnitsSettings _unitsSettings;
    
    private UnitManager _unitManager;
    
    private void Start()
    {
        Map = _mapGenerator.Generate(_mapGenerationSettings);
        _unitManager = new UnitManager(Map, _unitsSettings);
    }

    private void OnDestroy()
    {
        _unitManager.Dispose();
    }
}
