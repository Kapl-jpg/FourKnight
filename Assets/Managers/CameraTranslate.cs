using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
    private GeneralInformation _generalInformation;

    public GeneralInformation GeneralInformation
    {
        set => _generalInformation = value;
    }

    [SerializeField] private Vector3 offset;

    [SerializeField] private float speed;

    private float _speedTranslate;
    private Transform _activeKnightTransform;

    public void GetTransform()
    {
        _activeKnightTransform = _generalInformation.ActiveKnight.transform;
        transform.position = _activeKnightTransform.transform.position + offset;
    }

    void Update()
    {
        if (_generalInformation.ActiveKnight != null)
        {
            Transform transform1;
            (transform1 = transform).position = Vector3.MoveTowards(transform.position,
                _activeKnightTransform.transform.position + offset, _speedTranslate);
            _speedTranslate = speed * Vector2.Distance(transform1.position, _activeKnightTransform.position);
        }
    }
}
