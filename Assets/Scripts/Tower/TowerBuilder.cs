using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PathCreator _pathCreator;
    private Vector3 _spawnPoint;
    private float _positionY;

    private void Start()
    {
        _spawnPoint = _pathCreator.path.GetPointAtDistance(0);
        Debug.Log(_spawnPoint);
    }

    public List<Player> Build(int playersCount, float distance)
    {
        List<Player> players = new List<Player>();

        for (int i = 0; i < playersCount; i++)
        {
            Player player = BuildPlayer();
            players.Add(player);
        }

        _positionY = 0f;
        _spawnPoint.x += distance;

        return players;
    }

    private Player BuildPlayer()
    {
        return Instantiate(_player, GetPosition(ref _spawnPoint, _positionY), Quaternion.identity, transform);
    }

    private Vector3 GetPosition(ref Vector3 previousPosition, float positionY)
    {
        Vector3 position = previousPosition;
        position.y = _positionY;
        previousPosition = position;
        _positionY += _player.GetComponent<BoxCollider>().size.y;

        return position;
    }
}
