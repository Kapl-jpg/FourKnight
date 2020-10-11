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

    private readonly List<GameObject> pool = new List<GameObject>();

    void CreatePool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var instance = Instantiate(prefabSquare, poolParent.transform, true);
            instance.SetActive(false);
            pool.Add(instance);
        }
    }

    private void Start()
    {
        CreatePool();
    }

    void Instance(Vector2 spawnPosition)
    {
        foreach (var instance in pool)
        {
            if (!instance.activeInHierarchy)
            {
                instance.SetActive(true);
                instance.transform.position = spawnPosition;
                return;
            }
        }
    }

    private void Update()
    {
        var leftPosition = left.position;
        Instance(new Vector2(Random.Range(leftPosition.x, right.position.x), leftPosition.y));
        foreach (var unit in pool)
        {
            if (unit.activeInHierarchy)
            {
                unit.transform.Translate(Vector3.up * speed);
            }

            if (unit.transform.position.y > left.position.y + upBorder)
            {
                unit.SetActive(false);
            }
        }
    }
}
