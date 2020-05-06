using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class KnightController : MonoBehaviour
{
    [Header("Defence/Prefab")] [SerializeField]
    private GameObject prefabSquare;

    [SerializeField] private GameObject parentSquare;
    [SerializeField] [Range(0, 0.1f)] private float speedTranslate;
    [SerializeField] [Range(0, 500)] private int prefabsAmount;
    [SerializeField] [Range(0, 0.5f)] private float timeToNewPrefab;
    [SerializeField] [Range(0, 0.5f)] private float prefabBorder;
    private SpriteRenderer[] _spriteRenderers;
    private float[] _aInColor;
    private Vector3[] _prefabSpawnPosition;
    private float _timer;
    private List<GameObject> _pool = new List<GameObject>();

    [Header("Character")] [SerializeField] private GameObject grow;
    [SerializeField] [Range(0, 5)] private float speed;
    [SerializeField] [Range(0, 5)] private float force;
    [SerializeField] [Range(0, 10)] private int secondsToWaitAnimation;
    private Rigidbody2D _rb2D;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _timerWait;
    private float _colliderOffsetX;

    public bool damage;
    public bool guard;
    public bool defence;
    private bool _ground;
    private bool _attack;
    private bool _run;
    private bool _wait;
    private bool _stay;

    [Header("Health")] [SerializeField]
    private GameObject[] health;
    [SerializeField] private float eternityTime;
    private bool eternity;
    public int healthAmount;
    private float _eternityTimer;

    [Header("Armor")] [SerializeField] private GameObject[] armor;
    [SerializeField] private float recoveryArmorTime;
     public int armorAmount;
    private float _recoveryArmorTimer;
    
    void Pooled()
    {
        for (int i = 0; i < prefabsAmount; i++)
        {
            var instant = Instantiate(prefabSquare);
            instant.SetActive(false);
            instant.transform.parent = parentSquare.transform;
            _pool.Add(instant);
        }
    }

    void GetComponents()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        GetComponents();
        _colliderOffsetX = _boxCollider2D.offset.x;
        healthAmount = health.Length;
        armorAmount = armor.Length;
        _spriteRenderers = new SpriteRenderer[prefabsAmount];
        _aInColor = new float[prefabsAmount];
        _prefabSpawnPosition = new Vector3[prefabsAmount];
        Pooled();
        for (int i = 0; i < prefabsAmount; i++)
        {
            _spriteRenderers[i] = _pool[i].GetComponent<SpriteRenderer>();
            _prefabSpawnPosition[i] = new Vector3(
                Random.Range(-_boxCollider2D.bounds.extents.x, _boxCollider2D.bounds.extents.x),
                -_boxCollider2D.bounds.extents.y);
        }
    }

    void Update()
    {
        ArmorPoint();
        HealthPoint();
        Animation();
        Controlling();
        Defense();
    }

    public GameObject Instantiate(Vector3 prefabPosition)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                _pool[i].SetActive(true);
                _pool[i].transform.position = prefabPosition + _prefabSpawnPosition[i];
                return _pool[i];
            }
        }

        return null;
    }

    void Defense()
    {
        if (defence)
        {
            _timer += Time.deltaTime;
            if (_timer > timeToNewPrefab * speedTranslate)
            {
                Instantiate(_boxCollider2D.bounds.center);
                _timer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            _timer = 0;
        }

        for (int i = 0; i < _pool.Count; i++)
        {
            _aInColor[i] = 1 - (_pool[i].transform.position.y / (transform.position.y + prefabBorder));
            if (_pool[i].activeInHierarchy)
            {
                _pool[i].transform.position = new Vector3(_boxCollider2D.bounds.center.x + _prefabSpawnPosition[i].x,
                    _pool[i].transform.position.y);
                _pool[i].transform.Translate(new Vector3(0, speedTranslate));
                _spriteRenderers[i].color = new Color(_spriteRenderers[i].color.r, _spriteRenderers[i].color.g,
                    _spriteRenderers[i].color.b, _aInColor[i]);
                if (_pool[i].transform.position.y > transform.position.y + prefabBorder)
                {
                    _pool[i].SetActive(false);
                }
            }
        }
    }

    void Controlling()
    {
        if (Input.GetKey(KeyCode.V) && armorAmount > 0)
        {
            defence = true;
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            defence = false;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.A) && !_attack) //Идти влево
        {
            _rb2D.velocity = new Vector2(-speed, _rb2D.velocity.y);
            _spriteRenderer.flipX = true;
            _boxCollider2D.offset = new Vector2(-_colliderOffsetX, _boxCollider2D.offset.y);
            _run = true;
        }

        if (Input.GetKey(KeyCode.D) && !_attack) //Идти вправо
        {
            _rb2D.velocity = new Vector2(speed, _rb2D.velocity.y);
            _spriteRenderer.flipX = false;
            _boxCollider2D.offset = new Vector2(_colliderOffsetX, _boxCollider2D.offset.y);
            _run = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _ground) //Прыгнуть
        {
            _rb2D.AddForce(new Vector2(0, force));
        }

        if (Input.GetKey(KeyCode.C) && _ground && !defence) //Атака
        {
            _attack = true;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _run = false;
        }

        if (Input.anyKey)
        {
            _wait = false;
            _timerWait = 0;
        }

        if (!_attack && _ground && !_wait && !_run)
        {
            _stay = true;
        }

        if (_ground && _stay)
        {
            _timerWait += Time.deltaTime;
        }

        if (_timerWait >= secondsToWaitAnimation)
        {
            _wait = true;
        }
    }

    void Animation()
    {
        if (_stay)
        {
            _animator.SetBool("Stay", true);
        }
        else
        {
            _animator.SetBool("Stay", false);
        }

        if (_attack)
        {
            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
        }

        if (_run && _ground)
        {
            _stay = false;
            _animator.SetBool("Run", true);
        }

        if (!_run)
        {
            _animator.SetBool("Run", false);
        }

        if (!_ground)
        {
            _stay = false;
            _animator.SetBool("Jump", true);
        }

        if (_ground)
        {
            _animator.SetBool("Jump", false);
        }

        if (_wait)
        {
            _animator.SetBool("Wait", true);
            _stay = false;
        }
        else
        {
            _animator.SetBool("Wait", false);
        }

        if (_attack)
        {
            _stay = false;
            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
    }

    void Eternity()
    {
        print(eternity);
        _eternityTimer += Time.deltaTime;
        if (_eternityTimer >= eternityTime)
        {
            eternity = false;
            damage = false;
            _eternityTimer = 0;
        }
    }

    void HealthPoint()
    {
        if (!guard && damage && !eternity)
        {
            healthAmount--;
            health[healthAmount].SetActive(false);
            Eternity();
            eternity = true;
        }
    }

    void ArmorPoint()
    {
        if (guard && damage)
        {
            armorAmount--;
            armor[armorAmount].SetActive(false);
            guard = false;
        }
        if (defence)
        {
            for (int i = 0; i < armorAmount; i++)
            {
                armor[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < armorAmount; i++)
            {
                armor[i].SetActive(false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject == grow)
        {
            Resume();
            _ground = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == grow)
        {
            _ground = false;
        }
    }

    public void PauseAnimation()
    {
        _animator.speed = 0;
    }

    public void EndAttack()
    {
        _attack = false;
    }

    void Resume()
    {
        _animator.speed = 1;
    }
}
