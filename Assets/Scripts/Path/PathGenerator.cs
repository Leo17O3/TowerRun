using PathCreation;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;

    public Vector3 GetPathPoint(float xPoint, float offsetX = 0f)
    {
        Vector3 newPoint = Vector3.zero;

        newPoint.x = _pathCreator.path.GetPointAtDistance(xPoint + offsetX).x;
        newPoint.z = _pathCreator.path.GetPointAtDistance(newPoint.x).z;
        newPoint.z = _pathCreator.path.GetClosestPointOnPath(newPoint).z;

        return newPoint;
    }
}
