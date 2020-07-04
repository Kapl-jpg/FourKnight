using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    private Vector3 _offset;

    public Vector3 Offset
    {
        set => _offset = value;
    }
    private float _speed;

    public float Speed
    {
        set => _speed = value;
    }
    private float _speedTranslate;
    private Transform _gameObjectTransform;
    private Transform _activeKnightTransform;

    public void GetTransform()
    {
        _activeKnightTransform = _generalInformation.ActiveKnight.transform;
        _gameObjectTransform = transform;
    }

    void Update()
    {
        if (_generalInformation.ActiveKnight != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _activeKnightTransform.transform.position + _offset, _speedTranslate);
            _speedTranslate = _speed * Vector2.Distance(_gameObjectTransform.position, _activeKnightTransform.position);
        }
    }
}
