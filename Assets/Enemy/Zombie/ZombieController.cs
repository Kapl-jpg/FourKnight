using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private GameObject knight;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToKnight;
    [SerializeField] private int live;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private float _distance;

    private bool _run;
    private static readonly int Run = Animator.StringToHash("Run");

    void GetComponents()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GetComponents();
    }

    void Update()
    {
        Animation();
        _distance = Vector3.Distance(transform.position, knight.transform.position);
        if (Mathf.Abs(_distance) < distanceToKnight)
        {
            _run = true;
        }
        else
        {
            _run = false;
        }

        if (_run)
        {
            if (knight.transform.position.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
                _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
            }
            else
            {
                _spriteRenderer.flipX = true;
                _rigidbody2D.velocity = new Vector2(-speed, _rigidbody2D.velocity.y);
            }
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        if (live <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void Animation()
    {
        if (_run)
        {
            _animator.SetBool(Run, true);
        }
        else
        {
            _animator.SetBool(Run, false);
        }
    }
}