using System;
using UnityEngine;
using UnityEngine.UI;

public class ParametersCharacter : MonoBehaviour
{
    [Header("Related to the character")]
    
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private KnightController _knightController;

    [SerializeField] private GameObject theExclamationMark;

    [SerializeField] private GameObject dialogWindow;

    [SerializeField] private Text activeMarkText;

    [SerializeField] private float runningSpeed;

    [SerializeField] private float forceJump;

    [SerializeField] private float speedStair;

    [SerializeField] private float secondsToWaitAnimation;

    [SerializeField] private Text coinText;
    
    [SerializeField] private int coin;

    private void Start()
    {
        coinText.text = coin.ToString();
    }

    public void SetValue()
    {
        _knightController = _generalInformation.ActiveKnight.GetComponent<KnightController>();
        _knightController.Speed = runningSpeed;
        _knightController.SpeedStair = speedStair;
        _knightController.Force = forceJump;
        _knightController.SecondsToWaitAnimation = secondsToWaitAnimation;
        _knightController.Dialog = dialogWindow;
        _knightController.TheExclamationMark = theExclamationMark;
        _knightController.ActiveMarkText = activeMarkText;
    }
}
