using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    [SerializeField] private Vector3 offset;

    public Vector3 Offset
    {
        get => offset;
        set => offset = value;
    }
    [SerializeField] private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
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
                _activeKnightTransform.transform.position + offset, _speedTranslate);
            _speedTranslate = speed * Vector2.Distance(_gameObjectTransform.position, _activeKnightTransform.position);
        }
    }
}
