using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Humanoid _player;
    [SerializeField] private PathCreator _pathCreator;
    private Vector3 _spawnPoint;
    [SerializeField] private float _offsetX;
    private float _positionY;

    private void Awake()
    {
        _spawnPoint.x = _pathCreator.path.GetPointAtDistance(0).x + _offsetX;
        _spawnPoint.z = _pathCreator.path.GetPointAtDistance(_spawnPoint.x).z;
    }

    public List<Humanoid> Build(int playersCount, float distance)
    {
        List<Humanoid> players = new List<Humanoid>();

        for (int i = 0; i < playersCount; i++)
        {
            Humanoid player = BuildPlayer();
            players.Add(player);
        }

        _positionY = 0f;
        _spawnPoint.x += distance;
        _spawnPoint.z = _pathCreator.path.GetPointAtDistance(_spawnPoint.x).z;

        return players;
    }

    private Humanoid BuildPlayer()
    {
        return Instantiate(_player, GetPosition(ref _spawnPoint, _positionY), Quaternion.Euler(0, 90, 0), transform);
    }

    private Vector3 GetPosition(ref Vector3 previousPosition, float positionY)
    {
        Vector3 position = previousPosition;
        position.y = _positionY;
        previousPosition = position;
        position.z = -previousPosition.z;
        _positionY += _player.GetComponent<BoxCollider>().size.y;

        return position;
    }
}
