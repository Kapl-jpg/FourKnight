using System.Collections.Generic;
using UnityEngine;

public class GeneralInformation : MonoBehaviour
{
    private GameObject _dontDestroyManager;

    public GameObject DontDestroyManager
    {
        get => _dontDestroyManager;
        set => _dontDestroyManager = value;
    }

    [Header("Parameters characters")]

    private SaveLoad _saveLoad;

    public SaveLoad SaveLoad
    {
        get => _saveLoad;
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

    private GameObject[] transparentOverlap;

    public GameObject[] TransparentOverlap
    {
        get => transparentOverlap;
        set => transparentOverlap = value;
    }
    [SerializeField] private List <GameObject> ground;

    public List<GameObject> Ground
    {
        get => ground;
        set => ground = value;
    }

    [SerializeField] private GameObject theExclamationMark;

    public GameObject TheExclamationMark
    {
        get => theExclamationMark;
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

    private GameObject[] stairPool;

    public GameObject[] StairPool
    {
        get => stairPool;
        set => stairPool = value;
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
        _buttonControl = gameObject.GetComponent<ButtonControl>();
    }
}