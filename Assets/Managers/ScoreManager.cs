using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject coinPrefab;
    public GameObject[] spawnPointCoin;
    public GameObject coinParent;
    public Text coinAmountText;
    public int coinAmount;
    
    [HideInInspector] public List<GameObject> coinPool = new List<GameObject>();

    void Start()
    {
        if (spawnPointCoin != null)
        {
            for (int i = 0; i < spawnPointCoin.Length; i++)
            {
                var inst = Instantiate(coinPrefab, spawnPointCoin[i].transform.position, Quaternion.identity);
                inst.transform.parent = coinParent.transform;
                coinPool.Add(inst);
                coinPool[i].GetComponent<CoinCollider>().scoreManager = this;
            }
        }
    }

    public void GetKnight()
    {
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (coinPool[i].GetComponent<CoinCollider>().knight == null)
            {
                coinPool[i].GetComponent<CoinCollider>().knight = knight;
            }
        }
    }

    void Update()
    {
        coinAmountText.text = coinAmount.ToString();
    }
}
