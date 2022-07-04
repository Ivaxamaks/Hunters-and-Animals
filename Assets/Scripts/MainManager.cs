using MapGeneration;
using UnityEngine;
using Units;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator _mapGenerator;
    [SerializeField]
    private MapGenerationSettings _mapGenerationSettings;
    [SerializeField]
    private UnitsSettings _unitsSettings;
    
    private UnitManager _unitManager;
    private GameStateController _gameStateController;
    
    private void Start()
    {
        var map = _mapGenerator.Generate(_mapGenerationSettings);
        _unitManager = new UnitManager(map, _unitsSettings);
        _gameStateController = new GameStateController();
    }

    private void OnDestroy()
    {
        _unitManager.Dispose();
        _gameStateController.Dispose();
    }
}
