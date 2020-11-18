using System.Collections.Generic;
using UnityEngine;

public class CollectionsOfContainer : MonoBehaviour
{
    public List<GameObject> Floor { get; set; } = new List<GameObject>();
    public List<GameObject> Platforms { get; set; } = new List<GameObject>();
    public List<GameObject> Stair { get; set; } = new List<GameObject>();
    public List<GameObject> TransparentOverlap { get; set; } = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
