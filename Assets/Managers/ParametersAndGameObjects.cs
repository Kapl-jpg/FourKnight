using UnityEngine;
using UnityEngine.UI;

public class ParametersAndGameObjects : MonoBehaviour
{
    [Header("Related to the Main camera")] [SerializeField]
    private CameraTranslate cameraTranslate;
    
    [SerializeField]private float cameraSpeed;

    [SerializeField] private Vector3 cameraOffset;
    
    [Header("Related to the character")] [SerializeField] 
    private GameObject[] knights;
    
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private KnightController _knightController;

    [SerializeField] private GameObject theExclamationMark;

    [SerializeField] private GameObject dialogWindow;

    [SerializeField] private Text activeText;

    [SerializeField] private float runningSpeed;

    [SerializeField] private float forceJump;

    [SerializeField] private float speedStair;

    [SerializeField] private float secondsToWaitAnimation;


    void Start()
    {
        cameraTranslate.Offset = cameraOffset;
        cameraTranslate.Speed = cameraSpeed;
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
        _knightController.ActiveMarkText = activeText;
    }
}
