using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTowerJumper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _rigidbody.AddForce(transform.up * _jumpForce);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _isGrounded = true;
        }
    }
}
