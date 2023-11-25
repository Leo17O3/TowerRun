using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _additiveOffsetY;
    [SerializeField] private Transform _target;
    int _previousChildCount = 1;

    private void OnValidate()
    {
        if (_offset == Vector3.zero)
        {
            _offset = _target.position - transform.position;
        }
    }

    private void LateUpdate()
    {
        if (_target.childCount != _previousChildCount)
        {
            _offset.z = _target.childCount * _additiveOffsetY;
            _previousChildCount = _target.childCount;
        }

        transform.position = _target.position - _target.rotation * _offset;
        transform.LookAt(_target);
    }
}
