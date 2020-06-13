using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndArmor : MonoBehaviour
{
    [Header("General settings")] [SerializeField]
    private float indicators;
    [SerializeField] private float scale;
    [SerializeField] private float interval;
    [SerializeField] private Vector2 offset;

    [Header("Health image parameters")] [SerializeField]
    private Image healthImage;

    [SerializeField] private GameObject healthParent;
    [SerializeField] private float minAnchorsXHealth;
    [SerializeField] private float minAnchorsYHealth;
    [SerializeField] private float maxAnchorsXHealth;
    [SerializeField] private float maxAnchorsYHealth;

    [Header("Armor image parameters")] [SerializeField]
    private Image armorImage;

    [SerializeField] private GameObject armorParent;
    [SerializeField] private float minAnchorsXArmor;
    [SerializeField] private float minAnchorsYArmor;
    [SerializeField] private float maxAnchorsXArmor;
    [SerializeField] private float maxAnchorsYArmor;

    private readonly List<Image> _healthPool = new List<Image>();
    private readonly List<Image> _armorPool = new List<Image>();


    private void Start()
    {
        for (int i = _armorPool.Count; i < indicators; i++)
        {
            _armorPool.Add(Instantiate(armorImage));
            _armorPool[i].transform.SetParent(armorParent.transform);
            _armorPool[i].rectTransform.anchorMax =
                new Vector2(maxAnchorsXArmor + interval * i + offset.x, maxAnchorsYArmor + offset.y);
            _armorPool[i].rectTransform.anchorMin =
                new Vector2(minAnchorsXArmor + interval * i + offset.x, minAnchorsYArmor + offset.y);
            _armorPool[i].rectTransform.offsetMax = new Vector2(0, 0);
            _armorPool[i].rectTransform.offsetMin = new Vector2(0, 0);
            _armorPool[i].rectTransform.anchoredPosition3D = Vector3.zero;
            _armorPool[i].rectTransform.localScale = new Vector3(scale, scale, 0);
            _armorPool[i].gameObject.SetActive(false);
        }

        for (int i = _healthPool.Count; i < indicators; i++)
        {
            _healthPool.Add(Instantiate(healthImage));
            _healthPool[i].transform.SetParent(healthParent.transform);
            _healthPool[i].rectTransform.anchorMax =
                new Vector2(maxAnchorsXHealth + interval * i + offset.x, maxAnchorsYHealth + offset.y);
            _healthPool[i].rectTransform.anchorMin =
                new Vector2(minAnchorsXHealth + interval * i + offset.x, minAnchorsYHealth + offset.y);
            _healthPool[i].rectTransform.offsetMax = new Vector2(0, 0);
            _healthPool[i].rectTransform.offsetMin = new Vector2(0, 0);
            _healthPool[i].rectTransform.anchoredPosition3D = Vector3.zero;
            _healthPool[i].rectTransform.localScale = new Vector3(scale, scale, 0);
            _healthPool[i].gameObject.SetActive(false);
        }
    }

    public void CreateHealthAndArmor(int healthAmount, int armorAmount)
    {
        for (int i = 0; i < healthAmount; i++)
        {
            _healthPool[i].gameObject.SetActive(true);
        }

        for (int i = healthAmount; i < _healthPool.Count; i++)
        {
            _healthPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < armorAmount; i++)
        {
            _armorPool[i].gameObject.SetActive(true);
        }

        for (int i = armorAmount; i < _armorPool.Count; i++)
        {
            _armorPool[i].gameObject.SetActive(false);
        }
    }
}