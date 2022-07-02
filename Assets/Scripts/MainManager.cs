using MapGeneration;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public Map Map { get; private set; }
    
    [SerializeField]
    private MapGenerator _mapGenerator;
    [SerializeField]
    private MapGenerationSettings _mapGenerationSettings;
    
    private void Start()
    {
        Map = _mapGenerator.Generate(_mapGenerationSettings);
    }
}
