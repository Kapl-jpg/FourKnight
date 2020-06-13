using UnityEngine;
using UnityEngine.UI;

public class GeneralInformation : MonoBehaviour
{
    private GameObject _dontDestroyManager;

    public GameObject DontDestroyManager
    {
        get => _dontDestroyManager;
        set => _dontDestroyManager = value;
    }

    [Header("Parameters characters")] [SerializeField]

    private SaveLoad _saveLoad;

    public SaveLoad SaveLoad
    {
        get => _saveLoad;
    }

    //Скрипт ведения очков
    private ScoreManager _scoreManager;

    public ScoreManager ScoreManager
    {
        get => _scoreManager;
        set => _scoreManager = value;
    }

    //Скрипт кнопок управления персонажем
    private ButtonControl _buttonControl;

    public ButtonControl ButtonControl
    {
        get => _buttonControl;
    }

    //Скрипт управления персонажем
    private KnightController _knightController;

    public KnightController KnightController
    {
        get => _knightController;
        set => _knightController = value;
    }

    //Скрипт выбора персонажа
    private ChoseKnight _choseKnight;

    //Скрипт, который отслеживает жизни и броню
    private HealthAndArmor _healthAndArmor;

    //Скрипт камеры
    private CameraTranslate _cameraTranslate;

    public CameraTranslate CameraTranslate
    {
        get => _cameraTranslate;
    }

    [Header("Get game objects")] [SerializeField]
    private GameObject mainCamera;

    public GameObject MainCamera
    {
        get => mainCamera;
    }

    [SerializeField] private GameObject portal;

    public GameObject Portal
    {
        get => portal;
        set => portal = value;
    }
    [SerializeField] private GameObject[] ground;

    public GameObject[] Ground
    {
        get => ground;
    }

    [SerializeField] private GameObject stair;

    public GameObject Stair
    {
        get => stair;
    }

    [SerializeField] private GameObject theExclamationMark;

    public GameObject TheExclamationMark
    {
        get => theExclamationMark;
    }

    [SerializeField] private GameObject transparentOverlap;

    public GameObject TransparentOverlap
    {
        get => transparentOverlap;
    }

    private GameObject _activeKnight;

    public GameObject ActiveKnight
    {
        get => _activeKnight;
        set => _activeKnight = value;
    }

    [SerializeField] private string stairText;

    public string StairText
    {
        get => stairText;
    }

    [SerializeField] private string choseKnightText;

    public string ChoseKnightText
    {
        get => choseKnightText;
    }

    [SerializeField] private string portalText;

    public string PortalText
    {
        get => portalText;
    }

    private void Awake()
    {
        _cameraTranslate = mainCamera.GetComponent<CameraTranslate>();
        _saveLoad = gameObject.GetComponent<SaveLoad>();
        _saveLoad.CameraTranslate = _cameraTranslate;
        _saveLoad.Camera = MainCamera.GetComponent<Camera>();
        _cameraTranslate.GeneralInformation = this;
        _dontDestroyManager = GameObject.FindGameObjectWithTag("MainManager");
        _dontDestroyManager.GetComponent<ParametersAndGameObjects>().GeneralInformation = this;
        _healthAndArmor = _dontDestroyManager.GetComponent<HealthAndArmor>();
        _scoreManager = gameObject.GetComponent<ScoreManager>();
        _choseKnight = gameObject.GetComponent<ChoseKnight>();
        _buttonControl = gameObject.GetComponent<ButtonControl>();
    }
}