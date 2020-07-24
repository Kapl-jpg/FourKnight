using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        get => _generalInformation;
        set => _generalInformation = value;
    }
    private ChoseKnight _choseKnight;

    public ChoseKnight ChoseKnight
    {
        get => _choseKnight;
        set => _choseKnight = value;
    }
    
    [SerializeField] private GameObject[] platform;

    public GameObject[] Platform
    {
        get => platform;
        set => platform = value;
    }

    [SerializeField] private GameObject[] floor;

    public GameObject[] Floor
    {
        get => floor;
        set => floor = value;
    }

    [SerializeField] private List<GameObject> stairs = new List<GameObject>();


    [SerializeField] private List<GameObject> transparentOverlap = new List<GameObject>();

    [Header("Spawns point")]
    [SerializeField] private Transform spawnKnight;

    public Transform SpawnKnight
    {
        get => spawnKnight;
        set => spawnKnight = value;
    }
    [SerializeField] private Transform[] zombieSpawn;
    [SerializeField] private Transform[] swardSpawn;
    [SerializeField] private Transform[] batSpawn;
    [SerializeField] private Transform[] wizardSpawn;
    [Header("Map point")]
    [SerializeField] private Transform beginMap;

    public Transform BeginMap
    {
        get => beginMap;
        set => beginMap = value;
    }
    [SerializeField] private Transform endMap;

    public Transform EndMap
    {
        get => endMap;
        set => endMap = value;
    }

    
    void Start()
    {
        foreach (var stair in stairs)
        {
            _generalInformation.StairPool.Add(stair);
        }

        foreach (var ground in floor)
        {
            _generalInformation.Ground.Add(ground);
        }

        foreach (var ground in platform)
        {
            _generalInformation.Ground.Add(ground);
        }

        foreach (var trOver in transparentOverlap)
        {
            _generalInformation.TransparentOverlap.Add(trOver);
        }
    }
}
