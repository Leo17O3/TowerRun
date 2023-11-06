using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Player[] _players;
    [SerializeField] private PathCreator _pathCreator;
    private Vector3 _spawnPoint;
    [SerializeField] private float _offsetX;
    private float _positionY;

    private void Awake()
    {
        _spawnPoint.x = _pathCreator.path.GetPointAtDistance(0).x + _offsetX;
        _spawnPoint.z = _pathCreator.path.GetPointAtDistance(_spawnPoint.x).z;
    }

    public List<Player> Build(int playersCount, float distance)
    {
        List<Player> players = new List<Player>();

        for (int i = 0; i < playersCount; i++)
        {
            int index = Random.Range(0, _players.Length);
            Player player = BuildPlayer(index);
            players.Add(player);
        }

        _positionY = 0f;
        _spawnPoint.x += distance;
        _spawnPoint.z = _pathCreator.path.GetPointAtDistance(_spawnPoint.x).z;

        return players;
    }

    private Player BuildPlayer(int index)
    {
        return Instantiate(_players[index], GetPosition(ref _spawnPoint, _positionY, index), Quaternion.Euler(0, 90, 0), transform);
    }

    private Vector3 GetPosition(ref Vector3 previousPosition, float positionY, int index)
    {
        Vector3 position = previousPosition;
        position.y = _positionY;
        previousPosition = position;
        position.z = -previousPosition.z;
        _positionY += _players[index].GetComponent<BoxCollider>().size.y;

        return position;
    }
}
