using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject prefabSquare;
    [SerializeField] private GameObject poolParent;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private float speed;
    [SerializeField] private float upBorder;
    [SerializeField] private int poolCount;

    List<GameObject> pool = new List<GameObject>();

    void CreatePool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var instance = Instantiate(prefabSquare, poolParent.transform, true);
            instance.SetActive(false);
            pool.Add(instance);
        }
    }

    void Start()
    {
        CreatePool();
        
    }

    GameObject Instance(Vector2 spawnPosition)
    {
        foreach (var instance in pool)
        {
            if (!instance.activeInHierarchy)
            {
                instance.SetActive(true);
                instance.transform.position = spawnPosition;
                return instance;
            }
        }

        return null;
    }

    void Update()
    {
        var leftPosition = left.position;
        Instance(new Vector2(Random.Range(leftPosition.x, right.position.x), leftPosition.y));
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy)
            {
                pool[i].transform.Translate(Vector3.up * speed);
            }

            if (pool[i].transform.position.y > left.position.y + upBorder)
            {
                pool[i].SetActive(false);
            }
        }
    }
}
