using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Human[] _players;
    [SerializeField] private int _minHumansCountInTower;
    [SerializeField] private int _maxHumansCountInTower;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private PathGenerator _pathGenerator;
    private Vector3 _spawnPoint;
    [SerializeField] private float _offsetX;
    private float _positionY;

    private void Awake()
    {
        _spawnPoint = _pathGenerator.GetPathPoint(0, _offsetX);
    }

    public List<Human> Build(float distance)
    {
        List<Human> humans = new List<Human>();

        int humansCountInTower = Random.Range(_minHumansCountInTower, _maxHumansCountInTower);

        for (int i = 0; i < humansCountInTower; i++)
        {
            int index = Random.Range(0, _players.Length);
            Human player = BuildPlayer(index);
            humans.Add(player);
        }

        _positionY = 0f;
        _spawnPoint = _pathGenerator.GetPathPoint(_spawnPoint.x + distance);

        return humans;
    }

    private Human BuildPlayer(int index)
    {
        return Instantiate(_players[index], GetPosition(ref _spawnPoint, _positionY, index), Quaternion.Euler(0, 90, 0), transform);
    }

    private Vector3 GetPosition(ref Vector3 previousPosition, float positionY, int index)
    {
        Vector3 position = previousPosition;
        position.y = _positionY;
        previousPosition = position;
        position.z = previousPosition.z;
        _positionY += _players[index].GetComponent<BoxCollider>().size.y;

        return position;
    }
}
