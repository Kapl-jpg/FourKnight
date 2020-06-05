using UnityEngine;

public class KnightAnimation : MonoBehaviour
{
    [SerializeField] private int quantityAttack;
    private Animator _animator;
    private KnightController _knightController;
    private bool _clickAttackButton;
    private int _numberAttack;

    public int NumberAttack
    {
        get => _numberAttack;
        set => _numberAttack = value;
    }

    private bool _attackButton;

    public bool AttackButton
    {
        get => _attackButton;
        set => _attackButton = value;
    }

    private bool _ground;

    public bool Ground
    {
        get => _ground;
        set => _ground = value;
    }

    private bool _attack;

    public bool BoolAttack
    {
        get => _attack;
        set => _attack = value;
    }

    private bool _run;

    public bool BoolRun
    {
        get => _run;
        set => _run = value;
    }

    private bool _wait;

    public bool BoolWait
    {
        get => _wait;
        set => _wait = value;
    }

    private bool _playAttackAnimation;

    public bool PlayAttackAnimation
    {
        get => _playAttackAnimation;
        set => _playAttackAnimation = value;
    }

    private static readonly int Wait = Animator.StringToHash("Wait");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Attack3 = Animator.StringToHash("Attack3");

    public void GetComponents()
    {
        _knightController = gameObject.GetComponent<KnightController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GetComponents();
    }

    void Update()
    {
        if (!_knightController.Afk)
        {
            Animation();
            AttackAnimation();
        }
    }

    void Animation()
    {
        if (_run && _ground)
        {
            _animator.SetBool(Run, true);
        }
        else
        {
            _animator.SetBool(Run, false);
        }

        if (_ground)
        {
            _animator.SetBool(Jump, false);
        }
        else
        {
            _animator.SetBool(Jump, true);
        }

        if (_wait)
        {
            _animator.SetBool(Wait, true);
        }
        else
        {
            _animator.SetBool(Wait, false);
        }

        if (quantityAttack == 1)
        {
            if (_attackButton)
            {
                _animator.SetBool(Attack, true);
            }
            else
            {
                _animator.SetBool(Attack, false);
            }
        }
    }

    void AttackAnimation()
    {
        if (quantityAttack == 1)
        {
            if (_attackButton)
            {
                _animator.SetBool(Attack, true);
            }
            else
            {
                _animator.SetBool(Attack, false);
            }
        }

        if (quantityAttack == 2)
        {
            if (_numberAttack == 0 && _attackButton)
            {
                _animator.SetBool(Attack1, true);
                _animator.SetBool(Attack2, false);
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                if (_attackButton)
                {
                    _attack = true;
                }
            }

            if (_attack && _numberAttack == 1)
            {
                _animator.SetBool(Attack2, true);
                _animator.SetBool(Attack1, false);
                _attackButton = false;
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                if (_attackButton)
                {
                    _attack = true;
                }
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                _playAttackAnimation = true;
            }
            else
            {
                _playAttackAnimation = false;
            }
        }

        if (quantityAttack == 3)
        {
            if (_ground)
            {
                if (_numberAttack == 0 && _attackButton)
                {
                    _animator.SetBool(Attack1, true);
                    _animator.SetBool(Attack2, false);
                    _animator.SetBool(Attack3, false);
                }

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {
                    if (_attackButton)
                    {
                        _attack = true;
                    }
                }

                if (_attack && _numberAttack == 1)
                {
                    _animator.SetBool(Attack2, true);
                    _animator.SetBool(Attack3, false);
                    _animator.SetBool(Attack1, false);
                    _attackButton = false;
                }

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                {
                    if (_attackButton)
                    {
                        _attack = true;
                    }
                }

                if (_attack && _numberAttack == 2)
                {
                    _animator.SetBool(Attack3, true);
                    _animator.SetBool(Attack1, false);
                    _animator.SetBool(Attack2, false);
                    _attackButton = false;
                }

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
                    _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
                    _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                {
                    _playAttackAnimation = true;
                }
                else
                {
                    _playAttackAnimation = false;
                }
            }
        }
    }

    public void NotAttack()
    {
        _attack = false;
        _attackButton = false;
        _knightController.AttackButton = false;
    }

    public void FirstAttack()
    {
        if (!_attack)
        {
            _animator.SetBool(Attack1, false);
        }

        _numberAttack = 1;
    }

    public void SecondAttack()
    {
        if (!_attack)
        {
            _animator.SetBool(Attack2, false);
        }

        _numberAttack = 2;
    }

    public void ThirdAttack()
    {
        if (!_attack)
        {
            _animator.SetBool(Attack3, false);
        }

        _numberAttack = 0;
    }

    public void PauseAnimation()
    {
        _animator.speed = 0;
    }

    public void ResumeAnimation()
    {
        _animator.speed = 1;
    }
}