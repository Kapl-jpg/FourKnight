using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton _instance;

    public void MakeSingleton()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        MakeSingleton();
    }
}