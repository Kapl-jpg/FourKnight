using UnityEngine;
using UnityEngine.UI;

public class ParametersCharacter : MonoBehaviour
{
    [SerializeField] private SingletonParameters singletonParameters;
    
    [Header("Related to the character")]
    private GeneralInformation _generalInformation;
    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private KnightController _knightController;

    [SerializeField] private GameObject theExclamationMark;

    [SerializeField] private Text coinText;
    
    [SerializeField] private int coin;

    private void Start()
    {
        coinText.text = coin.ToString();
    }

    public void SetValue()
    {
        _knightController.TheExclamationMark = theExclamationMark;
    }
}
