using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTowerJumper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    bool _isClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _rigidbody.AddForce(transform.up * _jumpForce);
            _isGrounded = false;
            _isClicked = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _isClicked = true;
        }
    }

    public void IncreaseJumpForce(float additiveForce)
    {
        _jumpForce *= additiveForce;
    }

    public void ReduceJumpForce(float additiveForce)
    {
        _jumpForce /= additiveForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _isGrounded = true;
        }
        else if(_isClicked)
        {
            Debug.Log(collision.gameObject.layer);
        }
    }
}
