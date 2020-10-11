using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoom : MonoBehaviour
{    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private GenerationOfDungeon _generationOfDungeon;

    public GenerationOfDungeon GenerationOfDungeon
    {
        set => _generationOfDungeon = value;
    }

    [SerializeField]private int roomPriority;

    public int RoomPriority
    {
        set => roomPriority = value;
    }

    [SerializeField] private GameObject platform;

    [SerializeField] private GameObject floor;

    [SerializeField] private List<GameObject> stairs = new List<GameObject>();

    [SerializeField] private List<GameObject> transparentOverlap = new List<GameObject>();

    [SerializeField] List<GameObject> gates = new List<GameObject>();

    public List<GameObject> Gates => gates;

    [SerializeField] List<GameObject> invisibleGates = new List<GameObject>();

    public List<GameObject> InvisibleGates => invisibleGates;

    [Header("Map point")] [SerializeField] private Transform beginMap;

    public Transform BeginMap => beginMap;

    [SerializeField] private Transform endMap;

    public Transform EndMap => endMap;

    [SerializeField] private bool levelClear;

    private Vector3 oneCell;
    
    [Header("Spawns point")] [SerializeField]
    private Transform spawnKnight;

    public Transform SpawnKnight => spawnKnight;

    void Start()
    {
        _generalInformation.Ground.Add(platform);
        _generalInformation.Ground.Add(floor);
        oneCell = GetComponent<Grid>().cellSize;
        foreach (var stair in stairs)
        {
            _generalInformation.StairPool.Add(stair);
        }

        foreach (var trOver in transparentOverlap)
        {
            _generalInformation.TransparentOverlap.Add(trOver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
