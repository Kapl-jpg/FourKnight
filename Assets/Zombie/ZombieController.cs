using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private GameObject knight;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToKnight;
    [SerializeField] private float force;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private KnightController _knightController;
    private float _distance;

    private bool _run;

    void GetComponents()
    {
        _knightController = knight.GetComponent<KnightController>();
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
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,_rigidbody2D.velocity.y);
        }
    }


    private void Animation()
    {
        if (_run)
        {
            _animator.SetBool("Run",true);
        }
        else
        {
            _animator.SetBool("Run",false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject == knight)
        {
            if (!_knightController.damage && !_knightController.defence)
            {
                _knightController.damage = true;
            }

            if (knight.transform.position.x > transform.position.x)
            {
                _rigidbody2D.AddForce(Vector2.left* force);
            }
            else
            {
                _rigidbody2D.AddForce(Vector2.right * force);
            }
        }
    }
}
