using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gates : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D wallCollider2D;
    [SerializeField] private float speed;
    [SerializeField] private bool openGates;
    
    private TilemapCollider2D _currentCollider2D;

    private void Start()
    {
        _currentCollider2D = GetComponent<TilemapCollider2D>();
    }


    void Update()
    {
        if (openGates)
        {
            if (_currentCollider2D.bounds.min.y < wallCollider2D.bounds.min.y)
            {
                transform.Translate(Vector3.up * speed);
            }
        }
        else
        {
            
            if (_currentCollider2D.bounds.max.y > wallCollider2D.bounds.min.y)
            {
                transform.Translate(Vector3.down *speed);
            }
        }
    }
}
