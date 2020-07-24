using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationOfDungeon : MonoBehaviour
{
    [SerializeField] private GeneralInformation generalInformation;
    [SerializeField] private ChoseKnight choseKnight;
    [SerializeField] private GameObject startLevel;
    [SerializeField] private GameObject[] levelRooms;
    [SerializeField] private List<GameObject> poolRooms = new List<GameObject>();
    private List<Transform> beginTransform = new List<Transform>();
    private List<Transform> endTransform = new List<Transform>();
    [SerializeField] private int countRooms;

    private List<int> _listNumbers = new List<int>();

    private void Awake()
    {
        CreateStartLevel();
    }

    void Start()
    {
       
        CreateLevels();
    }

    void CreateStartLevel()
    {
        var start = Instantiate(startLevel);
        poolRooms.Add(start);
        beginTransform.Add(start.GetComponent<Container>().BeginMap);
        endTransform.Add(start.GetComponent<Container>().EndMap);
        start.GetComponent<Container>().GeneralInformation = generalInformation;
        start.GetComponent<Container>().ChoseKnight = choseKnight;
        generalInformation.SpawnPointMainKnight = start.GetComponent<Container>().SpawnKnight;
    }

    void CreateLevels()
    {
        if (levelRooms.Length < countRooms)
        {
            countRooms = levelRooms.Length;
        }

        for (int i = 0; i < countRooms; i++)
        {
            if (i > 0)
            {
            int randomNumber = Random.Range(0, levelRooms.Length);
            if (!_listNumbers.Contains(randomNumber))
            {
                _listNumbers.Add(randomNumber);
                poolRooms.Add(Instantiate(levelRooms[randomNumber]));
                beginTransform.Add(poolRooms[i].GetComponent<Container>().BeginMap);
                endTransform.Add(poolRooms[i].GetComponent<Container>().EndMap);
                poolRooms[i].GetComponent<Container>().GeneralInformation = generalInformation;
            }
            else
            {
                i--;
            }

                poolRooms[i].transform.position = endTransform[i - 1].position +
                                                  (poolRooms[i].transform.position - beginTransform[i].position);
            }
        }
    }
}