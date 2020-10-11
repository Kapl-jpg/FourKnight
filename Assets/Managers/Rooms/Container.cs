using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Container : MonoBehaviour
{
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private GenerationOfDungeon _generationOfDungeon;

    public GenerationOfDungeon GenerationOfDungeon
    {
        set => _generationOfDungeon = value;
    }

    private int stage;

    public int Stage
    {
        set => stage = value;
    }

    [SerializeField] private GameObject platform;

    [SerializeField] private GameObject floor;

    [SerializeField] private List<GameObject> stairs = new List<GameObject>();

    [SerializeField] private List<GameObject> transparentOverlap = new List<GameObject>();

    [SerializeField] List<GameObject> gates = new List<GameObject>();

    public List<GameObject> Gates => gates;

    [SerializeField] List<GameObject> invisibleGates = new List<GameObject>();

    public List<GameObject> InvisibleGates => invisibleGates;

    [Header("Spawns point")] [SerializeField]
    private Transform spawnKnight;

    public Transform SpawnKnight => spawnKnight;

    [Header("Zombie")] //Create zombie
    [HideInInspector]
    public bool createZombie;

    private List<Transform> zombieSpawnPool = new List<Transform>();

    public List<Transform> ZombieSpawnPool
    {
        get => zombieSpawnPool;
        set => zombieSpawnPool = value;
    }

    [SerializeField] private GameObject zombieSpawnParent;

    public GameObject ZombieSpawnParent
    {
        get => zombieSpawnParent;
        set => zombieSpawnParent = value;
    }
    private readonly List<GameObject> zombiePool = new List<GameObject>();
    private readonly List<int> randomNumberSpawnZombie = new List<int>();
    private GameObject zombiePrefab;

    public GameObject ZombiePrefab
    {
        set => zombiePrefab = value;
    }
    [HideInInspector]
    public AnimationCurve zombieChance;


    [Header("Sward")] //Create sward
    
    [HideInInspector]
    public bool createSward;

    private List<Transform> swardSpawnPool;

    public List<Transform> SwardSpawnPool
    {
        get => swardSpawnPool;
        set => swardSpawnPool = value;
    }

    [SerializeField] private GameObject swardSpawnParent;

    public GameObject SwardSpawnParent
    {
        get => swardSpawnParent;
        set => swardSpawnParent = value;
    }
    
    private List<GameObject> swardPool = new List<GameObject>();
    private List<int> randomNumberSpawnSward = new List<int>();
    private GameObject swardPrefab;

    public GameObject SwardPrefab
    {
        set => swardPrefab = value;
    }
    
    [HideInInspector]
    public AnimationCurve swardChance;

    [Header("Bat")] //Create bat
    [HideInInspector]
    public bool createBat;

    private List<Transform> batSpawnPool;

    public List<Transform> BatSpawnPool
    {
        get => batSpawnPool;
        set => batSpawnPool = value;
    }

    [SerializeField] private GameObject batSpawnParent;

    public GameObject BatSpawnParent
    {
        get => batSpawnParent;
        set => batSpawnParent = value;
    }
    private List<GameObject> batPool = new List<GameObject>();
    private List<int> randomNumberSpawnBat = new List<int>();
    private GameObject batPrefab;
    
    public GameObject BatPrefab
    {
        set => batPrefab = value;
    }
    [HideInInspector]
    public AnimationCurve batChance;

    [Header("Wizard")] //Create wizard
    [HideInInspector]
    public bool createWizard;
    
    private List<Transform> wizardSpawnPool;

    public List<Transform> WizardSpawnPool
    {
        get => wizardSpawnPool;
        set => wizardSpawnPool = value;
    }

    [SerializeField] private GameObject wizardSpawnParent;

    public GameObject WizardSpawnParent
    {
        get => wizardSpawnParent;
        set => wizardSpawnParent = value;
    }
    private List<GameObject> wizardPool = new List<GameObject>();
    private readonly List<int> randomNumberSpawnWizard = new List<int>();
    private GameObject wizardPrefab;

    public GameObject WizardPrefab
    {
        set => wizardPrefab = value;
    }
    [HideInInspector]
    public AnimationCurve wizardChance;


    [Header("Map point")] [SerializeField] private Transform beginMap;

    public Transform BeginMap => beginMap;

    [SerializeField] private Transform endMap;

    public Transform EndMap => endMap;

    [SerializeField] private bool levelClear;

    private Vector3 oneCell;

    private bool _fight;
    [HideInInspector]
    public bool createEnemy;

    public void AddNewSpawn(GameObject parent, List<Transform> pool)
    {
        if (!pool.Contains(null))
        {
            pool.RemoveAll(x => x == null);
        }

        var spawn = new GameObject("Spawn (" + pool.Count+")");
        spawn.transform.parent = parent.transform;
        pool.Add(spawn.transform);
    }

    public void DeleteSpawn(List<Transform> pool, bool deleteAll, bool deleteAllInThisPool)
    {
        if (deleteAll) //Удалить все объекты
        {
            if (zombieSpawnPool != null && zombieSpawnPool.Count > 0)
            {
                for (int i = 0; i < zombieSpawnPool.Count; i++)
                {
                    DestroyImmediate(zombieSpawnPool[i].gameObject);
                    zombieSpawnPool.RemoveAt(i);
                }
            }

            if (batSpawnPool != null && batSpawnPool.Count > 0)
            {
                for (int i = 0; i < batSpawnPool.Count; i++)
                {
                    DestroyImmediate(batSpawnPool[i].gameObject);
                    batSpawnPool.RemoveAt(i);
                }
            }

            if (swardSpawnPool != null && swardSpawnPool.Count > 0)
            {
                for (int i = 0; i < swardSpawnPool.Count; i++)
                {
                    DestroyImmediate(swardSpawnPool[i].gameObject);
                    swardSpawnPool.RemoveAt(i);
                }
            }

            if (wizardSpawnPool != null && wizardSpawnPool.Count > 0)
            {
                for (int i = 0; i < wizardSpawnPool.Count; i++)
                {
                    DestroyImmediate(wizardSpawnPool[i].gameObject);
                    wizardSpawnPool.RemoveAt(i);
                }
            }
        }

        if (pool != null && pool.Count > 0)
        {
            //удалить все объекты в одному пуле
            if (deleteAllInThisPool)
            {
                if (pool.Count > 0)
                {
                    for (int i = 0; i < pool.Count; i++)
                    {
                        DestroyImmediate(pool[i].gameObject);
                        pool.RemoveAt(i);
                    }
                }
            }

            if (!deleteAll)
            {
                if (pool.Count > 0) //Удалять по одному объекту
                {
                    DestroyImmediate(pool[pool.Count - 1].gameObject);
                    pool.Remove(pool[pool.Count - 1]);
                }
            }

            if (pool.Count > 0 && pool.Contains(null))
            {
                pool.RemoveAll(x => x == null);
            }
        }
    }

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

        if (!createEnemy) return;
        if (zombieSpawnPool.Count <= 0 || swardSpawnPool.Count <= 0 || batSpawnPool.Count <= 0 ||
            wizardSpawnPool.Count <= 0) return;
        var randomCountZombie = Random.Range(1, zombieSpawnPool.Count + 1);
        var randomCountSward = Random.Range(1, SwardSpawnPool.Count + 1);
        var randomCountBat = Random.Range(1, BatSpawnPool.Count + 1);
        var randomCountWizard = Random.Range(1, WizardSpawnPool.Count + 1);
        CreateMonster(randomCountZombie, randomCountSward, randomCountBat, randomCountWizard);
    }

    private void Update()
    {
        
    }
    void CreateMonster(int randomCountZombie, int randomCountSward, int randomCountBat, int randomCountWizard)
    {
        //Zombie
        if(createZombie){
            for (var i = 0; i < randomCountZombie; i++)
            {
                var randomValue = Random.Range(0f, 1f);
                if (!(randomValue < zombieChance.Evaluate(stage))) continue;

                var randomSpawn = Random.Range(0, randomCountZombie);
                if (!randomNumberSpawnZombie.Contains(randomSpawn))
                {
                    randomNumberSpawnZombie.Add(randomSpawn);
                    var zombie = Instantiate(zombiePrefab, zombieSpawnPool[randomSpawn]);
                    zombie.transform.parent = zombieSpawnPool[randomSpawn];
                    zombie.SetActive(false);
                    zombiePool.Add(zombie);
                }
                else
                {
                    i--;
                }
            }
        }

        //Sward
        if (createSward)
        {
            for (var i = 0; i < randomCountSward; i++)
            {
                var randomValue = Random.Range(0, 1f);
                if (!(randomValue < swardChance.Evaluate(stage))) continue;
                var randomSpawn = Random.Range(0, randomCountSward);
                if (!randomNumberSpawnSward.Contains(randomSpawn))
                {
                    randomNumberSpawnSward.Add(randomSpawn);
                    var sward = Instantiate(swardPrefab, swardSpawnPool[randomSpawn]);
                    sward.transform.parent = swardSpawnPool[randomSpawn];
                    sward.SetActive(false);
                    swardPool.Add(sward);
                }
                else
                {
                    i--;
                }
            }
        }

        //Bat
        if (createBat)
        {
            for (var i = 0; i < randomCountBat; i++)
            {
                var randomValue = Random.Range(0, 1f);
                if (!(randomValue < batChance.Evaluate(stage))) continue;
                var randomSpawn = Random.Range(0, randomCountBat);
                if (!randomNumberSpawnBat.Contains(randomSpawn))
                {
                    randomNumberSpawnBat.Add(randomSpawn);
                    var bat = Instantiate(batPrefab, batSpawnPool[randomSpawn]);
                    bat.transform.parent = batSpawnPool[randomSpawn];
                    bat.SetActive(false);
                    batPool.Add(bat);
                }
                else
                {
                    i--;
                }
            }
        }

        //Wizard
        if (createWizard)
        {
            for (var i = 0; i < randomCountWizard; i++)
            {
                var randomValue = Random.Range(0, 1f);
                if (!(randomValue < wizardChance.Evaluate(stage))) continue;
                var randomSpawn = Random.Range(0, randomCountWizard);
                if (!randomNumberSpawnWizard.Contains(randomSpawn))
                {
                    randomNumberSpawnSward.Add(randomSpawn); 
                    var bat = Instantiate(wizardPrefab, wizardSpawnPool[randomSpawn]);
                    bat.transform.parent = wizardSpawnPool[randomSpawn];
                    bat.SetActive(false);
                    batPool.Add(bat);
                }
                else
                {
                    i--;
                }
            }
        }
    }
}


