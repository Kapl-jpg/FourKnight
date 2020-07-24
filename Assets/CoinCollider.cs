using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public GameObject knight;
    public ScoreManager scoreManager;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == knight)
        {
            scoreManager.coinAmount++;
            Destroy(gameObject);
        }
    }
}

