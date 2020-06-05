using UnityEngine;

public class SwardController : MonoBehaviour
{
    private GameObject _knight;
    private Vector3 _knightPosition;
    public GameObject Knight
    {
        get => _knight;
    }

    private Vector3 _targetPosition;
    private Vector3 _startTransform;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector3 rotAround;
    [SerializeField] private float limitRotation;
    [SerializeField] private float speedAmplitude;
    [SerializeField] private float speedAttack;
    [SerializeField] private float speedBackAttack;
    [SerializeField] private float speedPersecution;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float secondsPreAttack;
    [SerializeField] private float amplitude;
    private float _preAttackTimer;
    private bool _amplitudeUp;
    private bool _attack;
    private bool _backAttack;
    private bool _preAttack;
    private bool _process;
    private bool _knightRight;

    public void GetComponents()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _startTransform = transform.position;
        _knightPosition = Knight.transform.position;
    }

    private void Start()
    {
        GetComponents();
    }

    private void Update()
    {
        if (_preAttack || _attack || _backAttack)
        {
            _process = true;
        }
        else
        {
            _process = false;
        }

        if (Knight != null)
        {
            float distance = Vector2.Distance(transform.position, Knight.transform.position);
            if (distance < distanceAttack && !_attack && !_preAttack && !_backAttack)
            {
                if (Knight.transform.position.x > transform.position.x)
                {
                    _knightRight = true;
                    _targetPosition = new Vector3(_knightPosition.x - distanceAttack,
                       _knightPosition.y);
                }
                else
                {
                    _knightRight = false;
                    _targetPosition = new Vector3(_knightPosition.x + distanceAttack,
                       _knightPosition.y);
                }

                _preAttack = true;
            }
            else
            {
                if (!_process)
                {
                    AmplitudeTranslate();
                    Persecution();
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (_preAttack && !_backAttack && !_attack)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (_knightRight)
                {
                    _spriteRenderer.flipX = false;
                    transform.position = Vector3.MoveTowards(transform.position,
                        _targetPosition, 1 / (secondsPreAttack * 10));
                }
                else
                {
                    _spriteRenderer.flipX = true;
                    transform.position = Vector3.MoveTowards(transform.position,
                        _targetPosition, 1 / (secondsPreAttack * 10));
                }

                _preAttackTimer += Time.deltaTime;
                if (_preAttackTimer >= secondsPreAttack)
                {
                    _attack = true;
                    _preAttack = false;
                }
            }

            if (_attack && Mathf.Abs(gameObject.transform.rotation.z) < limitRotation)
            {
                if (_knightRight)
                {
                    transform.RotateAround(transform.position + rotAround, Vector3.back, speedAttack);
                }
                else
                {
                    transform.RotateAround(transform.position + rotAround, Vector3.forward, speedAttack);
                }

                _preAttackTimer = 0;
            }
            else
            {
                if (_attack)
                {
                    _backAttack = true;
                    _attack = false;
                }
            }

            if (_backAttack)
            {
                if (_knightRight)
                {
                    transform.RotateAround(transform.position + rotAround, Vector3.forward, speedBackAttack);
                }
                else
                {
                    transform.RotateAround(transform.position + rotAround, Vector3.back, speedBackAttack);
                }

                if (Mathf.Abs(transform.rotation.z) <= 0.01f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _backAttack = false;
                }
            }
        }
    }

    void AmplitudeTranslate()
    {
        if (gameObject.transform.position.y >= _startTransform.y + amplitude)
        {
            _amplitudeUp = true;
        }

        if (gameObject.transform.position.y <= _startTransform.y - amplitude)
        {
            _amplitudeUp = false;
        }

        if (!_amplitudeUp)
        {
            gameObject.transform.Translate(Vector2.up * speedAmplitude);
        }
        else
        {
            gameObject.transform.Translate(Vector2.down * speedAmplitude);
        }
    }

    void Persecution()
    {
        if (Knight.transform.position.x > gameObject.transform.position.x)
        {
            _spriteRenderer.flipX = false;
            gameObject.transform.Translate(Vector3.right * speedPersecution);
        }
        else
        {
            _spriteRenderer.flipX = true;
            gameObject.transform.Translate(Vector3.left * speedPersecution);
        }
    }
}
