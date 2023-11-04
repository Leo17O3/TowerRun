using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTowerJumper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _minDistance;
    [SerializeField] private Transform _ground;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _ground.position.y - transform.position.y > _minDistance)
        {
            _rigidbody.AddForce(transform.up * _jumpForce);
        }
    }
}
