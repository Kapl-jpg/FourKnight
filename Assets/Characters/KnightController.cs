using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnightController : MonoBehaviour
{
    public CollectionsOfContainer CollectionsOfContainer;
    //Скрипты, связанные с персонажем ***

    private HealthAndArmor _healthAndArmor;

    public HealthAndArmor HealthAndArmor
    {
        get => _healthAndArmor;
        set => _healthAndArmor = value;
    }

    private KnightAnimation _knightAnimation;

    //Параметры персонажа
    [SerializeField] private int quantityHealth;
    [SerializeField] private int quantityArmor;

    private GameObject _theExclamationMark;

    public GameObject TheExclamationMark
    {
        get => _theExclamationMark;
        set => _theExclamationMark = value;
    }

    public float Speed { get; set; }

    public float JumpForce { get; set; }

    public float SecondsToWaitAnimation { get; set; }

    public float SpeedStair { get; set; }

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

    private bool jumpButton;

    public bool JumpButton
    {
        set => jumpButton = value;
    }

    private List<CompositeCollider2D> _transparentOverlapComposite = new List<CompositeCollider2D>();

    public List<GameObject> Stairs { get; set; } = new List<GameObject>();

    public List<GameObject> TransparentOverlap { get; set; } = new List<GameObject>();

    public List<GameObject> Platforms { get; set; }= new List<GameObject>();
    
    public List<GameObject> Floor { get; set; } = new List<GameObject>();
    
    public bool ClickMark { get; set; }

    [SerializeField] private Vector2 spawnPosition;


    private void GetComponents()
    {
       // CollectionsOfContainer = FindObjectOfType<CollectionsOfContainer>();
        
            foreach (var platform in CollectionsOfContainer.Platforms)
            {
                Platforms.Add(platform);
            }

            foreach (var floor in CollectionsOfContainer.Floor)
            {
                Floor.Add(floor);
            }

            foreach (var stair in CollectionsOfContainer.Stair)
            {
                Stairs.Add(stair);
            }

            foreach (var overlap in CollectionsOfContainer.TransparentOverlap)
            {
                TransparentOverlap.Add(overlap);
            }
        
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _knightAnimation = gameObject.GetComponent<KnightAnimation>();
    }

    private void GetParameters()
    {
        //_theExclamationMark = generalInformation.TheExclamationMark;
//        _theExclamationMark.SetActive(false);


        for (int i = 0; i < TransparentOverlap.Count; i++)
        {
            _transparentOverlapComposite.Add(TransparentOverlap[i].GetComponent<CompositeCollider2D>());
        }

        _atTheTop = new bool[TransparentOverlap.Count];
        _stayStair = new bool[TransparentOverlap.Count];
        _stayTransparentOverlap = new bool[TransparentOverlap.Count];
    }

    private void Awake()
    {
        
    }

    void Start()
    {
        
        GetComponents();
        //generalInformation.SaveLoad.SaveNumber(_myNumber);
        if (!_afk)
        {
            GetParameters();
            _spriteRenderer.sortingOrder = 5;
//            _healthAndArmor.CreateHealthAndArmor(quantityHealth, quantityArmor);
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
        //_knightAnimation.AttackButton = _attackButton;
        if (!_afk)
        {
           /* for (int i = 0; i < _stayStair.Length; i++)
            {
                ActiveMark(_trigger || _stayPortal);
            }*/

            //ClimbingTheStairs();
            //Portal();
            Controlling();
        }
        else
        {
            //Rotate(generalInformation.ActiveKnight.transform.position.x < transform.position.x ? 180 : 0, _afk);
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
                _rb2D.velocity = new Vector2(-Speed, _rb2D.velocity.y);
                Rotate(180, _afk);
                _knightAnimation.BoolRun = true;
            }

            if (_rightButton) //Идти вправо
            {
                _rb2D.velocity = new Vector2(Speed, _rb2D.velocity.y);
                Rotate(0, _afk);
                _knightAnimation.BoolRun = true;
            }

            if (jumpButton && (_stayGround || _numberJump < 1)) //Прыгнуть
            {
                _numberJump++;
                _rb2D.velocity = new Vector2(0, 0);
                _rb2D.AddForce(new Vector2(0, JumpForce));
                jumpButton = false;
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

        if (_timerWait >= SecondsToWaitAnimation)
        {
            _knightAnimation.BoolWait = true;
        }
    }

    void Portal()
    {
        if (_stayPortal)
        {
            if (ClickMark)
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
               // ActiveMark(_stayStair[i]);
                
                if (ClickMark)
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
                        _rb2D.velocity = Vector2.up * SpeedStair;
                        _transparentOverlapComposite[i].isTrigger = true;
                        if (_atTheTop[i])
                        {
                            _transparentOverlapComposite[i].isTrigger = false;
                            _climbUp = false;
                            ClickMark = false;
                        }
                    }

                    if (_climbDown)
                    {
                        _transparentOverlapComposite[i].isTrigger = true;
                        if (!_atTheTop[i])
                        {
                            _transparentOverlapComposite[i].isTrigger = false;
                            _climbDown = false;
                            ClickMark = false;
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
            _theExclamationMark.SetActive(true);
        }
        else
        {
            _theExclamationMark.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (var platform in Platforms.Where(platform => other.gameObject == platform))
        {
            _numberJump = 0;
            _stayGround = true;
            _knightAnimation.ResumeAnimation();
            _knightAnimation.Ground = true;
        }
        foreach (var overlap in TransparentOverlap)
        {
            if (other.gameObject == overlap)
            {
                _numberJump = 0;
                _stayGround = true;
                _knightAnimation.ResumeAnimation();
                _knightAnimation.Ground = true;
            }
        }
        foreach (var floor in Floor)
        {
            if (other.gameObject == floor)
            {
                _numberJump = 0;
                _stayGround = true;
                _knightAnimation.ResumeAnimation();
                _knightAnimation.Ground = true;
            }
        }

        for (int j = 0; j < TransparentOverlap.Count; j++)
        {
            if (other.gameObject == TransparentOverlap[j])
            {
                _stayTransparentOverlap[j] = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        foreach (var overlap in TransparentOverlap)
        {
            if (other.gameObject != overlap) continue;
            _stayGround = false;
            _knightAnimation.Ground = false;
        }
        foreach (var platform in Platforms)
        {
            if (other.gameObject != platform) continue;
            _stayGround = false;
            _knightAnimation.Ground = false;
        }

        foreach (var floor in Floor)
        {
            if (other.gameObject != floor) continue;
            _stayGround = false;
            _knightAnimation.Ground = false;
        }

        for (int j = 0; j < TransparentOverlap.Count; j++)
        {
            if (other.gameObject == TransparentOverlap[j])
            {
                _stayTransparentOverlap[j] = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Stairs != null)
        {
            for (int i = 0; i < Stairs.Count; i++)
            {
                if (other.gameObject == Stairs[i])
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
        if (Stairs != null)
        {
            for (int i = 0; i < Stairs.Count; i++)
            {
                if (other.gameObject == Stairs[i])
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