using PathCreation;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed;

    private float _distantTravaled;

    private void Update()
    {
        _distantTravaled += _speed * Time.deltaTime;

        Vector3 nextPosition = _pathCreator.path.GetPointAtDistance(_distantTravaled, EndOfPathInstruction.Loop);
        nextPosition.y = transform.position.y;

        transform.LookAt(nextPosition);
        _rigidbody.MovePosition(nextPosition);
    }
}
