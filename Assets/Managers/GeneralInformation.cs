using UnityEngine;

public class GeneralInformation : MonoBehaviour
{
    [Header("Parameters characters")] [SerializeField] private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    [SerializeField] private float speedStairs;

    public float SpeedStairs
    {
        get => speedStairs;
        set => speedStairs = value;
    }
    [SerializeField] private float force;

    public float Force
    {
        get => force;
        set => force = value;
    }
    [SerializeField] private float secondsToWaitAnimation;

    public float SecondsToWaitAnimation
    {
        get => secondsToWaitAnimation;
        set => secondsToWaitAnimation = value;
    }
    
    private SaveLoad _saveLoad;

    public SaveLoad SaveLoad
    {
        get => _saveLoad;
        set => _saveLoad = value;
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
        set => _buttonControl = value;
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

    public ChoseKnight ChoseKnight
    {
        get => _choseKnight;
        set => _choseKnight = value;
    }
    //Скрипт, который отслеживает жизни и броню
    private HealthAndArmor _healthAndArmor;

    public HealthAndArmor HealthAndArmor
    {
        get => _healthAndArmor;
        set => _healthAndArmor = value;
    }
    //Скрипт камеры
    private CameraTranslate _cameraTranslate;

    public CameraTranslate CameraTranslate
    {
        get => _cameraTranslate;
        set => _cameraTranslate = value;
    }
    [Header("Get game objects")]
    
    [SerializeField] private GameObject mainCamera;

    public GameObject MainCamera
    {
        get => mainCamera;
        set => mainCamera = value;
    }

    [SerializeField] private GameObject[] ground;

    public GameObject[] Ground
    {
        get => ground;
        set => ground = value;
    }

    [SerializeField] private GameObject stair;

    public GameObject Stair
    {
        get => stair;
        set => stair = value;
    }

    [SerializeField] private GameObject theExclamationMark;

    public GameObject TheExclamationMark
    {
        get => theExclamationMark;
        set => theExclamationMark = value;
    }

    [SerializeField] private GameObject transparentOverlap;

    public GameObject TransparentOverlap
    {
        get => transparentOverlap;
        set => transparentOverlap = value;
    }
    
    private GameObject _activeKnight;

    public GameObject ActiveKnight
    {
        get => _activeKnight;
        set => _activeKnight = value;
    }
    private void Awake()
    {
        _cameraTranslate = mainCamera.GetComponent<CameraTranslate>();
        _saveLoad = gameObject.GetComponent<SaveLoad>();
        _saveLoad.CameraTranslate = _cameraTranslate;
        _saveLoad.Camera = MainCamera.GetComponent<Camera>();
        _cameraTranslate.GeneralInformation = this;
        _healthAndArmor = gameObject.GetComponent<HealthAndArmor>();
        _scoreManager = gameObject.GetComponent<ScoreManager>();
        _choseKnight = gameObject.GetComponent<ChoseKnight>();
        _buttonControl = gameObject.GetComponent<ButtonControl>();
    }
}