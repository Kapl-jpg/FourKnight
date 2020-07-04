using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private GeneralInformation generalInformation;

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

    [SerializeField] private GameObject[] stairs;

    public GameObject[] Stairs
    {
        get => stairs;
        set => stairs = value;
    }

    [SerializeField] private GameObject[] transparentOverlap;

    public GameObject[] TransparentOverlap
    {
        get => transparentOverlap;
        set => transparentOverlap = value;
    }

    [SerializeField] private Transform[] zombieSpawn;
    [SerializeField] private Transform[] swardSpawn;
    [SerializeField] private Transform[] batSpawn;
    [SerializeField] private Transform[] wizardSpawn;

    void Start()
    {
        generalInformation.StairPool = stairs;

        foreach (var ground in floor)
        {
            generalInformation.Ground.Add(ground);
        }
        foreach (var ground in platform)
        {
            generalInformation.Ground.Add(ground);
        }

        generalInformation.TransparentOverlap = transparentOverlap;
        
    }
}
