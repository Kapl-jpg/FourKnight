using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private float secondsUp;
    [SerializeField] private float secondsDown;
    [SerializeField] private float speedUp;
    [SerializeField] private float speedDown;
    private float _firstTimer;
    [SerializeField] private float border;
    private float _secondsTimer;
    [SerializeField] private bool thornsUp;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }
    
    void Update()
    {
        if (thornsUp)
        {
            _secondsTimer = 0;
            if (transform.position.y > _startPosition.y+0.0001f)
            {
                _firstTimer += Time.deltaTime;
                if (_firstTimer >= secondsUp)
                {
                    transform.position = Vector3.Lerp(transform.position,_startPosition, speedUp);
                }
            }
            else
            {
                thornsUp = false;
            }
        }
        else
        {
            _firstTimer = 0;
            if (transform.position.y < _startPosition.y + border-0.0001f)
            {
                _secondsTimer += Time.deltaTime;
                if (_secondsTimer >= secondsDown)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(_startPosition.x, _startPosition.y + border), speedDown);
                }
            }
            else
            {
                thornsUp = true;
            }
        }
    }
}