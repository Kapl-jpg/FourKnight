
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    //Скрипты, связанные с персонажем ***
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private HealthAndArmor _healthAndArmor;

    public HealthAndArmor HealthAndArmor
    {
        get => _healthAndArmor;
        set => _healthAndArmor = value;
    }

    private KnightAnimation _knightAnimation;

    //Параметры персонажа ***
    [SerializeField] private int quantityHealth;
    [SerializeField] private int quantityArmor;
    private List<GameObject> _ground = new List<GameObject>();
    private Text _activeMarkText;

    public Text ActiveMarkText
    {
        get => _activeMarkText;
        set => _activeMarkText = value;
    }

    private GameObject _dialog;

    public GameObject Dialog
    {
        get => _dialog;
        set => _dialog = value;
    }

    private GameObject _theExclamationMark;

    public GameObject TheExclamationMark
    {
        get => _theExclamationMark;
        set => _theExclamationMark = value;
    }

    private float _speed;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private float _force;

    public float Force
    {
        get => _force;
        set => _force = value;
    }

    private float _secondsToWaitAnimation;

    public float SecondsToWaitAnimation
    {
        get => _secondsToWaitAnimation;
        set => _secondsToWaitAnimation = value;
    }

    private float _speedStair;

    public float SpeedStair
    {
        get => _speedStair;
        set => _speedStair = value;
    }

    private bool _stayGround;
    private float _timerToNewAttack;
    private float _timerWait;
    private int _numberJump;
    private bool _attackButton;
    private bool[] _stayStair;
    private bool _climbUp;
    private bool _climbDown;
    private bool[] _atTheTop;
    private bool _afk;
    private int _myNumber;
    private Vector2 _collider;

    private GameObject _portal;

    public int MyNumber
    {
        get => _myNumber;
        set => _myNumber = value;
    }

    public bool Afk
    {
        get => _afk;
        set => _afk = value;
    }

    private bool _trigger;

    public bool Trigger
    {
        get => _trigger;
        set => _trigger = value;
    }

    private bool _stayPortal;

    private bool[] _stayTransparentOverlap;


    //Компоненты персонажа ***

    private Rigidbody2D _rb2D;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    //Передача нажатия на кнопки ***

    public bool AttackButton
    {
        set => _attackButton = value;
    }

    private bool _leftButton;

    public bool LeftButton
    {
        set => _leftButton = value;
    }

    private bool _rightButton;

    public bool RightButton
    {
        set => _rightButton = value;
    }

    private bool _jumpButton;

    public bool JumpButton
    {
        set => _jumpButton = value;
    }

    private CompositeCollider2D[] _transparentOverlapComposite;

    private List<GameObject> _stairs;

    private List<GameObject> _transparentOverlap;

    private bool _clickMark;

    public bool ClickMark
    {
        get => _clickMark;
        set => _clickMark = value;
    }

    [SerializeField] private Vector2 spawnPosition;


    private void GetComponents()
    {
        _ground = _generalInformation.Ground;
        _portal = _generalInformation.Portal;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _knightAnimation = gameObject.GetComponent<KnightAnimation>();
    }

    private void GetParameters()
    {
        _stairs = _generalInformation.StairPool;
        foreach (var transparentOverlap in _generalInformation.TransparentOverlap)
        {
            _ground.Add(transparentOverlap);
        }
        _theExclamationMark = _generalInformation.TheExclamationMark;
        _theExclamationMark.SetActive(false);
        _transparentOverlap = _generalInformation.TransparentOverlap;
        for (int i = 0; i < _transparentOverlap.Count; i++)
        {
            _transparentOverlapComposite[i] = _transparentOverlap[i].GetComponent<CompositeCollider2D>();
        }

        _atTheTop = new bool[_transparentOverlap.Count];
        _stayStair = new bool[_transparentOverlap.Count];
        _stayTransparentOverlap = new bool[_transparentOverlap.Count];
    }

    void Start()
    {
        GetComponents();
        _generalInformation.SaveLoad.SaveNumber(_myNumber);
        if (!_afk)
        {
            GetParameters();
            _spriteRenderer.sortingOrder = 5;
            _healthAndArmor.CreateHealthAndArmor(quantityHealth, quantityArmor);
        }
        else
        {
            _spriteRenderer.sortingOrder = 4;
            transform.position = spawnPosition;
            _rb2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.isTrigger = true;
        }
    }

    void Update()
    {
        _knightAnimation.AttackButton = _attackButton;
        if (!_afk)
        {
            for (int i = 0; i < _stayStair.Length; i++)
            {
                ActiveMark(_trigger || _stayPortal);
            }

            ClimbingTheStairs();
            Portal();
            Controlling();
        }
        else
        {
            Rotate(_generalInformation.ActiveKnight.transform.position.x < transform.position.x ? 180 : 0, _afk);
        }
    }

    void Rotate(float angle, bool afk)
    {
        if (afk)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            if (transform.eulerAngles.y > angle || transform.eulerAngles.y < angle)
            {
                transform.RotateAround(_boxCollider2D.bounds.center, Vector3.up, 180);
            }
        }
    }

    void Controlling()
    {
        if (!_knightAnimation.PlayAttackAnimation && !_climbUp && !_climbDown)
        {
            if (_leftButton) //Идти влево
            {
                _rb2D.velocity = new Vector2(-_speed, _rb2D.velocity.y);
                Rotate(180, _afk);
                _knightAnimation.BoolRun = true;
            }

            if (_rightButton) //Идти вправо
            {
                _rb2D.velocity = new Vector2(_speed, _rb2D.velocity.y);
                Rotate(0, _afk);
                _knightAnimation.BoolRun = true;
            }

            if (_jumpButton && (_stayGround || _numberJump < 1)) //Прыгнуть
            {
                _numberJump++;
                _rb2D.velocity = new Vector2(0, 0);
                _rb2D.AddForce(new Vector2(0, _force));
                _jumpButton = false;
            }
        }

        if (Input.anyKey)
        {
            _knightAnimation.BoolWait = false;
            _timerWait = 0;
        }

        if (!_knightAnimation.BoolAttack && _stayGround && !_knightAnimation.BoolWait && !_knightAnimation.BoolRun &&
            !_knightAnimation.PlayAttackAnimation)
        {
            _knightAnimation.NumberAttack = 0;
        }

        if (!_leftButton && !_rightButton)
        {
            _knightAnimation.BoolRun = false;
        }

        if (_stayGround && !Input.anyKey)
        {
            _timerWait += Time.deltaTime;
        }

        if (_timerWait >= _secondsToWaitAnimation)
        {
            _knightAnimation.BoolWait = true;
        }
    }

    void Portal()
    {
        if (_stayPortal)
        {
            if (_clickMark)
            {
                SceneManager.LoadScene("Parts");
            }
        }
    }

    void ClimbingTheStairs()
    {
        for (int i = 0; i < _stayStair.Length; i++)
        {
            if (_stayStair[i])
            {
                ActiveMark(_stayStair[i]);
                
                if (_clickMark)
                {
                    if (!_atTheTop[i] && !_climbUp && !_climbDown)
                    {
                        _climbUp = true;
                        _climbDown = false;
                    }

                    if (_atTheTop[i] && !_climbDown && !_climbUp)
                    {
                        _climbUp = false;
                        _climbDown = true;
                    }

                    if (_climbUp)
                    {
                        _rb2D.velocity = Vector2.up * _speedStair;
                        _transparentOverlapComposite[i].isTrigger = true;
                        if (_atTheTop[i])
                        {
                            _transparentOverlapComposite[i].isTrigger = false;
                            _climbUp = false;
                            _clickMark = false;
                        }
                    }

                    if (_climbDown)
                    {
                        _transparentOverlapComposite[i].isTrigger = true;
                        if (!_atTheTop[i])
                        {
                            _transparentOverlapComposite[i].isTrigger = false;
                            _climbDown = false;
                            _clickMark = false;
                        }
                    }
                }
                else
                {
                    _transparentOverlapComposite[i].isTrigger = false;
                }

                if (_boxCollider2D.bounds.min.y >= _transparentOverlapComposite[i].bounds.max.y)
                {
                    _atTheTop[i] = true;
                }
                else if (_boxCollider2D.bounds.max.y < _transparentOverlapComposite[i].bounds.min.y)
                {
                    _atTheTop[i] = false;
                }
            }
        }
    }

    void ActiveMark(bool active)
    {
        if (active)
        {
            _dialog.SetActive(true);
            for (int i = 0; i < _stayStair.Length; i++)
            {
                if (_stayStair[i])
                {
                    _activeMarkText.text = _generalInformation.StairText;
                }
            }


            if (_trigger)
            {
                _activeMarkText.text = _generalInformation.ChoseKnightText;
            }

            if (_stayPortal)
            {
                _activeMarkText.text = _generalInformation.PortalText;
            }

            _theExclamationMark.SetActive(true);
        }
        else
        {
            _dialog.SetActive(false);
            _theExclamationMark.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (var land in _ground)
        {
            if (other.gameObject == land)
            {
                _numberJump = 0;
                _stayGround = true;
                _knightAnimation.ResumeAnimation();
                _knightAnimation.Ground = true;
            }
        }

        for (int j = 0; j < _transparentOverlap.Count; j++)
        {
            if (other.gameObject == _transparentOverlap[j])
            {
                _stayTransparentOverlap[j] = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_ground != null)
        {
            foreach (var land in _ground)
            {
                if (other.gameObject == land)
                {
                    _stayGround = false;
                    _knightAnimation.Ground = false;
                }
            }

            for (int j = 0; j < _transparentOverlap.Count; j++)
            {
                if (other.gameObject == _transparentOverlap[j])
                {
                    _stayTransparentOverlap[j] = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_stairs != null)
        {
            for (int i = 0; i < _stairs.Count; i++)
            {
                if (other.gameObject == _stairs[i])
                {
                    _stayStair[i] = true;
                }
            }
        }

        if (_portal != null)
        {
            if (other.gameObject == _portal)
            {
                _stayPortal = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_stairs != null)
        {
            for (int i = 0; i < _stairs.Count; i++)
            {
                if (other.gameObject == _stairs[i])
                {
                    _stayStair[i] = false;
                }
            }
        }

        if (_portal != null)
        {
            if (other.gameObject == _portal)
            {
                _stayPortal = false;
            }
        }
    }
}