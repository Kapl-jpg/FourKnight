using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private float speed;

    private float speedTranslate;

    public GameObject ActiveKnight { get; set; }

    void Update()
    {
        var position = ActiveKnight.transform.position;
        var currentPosition = transform.position;
        currentPosition = Vector3.MoveTowards(new Vector3(currentPosition.x, currentPosition.y, offset.z),
            new Vector3(position.x, position.y + offset.y, position.z + offset.z), speedTranslate);
        transform.position = currentPosition;
        speedTranslate = speed * Vector2.Distance(currentPosition, position);
        
    }
}
