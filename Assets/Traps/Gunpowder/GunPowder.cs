using UnityEngine;

namespace Traps.Gunpowder
{
    public class GunPowder : MonoBehaviour
    {
        [SerializeField] private float force;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject wall;
        private bool _right;
        
        void Start()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (_right)
            {
                _rigidbody2D.velocity = Vector2.left * force;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.right*force;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == wall)
            {
                if (_right)
                    _right = false;
                else
                    _right = true;
            }
        }
    }
}
