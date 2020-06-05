using UnityEngine;

public class BonusTranslate : MonoBehaviour
{
    [SerializeField] private float speedTranslate;
    [SerializeField] private float speedRotate;
    [SerializeField] private float offsetY;
    private Vector2 _startPosition;
    private bool _up;
    private bool _down;
    private int _randomNumber;

    void Start()
    {
        _startPosition = transform.position;
        _randomNumber = Random.Range(0, 2);
        if (_randomNumber == 0)
        {
            _up = true;
        }
        else
        {
            _down = true;
        }
    }

    void Update()
    {
        transform.Rotate(0, speedRotate, 0);
        if (_up)
        {
            _down = false;
            transform.Translate(Vector3.up * speedTranslate);
        }

        if (_down)
        {
            _up = false;
            transform.Translate(Vector3.down * speedTranslate);
        }

        if (transform.position.y > _startPosition.y + offsetY)
        {
            _down = true;
            _up = false;
        }

        if (transform.position.y < _startPosition.y - offsetY)
        {
            _down = false;
            _up = true;
        }
    }
}