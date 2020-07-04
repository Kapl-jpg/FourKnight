using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve zombieAnimationCurve;
    private GameObject knight;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject wizardPrefab;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject swardPrefab;

    [SerializeField] private List<GameObject> zombiePool = new List<GameObject>();
    [SerializeField] private List<GameObject> wizardPool = new List<GameObject>();
    [SerializeField] private List<GameObject> batPool = new List<GameObject>();
    [SerializeField] private List<GameObject> swardPool = new List<GameObject>();

    [SerializeField] private Transform[] zombiePrefabSpawn;
    [SerializeField] private Transform[] wizardPrefabSpawn;
    [SerializeField] private Transform[] batPrefabSpawn;
    [SerializeField] private Transform[] swardPrefabSpawn;

    
    private void Start()
    {
        
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            float randomValue = Random.Range(0, 101);
            print(randomValue);
            if (randomValue/100 < zombieAnimationCurve.Evaluate(levelNumber))
            {
                zombiePool.Add(Instantiate(zombiePrefab));
                
            }
        }*/
    }
}
