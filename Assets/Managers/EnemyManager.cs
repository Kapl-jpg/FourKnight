using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject zombiePrefab;
    public GameObject wizardPrefab;
    public GameObject batPrefab;
    public GameObject swardPrefab;
    public GameObject bonesPrefab;
    public GameObject[] spawnPointZombie;
    public GameObject[] spawnPointWizard;
    public GameObject[] spawnPointBat;
    public GameObject[] spawnPointArcher;
    public GameObject[] spawnPointBones;
    public GameObject coinParent;
    
    [HideInInspector] public List<GameObject> zombiePool = new List<GameObject>();
    [HideInInspector] public List<GameObject> wizardPool = new List<GameObject>();
    [HideInInspector] public List<GameObject> batPool = new List<GameObject>();
    [HideInInspector] public List<GameObject> swardPool = new List<GameObject>();
    [HideInInspector] public List<GameObject> bonesPool = new List<GameObject>();

}
